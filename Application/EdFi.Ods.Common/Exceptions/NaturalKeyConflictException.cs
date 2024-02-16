// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EdFi.Ods.Common.Exceptions;

public class NaturalKeyConflictException : ConflictException
{
    // Fields containing override values for Problem Details
    private const string TypePart = "natural-key";
    private const string TitleText = "Resource Not Unique Conflict due to natural-key";
    private const int StatusValue = StatusCodes.Status409Conflict;

    public NaturalKeyConflictException(string detail)
        : base(detail) { }

    public NaturalKeyConflictException(string detail, Exception innerException)
        : base(detail, innerException) { }


    public NaturalKeyConflictException(string detail, string[] errors)
    : base(detail)
    {
        ((IEdFiProblemDetails)this).Errors = errors;
    }
    // ---------------------------
    //  Boilerplate for overrides
    // ---------------------------
    public override string Title { get => TitleText; }

    public override int Status { get => StatusValue; }
    
    protected override IEnumerable<string> GetTypeParts()
    {
        foreach (var part in base.GetTypeParts())
        {
            yield return part;
        }

        yield return TypePart;
    }
    // ---------------------------
}
