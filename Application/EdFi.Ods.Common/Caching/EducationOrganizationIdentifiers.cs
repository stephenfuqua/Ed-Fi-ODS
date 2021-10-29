// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

namespace EdFi.Ods.Common.Caching
{
    public class EducationOrganizationIdentifiers
    {
        private EducationOrganizationIdentifiers(int educationOrganizationId)
        {
            EducationOrganizationId = educationOrganizationId;
        }

        public EducationOrganizationIdentifiers() { }

        public EducationOrganizationIdentifiers(
            int educationOrganizationId,
            string educationOrganizationType,
            string nameOfInstitution = null,
            string fullEducationOrganizationType = null)
        {
            EducationOrganizationId = educationOrganizationId;
            EducationOrganizationType = educationOrganizationType;      
            NameOfInstitution = nameOfInstitution;
            FullEducationOrganizationType = fullEducationOrganizationType;
        }
        
        public int EducationOrganizationId { get; private set; }

        public string EducationOrganizationType { get; private set; }

        public string NameOfInstitution { get; private set; }

        public string FullEducationOrganizationType { get; private set; }

        public bool IsDefault => EducationOrganizationId == default(int)
                                 && EducationOrganizationType == null;                              

        public static EducationOrganizationIdentifiers CreateLookupInstance(int educationOrganizationId)
        {
            return new EducationOrganizationIdentifiers(educationOrganizationId);
        }
    }
}
