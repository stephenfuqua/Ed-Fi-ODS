using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EdFi.Ods.Api.ExceptionHandling;
using EdFi.Ods.Api.Providers;

namespace EdFi.Ods.Api.Common.ExceptionHandling.Sample
{
    [ExcludeFromCodeCoverage]
    public class DatabaseMetadataProvider : IDatabaseMetadataProvider
    {
        public IndexDetails GetIndexDetails(string indexName)
        {
            IndexDetails indexDetails = null;

            IndexDetailsByName.TryGetValue(indexName, out indexDetails);

            return indexDetails;
        }

        private static readonly Dictionary<string, IndexDetails> IndexDetailsByName = new Dictionary<string, IndexDetails>(StringComparer.InvariantCultureIgnoreCase) {
            { "ArtMediumDescriptor_PK", new IndexDetails { IndexName = "ArtMediumDescriptor_PK", TableName = "ArtMediumDescriptor", ColumnNames = new List<string> { "ArtMediumDescriptorId" } } },
            { "Bus_PK", new IndexDetails { IndexName = "Bus_PK", TableName = "Bus", ColumnNames = new List<string> { "BusId" } } },
            { "UX_Bus_Id", new IndexDetails { IndexName = "UX_Bus_Id", TableName = "Bus", ColumnNames = new List<string> { "Id" } } },
            { "BusRoute_PK", new IndexDetails { IndexName = "BusRoute_PK", TableName = "BusRoute", ColumnNames = new List<string> { "BusId", "BusRouteNumber" } } },
            { "FK_BusRoute_DisabilityDescriptor", new IndexDetails { IndexName = "FK_BusRoute_DisabilityDescriptor", TableName = "BusRoute", ColumnNames = new List<string> { "DisabilityDescriptorId" } } },
            { "FK_BusRoute_StaffEducationOrganizationAssignmentAssociation", new IndexDetails { IndexName = "FK_BusRoute_StaffEducationOrganizationAssignmentAssociation", TableName = "BusRoute", ColumnNames = new List<string> { "BeginDate", "EducationOrganizationId", "StaffClassificationDescriptorId", "StaffUSI" } } },
            { "UX_BusRoute_Id", new IndexDetails { IndexName = "UX_BusRoute_Id", TableName = "BusRoute", ColumnNames = new List<string> { "Id" } } },
            { "BusRouteBusYear_PK", new IndexDetails { IndexName = "BusRouteBusYear_PK", TableName = "BusRouteBusYear", ColumnNames = new List<string> { "BusId", "BusRouteNumber", "BusYear" } } },
            { "FK_BusRouteBusYear_BusRoute", new IndexDetails { IndexName = "FK_BusRouteBusYear_BusRoute", TableName = "BusRouteBusYear", ColumnNames = new List<string> { "BusId", "BusRouteNumber" } } },
            { "BusRouteProgram_PK", new IndexDetails { IndexName = "BusRouteProgram_PK", TableName = "BusRouteProgram", ColumnNames = new List<string> { "BusId", "BusRouteNumber", "EducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId" } } },
            { "FK_BusRouteProgram_BusRoute", new IndexDetails { IndexName = "FK_BusRouteProgram_BusRoute", TableName = "BusRouteProgram", ColumnNames = new List<string> { "BusId", "BusRouteNumber" } } },
            { "FK_BusRouteProgram_Program", new IndexDetails { IndexName = "FK_BusRouteProgram_Program", TableName = "BusRouteProgram", ColumnNames = new List<string> { "EducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId" } } },
            { "BusRouteServiceAreaPostalCode_PK", new IndexDetails { IndexName = "BusRouteServiceAreaPostalCode_PK", TableName = "BusRouteServiceAreaPostalCode", ColumnNames = new List<string> { "BusId", "BusRouteNumber", "ServiceAreaPostalCode" } } },
            { "FK_BusRouteServiceAreaPostalCode_BusRoute", new IndexDetails { IndexName = "FK_BusRouteServiceAreaPostalCode_BusRoute", TableName = "BusRouteServiceAreaPostalCode", ColumnNames = new List<string> { "BusId", "BusRouteNumber" } } },
            { "BusRouteStartTime_PK", new IndexDetails { IndexName = "BusRouteStartTime_PK", TableName = "BusRouteStartTime", ColumnNames = new List<string> { "BusId", "BusRouteNumber", "StartTime" } } },
            { "FK_BusRouteStartTime_BusRoute", new IndexDetails { IndexName = "FK_BusRouteStartTime_BusRoute", TableName = "BusRouteStartTime", ColumnNames = new List<string> { "BusId", "BusRouteNumber" } } },
            { "BusRouteTelephone_PK", new IndexDetails { IndexName = "BusRouteTelephone_PK", TableName = "BusRouteTelephone", ColumnNames = new List<string> { "BusId", "BusRouteNumber", "TelephoneNumber", "TelephoneNumberTypeDescriptorId" } } },
            { "FK_BusRouteTelephone_BusRoute", new IndexDetails { IndexName = "FK_BusRouteTelephone_BusRoute", TableName = "BusRouteTelephone", ColumnNames = new List<string> { "BusId", "BusRouteNumber" } } },
            { "FK_BusRouteTelephone_TelephoneNumberTypeDescriptor", new IndexDetails { IndexName = "FK_BusRouteTelephone_TelephoneNumberTypeDescriptor", TableName = "BusRouteTelephone", ColumnNames = new List<string> { "TelephoneNumberTypeDescriptorId" } } },
            { "ContactAddressExtension_PK", new IndexDetails { IndexName = "ContactAddressExtension_PK", TableName = "ContactAddressExtension", ColumnNames = new List<string> { "ContactUSI", "AddressTypeDescriptorId", "City", "PostalCode", "StateAbbreviationDescriptorId", "StreetNumberName" } } },
            { "ContactAddressSchoolDistrict_PK", new IndexDetails { IndexName = "ContactAddressSchoolDistrict_PK", TableName = "ContactAddressSchoolDistrict", ColumnNames = new List<string> { "ContactUSI", "AddressTypeDescriptorId", "City", "PostalCode", "StateAbbreviationDescriptorId", "StreetNumberName", "SchoolDistrict" } } },
            { "FK_ContactAddressSchoolDistrict_ContactAddress", new IndexDetails { IndexName = "FK_ContactAddressSchoolDistrict_ContactAddress", TableName = "ContactAddressSchoolDistrict", ColumnNames = new List<string> { "ContactUSI", "AddressTypeDescriptorId", "City", "PostalCode", "StateAbbreviationDescriptorId", "StreetNumberName" } } },
            { "ContactAddressTerm_PK", new IndexDetails { IndexName = "ContactAddressTerm_PK", TableName = "ContactAddressTerm", ColumnNames = new List<string> { "ContactUSI", "AddressTypeDescriptorId", "City", "PostalCode", "StateAbbreviationDescriptorId", "StreetNumberName", "TermDescriptorId" } } },
            { "FK_ContactAddressTerm_ContactAddress", new IndexDetails { IndexName = "FK_ContactAddressTerm_ContactAddress", TableName = "ContactAddressTerm", ColumnNames = new List<string> { "ContactUSI", "AddressTypeDescriptorId", "City", "PostalCode", "StateAbbreviationDescriptorId", "StreetNumberName" } } },
            { "FK_ContactAddressTerm_TermDescriptor", new IndexDetails { IndexName = "FK_ContactAddressTerm_TermDescriptor", TableName = "ContactAddressTerm", ColumnNames = new List<string> { "TermDescriptorId" } } },
            { "ContactAuthor_PK", new IndexDetails { IndexName = "ContactAuthor_PK", TableName = "ContactAuthor", ColumnNames = new List<string> { "ContactUSI", "Author" } } },
            { "FK_ContactAuthor_Contact", new IndexDetails { IndexName = "FK_ContactAuthor_Contact", TableName = "ContactAuthor", ColumnNames = new List<string> { "ContactUSI" } } },
            { "ContactCeilingHeight_PK", new IndexDetails { IndexName = "ContactCeilingHeight_PK", TableName = "ContactCeilingHeight", ColumnNames = new List<string> { "ContactUSI", "CeilingHeight" } } },
            { "FK_ContactCeilingHeight_Contact", new IndexDetails { IndexName = "FK_ContactCeilingHeight_Contact", TableName = "ContactCeilingHeight", ColumnNames = new List<string> { "ContactUSI" } } },
            { "ContactCTEProgram_PK", new IndexDetails { IndexName = "ContactCTEProgram_PK", TableName = "ContactCTEProgram", ColumnNames = new List<string> { "ContactUSI" } } },
            { "FK_ContactCTEProgram_CareerPathwayDescriptor", new IndexDetails { IndexName = "FK_ContactCTEProgram_CareerPathwayDescriptor", TableName = "ContactCTEProgram", ColumnNames = new List<string> { "CareerPathwayDescriptorId" } } },
            { "ContactEducationContent_PK", new IndexDetails { IndexName = "ContactEducationContent_PK", TableName = "ContactEducationContent", ColumnNames = new List<string> { "ContactUSI", "ContentIdentifier" } } },
            { "FK_ContactEducationContent_Contact", new IndexDetails { IndexName = "FK_ContactEducationContent_Contact", TableName = "ContactEducationContent", ColumnNames = new List<string> { "ContactUSI" } } },
            { "FK_ContactEducationContent_EducationContent", new IndexDetails { IndexName = "FK_ContactEducationContent_EducationContent", TableName = "ContactEducationContent", ColumnNames = new List<string> { "ContentIdentifier" } } },
            { "ContactExtension_PK", new IndexDetails { IndexName = "ContactExtension_PK", TableName = "ContactExtension", ColumnNames = new List<string> { "ContactUSI" } } },
            { "FK_ContactExtension_CredentialFieldDescriptor", new IndexDetails { IndexName = "FK_ContactExtension_CredentialFieldDescriptor", TableName = "ContactExtension", ColumnNames = new List<string> { "CredentialFieldDescriptorId" } } },
            { "ContactFavoriteBookTitle_PK", new IndexDetails { IndexName = "ContactFavoriteBookTitle_PK", TableName = "ContactFavoriteBookTitle", ColumnNames = new List<string> { "ContactUSI", "FavoriteBookTitle" } } },
            { "FK_ContactFavoriteBookTitle_Contact", new IndexDetails { IndexName = "FK_ContactFavoriteBookTitle_Contact", TableName = "ContactFavoriteBookTitle", ColumnNames = new List<string> { "ContactUSI" } } },
            { "ContactStudentProgramAssociation_PK", new IndexDetails { IndexName = "ContactStudentProgramAssociation_PK", TableName = "ContactStudentProgramAssociation", ColumnNames = new List<string> { "ContactUSI", "BeginDate", "EducationOrganizationId", "ProgramEducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId", "StudentUSI" } } },
            { "FK_ContactStudentProgramAssociation_Contact", new IndexDetails { IndexName = "FK_ContactStudentProgramAssociation_Contact", TableName = "ContactStudentProgramAssociation", ColumnNames = new List<string> { "ContactUSI" } } },
            { "FK_ContactStudentProgramAssociation_StudentProgramAssociation", new IndexDetails { IndexName = "FK_ContactStudentProgramAssociation_StudentProgramAssociation", TableName = "ContactStudentProgramAssociation", ColumnNames = new List<string> { "BeginDate", "EducationOrganizationId", "ProgramEducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId", "StudentUSI" } } },
            { "ContactTeacherConference_PK", new IndexDetails { IndexName = "ContactTeacherConference_PK", TableName = "ContactTeacherConference", ColumnNames = new List<string> { "ContactUSI" } } },
            { "FavoriteBookCategoryDescriptor_PK", new IndexDetails { IndexName = "FavoriteBookCategoryDescriptor_PK", TableName = "FavoriteBookCategoryDescriptor", ColumnNames = new List<string> { "FavoriteBookCategoryDescriptorId" } } },
            { "MembershipTypeDescriptor_PK", new IndexDetails { IndexName = "MembershipTypeDescriptor_PK", TableName = "MembershipTypeDescriptor", ColumnNames = new List<string> { "MembershipTypeDescriptorId" } } },
            { "FK_SchoolCTEProgram_CareerPathwayDescriptor", new IndexDetails { IndexName = "FK_SchoolCTEProgram_CareerPathwayDescriptor", TableName = "SchoolCTEProgram", ColumnNames = new List<string> { "CareerPathwayDescriptorId" } } },
            { "SchoolCTEProgram_PK", new IndexDetails { IndexName = "SchoolCTEProgram_PK", TableName = "SchoolCTEProgram", ColumnNames = new List<string> { "SchoolId" } } },
            { "FK_SchoolDirectlyOwnedBus_Bus", new IndexDetails { IndexName = "FK_SchoolDirectlyOwnedBus_Bus", TableName = "SchoolDirectlyOwnedBus", ColumnNames = new List<string> { "DirectlyOwnedBusId" } } },
            { "FK_SchoolDirectlyOwnedBus_School", new IndexDetails { IndexName = "FK_SchoolDirectlyOwnedBus_School", TableName = "SchoolDirectlyOwnedBus", ColumnNames = new List<string> { "SchoolId" } } },
            { "SchoolDirectlyOwnedBus_PK", new IndexDetails { IndexName = "SchoolDirectlyOwnedBus_PK", TableName = "SchoolDirectlyOwnedBus", ColumnNames = new List<string> { "SchoolId", "DirectlyOwnedBusId" } } },
            { "SchoolExtension_PK", new IndexDetails { IndexName = "SchoolExtension_PK", TableName = "SchoolExtension", ColumnNames = new List<string> { "SchoolId" } } },
            { "StaffExtension_PK", new IndexDetails { IndexName = "StaffExtension_PK", TableName = "StaffExtension", ColumnNames = new List<string> { "StaffUSI" } } },
            { "FK_StaffPet_Staff", new IndexDetails { IndexName = "FK_StaffPet_Staff", TableName = "StaffPet", ColumnNames = new List<string> { "StaffUSI" } } },
            { "StaffPet_PK", new IndexDetails { IndexName = "StaffPet_PK", TableName = "StaffPet", ColumnNames = new List<string> { "StaffUSI", "PetName" } } },
            { "StaffPetPreference_PK", new IndexDetails { IndexName = "StaffPetPreference_PK", TableName = "StaffPetPreference", ColumnNames = new List<string> { "StaffUSI" } } },
            { "FK_StudentAquaticPet_Student", new IndexDetails { IndexName = "FK_StudentAquaticPet_Student", TableName = "StudentAquaticPet", ColumnNames = new List<string> { "StudentUSI" } } },
            { "StudentAquaticPet_PK", new IndexDetails { IndexName = "StudentAquaticPet_PK", TableName = "StudentAquaticPet", ColumnNames = new List<string> { "StudentUSI", "MimimumTankVolume", "PetName" } } },
            { "StudentArtProgramAssociation_PK", new IndexDetails { IndexName = "StudentArtProgramAssociation_PK", TableName = "StudentArtProgramAssociation", ColumnNames = new List<string> { "BeginDate", "EducationOrganizationId", "ProgramEducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId", "StudentUSI" } } },
            { "FK_StudentArtProgramAssociationArtMedium_ArtMediumDescriptor", new IndexDetails { IndexName = "FK_StudentArtProgramAssociationArtMedium_ArtMediumDescriptor", TableName = "StudentArtProgramAssociationArtMedium", ColumnNames = new List<string> { "ArtMediumDescriptorId" } } },
            { "FK_StudentArtProgramAssociationArtMedium_StudentArtProgramAssociation", new IndexDetails { IndexName = "FK_StudentArtProgramAssociationArtMedium_StudentArtProgramAssociation", TableName = "StudentArtProgramAssociationArtMedium", ColumnNames = new List<string> { "BeginDate", "EducationOrganizationId", "ProgramEducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId", "StudentUSI" } } },
            { "StudentArtProgramAssociationArtMedium_PK", new IndexDetails { IndexName = "StudentArtProgramAssociationArtMedium_PK", TableName = "StudentArtProgramAssociationArtMedium", ColumnNames = new List<string> { "BeginDate", "EducationOrganizationId", "ProgramEducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId", "StudentUSI", "ArtMediumDescriptorId" } } },
            { "FK_StudentArtProgramAssociationPortfolioYears_StudentArtProgramAssociation", new IndexDetails { IndexName = "FK_StudentArtProgramAssociationPortfolioYears_StudentArtProgramAssociation", TableName = "StudentArtProgramAssociationPortfolioYears", ColumnNames = new List<string> { "BeginDate", "EducationOrganizationId", "ProgramEducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId", "StudentUSI" } } },
            { "StudentArtProgramAssociationPortfolioYears_PK", new IndexDetails { IndexName = "StudentArtProgramAssociationPortfolioYears_PK", TableName = "StudentArtProgramAssociationPortfolioYears", ColumnNames = new List<string> { "BeginDate", "EducationOrganizationId", "ProgramEducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId", "StudentUSI", "PortfolioYears" } } },
            { "FK_StudentArtProgramAssociationService_ServiceDescriptor", new IndexDetails { IndexName = "FK_StudentArtProgramAssociationService_ServiceDescriptor", TableName = "StudentArtProgramAssociationService", ColumnNames = new List<string> { "ServiceDescriptorId" } } },
            { "FK_StudentArtProgramAssociationService_StudentArtProgramAssociation", new IndexDetails { IndexName = "FK_StudentArtProgramAssociationService_StudentArtProgramAssociation", TableName = "StudentArtProgramAssociationService", ColumnNames = new List<string> { "BeginDate", "EducationOrganizationId", "ProgramEducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId", "StudentUSI" } } },
            { "StudentArtProgramAssociationService_PK", new IndexDetails { IndexName = "StudentArtProgramAssociationService_PK", TableName = "StudentArtProgramAssociationService", ColumnNames = new List<string> { "BeginDate", "EducationOrganizationId", "ProgramEducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId", "StudentUSI", "ServiceDescriptorId" } } },
            { "FK_StudentArtProgramAssociationStyle_StudentArtProgramAssociation", new IndexDetails { IndexName = "FK_StudentArtProgramAssociationStyle_StudentArtProgramAssociation", TableName = "StudentArtProgramAssociationStyle", ColumnNames = new List<string> { "BeginDate", "EducationOrganizationId", "ProgramEducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId", "StudentUSI" } } },
            { "StudentArtProgramAssociationStyle_PK", new IndexDetails { IndexName = "StudentArtProgramAssociationStyle_PK", TableName = "StudentArtProgramAssociationStyle", ColumnNames = new List<string> { "BeginDate", "EducationOrganizationId", "ProgramEducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId", "StudentUSI", "Style" } } },
            { "FK_StudentContactAssociationDiscipline_DisciplineDescriptor", new IndexDetails { IndexName = "FK_StudentContactAssociationDiscipline_DisciplineDescriptor", TableName = "StudentContactAssociationDiscipline", ColumnNames = new List<string> { "DisciplineDescriptorId" } } },
            { "FK_StudentContactAssociationDiscipline_StudentContactAssociation", new IndexDetails { IndexName = "FK_StudentContactAssociationDiscipline_StudentContactAssociation", TableName = "StudentContactAssociationDiscipline", ColumnNames = new List<string> { "ContactUSI", "StudentUSI" } } },
            { "StudentContactAssociationDiscipline_PK", new IndexDetails { IndexName = "StudentContactAssociationDiscipline_PK", TableName = "StudentContactAssociationDiscipline", ColumnNames = new List<string> { "ContactUSI", "StudentUSI", "DisciplineDescriptorId" } } },
            { "FK_StudentContactAssociationExtension_InterventionStudy", new IndexDetails { IndexName = "FK_StudentContactAssociationExtension_InterventionStudy", TableName = "StudentContactAssociationExtension", ColumnNames = new List<string> { "EducationOrganizationId", "InterventionStudyIdentificationCode" } } },
            { "StudentContactAssociationExtension_PK", new IndexDetails { IndexName = "StudentContactAssociationExtension_PK", TableName = "StudentContactAssociationExtension", ColumnNames = new List<string> { "ContactUSI", "StudentUSI" } } },
            { "FK_StudentContactAssociationFavoriteBookTitle_StudentContactAssociation", new IndexDetails { IndexName = "FK_StudentContactAssociationFavoriteBookTitle_StudentContactAssociation", TableName = "StudentContactAssociationFavoriteBookTitle", ColumnNames = new List<string> { "ContactUSI", "StudentUSI" } } },
            { "StudentContactAssociationFavoriteBookTitle_PK", new IndexDetails { IndexName = "StudentContactAssociationFavoriteBookTitle_PK", TableName = "StudentContactAssociationFavoriteBookTitle", ColumnNames = new List<string> { "ContactUSI", "StudentUSI", "FavoriteBookTitle" } } },
            { "FK_StudentContactAssociationHoursPerWeek_StudentContactAssociation", new IndexDetails { IndexName = "FK_StudentContactAssociationHoursPerWeek_StudentContactAssociation", TableName = "StudentContactAssociationHoursPerWeek", ColumnNames = new List<string> { "ContactUSI", "StudentUSI" } } },
            { "StudentContactAssociationHoursPerWeek_PK", new IndexDetails { IndexName = "StudentContactAssociationHoursPerWeek_PK", TableName = "StudentContactAssociationHoursPerWeek", ColumnNames = new List<string> { "ContactUSI", "StudentUSI", "HoursPerWeek" } } },
            { "FK_StudentContactAssociationPagesRead_StudentContactAssociation", new IndexDetails { IndexName = "FK_StudentContactAssociationPagesRead_StudentContactAssociation", TableName = "StudentContactAssociationPagesRead", ColumnNames = new List<string> { "ContactUSI", "StudentUSI" } } },
            { "StudentContactAssociationPagesRead_PK", new IndexDetails { IndexName = "StudentContactAssociationPagesRead_PK", TableName = "StudentContactAssociationPagesRead", ColumnNames = new List<string> { "ContactUSI", "StudentUSI", "PagesRead" } } },
            { "FK_StudentContactAssociationStaffEducationOrganizationEmploymentAssociation_StaffEducationOrganizationEmploymentAssociation", new IndexDetails { IndexName = "FK_StudentContactAssociationStaffEducationOrganizationEmploymentAssociation_StaffEducationOrganizationEmploymentAssociation", TableName = "StudentContactAssociationStaffEducationOrganizationEmploymentAssociation", ColumnNames = new List<string> { "EducationOrganizationId", "EmploymentStatusDescriptorId", "HireDate", "StaffUSI" } } },
            { "FK_StudentContactAssociationStaffEducationOrganizationEmploymentAssociation_StudentContactAssociation", new IndexDetails { IndexName = "FK_StudentContactAssociationStaffEducationOrganizationEmploymentAssociation_StudentContactAssociation", TableName = "StudentContactAssociationStaffEducationOrganizationEmploymentAssociation", ColumnNames = new List<string> { "ContactUSI", "StudentUSI" } } },
            { "StudentContactAssociationStaffEducationOrganizationEmploymentAssociation_PK", new IndexDetails { IndexName = "StudentContactAssociationStaffEducationOrganizationEmploymentAssociation_PK", TableName = "StudentContactAssociationStaffEducationOrganizationEmploymentAssociation", ColumnNames = new List<string> { "ContactUSI", "StudentUSI", "EducationOrganizationId", "EmploymentStatusDescriptorId", "HireDate", "StaffUSI" } } },
            { "FK_StudentContactAssociationTelephone_TelephoneNumberTypeDescriptor", new IndexDetails { IndexName = "FK_StudentContactAssociationTelephone_TelephoneNumberTypeDescriptor", TableName = "StudentContactAssociationTelephone", ColumnNames = new List<string> { "TelephoneNumberTypeDescriptorId" } } },
            { "StudentContactAssociationTelephone_PK", new IndexDetails { IndexName = "StudentContactAssociationTelephone_PK", TableName = "StudentContactAssociationTelephone", ColumnNames = new List<string> { "ContactUSI", "StudentUSI" } } },
            { "StudentCTEProgramAssociationExtension_PK", new IndexDetails { IndexName = "StudentCTEProgramAssociationExtension_PK", TableName = "StudentCTEProgramAssociationExtension", ColumnNames = new List<string> { "BeginDate", "EducationOrganizationId", "ProgramEducationOrganizationId", "ProgramName", "ProgramTypeDescriptorId", "StudentUSI" } } },
            { "StudentEducationOrganizationAssociationAddressExtension_PK", new IndexDetails { IndexName = "StudentEducationOrganizationAssociationAddressExtension_PK", TableName = "StudentEducationOrganizationAssociationAddressExtension", ColumnNames = new List<string> { "EducationOrganizationId", "StudentUSI", "AddressTypeDescriptorId", "City", "PostalCode", "StateAbbreviationDescriptorId", "StreetNumberName" } } },
            { "FK_StudentEducationOrganizationAssociationAddressSchoolDistrict_StudentEducationOrganizationAssociationAddress", new IndexDetails { IndexName = "FK_StudentEducationOrganizationAssociationAddressSchoolDistrict_StudentEducationOrganizationAssociationAddress", TableName = "StudentEducationOrganizationAssociationAddressSchoolDistrict", ColumnNames = new List<string> { "EducationOrganizationId", "StudentUSI", "AddressTypeDescriptorId", "City", "PostalCode", "StateAbbreviationDescriptorId", "StreetNumberName" } } },
            { "StudentEducationOrganizationAssociationAddressSchoolDistrict_PK", new IndexDetails { IndexName = "StudentEducationOrganizationAssociationAddressSchoolDistrict_PK", TableName = "StudentEducationOrganizationAssociationAddressSchoolDistrict", ColumnNames = new List<string> { "EducationOrganizationId", "StudentUSI", "AddressTypeDescriptorId", "City", "PostalCode", "StateAbbreviationDescriptorId", "StreetNumberName", "SchoolDistrict" } } },
            { "FK_StudentEducationOrganizationAssociationAddressTerm_StudentEducationOrganizationAssociationAddress", new IndexDetails { IndexName = "FK_StudentEducationOrganizationAssociationAddressTerm_StudentEducationOrganizationAssociationAddress", TableName = "StudentEducationOrganizationAssociationAddressTerm", ColumnNames = new List<string> { "EducationOrganizationId", "StudentUSI", "AddressTypeDescriptorId", "City", "PostalCode", "StateAbbreviationDescriptorId", "StreetNumberName" } } },
            { "FK_StudentEducationOrganizationAssociationAddressTerm_TermDescriptor", new IndexDetails { IndexName = "FK_StudentEducationOrganizationAssociationAddressTerm_TermDescriptor", TableName = "StudentEducationOrganizationAssociationAddressTerm", ColumnNames = new List<string> { "TermDescriptorId" } } },
            { "StudentEducationOrganizationAssociationAddressTerm_PK", new IndexDetails { IndexName = "StudentEducationOrganizationAssociationAddressTerm_PK", TableName = "StudentEducationOrganizationAssociationAddressTerm", ColumnNames = new List<string> { "EducationOrganizationId", "StudentUSI", "AddressTypeDescriptorId", "City", "PostalCode", "StateAbbreviationDescriptorId", "StreetNumberName", "TermDescriptorId" } } },
            { "FK_StudentEducationOrganizationAssociationExtension_Program", new IndexDetails { IndexName = "FK_StudentEducationOrganizationAssociationExtension_Program", TableName = "StudentEducationOrganizationAssociationExtension", ColumnNames = new List<string> { "EducationOrganizationId", "FavoriteProgramName", "FavoriteProgramTypeDescriptorId" } } },
            { "StudentEducationOrganizationAssociationExtension_PK", new IndexDetails { IndexName = "StudentEducationOrganizationAssociationExtension_PK", TableName = "StudentEducationOrganizationAssociationExtension", ColumnNames = new List<string> { "EducationOrganizationId", "StudentUSI" } } },
            { "FK_StudentEducationOrganizationAssociationStudentCharacteristicStudentNeed_StudentEducationOrganizationAssociationStudentCharact", new IndexDetails { IndexName = "FK_StudentEducationOrganizationAssociationStudentCharacteristicStudentNeed_StudentEducationOrganizationAssociationStudentCharact", TableName = "StudentEducationOrganizationAssociationStudentCharacteristicStudentNeed", ColumnNames = new List<string> { "EducationOrganizationId", "StudentUSI", "StudentCharacteristicDescriptorId" } } },
            { "StudentEducationOrganizationAssociationStudentCharacteristicStudentNeed_PK", new IndexDetails { IndexName = "StudentEducationOrganizationAssociationStudentCharacteristicStudentNeed_PK", TableName = "StudentEducationOrganizationAssociationStudentCharacteristicStudentNeed", ColumnNames = new List<string> { "EducationOrganizationId", "StudentUSI", "StudentCharacteristicDescriptorId", "BeginDate" } } },
            { "FK_StudentFavoriteBook_FavoriteBookCategoryDescriptor", new IndexDetails { IndexName = "FK_StudentFavoriteBook_FavoriteBookCategoryDescriptor", TableName = "StudentFavoriteBook", ColumnNames = new List<string> { "FavoriteBookCategoryDescriptorId" } } },
            { "FK_StudentFavoriteBook_Student", new IndexDetails { IndexName = "FK_StudentFavoriteBook_Student", TableName = "StudentFavoriteBook", ColumnNames = new List<string> { "StudentUSI" } } },
            { "StudentFavoriteBook_PK", new IndexDetails { IndexName = "StudentFavoriteBook_PK", TableName = "StudentFavoriteBook", ColumnNames = new List<string> { "StudentUSI", "FavoriteBookCategoryDescriptorId" } } },
            { "FK_StudentFavoriteBookArtMedium_ArtMediumDescriptor", new IndexDetails { IndexName = "FK_StudentFavoriteBookArtMedium_ArtMediumDescriptor", TableName = "StudentFavoriteBookArtMedium", ColumnNames = new List<string> { "ArtMediumDescriptorId" } } },
            { "FK_StudentFavoriteBookArtMedium_StudentFavoriteBook", new IndexDetails { IndexName = "FK_StudentFavoriteBookArtMedium_StudentFavoriteBook", TableName = "StudentFavoriteBookArtMedium", ColumnNames = new List<string> { "StudentUSI", "FavoriteBookCategoryDescriptorId" } } },
            { "StudentFavoriteBookArtMedium_PK", new IndexDetails { IndexName = "StudentFavoriteBookArtMedium_PK", TableName = "StudentFavoriteBookArtMedium", ColumnNames = new List<string> { "StudentUSI", "FavoriteBookCategoryDescriptorId", "ArtMediumDescriptorId" } } },
            { "FK_StudentGraduationPlanAssociation_GraduationPlan", new IndexDetails { IndexName = "FK_StudentGraduationPlanAssociation_GraduationPlan", TableName = "StudentGraduationPlanAssociation", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear" } } },
            { "FK_StudentGraduationPlanAssociation_Staff", new IndexDetails { IndexName = "FK_StudentGraduationPlanAssociation_Staff", TableName = "StudentGraduationPlanAssociation", ColumnNames = new List<string> { "StaffUSI" } } },
            { "FK_StudentGraduationPlanAssociation_Student", new IndexDetails { IndexName = "FK_StudentGraduationPlanAssociation_Student", TableName = "StudentGraduationPlanAssociation", ColumnNames = new List<string> { "StudentUSI" } } },
            { "StudentGraduationPlanAssociation_PK", new IndexDetails { IndexName = "StudentGraduationPlanAssociation_PK", TableName = "StudentGraduationPlanAssociation", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI" } } },
            { "UX_StudentGraduationPlanAssociation_Id", new IndexDetails { IndexName = "UX_StudentGraduationPlanAssociation_Id", TableName = "StudentGraduationPlanAssociation", ColumnNames = new List<string> { "Id" } } },
            { "FK_StudentGraduationPlanAssociationAcademicSubject_AcademicSubjectDescriptor", new IndexDetails { IndexName = "FK_StudentGraduationPlanAssociationAcademicSubject_AcademicSubjectDescriptor", TableName = "StudentGraduationPlanAssociationAcademicSubject", ColumnNames = new List<string> { "AcademicSubjectDescriptorId" } } },
            { "FK_StudentGraduationPlanAssociationAcademicSubject_StudentGraduationPlanAssociation", new IndexDetails { IndexName = "FK_StudentGraduationPlanAssociationAcademicSubject_StudentGraduationPlanAssociation", TableName = "StudentGraduationPlanAssociationAcademicSubject", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI" } } },
            { "StudentGraduationPlanAssociationAcademicSubject_PK", new IndexDetails { IndexName = "StudentGraduationPlanAssociationAcademicSubject_PK", TableName = "StudentGraduationPlanAssociationAcademicSubject", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI", "AcademicSubjectDescriptorId" } } },
            { "FK_StudentGraduationPlanAssociationCareerPathwayCode_StudentGraduationPlanAssociation", new IndexDetails { IndexName = "FK_StudentGraduationPlanAssociationCareerPathwayCode_StudentGraduationPlanAssociation", TableName = "StudentGraduationPlanAssociationCareerPathwayCode", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI" } } },
            { "StudentGraduationPlanAssociationCareerPathwayCode_PK", new IndexDetails { IndexName = "StudentGraduationPlanAssociationCareerPathwayCode_PK", TableName = "StudentGraduationPlanAssociationCareerPathwayCode", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI", "CareerPathwayCode" } } },
            { "FK_StudentGraduationPlanAssociationCTEProgram_CareerPathwayDescriptor", new IndexDetails { IndexName = "FK_StudentGraduationPlanAssociationCTEProgram_CareerPathwayDescriptor", TableName = "StudentGraduationPlanAssociationCTEProgram", ColumnNames = new List<string> { "CareerPathwayDescriptorId" } } },
            { "StudentGraduationPlanAssociationCTEProgram_PK", new IndexDetails { IndexName = "StudentGraduationPlanAssociationCTEProgram_PK", TableName = "StudentGraduationPlanAssociationCTEProgram", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI" } } },
            { "FK_StudentGraduationPlanAssociationDescription_StudentGraduationPlanAssociation", new IndexDetails { IndexName = "FK_StudentGraduationPlanAssociationDescription_StudentGraduationPlanAssociation", TableName = "StudentGraduationPlanAssociationDescription", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI" } } },
            { "StudentGraduationPlanAssociationDescription_PK", new IndexDetails { IndexName = "StudentGraduationPlanAssociationDescription_PK", TableName = "StudentGraduationPlanAssociationDescription", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI", "Description" } } },
            { "FK_StudentGraduationPlanAssociationDesignatedBy_StudentGraduationPlanAssociation", new IndexDetails { IndexName = "FK_StudentGraduationPlanAssociationDesignatedBy_StudentGraduationPlanAssociation", TableName = "StudentGraduationPlanAssociationDesignatedBy", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI" } } },
            { "StudentGraduationPlanAssociationDesignatedBy_PK", new IndexDetails { IndexName = "StudentGraduationPlanAssociationDesignatedBy_PK", TableName = "StudentGraduationPlanAssociationDesignatedBy", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI", "DesignatedBy" } } },
            { "FK_StudentGraduationPlanAssociationIndustryCredential_StudentGraduationPlanAssociation", new IndexDetails { IndexName = "FK_StudentGraduationPlanAssociationIndustryCredential_StudentGraduationPlanAssociation", TableName = "StudentGraduationPlanAssociationIndustryCredential", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI" } } },
            { "StudentGraduationPlanAssociationIndustryCredential_PK", new IndexDetails { IndexName = "StudentGraduationPlanAssociationIndustryCredential_PK", TableName = "StudentGraduationPlanAssociationIndustryCredential", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI", "IndustryCredential" } } },
            { "FK_StudentGraduationPlanAssociationStudentContactAssociation_StudentContactAssociation", new IndexDetails { IndexName = "FK_StudentGraduationPlanAssociationStudentContactAssociation_StudentContactAssociation", TableName = "StudentGraduationPlanAssociationStudentContactAssociation", ColumnNames = new List<string> { "ContactUSI", "StudentUSI" } } },
            { "FK_StudentGraduationPlanAssociationStudentContactAssociation_StudentGraduationPlanAssociation", new IndexDetails { IndexName = "FK_StudentGraduationPlanAssociationStudentContactAssociation_StudentGraduationPlanAssociation", TableName = "StudentGraduationPlanAssociationStudentContactAssociation", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI" } } },
            { "StudentGraduationPlanAssociationStudentContactAssociation_PK", new IndexDetails { IndexName = "StudentGraduationPlanAssociationStudentContactAssociation_PK", TableName = "StudentGraduationPlanAssociationStudentContactAssociation", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI", "ContactUSI" } } },
            { "FK_StudentGraduationPlanAssociationYearsAttended_StudentGraduationPlanAssociation", new IndexDetails { IndexName = "FK_StudentGraduationPlanAssociationYearsAttended_StudentGraduationPlanAssociation", TableName = "StudentGraduationPlanAssociationYearsAttended", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI" } } },
            { "StudentGraduationPlanAssociationYearsAttended_PK", new IndexDetails { IndexName = "StudentGraduationPlanAssociationYearsAttended_PK", TableName = "StudentGraduationPlanAssociationYearsAttended", ColumnNames = new List<string> { "EducationOrganizationId", "GraduationPlanTypeDescriptorId", "GraduationSchoolYear", "StudentUSI", "YearsAttended" } } },
            { "FK_StudentPet_Student", new IndexDetails { IndexName = "FK_StudentPet_Student", TableName = "StudentPet", ColumnNames = new List<string> { "StudentUSI" } } },
            { "StudentPet_PK", new IndexDetails { IndexName = "StudentPet_PK", TableName = "StudentPet", ColumnNames = new List<string> { "StudentUSI", "PetName" } } },
            { "StudentPetPreference_PK", new IndexDetails { IndexName = "StudentPetPreference_PK", TableName = "StudentPetPreference", ColumnNames = new List<string> { "StudentUSI" } } },
            { "FK_StudentSchoolAssociationExtension_MembershipTypeDescriptor", new IndexDetails { IndexName = "FK_StudentSchoolAssociationExtension_MembershipTypeDescriptor", TableName = "StudentSchoolAssociationExtension", ColumnNames = new List<string> { "MembershipTypeDescriptorId" } } },
            { "StudentSchoolAssociationExtension_PK", new IndexDetails { IndexName = "StudentSchoolAssociationExtension_PK", TableName = "StudentSchoolAssociationExtension", ColumnNames = new List<string> { "EntryDate", "SchoolId", "StudentUSI" } } },
            { "FK_StudentSectionAssociationRelatedGeneralStudentProgramAssociation_GeneralStudentProgramAssociation", new IndexDetails { IndexName = "FK_StudentSectionAssociationRelatedGeneralStudentProgramAssociation_GeneralStudentProgramAssociation", TableName = "StudentSectionAssociationRelatedGeneralStudentProgramAssociation", ColumnNames = new List<string> { "RelatedBeginDate", "RelatedEducationOrganizationId", "RelatedProgramEducationOrganizationId", "RelatedProgramName", "RelatedProgramTypeDescriptorId", "StudentUSI" } } },
            { "FK_StudentSectionAssociationRelatedGeneralStudentProgramAssociation_StudentSectionAssociation", new IndexDetails { IndexName = "FK_StudentSectionAssociationRelatedGeneralStudentProgramAssociation_StudentSectionAssociation", TableName = "StudentSectionAssociationRelatedGeneralStudentProgramAssociation", ColumnNames = new List<string> { "BeginDate", "LocalCourseCode", "SchoolId", "SchoolYear", "SectionIdentifier", "SessionName", "StudentUSI" } } },
            { "StudentSectionAssociationRelatedGeneralStudentProgramAssociation_PK", new IndexDetails { IndexName = "StudentSectionAssociationRelatedGeneralStudentProgramAssociation_PK", TableName = "StudentSectionAssociationRelatedGeneralStudentProgramAssociation", ColumnNames = new List<string> { "BeginDate", "LocalCourseCode", "SchoolId", "SchoolYear", "SectionIdentifier", "SessionName", "StudentUSI", "RelatedBeginDate", "RelatedEducationOrganizationId", "RelatedProgramEducationOrganizationId", "RelatedProgramName", "RelatedProgramTypeDescriptorId" } } },
        };
    }
}
