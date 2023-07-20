// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using EdFi.Common.Configuration;
using EdFi.Common.Extensions;

namespace EdFi.Ods.Common.Configuration
{
    public class ApiSettings
    {
        private readonly Lazy<DatabaseEngine> _databaseEngine;
        private string _odsContextRoutePath;
        private string[] _odsContextRouteTemplateKeys;

        public ApiSettings()
        {
            _databaseEngine = new Lazy<DatabaseEngine>(() => DatabaseEngine.TryParseEngine(Engine));
        }

        public int BearerTokenTimeoutMinutes { get; set; } = 60;

        public int DefaultPageSizeLimit { get; set; } = 500;

        public int LogRequestResponseContentForMinutes { get; set; } = 0;

        public string Engine { get; set; }

        public bool PlainTextSecrets { get; set; }

        public int[] Years { get; set; }

        public List<Feature> Features { get; set; } = new();

        public List<ScheduledJobSettings> ScheduledJobs { get; set; } = new();

        public List<string> ExcludedExtensions { get; set; } = new();

        public bool? UseReverseProxyHeaders { get; set; }

        public string OverrideForForwardingHostServer { get; set; }

        public int? OverrideForForwardingHostPort { get; set; }

        public string PathBase { get; set; }

        public string OdsContextRouteTemplate { get; set; }
        
        public CacheSettings Caching { get; set; } = new();

        public ReverseProxySettings GetReverseProxySettings()
        {
            return new ReverseProxySettings(
                UseReverseProxyHeaders,
                OverrideForForwardingHostServer,
                OverrideForForwardingHostPort);
        }

        public DatabaseEngine GetDatabaseEngine() => _databaseEngine.Value;

        /// <summary>
        /// Gets the raw path from the route template, removing any ASP.NET route constraints defined in the configured route template defined in <see cref="OdsContextRouteTemplate"/>.
        /// </summary>
        /// <returns>The path that can be presented to API clients in a manner consistent with URI templates.</returns>
        public string GetOdsContextRoutePath()
        {
            return _odsContextRoutePath ??= new(OdsContextRouteTemplateHelpers.GetOdsContextPathChars(OdsContextRouteTemplate).ToArray());
        }

        /// <summary>
        /// Gets the array of route key names defined in the configured route template defined in <see cref="OdsContextRouteTemplate"/>.
        /// </summary>
        /// <returns>An array of keys for extracting values from the route values associated with an API request.</returns>
        public string[] GetOdsContextRouteTemplateKeys()
        {
            if (OdsContextRouteTemplate != null)
            {
                return _odsContextRouteTemplateKeys ??= OdsContextRouteTemplateHelpers.GetRouteTemplateKeys(OdsContextRouteTemplate);
            }

            return Array.Empty<string>();
        }

        public bool IsFeatureEnabled(string featureName)
            => Features.SingleOrDefault(x => x.Name.EqualsIgnoreCase(featureName) && x.IsEnabled) != null;
    }
}
