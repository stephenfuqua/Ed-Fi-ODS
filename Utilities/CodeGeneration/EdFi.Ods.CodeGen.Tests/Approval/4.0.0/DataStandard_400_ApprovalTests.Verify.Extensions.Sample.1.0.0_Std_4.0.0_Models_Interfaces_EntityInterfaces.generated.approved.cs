using System;
using System.Linq;
using System.Collections.Generic;
using EdFi.Ods.Api.Attributes;
using EdFi.Ods.Common.Attributes;
using EdFi.Ods.Common;
using EdFi.Ods.Common.Extensions;
using EdFi.Ods.Common.Models;
using EdFi.Ods.Entities.Common.EdFi;

#pragma warning disable 108,114

namespace EdFi.Ods.Entities.Common.Sample
{

    /// <summary>
    /// Defines available properties and methods for the abstraction of the ArtMediumDescriptor model.
    /// </summary>
    public interface IArtMediumDescriptor : EdFi.IDescriptor, ISynchronizable, IMappable, IHasIdentifier, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember][AutoIncrement]
        int ArtMediumDescriptorId { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class ArtMediumDescriptorMappingContract : IMappingContract
    {
        public ArtMediumDescriptorMappingContract(
            bool isCodeValueSupported,
            bool isDescriptionSupported,
            bool isEffectiveBeginDateSupported,
            bool isEffectiveEndDateSupported,
            bool isNamespaceSupported,
            bool isPriorDescriptorIdSupported,
            bool isShortDescriptionSupported
            )
        {
            IsCodeValueSupported = isCodeValueSupported;
            IsDescriptionSupported = isDescriptionSupported;
            IsEffectiveBeginDateSupported = isEffectiveBeginDateSupported;
            IsEffectiveEndDateSupported = isEffectiveEndDateSupported;
            IsNamespaceSupported = isNamespaceSupported;
            IsPriorDescriptorIdSupported = isPriorDescriptorIdSupported;
            IsShortDescriptionSupported = isShortDescriptionSupported;
        }

        public bool IsCodeValueSupported { get; }
        public bool IsDescriptionSupported { get; }
        public bool IsEffectiveBeginDateSupported { get; }
        public bool IsEffectiveEndDateSupported { get; }
        public bool IsNamespaceSupported { get; }
        public bool IsPriorDescriptorIdSupported { get; }
        public bool IsShortDescriptionSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "CodeValue":
                    return IsCodeValueSupported;
                case "Description":
                    return IsDescriptionSupported;
                case "EffectiveBeginDate":
                    return IsEffectiveBeginDateSupported;
                case "EffectiveEndDate":
                    return IsEffectiveEndDateSupported;
                case "Namespace":
                    return IsNamespaceSupported;
                case "PriorDescriptorId":
                    return IsPriorDescriptorIdSupported;
                case "ShortDescription":
                    return IsShortDescriptionSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the Bus model.
    /// </summary>
    public interface IBus : ISynchronizable, IMappable, IHasIdentifier, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        string BusId { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class BusMappingContract : IMappingContract
    {
        public BusMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the BusRoute model.
    /// </summary>
    public interface IBusRoute : ISynchronizable, IMappable, IHasIdentifier, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        string BusId { get; set; }
        [NaturalKeyMember]
        int BusRouteNumber { get; set; }

        // Non-PK properties
        DateTime? BeginDate { get; set; }
        string BusRouteDirection { get; set; }
        int? BusRouteDuration { get; set; }
        bool? Daily { get; set; }
        string DisabilityDescriptor { get; set; }
        int? EducationOrganizationId { get; set; }
        string ExpectedTransitTime { get; set; }
        decimal HoursPerWeek { get; set; }
        decimal OperatingCost { get; set; }
        decimal? OptimalCapacity { get; set; }
        string StaffClassificationDescriptor { get; set; }
        string StaffUniqueId { get; set; }
        DateTime? StartDate { get; set; }
        decimal? WeeklyMileage { get; set; }

        // One-to-one relationships

        // Lists
        ICollection<IBusRouteBusYear> BusRouteBusYears { get; set; }
        ICollection<IBusRouteProgram> BusRoutePrograms { get; set; }
        ICollection<IBusRouteServiceAreaPostalCode> BusRouteServiceAreaPostalCodes { get; set; }
        ICollection<IBusRouteStartTime> BusRouteStartTimes { get; set; }
        ICollection<IBusRouteTelephone> BusRouteTelephones { get; set; }

        // Resource reference data
        Guid? StaffEducationOrganizationAssignmentAssociationResourceId { get; set; }
        string StaffEducationOrganizationAssignmentAssociationDiscriminator { get; set; }
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class BusRouteMappingContract : IMappingContract
    {
        public BusRouteMappingContract(
            bool isBeginDateSupported,
            bool isBusRouteBusYearsSupported,
            bool isBusRouteDirectionSupported,
            bool isBusRouteDurationSupported,
            bool isBusRouteProgramsSupported,
            bool isBusRouteServiceAreaPostalCodesSupported,
            bool isBusRouteStartTimesSupported,
            bool isBusRouteTelephonesSupported,
            bool isDailySupported,
            bool isDisabilityDescriptorSupported,
            bool isEducationOrganizationIdSupported,
            bool isExpectedTransitTimeSupported,
            bool isHoursPerWeekSupported,
            bool isOperatingCostSupported,
            bool isOptimalCapacitySupported,
            bool isStaffClassificationDescriptorSupported,
            bool isStaffUniqueIdSupported,
            bool isStartDateSupported,
            bool isWeeklyMileageSupported,
            Func<IBusRouteBusYear, bool> isBusRouteBusYearIncluded,
            Func<IBusRouteProgram, bool> isBusRouteProgramIncluded,
            Func<IBusRouteServiceAreaPostalCode, bool> isBusRouteServiceAreaPostalCodeIncluded,
            Func<IBusRouteStartTime, bool> isBusRouteStartTimeIncluded,
            Func<IBusRouteTelephone, bool> isBusRouteTelephoneIncluded
            )
        {
            IsBeginDateSupported = isBeginDateSupported;
            IsBusRouteBusYearsSupported = isBusRouteBusYearsSupported;
            IsBusRouteDirectionSupported = isBusRouteDirectionSupported;
            IsBusRouteDurationSupported = isBusRouteDurationSupported;
            IsBusRouteProgramsSupported = isBusRouteProgramsSupported;
            IsBusRouteServiceAreaPostalCodesSupported = isBusRouteServiceAreaPostalCodesSupported;
            IsBusRouteStartTimesSupported = isBusRouteStartTimesSupported;
            IsBusRouteTelephonesSupported = isBusRouteTelephonesSupported;
            IsDailySupported = isDailySupported;
            IsDisabilityDescriptorSupported = isDisabilityDescriptorSupported;
            IsEducationOrganizationIdSupported = isEducationOrganizationIdSupported;
            IsExpectedTransitTimeSupported = isExpectedTransitTimeSupported;
            IsHoursPerWeekSupported = isHoursPerWeekSupported;
            IsOperatingCostSupported = isOperatingCostSupported;
            IsOptimalCapacitySupported = isOptimalCapacitySupported;
            IsStaffClassificationDescriptorSupported = isStaffClassificationDescriptorSupported;
            IsStaffUniqueIdSupported = isStaffUniqueIdSupported;
            IsStartDateSupported = isStartDateSupported;
            IsWeeklyMileageSupported = isWeeklyMileageSupported;
            IsBusRouteBusYearIncluded = isBusRouteBusYearIncluded;
            IsBusRouteProgramIncluded = isBusRouteProgramIncluded;
            IsBusRouteServiceAreaPostalCodeIncluded = isBusRouteServiceAreaPostalCodeIncluded;
            IsBusRouteStartTimeIncluded = isBusRouteStartTimeIncluded;
            IsBusRouteTelephoneIncluded = isBusRouteTelephoneIncluded;
        }

        public bool IsBeginDateSupported { get; }
        public bool IsBusRouteBusYearsSupported { get; }
        public bool IsBusRouteDirectionSupported { get; }
        public bool IsBusRouteDurationSupported { get; }
        public bool IsBusRouteProgramsSupported { get; }
        public bool IsBusRouteServiceAreaPostalCodesSupported { get; }
        public bool IsBusRouteStartTimesSupported { get; }
        public bool IsBusRouteTelephonesSupported { get; }
        public bool IsDailySupported { get; }
        public bool IsDisabilityDescriptorSupported { get; }
        public bool IsEducationOrganizationIdSupported { get; }
        public bool IsExpectedTransitTimeSupported { get; }
        public bool IsHoursPerWeekSupported { get; }
        public bool IsOperatingCostSupported { get; }
        public bool IsOptimalCapacitySupported { get; }
        public bool IsStaffClassificationDescriptorSupported { get; }
        public bool IsStaffUniqueIdSupported { get; }
        public bool IsStartDateSupported { get; }
        public bool IsWeeklyMileageSupported { get; }
        public Func<IBusRouteBusYear, bool> IsBusRouteBusYearIncluded { get; }
        public Func<IBusRouteProgram, bool> IsBusRouteProgramIncluded { get; }
        public Func<IBusRouteServiceAreaPostalCode, bool> IsBusRouteServiceAreaPostalCodeIncluded { get; }
        public Func<IBusRouteStartTime, bool> IsBusRouteStartTimeIncluded { get; }
        public Func<IBusRouteTelephone, bool> IsBusRouteTelephoneIncluded { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "BeginDate":
                    return IsBeginDateSupported;
                case "BusRouteBusYears":
                    return IsBusRouteBusYearsSupported;
                case "BusRouteDirection":
                    return IsBusRouteDirectionSupported;
                case "BusRouteDuration":
                    return IsBusRouteDurationSupported;
                case "BusRoutePrograms":
                    return IsBusRouteProgramsSupported;
                case "BusRouteServiceAreaPostalCodes":
                    return IsBusRouteServiceAreaPostalCodesSupported;
                case "BusRouteStartTimes":
                    return IsBusRouteStartTimesSupported;
                case "BusRouteTelephones":
                    return IsBusRouteTelephonesSupported;
                case "Daily":
                    return IsDailySupported;
                case "DisabilityDescriptor":
                    return IsDisabilityDescriptorSupported;
                case "EducationOrganizationId":
                    return IsEducationOrganizationIdSupported;
                case "ExpectedTransitTime":
                    return IsExpectedTransitTimeSupported;
                case "HoursPerWeek":
                    return IsHoursPerWeekSupported;
                case "OperatingCost":
                    return IsOperatingCostSupported;
                case "OptimalCapacity":
                    return IsOptimalCapacitySupported;
                case "StaffClassificationDescriptor":
                    return IsStaffClassificationDescriptorSupported;
                case "StaffUniqueId":
                    return IsStaffUniqueIdSupported;
                case "StartDate":
                    return IsStartDateSupported;
                case "WeeklyMileage":
                    return IsWeeklyMileageSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the BusRouteBusYear model.
    /// </summary>
    public interface IBusRouteBusYear : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IBusRoute BusRoute { get; set; }
        [NaturalKeyMember]
        short BusYear { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class BusRouteBusYearMappingContract : IMappingContract
    {
        public BusRouteBusYearMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the BusRouteProgram model.
    /// </summary>
    public interface IBusRouteProgram : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IBusRoute BusRoute { get; set; }
        [NaturalKeyMember]
        int EducationOrganizationId { get; set; }
        [NaturalKeyMember]
        string ProgramName { get; set; }
        [NaturalKeyMember]
        string ProgramTypeDescriptor { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
        Guid? ProgramResourceId { get; set; }
        string ProgramDiscriminator { get; set; }
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class BusRouteProgramMappingContract : IMappingContract
    {
        public BusRouteProgramMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the BusRouteServiceAreaPostalCode model.
    /// </summary>
    public interface IBusRouteServiceAreaPostalCode : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IBusRoute BusRoute { get; set; }
        [NaturalKeyMember]
        string ServiceAreaPostalCode { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class BusRouteServiceAreaPostalCodeMappingContract : IMappingContract
    {
        public BusRouteServiceAreaPostalCodeMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the BusRouteStartTime model.
    /// </summary>
    public interface IBusRouteStartTime : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IBusRoute BusRoute { get; set; }
        [NaturalKeyMember]
        TimeSpan StartTime { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class BusRouteStartTimeMappingContract : IMappingContract
    {
        public BusRouteStartTimeMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the BusRouteTelephone model.
    /// </summary>
    public interface IBusRouteTelephone : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IBusRoute BusRoute { get; set; }
        [NaturalKeyMember]
        string TelephoneNumber { get; set; }
        [NaturalKeyMember]
        string TelephoneNumberTypeDescriptor { get; set; }

        // Non-PK properties
        bool? DoNotPublishIndicator { get; set; }
        int? OrderOfPriority { get; set; }
        bool? TextMessageCapabilityIndicator { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class BusRouteTelephoneMappingContract : IMappingContract
    {
        public BusRouteTelephoneMappingContract(
            bool isDoNotPublishIndicatorSupported,
            bool isOrderOfPrioritySupported,
            bool isTextMessageCapabilityIndicatorSupported
            )
        {
            IsDoNotPublishIndicatorSupported = isDoNotPublishIndicatorSupported;
            IsOrderOfPrioritySupported = isOrderOfPrioritySupported;
            IsTextMessageCapabilityIndicatorSupported = isTextMessageCapabilityIndicatorSupported;
        }

        public bool IsDoNotPublishIndicatorSupported { get; }
        public bool IsOrderOfPrioritySupported { get; }
        public bool IsTextMessageCapabilityIndicatorSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "DoNotPublishIndicator":
                    return IsDoNotPublishIndicatorSupported;
                case "OrderOfPriority":
                    return IsOrderOfPrioritySupported;
                case "TextMessageCapabilityIndicator":
                    return IsTextMessageCapabilityIndicatorSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the FavoriteBookCategoryDescriptor model.
    /// </summary>
    public interface IFavoriteBookCategoryDescriptor : EdFi.IDescriptor, ISynchronizable, IMappable, IHasIdentifier, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember][AutoIncrement]
        int FavoriteBookCategoryDescriptorId { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class FavoriteBookCategoryDescriptorMappingContract : IMappingContract
    {
        public FavoriteBookCategoryDescriptorMappingContract(
            bool isCodeValueSupported,
            bool isDescriptionSupported,
            bool isEffectiveBeginDateSupported,
            bool isEffectiveEndDateSupported,
            bool isNamespaceSupported,
            bool isPriorDescriptorIdSupported,
            bool isShortDescriptionSupported
            )
        {
            IsCodeValueSupported = isCodeValueSupported;
            IsDescriptionSupported = isDescriptionSupported;
            IsEffectiveBeginDateSupported = isEffectiveBeginDateSupported;
            IsEffectiveEndDateSupported = isEffectiveEndDateSupported;
            IsNamespaceSupported = isNamespaceSupported;
            IsPriorDescriptorIdSupported = isPriorDescriptorIdSupported;
            IsShortDescriptionSupported = isShortDescriptionSupported;
        }

        public bool IsCodeValueSupported { get; }
        public bool IsDescriptionSupported { get; }
        public bool IsEffectiveBeginDateSupported { get; }
        public bool IsEffectiveEndDateSupported { get; }
        public bool IsNamespaceSupported { get; }
        public bool IsPriorDescriptorIdSupported { get; }
        public bool IsShortDescriptionSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "CodeValue":
                    return IsCodeValueSupported;
                case "Description":
                    return IsDescriptionSupported;
                case "EffectiveBeginDate":
                    return IsEffectiveBeginDateSupported;
                case "EffectiveEndDate":
                    return IsEffectiveEndDateSupported;
                case "Namespace":
                    return IsNamespaceSupported;
                case "PriorDescriptorId":
                    return IsPriorDescriptorIdSupported;
                case "ShortDescription":
                    return IsShortDescriptionSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the MembershipTypeDescriptor model.
    /// </summary>
    public interface IMembershipTypeDescriptor : EdFi.IDescriptor, ISynchronizable, IMappable, IHasIdentifier, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember][AutoIncrement]
        int MembershipTypeDescriptorId { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class MembershipTypeDescriptorMappingContract : IMappingContract
    {
        public MembershipTypeDescriptorMappingContract(
            bool isCodeValueSupported,
            bool isDescriptionSupported,
            bool isEffectiveBeginDateSupported,
            bool isEffectiveEndDateSupported,
            bool isNamespaceSupported,
            bool isPriorDescriptorIdSupported,
            bool isShortDescriptionSupported
            )
        {
            IsCodeValueSupported = isCodeValueSupported;
            IsDescriptionSupported = isDescriptionSupported;
            IsEffectiveBeginDateSupported = isEffectiveBeginDateSupported;
            IsEffectiveEndDateSupported = isEffectiveEndDateSupported;
            IsNamespaceSupported = isNamespaceSupported;
            IsPriorDescriptorIdSupported = isPriorDescriptorIdSupported;
            IsShortDescriptionSupported = isShortDescriptionSupported;
        }

        public bool IsCodeValueSupported { get; }
        public bool IsDescriptionSupported { get; }
        public bool IsEffectiveBeginDateSupported { get; }
        public bool IsEffectiveEndDateSupported { get; }
        public bool IsNamespaceSupported { get; }
        public bool IsPriorDescriptorIdSupported { get; }
        public bool IsShortDescriptionSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "CodeValue":
                    return IsCodeValueSupported;
                case "Description":
                    return IsDescriptionSupported;
                case "EffectiveBeginDate":
                    return IsEffectiveBeginDateSupported;
                case "EffectiveEndDate":
                    return IsEffectiveEndDateSupported;
                case "Namespace":
                    return IsNamespaceSupported;
                case "PriorDescriptorId":
                    return IsPriorDescriptorIdSupported;
                case "ShortDescription":
                    return IsShortDescriptionSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the ParentAddressExtension model.
    /// </summary>
    public interface IParentAddressExtension : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        EdFi.IParentAddress ParentAddress { get; set; }

        // Non-PK properties
        string Complex { get; set; }
        bool OnBusRoute { get; set; }

        // One-to-one relationships

        // Lists
        ICollection<IParentAddressSchoolDistrict> ParentAddressSchoolDistricts { get; set; }
        ICollection<IParentAddressTerm> ParentAddressTerms { get; set; }

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class ParentAddressExtensionMappingContract : IMappingContract
    {
        public ParentAddressExtensionMappingContract(
            bool isComplexSupported,
            bool isOnBusRouteSupported,
            bool isParentAddressSchoolDistrictsSupported,
            bool isParentAddressTermsSupported,
            Func<IParentAddressSchoolDistrict, bool> isParentAddressSchoolDistrictIncluded,
            Func<IParentAddressTerm, bool> isParentAddressTermIncluded
            )
        {
            IsComplexSupported = isComplexSupported;
            IsOnBusRouteSupported = isOnBusRouteSupported;
            IsParentAddressSchoolDistrictsSupported = isParentAddressSchoolDistrictsSupported;
            IsParentAddressTermsSupported = isParentAddressTermsSupported;
            IsParentAddressSchoolDistrictIncluded = isParentAddressSchoolDistrictIncluded;
            IsParentAddressTermIncluded = isParentAddressTermIncluded;
        }

        public bool IsComplexSupported { get; }
        public bool IsOnBusRouteSupported { get; }
        public bool IsParentAddressSchoolDistrictsSupported { get; }
        public bool IsParentAddressTermsSupported { get; }
        public Func<IParentAddressSchoolDistrict, bool> IsParentAddressSchoolDistrictIncluded { get; }
        public Func<IParentAddressTerm, bool> IsParentAddressTermIncluded { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "Complex":
                    return IsComplexSupported;
                case "OnBusRoute":
                    return IsOnBusRouteSupported;
                case "ParentAddressSchoolDistricts":
                    return IsParentAddressSchoolDistrictsSupported;
                case "ParentAddressTerms":
                    return IsParentAddressTermsSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the ParentAddressSchoolDistrict model.
    /// </summary>
    public interface IParentAddressSchoolDistrict : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IParentAddressExtension ParentAddressExtension { get; set; }
        [NaturalKeyMember]
        string SchoolDistrict { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class ParentAddressSchoolDistrictMappingContract : IMappingContract
    {
        public ParentAddressSchoolDistrictMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the ParentAddressTerm model.
    /// </summary>
    public interface IParentAddressTerm : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IParentAddressExtension ParentAddressExtension { get; set; }
        [NaturalKeyMember]
        string TermDescriptor { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class ParentAddressTermMappingContract : IMappingContract
    {
        public ParentAddressTermMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the ParentAuthor model.
    /// </summary>
    public interface IParentAuthor : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IParentExtension ParentExtension { get; set; }
        [NaturalKeyMember]
        string Author { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class ParentAuthorMappingContract : IMappingContract
    {
        public ParentAuthorMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the ParentCeilingHeight model.
    /// </summary>
    public interface IParentCeilingHeight : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IParentExtension ParentExtension { get; set; }
        [NaturalKeyMember]
        decimal CeilingHeight { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class ParentCeilingHeightMappingContract : IMappingContract
    {
        public ParentCeilingHeightMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the ParentCTEProgram model.
    /// </summary>
    public interface IParentCTEProgram : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IParentExtension ParentExtension { get; set; }

        // Non-PK properties
        string CareerPathwayDescriptor { get; set; }
        string CIPCode { get; set; }
        bool? CTEProgramCompletionIndicator { get; set; }
        bool? PrimaryCTEProgramIndicator { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class ParentCTEProgramMappingContract : IMappingContract
    {
        public ParentCTEProgramMappingContract(
            bool isCareerPathwayDescriptorSupported,
            bool isCIPCodeSupported,
            bool isCTEProgramCompletionIndicatorSupported,
            bool isPrimaryCTEProgramIndicatorSupported
            )
        {
            IsCareerPathwayDescriptorSupported = isCareerPathwayDescriptorSupported;
            IsCIPCodeSupported = isCIPCodeSupported;
            IsCTEProgramCompletionIndicatorSupported = isCTEProgramCompletionIndicatorSupported;
            IsPrimaryCTEProgramIndicatorSupported = isPrimaryCTEProgramIndicatorSupported;
        }

        public bool IsCareerPathwayDescriptorSupported { get; }
        public bool IsCIPCodeSupported { get; }
        public bool IsCTEProgramCompletionIndicatorSupported { get; }
        public bool IsPrimaryCTEProgramIndicatorSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "CareerPathwayDescriptor":
                    return IsCareerPathwayDescriptorSupported;
                case "CIPCode":
                    return IsCIPCodeSupported;
                case "CTEProgramCompletionIndicator":
                    return IsCTEProgramCompletionIndicatorSupported;
                case "PrimaryCTEProgramIndicator":
                    return IsPrimaryCTEProgramIndicatorSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the ParentEducationContent model.
    /// </summary>
    public interface IParentEducationContent : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IParentExtension ParentExtension { get; set; }
        [NaturalKeyMember]
        string ContentIdentifier { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
        Guid? EducationContentResourceId { get; set; }
        string EducationContentDiscriminator { get; set; }
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class ParentEducationContentMappingContract : IMappingContract
    {
        public ParentEducationContentMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the ParentExtension model.
    /// </summary>
    public interface IParentExtension : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        EdFi.IParent Parent { get; set; }

        // Non-PK properties
        string AverageCarLineWait { get; set; }
        short? BecameParent { get; set; }
        decimal? CoffeeSpend { get; set; }
        string CredentialFieldDescriptor { get; set; }
        int? Duration { get; set; }
        decimal? GPA { get; set; }
        DateTime? GraduationDate { get; set; }
        bool IsSportsFan { get; set; }
        int? LuckyNumber { get; set; }
        TimeSpan? PreferredWakeUpTime { get; set; }
        decimal? RainCertainty { get; set; }

        // One-to-one relationships

        IParentCTEProgram ParentCTEProgram { get; set; }

        IParentTeacherConference ParentTeacherConference { get; set; }

        // Lists
        ICollection<IParentAuthor> ParentAuthors { get; set; }
        ICollection<IParentCeilingHeight> ParentCeilingHeights { get; set; }
        ICollection<IParentEducationContent> ParentEducationContents { get; set; }
        ICollection<IParentFavoriteBookTitle> ParentFavoriteBookTitles { get; set; }
        ICollection<IParentStudentProgramAssociation> ParentStudentProgramAssociations { get; set; }

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class ParentExtensionMappingContract : IMappingContract
    {
        public ParentExtensionMappingContract(
            bool isAverageCarLineWaitSupported,
            bool isBecameParentSupported,
            bool isCoffeeSpendSupported,
            bool isCredentialFieldDescriptorSupported,
            bool isDurationSupported,
            bool isGPASupported,
            bool isGraduationDateSupported,
            bool isIsSportsFanSupported,
            bool isLuckyNumberSupported,
            bool isParentAuthorsSupported,
            bool isParentCeilingHeightsSupported,
            bool isParentCTEProgramSupported,
            bool isParentEducationContentsSupported,
            bool isParentFavoriteBookTitlesSupported,
            bool isParentStudentProgramAssociationsSupported,
            bool isParentTeacherConferenceSupported,
            bool isPreferredWakeUpTimeSupported,
            bool isRainCertaintySupported,
            Func<IParentAuthor, bool> isParentAuthorIncluded,
            Func<IParentCeilingHeight, bool> isParentCeilingHeightIncluded,
            Func<IParentEducationContent, bool> isParentEducationContentIncluded,
            Func<IParentFavoriteBookTitle, bool> isParentFavoriteBookTitleIncluded,
            Func<IParentStudentProgramAssociation, bool> isParentStudentProgramAssociationIncluded
            )
        {
            IsAverageCarLineWaitSupported = isAverageCarLineWaitSupported;
            IsBecameParentSupported = isBecameParentSupported;
            IsCoffeeSpendSupported = isCoffeeSpendSupported;
            IsCredentialFieldDescriptorSupported = isCredentialFieldDescriptorSupported;
            IsDurationSupported = isDurationSupported;
            IsGPASupported = isGPASupported;
            IsGraduationDateSupported = isGraduationDateSupported;
            IsIsSportsFanSupported = isIsSportsFanSupported;
            IsLuckyNumberSupported = isLuckyNumberSupported;
            IsParentAuthorsSupported = isParentAuthorsSupported;
            IsParentCeilingHeightsSupported = isParentCeilingHeightsSupported;
            IsParentCTEProgramSupported = isParentCTEProgramSupported;
            IsParentEducationContentsSupported = isParentEducationContentsSupported;
            IsParentFavoriteBookTitlesSupported = isParentFavoriteBookTitlesSupported;
            IsParentStudentProgramAssociationsSupported = isParentStudentProgramAssociationsSupported;
            IsParentTeacherConferenceSupported = isParentTeacherConferenceSupported;
            IsPreferredWakeUpTimeSupported = isPreferredWakeUpTimeSupported;
            IsRainCertaintySupported = isRainCertaintySupported;
            IsParentAuthorIncluded = isParentAuthorIncluded;
            IsParentCeilingHeightIncluded = isParentCeilingHeightIncluded;
            IsParentEducationContentIncluded = isParentEducationContentIncluded;
            IsParentFavoriteBookTitleIncluded = isParentFavoriteBookTitleIncluded;
            IsParentStudentProgramAssociationIncluded = isParentStudentProgramAssociationIncluded;
        }

        public bool IsAverageCarLineWaitSupported { get; }
        public bool IsBecameParentSupported { get; }
        public bool IsCoffeeSpendSupported { get; }
        public bool IsCredentialFieldDescriptorSupported { get; }
        public bool IsDurationSupported { get; }
        public bool IsGPASupported { get; }
        public bool IsGraduationDateSupported { get; }
        public bool IsIsSportsFanSupported { get; }
        public bool IsLuckyNumberSupported { get; }
        public bool IsParentAuthorsSupported { get; }
        public bool IsParentCeilingHeightsSupported { get; }
        public bool IsParentCTEProgramSupported { get; }
        public bool IsParentEducationContentsSupported { get; }
        public bool IsParentFavoriteBookTitlesSupported { get; }
        public bool IsParentStudentProgramAssociationsSupported { get; }
        public bool IsParentTeacherConferenceSupported { get; }
        public bool IsPreferredWakeUpTimeSupported { get; }
        public bool IsRainCertaintySupported { get; }
        public Func<IParentAuthor, bool> IsParentAuthorIncluded { get; }
        public Func<IParentCeilingHeight, bool> IsParentCeilingHeightIncluded { get; }
        public Func<IParentEducationContent, bool> IsParentEducationContentIncluded { get; }
        public Func<IParentFavoriteBookTitle, bool> IsParentFavoriteBookTitleIncluded { get; }
        public Func<IParentStudentProgramAssociation, bool> IsParentStudentProgramAssociationIncluded { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "AverageCarLineWait":
                    return IsAverageCarLineWaitSupported;
                case "BecameParent":
                    return IsBecameParentSupported;
                case "CoffeeSpend":
                    return IsCoffeeSpendSupported;
                case "CredentialFieldDescriptor":
                    return IsCredentialFieldDescriptorSupported;
                case "Duration":
                    return IsDurationSupported;
                case "GPA":
                    return IsGPASupported;
                case "GraduationDate":
                    return IsGraduationDateSupported;
                case "IsSportsFan":
                    return IsIsSportsFanSupported;
                case "LuckyNumber":
                    return IsLuckyNumberSupported;
                case "ParentAuthors":
                    return IsParentAuthorsSupported;
                case "ParentCeilingHeights":
                    return IsParentCeilingHeightsSupported;
                case "ParentCTEProgram":
                    return IsParentCTEProgramSupported;
                case "ParentEducationContents":
                    return IsParentEducationContentsSupported;
                case "ParentFavoriteBookTitles":
                    return IsParentFavoriteBookTitlesSupported;
                case "ParentStudentProgramAssociations":
                    return IsParentStudentProgramAssociationsSupported;
                case "ParentTeacherConference":
                    return IsParentTeacherConferenceSupported;
                case "PreferredWakeUpTime":
                    return IsPreferredWakeUpTimeSupported;
                case "RainCertainty":
                    return IsRainCertaintySupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the ParentFavoriteBookTitle model.
    /// </summary>
    public interface IParentFavoriteBookTitle : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IParentExtension ParentExtension { get; set; }
        [NaturalKeyMember]
        string FavoriteBookTitle { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class ParentFavoriteBookTitleMappingContract : IMappingContract
    {
        public ParentFavoriteBookTitleMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the ParentStudentProgramAssociation model.
    /// </summary>
    public interface IParentStudentProgramAssociation : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IParentExtension ParentExtension { get; set; }
        [NaturalKeyMember]
        DateTime BeginDate { get; set; }
        [NaturalKeyMember]
        int EducationOrganizationId { get; set; }
        [NaturalKeyMember]
        int ProgramEducationOrganizationId { get; set; }
        [NaturalKeyMember]
        string ProgramName { get; set; }
        [NaturalKeyMember]
        string ProgramTypeDescriptor { get; set; }
        [NaturalKeyMember]
        string StudentUniqueId { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
        Guid? StudentProgramAssociationResourceId { get; set; }
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class ParentStudentProgramAssociationMappingContract : IMappingContract
    {
        public ParentStudentProgramAssociationMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the ParentTeacherConference model.
    /// </summary>
    public interface IParentTeacherConference : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IParentExtension ParentExtension { get; set; }

        // Non-PK properties
        string DayOfWeek { get; set; }
        TimeSpan EndTime { get; set; }
        TimeSpan StartTime { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class ParentTeacherConferenceMappingContract : IMappingContract
    {
        public ParentTeacherConferenceMappingContract(
            bool isDayOfWeekSupported,
            bool isEndTimeSupported,
            bool isStartTimeSupported
            )
        {
            IsDayOfWeekSupported = isDayOfWeekSupported;
            IsEndTimeSupported = isEndTimeSupported;
            IsStartTimeSupported = isStartTimeSupported;
        }

        public bool IsDayOfWeekSupported { get; }
        public bool IsEndTimeSupported { get; }
        public bool IsStartTimeSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "DayOfWeek":
                    return IsDayOfWeekSupported;
                case "EndTime":
                    return IsEndTimeSupported;
                case "StartTime":
                    return IsStartTimeSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the SchoolCTEProgram model.
    /// </summary>
    public interface ISchoolCTEProgram : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        ISchoolExtension SchoolExtension { get; set; }

        // Non-PK properties
        string CareerPathwayDescriptor { get; set; }
        string CIPCode { get; set; }
        bool? CTEProgramCompletionIndicator { get; set; }
        bool? PrimaryCTEProgramIndicator { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class SchoolCTEProgramMappingContract : IMappingContract
    {
        public SchoolCTEProgramMappingContract(
            bool isCareerPathwayDescriptorSupported,
            bool isCIPCodeSupported,
            bool isCTEProgramCompletionIndicatorSupported,
            bool isPrimaryCTEProgramIndicatorSupported
            )
        {
            IsCareerPathwayDescriptorSupported = isCareerPathwayDescriptorSupported;
            IsCIPCodeSupported = isCIPCodeSupported;
            IsCTEProgramCompletionIndicatorSupported = isCTEProgramCompletionIndicatorSupported;
            IsPrimaryCTEProgramIndicatorSupported = isPrimaryCTEProgramIndicatorSupported;
        }

        public bool IsCareerPathwayDescriptorSupported { get; }
        public bool IsCIPCodeSupported { get; }
        public bool IsCTEProgramCompletionIndicatorSupported { get; }
        public bool IsPrimaryCTEProgramIndicatorSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "CareerPathwayDescriptor":
                    return IsCareerPathwayDescriptorSupported;
                case "CIPCode":
                    return IsCIPCodeSupported;
                case "CTEProgramCompletionIndicator":
                    return IsCTEProgramCompletionIndicatorSupported;
                case "PrimaryCTEProgramIndicator":
                    return IsPrimaryCTEProgramIndicatorSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the SchoolDirectlyOwnedBus model.
    /// </summary>
    public interface ISchoolDirectlyOwnedBus : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        ISchoolExtension SchoolExtension { get; set; }
        [NaturalKeyMember]
        string DirectlyOwnedBusId { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
        Guid? DirectlyOwnedBusResourceId { get; set; }
        string DirectlyOwnedBusDiscriminator { get; set; }
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class SchoolDirectlyOwnedBusMappingContract : IMappingContract
    {
        public SchoolDirectlyOwnedBusMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the SchoolExtension model.
    /// </summary>
    public interface ISchoolExtension : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        EdFi.ISchool School { get; set; }

        // Non-PK properties
        bool? IsExemplary { get; set; }

        // One-to-one relationships

        ISchoolCTEProgram SchoolCTEProgram { get; set; }

        // Lists
        ICollection<ISchoolDirectlyOwnedBus> SchoolDirectlyOwnedBuses { get; set; }

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class SchoolExtensionMappingContract : IMappingContract
    {
        public SchoolExtensionMappingContract(
            bool isIsExemplarySupported,
            bool isSchoolCTEProgramSupported,
            bool isSchoolDirectlyOwnedBusesSupported,
            Func<ISchoolDirectlyOwnedBus, bool> isSchoolDirectlyOwnedBusIncluded
            )
        {
            IsIsExemplarySupported = isIsExemplarySupported;
            IsSchoolCTEProgramSupported = isSchoolCTEProgramSupported;
            IsSchoolDirectlyOwnedBusesSupported = isSchoolDirectlyOwnedBusesSupported;
            IsSchoolDirectlyOwnedBusIncluded = isSchoolDirectlyOwnedBusIncluded;
        }

        public bool IsIsExemplarySupported { get; }
        public bool IsSchoolCTEProgramSupported { get; }
        public bool IsSchoolDirectlyOwnedBusesSupported { get; }
        public Func<ISchoolDirectlyOwnedBus, bool> IsSchoolDirectlyOwnedBusIncluded { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "IsExemplary":
                    return IsIsExemplarySupported;
                case "SchoolCTEProgram":
                    return IsSchoolCTEProgramSupported;
                case "SchoolDirectlyOwnedBuses":
                    return IsSchoolDirectlyOwnedBusesSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StaffExtension model.
    /// </summary>
    public interface IStaffExtension : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        EdFi.IStaff Staff { get; set; }

        // Non-PK properties
        DateTime? FirstPetOwnedDate { get; set; }

        // One-to-one relationships

        IStaffPetPreference StaffPetPreference { get; set; }

        // Lists
        ICollection<IStaffPet> StaffPets { get; set; }

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StaffExtensionMappingContract : IMappingContract
    {
        public StaffExtensionMappingContract(
            bool isFirstPetOwnedDateSupported,
            bool isStaffPetPreferenceSupported,
            bool isStaffPetsSupported,
            Func<IStaffPet, bool> isStaffPetIncluded
            )
        {
            IsFirstPetOwnedDateSupported = isFirstPetOwnedDateSupported;
            IsStaffPetPreferenceSupported = isStaffPetPreferenceSupported;
            IsStaffPetsSupported = isStaffPetsSupported;
            IsStaffPetIncluded = isStaffPetIncluded;
        }

        public bool IsFirstPetOwnedDateSupported { get; }
        public bool IsStaffPetPreferenceSupported { get; }
        public bool IsStaffPetsSupported { get; }
        public Func<IStaffPet, bool> IsStaffPetIncluded { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "FirstPetOwnedDate":
                    return IsFirstPetOwnedDateSupported;
                case "StaffPetPreference":
                    return IsStaffPetPreferenceSupported;
                case "StaffPets":
                    return IsStaffPetsSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StaffPet model.
    /// </summary>
    public interface IStaffPet : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStaffExtension StaffExtension { get; set; }
        [NaturalKeyMember]
        string PetName { get; set; }

        // Non-PK properties
        bool? IsFixed { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StaffPetMappingContract : IMappingContract
    {
        public StaffPetMappingContract(
            bool isIsFixedSupported
            )
        {
            IsIsFixedSupported = isIsFixedSupported;
        }

        public bool IsIsFixedSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "IsFixed":
                    return IsIsFixedSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StaffPetPreference model.
    /// </summary>
    public interface IStaffPetPreference : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStaffExtension StaffExtension { get; set; }

        // Non-PK properties
        int MaximumWeight { get; set; }
        int MinimumWeight { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StaffPetPreferenceMappingContract : IMappingContract
    {
        public StaffPetPreferenceMappingContract(
            bool isMaximumWeightSupported,
            bool isMinimumWeightSupported
            )
        {
            IsMaximumWeightSupported = isMaximumWeightSupported;
            IsMinimumWeightSupported = isMinimumWeightSupported;
        }

        public bool IsMaximumWeightSupported { get; }
        public bool IsMinimumWeightSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "MaximumWeight":
                    return IsMaximumWeightSupported;
                case "MinimumWeight":
                    return IsMinimumWeightSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentAquaticPet model.
    /// </summary>
    public interface IStudentAquaticPet : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentExtension StudentExtension { get; set; }
        [NaturalKeyMember]
        int MimimumTankVolume { get; set; }
        [NaturalKeyMember]
        string PetName { get; set; }

        // Non-PK properties
        bool? IsFixed { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentAquaticPetMappingContract : IMappingContract
    {
        public StudentAquaticPetMappingContract(
            bool isIsFixedSupported
            )
        {
            IsIsFixedSupported = isIsFixedSupported;
        }

        public bool IsIsFixedSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "IsFixed":
                    return IsIsFixedSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentArtProgramAssociation model.
    /// </summary>
    public interface IStudentArtProgramAssociation : EdFi.IGeneralStudentProgramAssociation, ISynchronizable, IMappable, IHasIdentifier, IGetByExample
    {
        // Primary Key properties

        // Non-PK properties
        int? ArtPieces { get; set; }
        DateTime? ExhibitDate { get; set; }
        decimal? HoursPerDay { get; set; }
        string IdentificationCode { get; set; }
        TimeSpan? KilnReservation { get; set; }
        string KilnReservationLength { get; set; }
        decimal? MasteredMediums { get; set; }
        decimal? NumberOfDaysInAttendance { get; set; }
        int? PortfolioPieces { get; set; }
        bool PrivateArtProgram { get; set; }
        decimal? ProgramFees { get; set; }

        // One-to-one relationships

        // Lists
        ICollection<IStudentArtProgramAssociationArtMedium> StudentArtProgramAssociationArtMedia { get; set; }
        ICollection<IStudentArtProgramAssociationPortfolioYears> StudentArtProgramAssociationPortfolioYears { get; set; }
        ICollection<IStudentArtProgramAssociationService> StudentArtProgramAssociationServices { get; set; }
        ICollection<IStudentArtProgramAssociationStyle> StudentArtProgramAssociationStyles { get; set; }

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentArtProgramAssociationMappingContract : IMappingContract
    {
        public StudentArtProgramAssociationMappingContract(
            bool isArtPiecesSupported,
            bool isEndDateSupported,
            bool isExhibitDateSupported,
            bool isGeneralStudentProgramAssociationParticipationStatusSupported,
            bool isGeneralStudentProgramAssociationProgramParticipationStatusesSupported,
            bool isHoursPerDaySupported,
            bool isIdentificationCodeSupported,
            bool isKilnReservationSupported,
            bool isKilnReservationLengthSupported,
            bool isMasteredMediumsSupported,
            bool isNumberOfDaysInAttendanceSupported,
            bool isPortfolioPiecesSupported,
            bool isPrivateArtProgramSupported,
            bool isProgramFeesSupported,
            bool isReasonExitedDescriptorSupported,
            bool isServedOutsideOfRegularSessionSupported,
            bool isStudentArtProgramAssociationArtMediaSupported,
            bool isStudentArtProgramAssociationPortfolioYearsSupported,
            bool isStudentArtProgramAssociationServicesSupported,
            bool isStudentArtProgramAssociationStylesSupported,
            Func<IGeneralStudentProgramAssociationProgramParticipationStatus, bool> isGeneralStudentProgramAssociationProgramParticipationStatusIncluded,
            Func<IStudentArtProgramAssociationArtMedium, bool> isStudentArtProgramAssociationArtMediumIncluded,
            Func<IStudentArtProgramAssociationPortfolioYears, bool> isStudentArtProgramAssociationPortfolioYearsIncluded,
            Func<IStudentArtProgramAssociationService, bool> isStudentArtProgramAssociationServiceIncluded,
            Func<IStudentArtProgramAssociationStyle, bool> isStudentArtProgramAssociationStyleIncluded
            )
        {
            IsArtPiecesSupported = isArtPiecesSupported;
            IsEndDateSupported = isEndDateSupported;
            IsExhibitDateSupported = isExhibitDateSupported;
            IsGeneralStudentProgramAssociationParticipationStatusSupported = isGeneralStudentProgramAssociationParticipationStatusSupported;
            IsGeneralStudentProgramAssociationProgramParticipationStatusesSupported = isGeneralStudentProgramAssociationProgramParticipationStatusesSupported;
            IsHoursPerDaySupported = isHoursPerDaySupported;
            IsIdentificationCodeSupported = isIdentificationCodeSupported;
            IsKilnReservationSupported = isKilnReservationSupported;
            IsKilnReservationLengthSupported = isKilnReservationLengthSupported;
            IsMasteredMediumsSupported = isMasteredMediumsSupported;
            IsNumberOfDaysInAttendanceSupported = isNumberOfDaysInAttendanceSupported;
            IsPortfolioPiecesSupported = isPortfolioPiecesSupported;
            IsPrivateArtProgramSupported = isPrivateArtProgramSupported;
            IsProgramFeesSupported = isProgramFeesSupported;
            IsReasonExitedDescriptorSupported = isReasonExitedDescriptorSupported;
            IsServedOutsideOfRegularSessionSupported = isServedOutsideOfRegularSessionSupported;
            IsStudentArtProgramAssociationArtMediaSupported = isStudentArtProgramAssociationArtMediaSupported;
            IsStudentArtProgramAssociationPortfolioYearsSupported = isStudentArtProgramAssociationPortfolioYearsSupported;
            IsStudentArtProgramAssociationServicesSupported = isStudentArtProgramAssociationServicesSupported;
            IsStudentArtProgramAssociationStylesSupported = isStudentArtProgramAssociationStylesSupported;
            IsGeneralStudentProgramAssociationProgramParticipationStatusIncluded = isGeneralStudentProgramAssociationProgramParticipationStatusIncluded;
            IsStudentArtProgramAssociationArtMediumIncluded = isStudentArtProgramAssociationArtMediumIncluded;
            IsStudentArtProgramAssociationPortfolioYearsIncluded = isStudentArtProgramAssociationPortfolioYearsIncluded;
            IsStudentArtProgramAssociationServiceIncluded = isStudentArtProgramAssociationServiceIncluded;
            IsStudentArtProgramAssociationStyleIncluded = isStudentArtProgramAssociationStyleIncluded;
        }

        public bool IsArtPiecesSupported { get; }
        public bool IsEndDateSupported { get; }
        public bool IsExhibitDateSupported { get; }
        public bool IsGeneralStudentProgramAssociationParticipationStatusSupported { get; }
        public bool IsGeneralStudentProgramAssociationProgramParticipationStatusesSupported { get; }
        public bool IsHoursPerDaySupported { get; }
        public bool IsIdentificationCodeSupported { get; }
        public bool IsKilnReservationSupported { get; }
        public bool IsKilnReservationLengthSupported { get; }
        public bool IsMasteredMediumsSupported { get; }
        public bool IsNumberOfDaysInAttendanceSupported { get; }
        public bool IsPortfolioPiecesSupported { get; }
        public bool IsPrivateArtProgramSupported { get; }
        public bool IsProgramFeesSupported { get; }
        public bool IsReasonExitedDescriptorSupported { get; }
        public bool IsServedOutsideOfRegularSessionSupported { get; }
        public bool IsStudentArtProgramAssociationArtMediaSupported { get; }
        public bool IsStudentArtProgramAssociationPortfolioYearsSupported { get; }
        public bool IsStudentArtProgramAssociationServicesSupported { get; }
        public bool IsStudentArtProgramAssociationStylesSupported { get; }
        public Func<IGeneralStudentProgramAssociationProgramParticipationStatus, bool> IsGeneralStudentProgramAssociationProgramParticipationStatusIncluded { get; }
        public Func<IStudentArtProgramAssociationArtMedium, bool> IsStudentArtProgramAssociationArtMediumIncluded { get; }
        public Func<IStudentArtProgramAssociationPortfolioYears, bool> IsStudentArtProgramAssociationPortfolioYearsIncluded { get; }
        public Func<IStudentArtProgramAssociationService, bool> IsStudentArtProgramAssociationServiceIncluded { get; }
        public Func<IStudentArtProgramAssociationStyle, bool> IsStudentArtProgramAssociationStyleIncluded { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "ArtPieces":
                    return IsArtPiecesSupported;
                case "EndDate":
                    return IsEndDateSupported;
                case "ExhibitDate":
                    return IsExhibitDateSupported;
                case "GeneralStudentProgramAssociationParticipationStatus":
                    return IsGeneralStudentProgramAssociationParticipationStatusSupported;
                case "GeneralStudentProgramAssociationProgramParticipationStatuses":
                    return IsGeneralStudentProgramAssociationProgramParticipationStatusesSupported;
                case "HoursPerDay":
                    return IsHoursPerDaySupported;
                case "IdentificationCode":
                    return IsIdentificationCodeSupported;
                case "KilnReservation":
                    return IsKilnReservationSupported;
                case "KilnReservationLength":
                    return IsKilnReservationLengthSupported;
                case "MasteredMediums":
                    return IsMasteredMediumsSupported;
                case "NumberOfDaysInAttendance":
                    return IsNumberOfDaysInAttendanceSupported;
                case "PortfolioPieces":
                    return IsPortfolioPiecesSupported;
                case "PrivateArtProgram":
                    return IsPrivateArtProgramSupported;
                case "ProgramFees":
                    return IsProgramFeesSupported;
                case "ReasonExitedDescriptor":
                    return IsReasonExitedDescriptorSupported;
                case "ServedOutsideOfRegularSession":
                    return IsServedOutsideOfRegularSessionSupported;
                case "StudentArtProgramAssociationArtMedia":
                    return IsStudentArtProgramAssociationArtMediaSupported;
                case "StudentArtProgramAssociationPortfolioYears":
                    return IsStudentArtProgramAssociationPortfolioYearsSupported;
                case "StudentArtProgramAssociationServices":
                    return IsStudentArtProgramAssociationServicesSupported;
                case "StudentArtProgramAssociationStyles":
                    return IsStudentArtProgramAssociationStylesSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentArtProgramAssociationArtMedium model.
    /// </summary>
    public interface IStudentArtProgramAssociationArtMedium : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentArtProgramAssociation StudentArtProgramAssociation { get; set; }
        [NaturalKeyMember]
        string ArtMediumDescriptor { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentArtProgramAssociationArtMediumMappingContract : IMappingContract
    {
        public StudentArtProgramAssociationArtMediumMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentArtProgramAssociationPortfolioYears model.
    /// </summary>
    public interface IStudentArtProgramAssociationPortfolioYears : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentArtProgramAssociation StudentArtProgramAssociation { get; set; }
        [NaturalKeyMember]
        short PortfolioYears { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentArtProgramAssociationPortfolioYearsMappingContract : IMappingContract
    {
        public StudentArtProgramAssociationPortfolioYearsMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentArtProgramAssociationService model.
    /// </summary>
    public interface IStudentArtProgramAssociationService : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentArtProgramAssociation StudentArtProgramAssociation { get; set; }
        [NaturalKeyMember]
        string ServiceDescriptor { get; set; }

        // Non-PK properties
        bool? PrimaryIndicator { get; set; }
        DateTime? ServiceBeginDate { get; set; }
        DateTime? ServiceEndDate { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentArtProgramAssociationServiceMappingContract : IMappingContract
    {
        public StudentArtProgramAssociationServiceMappingContract(
            bool isPrimaryIndicatorSupported,
            bool isServiceBeginDateSupported,
            bool isServiceEndDateSupported
            )
        {
            IsPrimaryIndicatorSupported = isPrimaryIndicatorSupported;
            IsServiceBeginDateSupported = isServiceBeginDateSupported;
            IsServiceEndDateSupported = isServiceEndDateSupported;
        }

        public bool IsPrimaryIndicatorSupported { get; }
        public bool IsServiceBeginDateSupported { get; }
        public bool IsServiceEndDateSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "PrimaryIndicator":
                    return IsPrimaryIndicatorSupported;
                case "ServiceBeginDate":
                    return IsServiceBeginDateSupported;
                case "ServiceEndDate":
                    return IsServiceEndDateSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentArtProgramAssociationStyle model.
    /// </summary>
    public interface IStudentArtProgramAssociationStyle : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentArtProgramAssociation StudentArtProgramAssociation { get; set; }
        [NaturalKeyMember]
        string Style { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentArtProgramAssociationStyleMappingContract : IMappingContract
    {
        public StudentArtProgramAssociationStyleMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentCTEProgramAssociationExtension model.
    /// </summary>
    public interface IStudentCTEProgramAssociationExtension : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        EdFi.IStudentCTEProgramAssociation StudentCTEProgramAssociation { get; set; }

        // Non-PK properties
        bool? AnalysisCompleted { get; set; }
        DateTime? AnalysisDate { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentCTEProgramAssociationExtensionMappingContract : IMappingContract
    {
        public StudentCTEProgramAssociationExtensionMappingContract(
            bool isAnalysisCompletedSupported,
            bool isAnalysisDateSupported
            )
        {
            IsAnalysisCompletedSupported = isAnalysisCompletedSupported;
            IsAnalysisDateSupported = isAnalysisDateSupported;
        }

        public bool IsAnalysisCompletedSupported { get; }
        public bool IsAnalysisDateSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "AnalysisCompleted":
                    return IsAnalysisCompletedSupported;
                case "AnalysisDate":
                    return IsAnalysisDateSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentEducationOrganizationAssociationAddressExtension model.
    /// </summary>
    public interface IStudentEducationOrganizationAssociationAddressExtension : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        EdFi.IStudentEducationOrganizationAssociationAddress StudentEducationOrganizationAssociationAddress { get; set; }

        // Non-PK properties
        string Complex { get; set; }
        bool OnBusRoute { get; set; }

        // One-to-one relationships

        // Lists
        ICollection<IStudentEducationOrganizationAssociationAddressSchoolDistrict> StudentEducationOrganizationAssociationAddressSchoolDistricts { get; set; }
        ICollection<IStudentEducationOrganizationAssociationAddressTerm> StudentEducationOrganizationAssociationAddressTerms { get; set; }

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentEducationOrganizationAssociationAddressExtensionMappingContract : IMappingContract
    {
        public StudentEducationOrganizationAssociationAddressExtensionMappingContract(
            bool isComplexSupported,
            bool isOnBusRouteSupported,
            bool isStudentEducationOrganizationAssociationAddressSchoolDistrictsSupported,
            bool isStudentEducationOrganizationAssociationAddressTermsSupported,
            Func<IStudentEducationOrganizationAssociationAddressSchoolDistrict, bool> isStudentEducationOrganizationAssociationAddressSchoolDistrictIncluded,
            Func<IStudentEducationOrganizationAssociationAddressTerm, bool> isStudentEducationOrganizationAssociationAddressTermIncluded
            )
        {
            IsComplexSupported = isComplexSupported;
            IsOnBusRouteSupported = isOnBusRouteSupported;
            IsStudentEducationOrganizationAssociationAddressSchoolDistrictsSupported = isStudentEducationOrganizationAssociationAddressSchoolDistrictsSupported;
            IsStudentEducationOrganizationAssociationAddressTermsSupported = isStudentEducationOrganizationAssociationAddressTermsSupported;
            IsStudentEducationOrganizationAssociationAddressSchoolDistrictIncluded = isStudentEducationOrganizationAssociationAddressSchoolDistrictIncluded;
            IsStudentEducationOrganizationAssociationAddressTermIncluded = isStudentEducationOrganizationAssociationAddressTermIncluded;
        }

        public bool IsComplexSupported { get; }
        public bool IsOnBusRouteSupported { get; }
        public bool IsStudentEducationOrganizationAssociationAddressSchoolDistrictsSupported { get; }
        public bool IsStudentEducationOrganizationAssociationAddressTermsSupported { get; }
        public Func<IStudentEducationOrganizationAssociationAddressSchoolDistrict, bool> IsStudentEducationOrganizationAssociationAddressSchoolDistrictIncluded { get; }
        public Func<IStudentEducationOrganizationAssociationAddressTerm, bool> IsStudentEducationOrganizationAssociationAddressTermIncluded { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "Complex":
                    return IsComplexSupported;
                case "OnBusRoute":
                    return IsOnBusRouteSupported;
                case "StudentEducationOrganizationAssociationAddressSchoolDistricts":
                    return IsStudentEducationOrganizationAssociationAddressSchoolDistrictsSupported;
                case "StudentEducationOrganizationAssociationAddressTerms":
                    return IsStudentEducationOrganizationAssociationAddressTermsSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentEducationOrganizationAssociationAddressSchoolDistrict model.
    /// </summary>
    public interface IStudentEducationOrganizationAssociationAddressSchoolDistrict : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentEducationOrganizationAssociationAddressExtension StudentEducationOrganizationAssociationAddressExtension { get; set; }
        [NaturalKeyMember]
        string SchoolDistrict { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentEducationOrganizationAssociationAddressSchoolDistrictMappingContract : IMappingContract
    {
        public StudentEducationOrganizationAssociationAddressSchoolDistrictMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentEducationOrganizationAssociationAddressTerm model.
    /// </summary>
    public interface IStudentEducationOrganizationAssociationAddressTerm : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentEducationOrganizationAssociationAddressExtension StudentEducationOrganizationAssociationAddressExtension { get; set; }
        [NaturalKeyMember]
        string TermDescriptor { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentEducationOrganizationAssociationAddressTermMappingContract : IMappingContract
    {
        public StudentEducationOrganizationAssociationAddressTermMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentEducationOrganizationAssociationExtension model.
    /// </summary>
    public interface IStudentEducationOrganizationAssociationExtension : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        EdFi.IStudentEducationOrganizationAssociation StudentEducationOrganizationAssociation { get; set; }

        // Non-PK properties
        string FavoriteProgramName { get; set; }
        string FavoriteProgramTypeDescriptor { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
        Guid? FavoriteProgramResourceId { get; set; }
        string FavoriteProgramDiscriminator { get; set; }
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentEducationOrganizationAssociationExtensionMappingContract : IMappingContract
    {
        public StudentEducationOrganizationAssociationExtensionMappingContract(
            bool isFavoriteProgramNameSupported,
            bool isFavoriteProgramTypeDescriptorSupported
            )
        {
            IsFavoriteProgramNameSupported = isFavoriteProgramNameSupported;
            IsFavoriteProgramTypeDescriptorSupported = isFavoriteProgramTypeDescriptorSupported;
        }

        public bool IsFavoriteProgramNameSupported { get; }
        public bool IsFavoriteProgramTypeDescriptorSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "FavoriteProgramName":
                    return IsFavoriteProgramNameSupported;
                case "FavoriteProgramTypeDescriptor":
                    return IsFavoriteProgramTypeDescriptorSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentEducationOrganizationAssociationStudentCharacteristicExtension model.
    /// </summary>
    public interface IStudentEducationOrganizationAssociationStudentCharacteristicExtension : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        EdFi.IStudentEducationOrganizationAssociationStudentCharacteristic StudentEducationOrganizationAssociationStudentCharacteristic { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists
        ICollection<IStudentEducationOrganizationAssociationStudentCharacteristicStudentNeed> StudentEducationOrganizationAssociationStudentCharacteristicStudentNeeds { get; set; }

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentEducationOrganizationAssociationStudentCharacteristicExtensionMappingContract : IMappingContract
    {
        public StudentEducationOrganizationAssociationStudentCharacteristicExtensionMappingContract(
            bool isStudentEducationOrganizationAssociationStudentCharacteristicStudentNeedsSupported,
            Func<IStudentEducationOrganizationAssociationStudentCharacteristicStudentNeed, bool> isStudentEducationOrganizationAssociationStudentCharacteristicStudentNeedIncluded
            )
        {
            IsStudentEducationOrganizationAssociationStudentCharacteristicStudentNeedsSupported = isStudentEducationOrganizationAssociationStudentCharacteristicStudentNeedsSupported;
            IsStudentEducationOrganizationAssociationStudentCharacteristicStudentNeedIncluded = isStudentEducationOrganizationAssociationStudentCharacteristicStudentNeedIncluded;
        }

        public bool IsStudentEducationOrganizationAssociationStudentCharacteristicStudentNeedsSupported { get; }
        public Func<IStudentEducationOrganizationAssociationStudentCharacteristicStudentNeed, bool> IsStudentEducationOrganizationAssociationStudentCharacteristicStudentNeedIncluded { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "StudentEducationOrganizationAssociationStudentCharacteristicStudentNeeds":
                    return IsStudentEducationOrganizationAssociationStudentCharacteristicStudentNeedsSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentEducationOrganizationAssociationStudentCharacteristicStudentNeed model.
    /// </summary>
    public interface IStudentEducationOrganizationAssociationStudentCharacteristicStudentNeed : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentEducationOrganizationAssociationStudentCharacteristicExtension StudentEducationOrganizationAssociationStudentCharacteristicExtension { get; set; }
        [NaturalKeyMember]
        DateTime BeginDate { get; set; }

        // Non-PK properties
        DateTime? EndDate { get; set; }
        bool? PrimaryStudentNeedIndicator { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentEducationOrganizationAssociationStudentCharacteristicStudentNeedMappingContract : IMappingContract
    {
        public StudentEducationOrganizationAssociationStudentCharacteristicStudentNeedMappingContract(
            bool isEndDateSupported,
            bool isPrimaryStudentNeedIndicatorSupported
            )
        {
            IsEndDateSupported = isEndDateSupported;
            IsPrimaryStudentNeedIndicatorSupported = isPrimaryStudentNeedIndicatorSupported;
        }

        public bool IsEndDateSupported { get; }
        public bool IsPrimaryStudentNeedIndicatorSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "EndDate":
                    return IsEndDateSupported;
                case "PrimaryStudentNeedIndicator":
                    return IsPrimaryStudentNeedIndicatorSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentExtension model.
    /// </summary>
    public interface IStudentExtension : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        EdFi.IStudent Student { get; set; }

        // Non-PK properties

        // One-to-one relationships

        IStudentPetPreference StudentPetPreference { get; set; }

        // Lists
        ICollection<IStudentAquaticPet> StudentAquaticPets { get; set; }
        ICollection<IStudentFavoriteBook> StudentFavoriteBooks { get; set; }
        ICollection<IStudentPet> StudentPets { get; set; }

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentExtensionMappingContract : IMappingContract
    {
        public StudentExtensionMappingContract(
            bool isStudentAquaticPetsSupported,
            bool isStudentFavoriteBooksSupported,
            bool isStudentPetPreferenceSupported,
            bool isStudentPetsSupported,
            Func<IStudentAquaticPet, bool> isStudentAquaticPetIncluded,
            Func<IStudentFavoriteBook, bool> isStudentFavoriteBookIncluded,
            Func<IStudentPet, bool> isStudentPetIncluded
            )
        {
            IsStudentAquaticPetsSupported = isStudentAquaticPetsSupported;
            IsStudentFavoriteBooksSupported = isStudentFavoriteBooksSupported;
            IsStudentPetPreferenceSupported = isStudentPetPreferenceSupported;
            IsStudentPetsSupported = isStudentPetsSupported;
            IsStudentAquaticPetIncluded = isStudentAquaticPetIncluded;
            IsStudentFavoriteBookIncluded = isStudentFavoriteBookIncluded;
            IsStudentPetIncluded = isStudentPetIncluded;
        }

        public bool IsStudentAquaticPetsSupported { get; }
        public bool IsStudentFavoriteBooksSupported { get; }
        public bool IsStudentPetPreferenceSupported { get; }
        public bool IsStudentPetsSupported { get; }
        public Func<IStudentAquaticPet, bool> IsStudentAquaticPetIncluded { get; }
        public Func<IStudentFavoriteBook, bool> IsStudentFavoriteBookIncluded { get; }
        public Func<IStudentPet, bool> IsStudentPetIncluded { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "StudentAquaticPets":
                    return IsStudentAquaticPetsSupported;
                case "StudentFavoriteBooks":
                    return IsStudentFavoriteBooksSupported;
                case "StudentPetPreference":
                    return IsStudentPetPreferenceSupported;
                case "StudentPets":
                    return IsStudentPetsSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentFavoriteBook model.
    /// </summary>
    public interface IStudentFavoriteBook : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentExtension StudentExtension { get; set; }
        [NaturalKeyMember]
        string FavoriteBookCategoryDescriptor { get; set; }

        // Non-PK properties
        string BookTitle { get; set; }

        // One-to-one relationships

        // Lists
        ICollection<IStudentFavoriteBookArtMedium> StudentFavoriteBookArtMedia { get; set; }

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentFavoriteBookMappingContract : IMappingContract
    {
        public StudentFavoriteBookMappingContract(
            bool isBookTitleSupported,
            bool isStudentFavoriteBookArtMediaSupported,
            Func<IStudentFavoriteBookArtMedium, bool> isStudentFavoriteBookArtMediumIncluded
            )
        {
            IsBookTitleSupported = isBookTitleSupported;
            IsStudentFavoriteBookArtMediaSupported = isStudentFavoriteBookArtMediaSupported;
            IsStudentFavoriteBookArtMediumIncluded = isStudentFavoriteBookArtMediumIncluded;
        }

        public bool IsBookTitleSupported { get; }
        public bool IsStudentFavoriteBookArtMediaSupported { get; }
        public Func<IStudentFavoriteBookArtMedium, bool> IsStudentFavoriteBookArtMediumIncluded { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "BookTitle":
                    return IsBookTitleSupported;
                case "StudentFavoriteBookArtMedia":
                    return IsStudentFavoriteBookArtMediaSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentFavoriteBookArtMedium model.
    /// </summary>
    public interface IStudentFavoriteBookArtMedium : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentFavoriteBook StudentFavoriteBook { get; set; }
        [NaturalKeyMember]
        string ArtMediumDescriptor { get; set; }

        // Non-PK properties
        int? ArtPieces { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentFavoriteBookArtMediumMappingContract : IMappingContract
    {
        public StudentFavoriteBookArtMediumMappingContract(
            bool isArtPiecesSupported
            )
        {
            IsArtPiecesSupported = isArtPiecesSupported;
        }

        public bool IsArtPiecesSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "ArtPieces":
                    return IsArtPiecesSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentGraduationPlanAssociation model.
    /// </summary>
    public interface IStudentGraduationPlanAssociation : ISynchronizable, IMappable, IHasIdentifier, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        int EducationOrganizationId { get; set; }
        [NaturalKeyMember]
        string GraduationPlanTypeDescriptor { get; set; }
        [NaturalKeyMember]
        short GraduationSchoolYear { get; set; }
        [NaturalKeyMember]
        string StudentUniqueId { get; set; }

        // Non-PK properties
        TimeSpan? CommencementTime { get; set; }
        DateTime EffectiveDate { get; set; }
        decimal? GraduationFee { get; set; }
        string HighSchoolDuration { get; set; }
        decimal HoursPerWeek { get; set; }
        bool? IsActivePlan { get; set; }
        decimal? RequiredAttendance { get; set; }
        string StaffUniqueId { get; set; }
        decimal TargetGPA { get; set; }

        // One-to-one relationships

        IStudentGraduationPlanAssociationCTEProgram StudentGraduationPlanAssociationCTEProgram { get; set; }

        // Lists
        ICollection<IStudentGraduationPlanAssociationAcademicSubject> StudentGraduationPlanAssociationAcademicSubjects { get; set; }
        ICollection<IStudentGraduationPlanAssociationCareerPathwayCode> StudentGraduationPlanAssociationCareerPathwayCodes { get; set; }
        ICollection<IStudentGraduationPlanAssociationDescription> StudentGraduationPlanAssociationDescriptions { get; set; }
        ICollection<IStudentGraduationPlanAssociationDesignatedBy> StudentGraduationPlanAssociationDesignatedBies { get; set; }
        ICollection<IStudentGraduationPlanAssociationIndustryCredential> StudentGraduationPlanAssociationIndustryCredentials { get; set; }
        ICollection<IStudentGraduationPlanAssociationStudentParentAssociation> StudentGraduationPlanAssociationStudentParentAssociations { get; set; }
        ICollection<IStudentGraduationPlanAssociationYearsAttended> StudentGraduationPlanAssociationYearsAttendeds { get; set; }

        // Resource reference data
        Guid? GraduationPlanResourceId { get; set; }
        string GraduationPlanDiscriminator { get; set; }
        Guid? StaffResourceId { get; set; }
        string StaffDiscriminator { get; set; }
        Guid? StudentResourceId { get; set; }
        string StudentDiscriminator { get; set; }
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentGraduationPlanAssociationMappingContract : IMappingContract
    {
        public StudentGraduationPlanAssociationMappingContract(
            bool isCommencementTimeSupported,
            bool isEffectiveDateSupported,
            bool isGraduationFeeSupported,
            bool isHighSchoolDurationSupported,
            bool isHoursPerWeekSupported,
            bool isIsActivePlanSupported,
            bool isRequiredAttendanceSupported,
            bool isStaffUniqueIdSupported,
            bool isStudentGraduationPlanAssociationAcademicSubjectsSupported,
            bool isStudentGraduationPlanAssociationCareerPathwayCodesSupported,
            bool isStudentGraduationPlanAssociationCTEProgramSupported,
            bool isStudentGraduationPlanAssociationDescriptionsSupported,
            bool isStudentGraduationPlanAssociationDesignatedBiesSupported,
            bool isStudentGraduationPlanAssociationIndustryCredentialsSupported,
            bool isStudentGraduationPlanAssociationStudentParentAssociationsSupported,
            bool isStudentGraduationPlanAssociationYearsAttendedsSupported,
            bool isTargetGPASupported,
            Func<IStudentGraduationPlanAssociationAcademicSubject, bool> isStudentGraduationPlanAssociationAcademicSubjectIncluded,
            Func<IStudentGraduationPlanAssociationCareerPathwayCode, bool> isStudentGraduationPlanAssociationCareerPathwayCodeIncluded,
            Func<IStudentGraduationPlanAssociationDescription, bool> isStudentGraduationPlanAssociationDescriptionIncluded,
            Func<IStudentGraduationPlanAssociationDesignatedBy, bool> isStudentGraduationPlanAssociationDesignatedByIncluded,
            Func<IStudentGraduationPlanAssociationIndustryCredential, bool> isStudentGraduationPlanAssociationIndustryCredentialIncluded,
            Func<IStudentGraduationPlanAssociationStudentParentAssociation, bool> isStudentGraduationPlanAssociationStudentParentAssociationIncluded,
            Func<IStudentGraduationPlanAssociationYearsAttended, bool> isStudentGraduationPlanAssociationYearsAttendedIncluded
            )
        {
            IsCommencementTimeSupported = isCommencementTimeSupported;
            IsEffectiveDateSupported = isEffectiveDateSupported;
            IsGraduationFeeSupported = isGraduationFeeSupported;
            IsHighSchoolDurationSupported = isHighSchoolDurationSupported;
            IsHoursPerWeekSupported = isHoursPerWeekSupported;
            IsIsActivePlanSupported = isIsActivePlanSupported;
            IsRequiredAttendanceSupported = isRequiredAttendanceSupported;
            IsStaffUniqueIdSupported = isStaffUniqueIdSupported;
            IsStudentGraduationPlanAssociationAcademicSubjectsSupported = isStudentGraduationPlanAssociationAcademicSubjectsSupported;
            IsStudentGraduationPlanAssociationCareerPathwayCodesSupported = isStudentGraduationPlanAssociationCareerPathwayCodesSupported;
            IsStudentGraduationPlanAssociationCTEProgramSupported = isStudentGraduationPlanAssociationCTEProgramSupported;
            IsStudentGraduationPlanAssociationDescriptionsSupported = isStudentGraduationPlanAssociationDescriptionsSupported;
            IsStudentGraduationPlanAssociationDesignatedBiesSupported = isStudentGraduationPlanAssociationDesignatedBiesSupported;
            IsStudentGraduationPlanAssociationIndustryCredentialsSupported = isStudentGraduationPlanAssociationIndustryCredentialsSupported;
            IsStudentGraduationPlanAssociationStudentParentAssociationsSupported = isStudentGraduationPlanAssociationStudentParentAssociationsSupported;
            IsStudentGraduationPlanAssociationYearsAttendedsSupported = isStudentGraduationPlanAssociationYearsAttendedsSupported;
            IsTargetGPASupported = isTargetGPASupported;
            IsStudentGraduationPlanAssociationAcademicSubjectIncluded = isStudentGraduationPlanAssociationAcademicSubjectIncluded;
            IsStudentGraduationPlanAssociationCareerPathwayCodeIncluded = isStudentGraduationPlanAssociationCareerPathwayCodeIncluded;
            IsStudentGraduationPlanAssociationDescriptionIncluded = isStudentGraduationPlanAssociationDescriptionIncluded;
            IsStudentGraduationPlanAssociationDesignatedByIncluded = isStudentGraduationPlanAssociationDesignatedByIncluded;
            IsStudentGraduationPlanAssociationIndustryCredentialIncluded = isStudentGraduationPlanAssociationIndustryCredentialIncluded;
            IsStudentGraduationPlanAssociationStudentParentAssociationIncluded = isStudentGraduationPlanAssociationStudentParentAssociationIncluded;
            IsStudentGraduationPlanAssociationYearsAttendedIncluded = isStudentGraduationPlanAssociationYearsAttendedIncluded;
        }

        public bool IsCommencementTimeSupported { get; }
        public bool IsEffectiveDateSupported { get; }
        public bool IsGraduationFeeSupported { get; }
        public bool IsHighSchoolDurationSupported { get; }
        public bool IsHoursPerWeekSupported { get; }
        public bool IsIsActivePlanSupported { get; }
        public bool IsRequiredAttendanceSupported { get; }
        public bool IsStaffUniqueIdSupported { get; }
        public bool IsStudentGraduationPlanAssociationAcademicSubjectsSupported { get; }
        public bool IsStudentGraduationPlanAssociationCareerPathwayCodesSupported { get; }
        public bool IsStudentGraduationPlanAssociationCTEProgramSupported { get; }
        public bool IsStudentGraduationPlanAssociationDescriptionsSupported { get; }
        public bool IsStudentGraduationPlanAssociationDesignatedBiesSupported { get; }
        public bool IsStudentGraduationPlanAssociationIndustryCredentialsSupported { get; }
        public bool IsStudentGraduationPlanAssociationStudentParentAssociationsSupported { get; }
        public bool IsStudentGraduationPlanAssociationYearsAttendedsSupported { get; }
        public bool IsTargetGPASupported { get; }
        public Func<IStudentGraduationPlanAssociationAcademicSubject, bool> IsStudentGraduationPlanAssociationAcademicSubjectIncluded { get; }
        public Func<IStudentGraduationPlanAssociationCareerPathwayCode, bool> IsStudentGraduationPlanAssociationCareerPathwayCodeIncluded { get; }
        public Func<IStudentGraduationPlanAssociationDescription, bool> IsStudentGraduationPlanAssociationDescriptionIncluded { get; }
        public Func<IStudentGraduationPlanAssociationDesignatedBy, bool> IsStudentGraduationPlanAssociationDesignatedByIncluded { get; }
        public Func<IStudentGraduationPlanAssociationIndustryCredential, bool> IsStudentGraduationPlanAssociationIndustryCredentialIncluded { get; }
        public Func<IStudentGraduationPlanAssociationStudentParentAssociation, bool> IsStudentGraduationPlanAssociationStudentParentAssociationIncluded { get; }
        public Func<IStudentGraduationPlanAssociationYearsAttended, bool> IsStudentGraduationPlanAssociationYearsAttendedIncluded { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "CommencementTime":
                    return IsCommencementTimeSupported;
                case "EffectiveDate":
                    return IsEffectiveDateSupported;
                case "GraduationFee":
                    return IsGraduationFeeSupported;
                case "HighSchoolDuration":
                    return IsHighSchoolDurationSupported;
                case "HoursPerWeek":
                    return IsHoursPerWeekSupported;
                case "IsActivePlan":
                    return IsIsActivePlanSupported;
                case "RequiredAttendance":
                    return IsRequiredAttendanceSupported;
                case "StaffUniqueId":
                    return IsStaffUniqueIdSupported;
                case "StudentGraduationPlanAssociationAcademicSubjects":
                    return IsStudentGraduationPlanAssociationAcademicSubjectsSupported;
                case "StudentGraduationPlanAssociationCareerPathwayCodes":
                    return IsStudentGraduationPlanAssociationCareerPathwayCodesSupported;
                case "StudentGraduationPlanAssociationCTEProgram":
                    return IsStudentGraduationPlanAssociationCTEProgramSupported;
                case "StudentGraduationPlanAssociationDescriptions":
                    return IsStudentGraduationPlanAssociationDescriptionsSupported;
                case "StudentGraduationPlanAssociationDesignatedBies":
                    return IsStudentGraduationPlanAssociationDesignatedBiesSupported;
                case "StudentGraduationPlanAssociationIndustryCredentials":
                    return IsStudentGraduationPlanAssociationIndustryCredentialsSupported;
                case "StudentGraduationPlanAssociationStudentParentAssociations":
                    return IsStudentGraduationPlanAssociationStudentParentAssociationsSupported;
                case "StudentGraduationPlanAssociationYearsAttendeds":
                    return IsStudentGraduationPlanAssociationYearsAttendedsSupported;
                case "TargetGPA":
                    return IsTargetGPASupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentGraduationPlanAssociationAcademicSubject model.
    /// </summary>
    public interface IStudentGraduationPlanAssociationAcademicSubject : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentGraduationPlanAssociation StudentGraduationPlanAssociation { get; set; }
        [NaturalKeyMember]
        string AcademicSubjectDescriptor { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentGraduationPlanAssociationAcademicSubjectMappingContract : IMappingContract
    {
        public StudentGraduationPlanAssociationAcademicSubjectMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentGraduationPlanAssociationCareerPathwayCode model.
    /// </summary>
    public interface IStudentGraduationPlanAssociationCareerPathwayCode : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentGraduationPlanAssociation StudentGraduationPlanAssociation { get; set; }
        [NaturalKeyMember]
        int CareerPathwayCode { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentGraduationPlanAssociationCareerPathwayCodeMappingContract : IMappingContract
    {
        public StudentGraduationPlanAssociationCareerPathwayCodeMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentGraduationPlanAssociationCTEProgram model.
    /// </summary>
    public interface IStudentGraduationPlanAssociationCTEProgram : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentGraduationPlanAssociation StudentGraduationPlanAssociation { get; set; }

        // Non-PK properties
        string CareerPathwayDescriptor { get; set; }
        string CIPCode { get; set; }
        bool? CTEProgramCompletionIndicator { get; set; }
        bool? PrimaryCTEProgramIndicator { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentGraduationPlanAssociationCTEProgramMappingContract : IMappingContract
    {
        public StudentGraduationPlanAssociationCTEProgramMappingContract(
            bool isCareerPathwayDescriptorSupported,
            bool isCIPCodeSupported,
            bool isCTEProgramCompletionIndicatorSupported,
            bool isPrimaryCTEProgramIndicatorSupported
            )
        {
            IsCareerPathwayDescriptorSupported = isCareerPathwayDescriptorSupported;
            IsCIPCodeSupported = isCIPCodeSupported;
            IsCTEProgramCompletionIndicatorSupported = isCTEProgramCompletionIndicatorSupported;
            IsPrimaryCTEProgramIndicatorSupported = isPrimaryCTEProgramIndicatorSupported;
        }

        public bool IsCareerPathwayDescriptorSupported { get; }
        public bool IsCIPCodeSupported { get; }
        public bool IsCTEProgramCompletionIndicatorSupported { get; }
        public bool IsPrimaryCTEProgramIndicatorSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "CareerPathwayDescriptor":
                    return IsCareerPathwayDescriptorSupported;
                case "CIPCode":
                    return IsCIPCodeSupported;
                case "CTEProgramCompletionIndicator":
                    return IsCTEProgramCompletionIndicatorSupported;
                case "PrimaryCTEProgramIndicator":
                    return IsPrimaryCTEProgramIndicatorSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentGraduationPlanAssociationDescription model.
    /// </summary>
    public interface IStudentGraduationPlanAssociationDescription : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentGraduationPlanAssociation StudentGraduationPlanAssociation { get; set; }
        [NaturalKeyMember]
        string Description { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentGraduationPlanAssociationDescriptionMappingContract : IMappingContract
    {
        public StudentGraduationPlanAssociationDescriptionMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentGraduationPlanAssociationDesignatedBy model.
    /// </summary>
    public interface IStudentGraduationPlanAssociationDesignatedBy : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentGraduationPlanAssociation StudentGraduationPlanAssociation { get; set; }
        [NaturalKeyMember]
        string DesignatedBy { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentGraduationPlanAssociationDesignatedByMappingContract : IMappingContract
    {
        public StudentGraduationPlanAssociationDesignatedByMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentGraduationPlanAssociationIndustryCredential model.
    /// </summary>
    public interface IStudentGraduationPlanAssociationIndustryCredential : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentGraduationPlanAssociation StudentGraduationPlanAssociation { get; set; }
        [NaturalKeyMember]
        string IndustryCredential { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentGraduationPlanAssociationIndustryCredentialMappingContract : IMappingContract
    {
        public StudentGraduationPlanAssociationIndustryCredentialMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentGraduationPlanAssociationStudentParentAssociation model.
    /// </summary>
    public interface IStudentGraduationPlanAssociationStudentParentAssociation : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentGraduationPlanAssociation StudentGraduationPlanAssociation { get; set; }
        [NaturalKeyMember]
        string ParentUniqueId { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
        Guid? StudentParentAssociationResourceId { get; set; }
        string StudentParentAssociationDiscriminator { get; set; }
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentGraduationPlanAssociationStudentParentAssociationMappingContract : IMappingContract
    {
        public StudentGraduationPlanAssociationStudentParentAssociationMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentGraduationPlanAssociationYearsAttended model.
    /// </summary>
    public interface IStudentGraduationPlanAssociationYearsAttended : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentGraduationPlanAssociation StudentGraduationPlanAssociation { get; set; }
        [NaturalKeyMember]
        short YearsAttended { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentGraduationPlanAssociationYearsAttendedMappingContract : IMappingContract
    {
        public StudentGraduationPlanAssociationYearsAttendedMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentParentAssociationDiscipline model.
    /// </summary>
    public interface IStudentParentAssociationDiscipline : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentParentAssociationExtension StudentParentAssociationExtension { get; set; }
        [NaturalKeyMember]
        string DisciplineDescriptor { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentParentAssociationDisciplineMappingContract : IMappingContract
    {
        public StudentParentAssociationDisciplineMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentParentAssociationExtension model.
    /// </summary>
    public interface IStudentParentAssociationExtension : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        EdFi.IStudentParentAssociation StudentParentAssociation { get; set; }

        // Non-PK properties
        bool BedtimeReader { get; set; }
        decimal? BedtimeReadingRate { get; set; }
        decimal? BookBudget { get; set; }
        int? BooksBorrowed { get; set; }
        int? EducationOrganizationId { get; set; }
        string InterventionStudyIdentificationCode { get; set; }
        int? LibraryDuration { get; set; }
        TimeSpan? LibraryTime { get; set; }
        short? LibraryVisits { get; set; }
        string PriorContactRestrictions { get; set; }
        DateTime? ReadGreenEggsAndHamDate { get; set; }
        string ReadingTimeSpent { get; set; }
        short? StudentRead { get; set; }

        // One-to-one relationships

        IStudentParentAssociationTelephone StudentParentAssociationTelephone { get; set; }

        // Lists
        ICollection<IStudentParentAssociationDiscipline> StudentParentAssociationDisciplines { get; set; }
        ICollection<IStudentParentAssociationFavoriteBookTitle> StudentParentAssociationFavoriteBookTitles { get; set; }
        ICollection<IStudentParentAssociationHoursPerWeek> StudentParentAssociationHoursPerWeeks { get; set; }
        ICollection<IStudentParentAssociationPagesRead> StudentParentAssociationPagesReads { get; set; }
        ICollection<IStudentParentAssociationStaffEducationOrganizationEmploymentAssociation> StudentParentAssociationStaffEducationOrganizationEmploymentAssociations { get; set; }

        // Resource reference data
        Guid? InterventionStudyResourceId { get; set; }
        string InterventionStudyDiscriminator { get; set; }
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentParentAssociationExtensionMappingContract : IMappingContract
    {
        public StudentParentAssociationExtensionMappingContract(
            bool isBedtimeReaderSupported,
            bool isBedtimeReadingRateSupported,
            bool isBookBudgetSupported,
            bool isBooksBorrowedSupported,
            bool isEducationOrganizationIdSupported,
            bool isInterventionStudyIdentificationCodeSupported,
            bool isLibraryDurationSupported,
            bool isLibraryTimeSupported,
            bool isLibraryVisitsSupported,
            bool isPriorContactRestrictionsSupported,
            bool isReadGreenEggsAndHamDateSupported,
            bool isReadingTimeSpentSupported,
            bool isStudentParentAssociationDisciplinesSupported,
            bool isStudentParentAssociationFavoriteBookTitlesSupported,
            bool isStudentParentAssociationHoursPerWeeksSupported,
            bool isStudentParentAssociationPagesReadsSupported,
            bool isStudentParentAssociationStaffEducationOrganizationEmploymentAssociationsSupported,
            bool isStudentParentAssociationTelephoneSupported,
            bool isStudentReadSupported,
            Func<IStudentParentAssociationDiscipline, bool> isStudentParentAssociationDisciplineIncluded,
            Func<IStudentParentAssociationFavoriteBookTitle, bool> isStudentParentAssociationFavoriteBookTitleIncluded,
            Func<IStudentParentAssociationHoursPerWeek, bool> isStudentParentAssociationHoursPerWeekIncluded,
            Func<IStudentParentAssociationPagesRead, bool> isStudentParentAssociationPagesReadIncluded,
            Func<IStudentParentAssociationStaffEducationOrganizationEmploymentAssociation, bool> isStudentParentAssociationStaffEducationOrganizationEmploymentAssociationIncluded
            )
        {
            IsBedtimeReaderSupported = isBedtimeReaderSupported;
            IsBedtimeReadingRateSupported = isBedtimeReadingRateSupported;
            IsBookBudgetSupported = isBookBudgetSupported;
            IsBooksBorrowedSupported = isBooksBorrowedSupported;
            IsEducationOrganizationIdSupported = isEducationOrganizationIdSupported;
            IsInterventionStudyIdentificationCodeSupported = isInterventionStudyIdentificationCodeSupported;
            IsLibraryDurationSupported = isLibraryDurationSupported;
            IsLibraryTimeSupported = isLibraryTimeSupported;
            IsLibraryVisitsSupported = isLibraryVisitsSupported;
            IsPriorContactRestrictionsSupported = isPriorContactRestrictionsSupported;
            IsReadGreenEggsAndHamDateSupported = isReadGreenEggsAndHamDateSupported;
            IsReadingTimeSpentSupported = isReadingTimeSpentSupported;
            IsStudentParentAssociationDisciplinesSupported = isStudentParentAssociationDisciplinesSupported;
            IsStudentParentAssociationFavoriteBookTitlesSupported = isStudentParentAssociationFavoriteBookTitlesSupported;
            IsStudentParentAssociationHoursPerWeeksSupported = isStudentParentAssociationHoursPerWeeksSupported;
            IsStudentParentAssociationPagesReadsSupported = isStudentParentAssociationPagesReadsSupported;
            IsStudentParentAssociationStaffEducationOrganizationEmploymentAssociationsSupported = isStudentParentAssociationStaffEducationOrganizationEmploymentAssociationsSupported;
            IsStudentParentAssociationTelephoneSupported = isStudentParentAssociationTelephoneSupported;
            IsStudentReadSupported = isStudentReadSupported;
            IsStudentParentAssociationDisciplineIncluded = isStudentParentAssociationDisciplineIncluded;
            IsStudentParentAssociationFavoriteBookTitleIncluded = isStudentParentAssociationFavoriteBookTitleIncluded;
            IsStudentParentAssociationHoursPerWeekIncluded = isStudentParentAssociationHoursPerWeekIncluded;
            IsStudentParentAssociationPagesReadIncluded = isStudentParentAssociationPagesReadIncluded;
            IsStudentParentAssociationStaffEducationOrganizationEmploymentAssociationIncluded = isStudentParentAssociationStaffEducationOrganizationEmploymentAssociationIncluded;
        }

        public bool IsBedtimeReaderSupported { get; }
        public bool IsBedtimeReadingRateSupported { get; }
        public bool IsBookBudgetSupported { get; }
        public bool IsBooksBorrowedSupported { get; }
        public bool IsEducationOrganizationIdSupported { get; }
        public bool IsInterventionStudyIdentificationCodeSupported { get; }
        public bool IsLibraryDurationSupported { get; }
        public bool IsLibraryTimeSupported { get; }
        public bool IsLibraryVisitsSupported { get; }
        public bool IsPriorContactRestrictionsSupported { get; }
        public bool IsReadGreenEggsAndHamDateSupported { get; }
        public bool IsReadingTimeSpentSupported { get; }
        public bool IsStudentParentAssociationDisciplinesSupported { get; }
        public bool IsStudentParentAssociationFavoriteBookTitlesSupported { get; }
        public bool IsStudentParentAssociationHoursPerWeeksSupported { get; }
        public bool IsStudentParentAssociationPagesReadsSupported { get; }
        public bool IsStudentParentAssociationStaffEducationOrganizationEmploymentAssociationsSupported { get; }
        public bool IsStudentParentAssociationTelephoneSupported { get; }
        public bool IsStudentReadSupported { get; }
        public Func<IStudentParentAssociationDiscipline, bool> IsStudentParentAssociationDisciplineIncluded { get; }
        public Func<IStudentParentAssociationFavoriteBookTitle, bool> IsStudentParentAssociationFavoriteBookTitleIncluded { get; }
        public Func<IStudentParentAssociationHoursPerWeek, bool> IsStudentParentAssociationHoursPerWeekIncluded { get; }
        public Func<IStudentParentAssociationPagesRead, bool> IsStudentParentAssociationPagesReadIncluded { get; }
        public Func<IStudentParentAssociationStaffEducationOrganizationEmploymentAssociation, bool> IsStudentParentAssociationStaffEducationOrganizationEmploymentAssociationIncluded { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "BedtimeReader":
                    return IsBedtimeReaderSupported;
                case "BedtimeReadingRate":
                    return IsBedtimeReadingRateSupported;
                case "BookBudget":
                    return IsBookBudgetSupported;
                case "BooksBorrowed":
                    return IsBooksBorrowedSupported;
                case "EducationOrganizationId":
                    return IsEducationOrganizationIdSupported;
                case "InterventionStudyIdentificationCode":
                    return IsInterventionStudyIdentificationCodeSupported;
                case "LibraryDuration":
                    return IsLibraryDurationSupported;
                case "LibraryTime":
                    return IsLibraryTimeSupported;
                case "LibraryVisits":
                    return IsLibraryVisitsSupported;
                case "PriorContactRestrictions":
                    return IsPriorContactRestrictionsSupported;
                case "ReadGreenEggsAndHamDate":
                    return IsReadGreenEggsAndHamDateSupported;
                case "ReadingTimeSpent":
                    return IsReadingTimeSpentSupported;
                case "StudentParentAssociationDisciplines":
                    return IsStudentParentAssociationDisciplinesSupported;
                case "StudentParentAssociationFavoriteBookTitles":
                    return IsStudentParentAssociationFavoriteBookTitlesSupported;
                case "StudentParentAssociationHoursPerWeeks":
                    return IsStudentParentAssociationHoursPerWeeksSupported;
                case "StudentParentAssociationPagesReads":
                    return IsStudentParentAssociationPagesReadsSupported;
                case "StudentParentAssociationStaffEducationOrganizationEmploymentAssociations":
                    return IsStudentParentAssociationStaffEducationOrganizationEmploymentAssociationsSupported;
                case "StudentParentAssociationTelephone":
                    return IsStudentParentAssociationTelephoneSupported;
                case "StudentRead":
                    return IsStudentReadSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentParentAssociationFavoriteBookTitle model.
    /// </summary>
    public interface IStudentParentAssociationFavoriteBookTitle : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentParentAssociationExtension StudentParentAssociationExtension { get; set; }
        [NaturalKeyMember]
        string FavoriteBookTitle { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentParentAssociationFavoriteBookTitleMappingContract : IMappingContract
    {
        public StudentParentAssociationFavoriteBookTitleMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentParentAssociationHoursPerWeek model.
    /// </summary>
    public interface IStudentParentAssociationHoursPerWeek : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentParentAssociationExtension StudentParentAssociationExtension { get; set; }
        [NaturalKeyMember]
        decimal HoursPerWeek { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentParentAssociationHoursPerWeekMappingContract : IMappingContract
    {
        public StudentParentAssociationHoursPerWeekMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentParentAssociationPagesRead model.
    /// </summary>
    public interface IStudentParentAssociationPagesRead : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentParentAssociationExtension StudentParentAssociationExtension { get; set; }
        [NaturalKeyMember]
        decimal PagesRead { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentParentAssociationPagesReadMappingContract : IMappingContract
    {
        public StudentParentAssociationPagesReadMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentParentAssociationStaffEducationOrganizationEmploymentAssociation model.
    /// </summary>
    public interface IStudentParentAssociationStaffEducationOrganizationEmploymentAssociation : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentParentAssociationExtension StudentParentAssociationExtension { get; set; }
        [NaturalKeyMember]
        int EducationOrganizationId { get; set; }
        [NaturalKeyMember]
        string EmploymentStatusDescriptor { get; set; }
        [NaturalKeyMember]
        DateTime HireDate { get; set; }
        [NaturalKeyMember]
        string StaffUniqueId { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
        Guid? StaffEducationOrganizationEmploymentAssociationResourceId { get; set; }
        string StaffEducationOrganizationEmploymentAssociationDiscriminator { get; set; }
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentParentAssociationStaffEducationOrganizationEmploymentAssociationMappingContract : IMappingContract
    {
        public StudentParentAssociationStaffEducationOrganizationEmploymentAssociationMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentParentAssociationTelephone model.
    /// </summary>
    public interface IStudentParentAssociationTelephone : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentParentAssociationExtension StudentParentAssociationExtension { get; set; }

        // Non-PK properties
        bool? DoNotPublishIndicator { get; set; }
        int? OrderOfPriority { get; set; }
        string TelephoneNumber { get; set; }
        string TelephoneNumberTypeDescriptor { get; set; }
        bool? TextMessageCapabilityIndicator { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentParentAssociationTelephoneMappingContract : IMappingContract
    {
        public StudentParentAssociationTelephoneMappingContract(
            bool isDoNotPublishIndicatorSupported,
            bool isOrderOfPrioritySupported,
            bool isTelephoneNumberSupported,
            bool isTelephoneNumberTypeDescriptorSupported,
            bool isTextMessageCapabilityIndicatorSupported
            )
        {
            IsDoNotPublishIndicatorSupported = isDoNotPublishIndicatorSupported;
            IsOrderOfPrioritySupported = isOrderOfPrioritySupported;
            IsTelephoneNumberSupported = isTelephoneNumberSupported;
            IsTelephoneNumberTypeDescriptorSupported = isTelephoneNumberTypeDescriptorSupported;
            IsTextMessageCapabilityIndicatorSupported = isTextMessageCapabilityIndicatorSupported;
        }

        public bool IsDoNotPublishIndicatorSupported { get; }
        public bool IsOrderOfPrioritySupported { get; }
        public bool IsTelephoneNumberSupported { get; }
        public bool IsTelephoneNumberTypeDescriptorSupported { get; }
        public bool IsTextMessageCapabilityIndicatorSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "DoNotPublishIndicator":
                    return IsDoNotPublishIndicatorSupported;
                case "OrderOfPriority":
                    return IsOrderOfPrioritySupported;
                case "TelephoneNumber":
                    return IsTelephoneNumberSupported;
                case "TelephoneNumberTypeDescriptor":
                    return IsTelephoneNumberTypeDescriptorSupported;
                case "TextMessageCapabilityIndicator":
                    return IsTextMessageCapabilityIndicatorSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentPet model.
    /// </summary>
    public interface IStudentPet : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentExtension StudentExtension { get; set; }
        [NaturalKeyMember]
        string PetName { get; set; }

        // Non-PK properties
        bool? IsFixed { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentPetMappingContract : IMappingContract
    {
        public StudentPetMappingContract(
            bool isIsFixedSupported
            )
        {
            IsIsFixedSupported = isIsFixedSupported;
        }

        public bool IsIsFixedSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "IsFixed":
                    return IsIsFixedSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentPetPreference model.
    /// </summary>
    public interface IStudentPetPreference : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentExtension StudentExtension { get; set; }

        // Non-PK properties
        int MaximumWeight { get; set; }
        int MinimumWeight { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentPetPreferenceMappingContract : IMappingContract
    {
        public StudentPetPreferenceMappingContract(
            bool isMaximumWeightSupported,
            bool isMinimumWeightSupported
            )
        {
            IsMaximumWeightSupported = isMaximumWeightSupported;
            IsMinimumWeightSupported = isMinimumWeightSupported;
        }

        public bool IsMaximumWeightSupported { get; }
        public bool IsMinimumWeightSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "MaximumWeight":
                    return IsMaximumWeightSupported;
                case "MinimumWeight":
                    return IsMinimumWeightSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentSchoolAssociationExtension model.
    /// </summary>
    public interface IStudentSchoolAssociationExtension : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        EdFi.IStudentSchoolAssociation StudentSchoolAssociation { get; set; }

        // Non-PK properties
        string MembershipTypeDescriptor { get; set; }

        // One-to-one relationships

        // Lists

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentSchoolAssociationExtensionMappingContract : IMappingContract
    {
        public StudentSchoolAssociationExtensionMappingContract(
            bool isMembershipTypeDescriptorSupported
            )
        {
            IsMembershipTypeDescriptorSupported = isMembershipTypeDescriptorSupported;
        }

        public bool IsMembershipTypeDescriptorSupported { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "MembershipTypeDescriptor":
                    return IsMembershipTypeDescriptorSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentSectionAssociationExtension model.
    /// </summary>
    public interface IStudentSectionAssociationExtension : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        EdFi.IStudentSectionAssociation StudentSectionAssociation { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists
        ICollection<IStudentSectionAssociationRelatedGeneralStudentProgramAssociation> StudentSectionAssociationRelatedGeneralStudentProgramAssociations { get; set; }

        // Resource reference data
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentSectionAssociationExtensionMappingContract : IMappingContract
    {
        public StudentSectionAssociationExtensionMappingContract(
            bool isStudentSectionAssociationRelatedGeneralStudentProgramAssociationsSupported,
            Func<IStudentSectionAssociationRelatedGeneralStudentProgramAssociation, bool> isStudentSectionAssociationRelatedGeneralStudentProgramAssociationIncluded
            )
        {
            IsStudentSectionAssociationRelatedGeneralStudentProgramAssociationsSupported = isStudentSectionAssociationRelatedGeneralStudentProgramAssociationsSupported;
            IsStudentSectionAssociationRelatedGeneralStudentProgramAssociationIncluded = isStudentSectionAssociationRelatedGeneralStudentProgramAssociationIncluded;
        }

        public bool IsStudentSectionAssociationRelatedGeneralStudentProgramAssociationsSupported { get; }
        public Func<IStudentSectionAssociationRelatedGeneralStudentProgramAssociation, bool> IsStudentSectionAssociationRelatedGeneralStudentProgramAssociationIncluded { get; }

        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                case "StudentSectionAssociationRelatedGeneralStudentProgramAssociations":
                    return IsStudentSectionAssociationRelatedGeneralStudentProgramAssociationsSupported;
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }

    /// <summary>
    /// Defines available properties and methods for the abstraction of the StudentSectionAssociationRelatedGeneralStudentProgramAssociation model.
    /// </summary>
    public interface IStudentSectionAssociationRelatedGeneralStudentProgramAssociation : ISynchronizable, IMappable, IGetByExample
    {
        // Primary Key properties
        [NaturalKeyMember]
        IStudentSectionAssociationExtension StudentSectionAssociationExtension { get; set; }
        [NaturalKeyMember]
        DateTime RelatedBeginDate { get; set; }
        [NaturalKeyMember]
        int RelatedEducationOrganizationId { get; set; }
        [NaturalKeyMember]
        int RelatedProgramEducationOrganizationId { get; set; }
        [NaturalKeyMember]
        string RelatedProgramName { get; set; }
        [NaturalKeyMember]
        string RelatedProgramTypeDescriptor { get; set; }

        // Non-PK properties

        // One-to-one relationships

        // Lists

        // Resource reference data
        Guid? RelatedGeneralStudentProgramAssociationResourceId { get; set; }
        string RelatedGeneralStudentProgramAssociationDiscriminator { get; set; }
    }

    /// <summary>
    /// Defines a mapping contract appropriate for a particular context when data is either being mapped or synchronized
    /// between entities/resources during API request processing.
    /// </summary>
    public class StudentSectionAssociationRelatedGeneralStudentProgramAssociationMappingContract : IMappingContract
    {
        public StudentSectionAssociationRelatedGeneralStudentProgramAssociationMappingContract(
            )
        {
        }


        bool IMappingContract.IsMemberSupported(string memberName)
        {
            switch (memberName)
            {
                default:
                    throw new Exception($"Unknown member '{memberName}'.");
            }
        }

    }
}
