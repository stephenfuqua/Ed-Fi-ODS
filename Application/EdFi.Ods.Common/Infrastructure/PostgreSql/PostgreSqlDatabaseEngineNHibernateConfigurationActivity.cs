﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

#if NETSTANDARD
using EdFi.Ods.Common.Infrastructure.Configuration;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace EdFi.Ods.Common.Infrastructure.PostgreSql
{
    public class PostgreSqlDatabaseEngineNHibernateConfigurationActivity : INHibernateConfigurationActivity
    {
        /// <summary>
        /// Configure the defaults for PostgreSQL support, but only if not already set, such as from another
        /// configuration activity or from the xml config
        /// </summary>
        /// <param name="configuration">The NHibernate <see cref="Configuration"/> instance to be manipulated.</param>
        public void Execute(NHibernate.Cfg.Configuration configuration)
        {
            var configuredDialect = configuration.GetProperty(Environment.Dialect);
            var configuredDriver = configuration.GetProperty(Environment.ConnectionDriver);
            configuration.DataBaseIntegration(c =>
            {
                if (string.IsNullOrWhiteSpace(configuredDialect))
                {
                    c.Dialect<PostgreSQL83Dialect>();
                }

                if (string.IsNullOrWhiteSpace(configuredDriver))
                {
                    c.Driver<EdFiNpgsqlDriver>();
                }
            });
        }
    }
}
#endif