// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;

namespace EdFi.Ods.Common.Caching
{
    public class UniqueIdToIdentificationCodeKey : IEquatable<UniqueIdToIdentificationCodeKey>
    {
        public string PersonType;
        public string IdentificationSystemDescriptorUri;
        public string UniqueId;
        public int EducationOrganizationId;

        // Resharper generated equality members
        public bool Equals(UniqueIdToIdentificationCodeKey other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(PersonType, other.PersonType) &&
                   string.Equals(IdentificationSystemDescriptorUri, other.IdentificationSystemDescriptorUri) &&
                   string.Equals(UniqueId, other.UniqueId) &&
                   EducationOrganizationId == other.EducationOrganizationId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((UniqueIdToIdentificationCodeKey) obj);
        }

        public override int GetHashCode()
            => HashCode.Combine(PersonType, IdentificationSystemDescriptorUri, UniqueId, EducationOrganizationId);
    }
}
