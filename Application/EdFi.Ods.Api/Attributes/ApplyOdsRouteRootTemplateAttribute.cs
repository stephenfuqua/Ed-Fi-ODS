// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;

namespace EdFi.Ods.Api.Attributes;

/// <summary>
/// Applied to a Controller, this introduces the necessary root route template for accessing an ODS instance (including route values for the tenant and ODS, as configured for the API).
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class ApplyOdsRouteRootTemplateAttribute : Attribute { }
