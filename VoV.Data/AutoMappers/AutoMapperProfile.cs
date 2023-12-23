using AutoMapper;
using VoV.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.Entities;

namespace VoV.Data.AutoMappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region AppSetting
            CreateMap<AppSettingDTO, AppSetting>();
            CreateMap<AppSetting, AppSettingDTO>();
            #endregion

            #region UsersDTO
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
            #endregion

            #region BusinessSegment
            CreateMap<BusinessSegmentDTO, BusinessSegment>();
            CreateMap<BusinessSegment, BusinessSegmentDTO>();
            #endregion

            #region BussinessUnit
            CreateMap<BusinessUnitDTO, BusinessUnit>();
            CreateMap<BusinessUnit, BusinessUnitDTO>();
            #endregion

            #region Clients
            CreateMap<ClientDTO, Client>();
            CreateMap<Client, ClientDTO>();
            #endregion

            #region ClientAccount
            CreateMap<ClientAccountDTO, ClientAccount>();
            CreateMap<ClientAccount, ClientAccountDTO>();
            #endregion

            #region ClientBusinessUnit
            CreateMap<ClientBusinessUnitDTO, ClientBusinessUnit>();
            CreateMap<ClientBusinessUnit, ClientBusinessUnitDTO>();
            #endregion

            #region ClientEmployee
            CreateMap<ClientEmployeeDTO, ClientEmployee>();
            CreateMap<ClientEmployee, ClientEmployeeDTO>();
            #endregion

            #region ClientFinancial
            CreateMap<ClientFinancialDTO, ClientFinancial>()
                .ForMember(dest => dest.Currency, src => src.Ignore());
            CreateMap<ClientFinancial, ClientFinancialDTO>();
            #endregion

            #region ClientGroup
            CreateMap<ClientGroupDTO, ClientGroup>();
            CreateMap<ClientGroup, ClientGroupDTO>();
            #endregion

            //#region CompanyObservation
            //CreateMap<CompanyObservationDTO, CompanyObservation>();
            //CreateMap<CompanyObservation, CompanyObservationDTO>();
            //#endregion

            #region Company
            CreateMap<CompanyDTO, Company>();
            CreateMap<Company, CompanyDTO>();
            #endregion

            #region CompanyOpportunity
            CreateMap<CompanyOpportunityDTO, CompanyOpportunity>();
            CreateMap<CompanyOpportunity, CompanyOpportunityDTO>();
            #endregion

            #region CompanyRisk
            CreateMap<CompanyRiskDTO, CompanyRisk>();
            CreateMap<CompanyRisk, CompanyRiskDTO>();
            #endregion

            #region Designation
            CreateMap<DesignationDTO, Designation>();
            CreateMap<Designation, DesignationDTO>();
            #endregion

            #region FinancialYear
            CreateMap<FinancialYearDTO, FinancialYear>();
            CreateMap<FinancialYear, FinancialYearDTO>();
            #endregion

            #region Role
            CreateMap<RoleDTO, Role>();
            CreateMap<Role, RoleDTO>();
            #endregion

            #region Location
            CreateMap<LocationDTO, Location>();
            CreateMap<Location, LocationDTO>();
            #endregion

            //#region StandardObservation
            //CreateMap<StandardObservationDTO, StandardObservation>();
            //CreateMap<StandardObservation, StandardObservationDTO>();
            //#endregion

            #region StandardObservation
            CreateMap<StandardOpportunityDTO, StandardOpportunity>();
            CreateMap<StandardOpportunity, StandardOpportunityDTO>();
            #endregion

            #region StandardObservation
            CreateMap<ClientFinancialFileDTO, ClientFinancialFile>();
            CreateMap<ClientFinancialFile, ClientFinancialFileDTO>();
            #endregion

            #region AccountType
            CreateMap<AccountTypeDTO, AccountType>();
            CreateMap<AccountType, AccountTypeDTO>();
            #endregion

            #region StandardRisk
            CreateMap<StandardRiskDTO, StandardRisk>();
            CreateMap<StandardRisk, StandardRiskDTO>();
            #endregion

            #region Currency
            CreateMap<CurrencyDTO, Currency>();
            CreateMap<Currency, CurrencyDTO>();
            #endregion

            #region Meeting
            CreateMap<MeetingDTO, Meeting>();
            CreateMap<Meeting, MeetingDTO>();
            #endregion

            #region MeetingRisk
            CreateMap<MeetingRiskDTO, MeetingRisk>();
            CreateMap<MeetingRisk, MeetingRiskDTO>();
            #endregion

            #region MeetingClientAttendees
            CreateMap<MeetingClientAttendeesDTO, MeetingClientAttendee>();
            CreateMap<MeetingClientAttendee, MeetingClientAttendeesDTO>();
            #endregion

            #region MeetingCompanyAttendees
            CreateMap<MeetingCompanyAttendeesDTO, MeetingCompanyAttendee>();
            CreateMap<MeetingCompanyAttendee, MeetingClientAttendeesDTO>();
            #endregion

            #region MeetingRisk
            CreateMap<MeetingRiskDTO, MeetingRisk>();
            CreateMap<MeetingRisk, MeetingRiskDTO>();
            #endregion

            #region MeetingOpportunity
            CreateMap<MeetingOpportunityDTO, MeetingOpportunity>();
            CreateMap<MeetingOpportunity, MeetingOpportunityDTO>();
            #endregion

            #region MeetingObservationAndOtherMatter
            CreateMap<MeetingObservationAndOtherMatterDTO, MeetingObservationAndOtherMatter>();
            CreateMap<MeetingObservationAndOtherMatter, MeetingObservationAndOtherMatterDTO>();
            #endregion

            #region BusinessSubSegment
            CreateMap<BusinessSubSegmentDTO, BusinessSubSegment>();
            CreateMap<BusinessSubSegment, BusinessSubSegmentDTO>();
            #endregion
        }
    }
}
