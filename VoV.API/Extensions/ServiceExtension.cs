using VoV.Data.Entities;
using VoV.Services.Interface;
using VoV.Services.Service;

namespace VoV.API.Extensions
{
    public class ServiceExtension
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<ExceptionMiddleware>();
            services.AddScoped<IBusinessSegmentService, BusinessSegmentService>();
            services.AddScoped<IClientAccountService, ClientAccountService>();
            services.AddScoped<IClientBusinessUnitService, ClientBusinessUnitService>();
            services.AddScoped<IBusinessUnitService, BusinessUnitService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IClientEmployeeService, ClientEmployeeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClientFinancialService, ClientFinancialService>();
            services.AddScoped<IClientGroupService, ClientGroupService>();
            services.AddScoped<IClientService, ClientService>();
            //services.AddScoped<ICompanyObservationService, CompanyObservationService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICompanyOpportunityService, CompanyOpportunityService>();
            services.AddScoped<ICompanyRiskService, CompanyRiskService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IFinancialYearService, FinancialYearService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IRoleService, RoleService>();
            //services.AddScoped<IStandardObservationService, StandardObservationService>();
            services.AddScoped<IStandardRiskService, StandardRiskService>();
            services.AddScoped<IStandardOpportunityService, StandardOpportunityService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IAccountTypeService, AccountTypeService>();
            services.AddScoped<IClientFinancialFileService, ClientFinancialFileService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IHelperService, HelperService>();
            services.AddScoped<IMeetingService, MeetingService>();
            services.AddScoped<IMeetingClientAttendeesService, MeetingClientAttendeesService>();
            services.AddScoped<IMeetingCompanyAttendeesService, MeetingCompanyAttendeesService>();
            services.AddScoped<IMeetingRiskService, MeetingRiskService>();
            services.AddScoped<IMeetingOpportunityService, MeetingOpportunityService>();
            services.AddScoped<IMeetingObservationAndOtherMatterService, MeetingObservationAndOtherMatterService>();
            services.AddScoped<IBusinessSubSegmentService, BusinessSubSegmentService>();
        }
    }
}
