﻿// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace EdFi.Ods.Common.Exceptions
{
    public class NotModifiedException : EdFiProblemDetailsExceptionBase
    {
        // Fields containing override values for Problem Details
        private const string TypePart = "not-modified";
        private const string TitleText = "Not Modified";
        private const int StatusValue = StatusCodes.Status304NotModified;

        private const string DefaultDetail = "The specified resource has not changed since it was last retrieved.";

        public NotModifiedException()
            : base(DefaultDetail, DefaultDetail) { }

        public NotModifiedException(string message)
            : base(DefaultDetail, message) { }

        public NotModifiedException(string message, Exception inner)
            : base(DefaultDetail, message, inner) { }

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
}
