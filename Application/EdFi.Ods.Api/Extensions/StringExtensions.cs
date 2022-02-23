// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

namespace EdFi.Ods.Api.Extensions
{
    public static class StringExtensions
    {
        public static string TrimAt(this string text, string terminator)
        {
            if (text == null)
            {
                return null;
            }
            
            int pos = text.IndexOf(terminator);

            if (pos < 0)
            {
                return text;
            }

            return text.Substring(0, pos);
        }
    }
}
