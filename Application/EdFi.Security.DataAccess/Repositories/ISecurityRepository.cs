﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System.Collections.Generic;
using Autofac.Extras.DynamicProxy;
using EdFi.Security.DataAccess.Models;

namespace EdFi.Security.DataAccess.Repositories;

[Intercept("cache-security")]
public interface ISecurityRepository
{
    Action GetActionByName(string actionName);

    AuthorizationStrategy GetAuthorizationStrategyByName(string authorizationStrategyName);

    IEnumerable<ClaimSetResourceClaimAction> GetClaimsForClaimSet(string claimSetName);

    /// <summary>
    /// Gets the lineage up the taxonomy of resource claim URIs for the specified resource.
    /// </summary>
    /// <param name="resourceUri">The resource URI representing the resource.</param>
    /// <returns>The resource claim URIs.</returns>
    IEnumerable<string> GetResourceClaimLineage(string resourceUri);

    /// <summary>
    /// Gets the authorization metadata of the lineage up the taxonomy of resource claims
    /// for the specified resource.
    /// </summary>
    /// <param name="resourceClaimUri">The resource claim URI representing the resource.</param>
    /// <returns>The resource claim authorization metadata.</returns>
    IEnumerable<ResourceClaimAction> GetResourceClaimLineageMetadata(string resourceClaimUri, string action);

    ResourceClaim GetResourceByResourceName(string resourceName);
}
