﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EdFi.Ods.Common.Extensions;
using EdFi.Ods.Common.Security.Authorization;
using EdFi.Ods.Common.Security.Claims;
using EdFi.Ods.Common.Validation;
using Microsoft.Extensions.Primitives;
using NHibernate;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace EdFi.Ods.Common.Infrastructure.Listeners
{
    public class EdFiOdsPostUpdateEventListener : IPostUpdateEventListener
    {
        private readonly Lazy<IEntityAuthorizer> _entityAuthorizer;
        private readonly IAuthorizationContextProvider _authorizationContextProvider;

        public EdFiOdsPostUpdateEventListener(
            Func<IEntityAuthorizer> entityAuthorizerResolver,
            IAuthorizationContextProvider authorizationContextProvider)
        {
            _authorizationContextProvider = authorizationContextProvider;
            
            _entityAuthorizer = new Lazy<IEntityAuthorizer>(entityAuthorizerResolver);
        }

        public async Task OnPostUpdateAsync(PostUpdateEvent @event, CancellationToken cancellationToken)
        {
            await ProcessCascadingKeyValuesAsync(@event, cancellationToken);
        }

        public void OnPostUpdate(PostUpdateEvent @event)
        {
            ProcessCascadingKeyValuesAsync(@event, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private async Task ProcessCascadingKeyValuesAsync(PostUpdateEvent @event, CancellationToken cancellationToken)
        {
            // Quit if this is not an entity that supports cascading updates
            var cascadableEntity = @event.Entity as IHasCascadableKeyValues;

            if (cascadableEntity == null)
            {
                return;
            }

            // Quit if there are no modified key values to cascade
            var newKeyValues = cascadableEntity.NewKeyValues;

            if (newKeyValues == null)
            {
                return;
            }

            // Make sure the identity has an identifier for performing the update
            var hasIdentifier = @event.Entity as IHasIdentifier;

            if (hasIdentifier == null)
            {
                throw new NotSupportedException(
                    "Cascading updates are only supported for 'aggregate root' entities that implement IHasIdentifier (i.e. entities that have a GUID-based identifier property).");
            }

            // Apply the new key values to the entity and then perform validation before proceeding
            ApplyNewKeyValuesToEntity();

            // Authorize the entity
            var action = _authorizationContextProvider.GetAction();
            await _entityAuthorizer.Value.AuthorizeEntityAsync(@event.Entity, action, AuthorizationPhase.ProposedData, cancellationToken);

            string tableName;
            string[] updateTargetColumnNames;
            string[] valueSourceColumnNames;

            // Get the entity's metadata
            var classMetadata = (AbstractEntityPersister) @event.Session.SessionFactory.GetClassMetadata(@event.Entity.GetType());

            // If there is no root table, update using the class' metadata
            if (classMetadata.TableName == classMetadata.RootTableName)
            {
                tableName = classMetadata.TableName;
                updateTargetColumnNames = classMetadata.IdentifierColumnNames;
                valueSourceColumnNames = classMetadata.IdentifierColumnNames;
            }
            else
            {
                var rootClassMetadata = (AbstractEntityPersister) @event.Session.SessionFactory.GetClassMetadata(classMetadata.RootEntityName);
                tableName = rootClassMetadata.RootTableName;
                updateTargetColumnNames = rootClassMetadata.IdentifierColumnNames;
                valueSourceColumnNames = classMetadata.IdentifierColumnNames;
            }

            var query = CreateUpdateQuery(
                @event.Session,
                hasIdentifier.Id,
                tableName,
                updateTargetColumnNames,
                valueSourceColumnNames,
                newKeyValues);

            // Execute the update of the primary key
            await query.ExecuteUpdateAsync();

            void ApplyNewKeyValuesToEntity()
            {
                var typeInfo = @event.Entity.GetType().GetTypeInfo();

                foreach (var keyAsObject in newKeyValues.Keys)
                {
                    var property = typeInfo.GetProperty((string) keyAsObject);
                    property.SetValue(@event.Entity, newKeyValues[keyAsObject]);
                }
            }
        }

        private static IQuery CreateUpdateQuery(
            ISession session,
            Guid id,
            string tableName,
            string[] updateTargetColumnNames,
            string[] valueSourceColumnNames,
            OrderedDictionary newKeyValues)
        {
            // Build the SET clause
            string setClause = GetSetClause(updateTargetColumnNames, valueSourceColumnNames);

            // Build the UPDATE sql query
            string sql = $@"UPDATE {tableName} SET {setClause} WHERE Id = :id";

            var query = session.CreateSQLQuery(sql).SetGuid("id", id);

            // Create parameters for updating the primary key with the new values
            for (int i = 0; i < updateTargetColumnNames.Length; i++)
            {
                string targetColumnName = updateTargetColumnNames[i];
                string valueSourceColumnName = valueSourceColumnNames[i];

                query.SetParameter(targetColumnName, newKeyValues[valueSourceColumnName]);
            }

            return query;
        }

        private static string GetSetClause(string[] updateTargetColumnNames, string[] sourceValueColumnNames)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < updateTargetColumnNames.Length; i++)
            {
                string targetColumnName = updateTargetColumnNames[i];
                string sourceValueColumnName = sourceValueColumnNames[i];

                if (i > 0)
                {
                    sb.Append(',');
                }

                sb.Append(targetColumnName);
                sb.Append(" = :");
                sb.Append(sourceValueColumnName);
            }

            string setClause = sb.ToString();

            return setClause;
        }
    }
}
