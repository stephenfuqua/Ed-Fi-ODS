// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Linq;
using EdFi.Common.Extensions;
using EdFi.Ods.Common.Models.Resource;

namespace EdFi.Ods.Common.Specifications
{
    public class PersonEntitySpecification : IPersonEntitySpecification
    {
        private readonly IPersonTypesProvider _personTypesProvider;

        private static readonly string[] _suffixes;

        public PersonEntitySpecification(IPersonTypesProvider personTypesProvider)
        {
            _personTypesProvider = personTypesProvider;
        }

        static PersonEntitySpecification()
        {
            _suffixes = new[] { UniqueIdConventions.UsiSuffix, UniqueIdConventions.UniqueIdSuffix };
        }

        /// <inheritdoc cref="IPersonEntitySpecification.IsPersonEntity(System.Type)" />
        public bool IsPersonEntity(Type type)
        {
            return IsPersonEntity(type.Name);
        }

        /// <inheritdoc cref="IPersonEntitySpecification.IsPersonEntity(string)" />
        public bool IsPersonEntity(string typeName)
        {
            return _personTypesProvider.PersonTypes.Contains(typeName, StringComparer.OrdinalIgnoreCase);
        }

        /// <inheritdoc cref="IPersonEntitySpecification.IsPersonIdentifier(string,string)" />
        public bool IsPersonIdentifier(string propertyName, string personType = null)
        {
            if (personType != null && !_personTypesProvider.PersonTypes.Any(pt => pt.EqualsIgnoreCase(personType)))
            {
                throw new ArgumentException($"'{personType}' is not a supported person type.");
            }

            // TODO: Embedded convention (Person identifiers can end with "USI" or "UniqueId")
            return _suffixes.Any(
                    s => propertyName.EndsWith(s) 
                        && _personTypesProvider.PersonTypes.Any(
                            pt => 
                                // Person type was either not specified, or current person type matches what was specified
                                (personType == null || personType == pt) 
                                && PersonTypeAppearsImmediatelyBeforeSuffix(pt, s)));

            bool PersonTypeAppearsImmediatelyBeforeSuffix(string pt, string suffix)
            {
                int lastIndexOfPersonType = propertyName.LastIndexOf(pt, StringComparison.Ordinal);

                if (lastIndexOfPersonType < 0)
                {
                    // Person type is not in the property name
                    return false;
                }
                
                // Identify expected location of the person type in the property name
                int expectedLastIndexOfPersonType = propertyName.Length - (pt.Length + suffix.Length);

                return lastIndexOfPersonType == expectedLastIndexOfPersonType;
            }
        }

        /// <inheritdoc cref="IPersonEntitySpecification.GetUniqueIdPersonType" />
        public string GetUniqueIdPersonType(string propertyName)
        {
            string personType = _personTypesProvider.PersonTypes.FirstOrDefault(
                pt =>
                {
                    int personTypePos = propertyName.LastIndexOf(pt, StringComparison.OrdinalIgnoreCase);

                    if (personTypePos < 0 || personTypePos + pt.Length > propertyName.Length)
                    {
                        return false;
                    }

                    return propertyName.AsSpan(personTypePos + pt.Length)
                        .Equals(UniqueIdConventions.UniqueIdSuffix.AsSpan(), StringComparison.OrdinalIgnoreCase);
                });

            return personType;
        }

        /// <inheritdoc cref="IUniqueIdSpecification.TryGetUniqueIdPersonType" />
        public bool TryGetUniqueIdPersonType(string propertyName, out string personType)
        {
            personType = GetUniqueIdPersonType(propertyName);

            return personType != null;
        }

        /// <inheritdoc cref="IPersonEntitySpecification.GetUSIPersonType" />
        public string GetUSIPersonType(string propertyName)
        {
            string personType = _personTypesProvider.PersonTypes.FirstOrDefault(
                pt =>
                {
                    int personTypePos = propertyName.LastIndexOf(pt, StringComparison.OrdinalIgnoreCase);

                    if (personTypePos < 0 || personTypePos + pt.Length > propertyName.Length)
                    {
                        return false;
                    }

                    return propertyName.AsSpan(personTypePos + pt.Length)
                        .Equals(UniqueIdConventions.UsiSuffix.AsSpan(), StringComparison.OrdinalIgnoreCase);
                });

            return personType;
        }

        /// <inheritdoc cref="IPersonEntitySpecification.TryGetUSIPersonType" />
        public bool TryGetUSIPersonType(string propertyName, out string personType)
        {
            personType = GetUSIPersonType(propertyName);

            return personType != null;
        }

        /// <inheritdoc cref="IPersonEntitySpecification.TryGetUSIPersonTypeAndRoleName" />
        public bool TryGetUSIPersonTypeAndRoleName(string propertyName, out string personType, out string roleName)
        {
            roleName = null;

            personType = GetUSIPersonType(propertyName);
            
            if (personType == null)
            {
                return false;
            }

            int personStartPos;

            // Extract role name applied as a prefix
            if ((personStartPos = propertyName.LastIndexOf(personType)) > 0)
            {
                roleName = propertyName.Substring(0, personStartPos);
            }

            return true;
        }

        /// <inheritdoc cref="IPersonEntitySpecification.IsDefiningUniqueId" />
        public bool IsDefiningUniqueId(ResourceClassBase resourceClass, ResourceProperty property)
        {
            return IsPersonEntity(resourceClass.Name)
                && UniqueIdConventions.IsUniqueId(property.PropertyName, resourceClass.Name);
        }
    }
}
