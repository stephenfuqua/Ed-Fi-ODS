// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using EdFi.Ods.Common.Configuration;

namespace EdFi.Ods.Api.Configuration;

[Intercept("cache-ods-instances")]
public interface IOdsInstanceConfigurationProvider
{
    Task<OdsInstanceConfiguration> GetByIdAsync(int odsInstanceId);
}
