using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using VoV.Core.Enum;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using VoV.Services.Interface;
using static VoV.Data.DTOs.SearchMeetingDTO;

namespace VoV.Services.Service
{
    public class MeetingService : IMeetingService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        private readonly ApplicationSettings _appSettings;
        IHelperService _helperService;
        #endregion

        #region Constructor
        public MeetingService(VoVDbContext dbContext,
            IOptions<ApplicationSettings> appSettings,
            IHelperService helperService,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _helperService = helperService;
        }

        #endregion

        #region Method
        public async Task<Guid> AddMeeting(MeetingDTO model)
        {
            Meeting meetingEntity = new Meeting();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                meetingEntity = _mapper.Map<Meeting>(model);
                //meetingEntity.MeetingStatus = "-";

                meetingEntity.MeetingStatusId = 0;
                meetingEntity.Id = Guid.NewGuid();
                meetingEntity.CreatedOn = DateTime.Now;
                await _dbContext.Meetings.AddAsync(meetingEntity);
                if (model.MeetingClientAttendeesIds != null && model.MeetingClientAttendeesIds.Count > 0)
                {
                    //save in MeetingClientAttens table using foreach
                    foreach (var attendeesId in model.MeetingClientAttendeesIds)
                    {
                        MeetingClientAttendee meetingClient = new MeetingClientAttendee()
                        {
                            CreatedById = meetingEntity.CreatedById,
                            Id = Guid.NewGuid(),
                            MeetingId = meetingEntity.Id,
                            ClientEmployeeId = attendeesId,
                        };
                        await _dbContext.MeetingClientAttendees.AddAsync(meetingClient);
                    }
                }
                if (model.MeetingCompanyAttendeesIds != null && model.MeetingCompanyAttendeesIds.Count > 0)
                {
                    //save in MeetingClientAttens table using foreach
                    foreach (var attendeesId in model.MeetingCompanyAttendeesIds)
                    {
                        MeetingCompanyAttendee meetingCompany = new MeetingCompanyAttendee()
                        {
                            Id = Guid.NewGuid(),
                            CreatedById = meetingEntity.CreatedById,
                            MeetingId = meetingEntity.Id,
                            CompanyUserId = attendeesId
                        };

                        await _dbContext.MeetingCompanyAttendees.AddAsync(meetingCompany);
                    }
                }

                //#region Meeting Invitation Email
                ////comUserId > email
                //var companyUser = _dbContext.Users.Include(x => x.Location).ThenInclude(x => x.Company)
                //    .Where(x => x.Id == meetingEntity.CompanyUserId).FirstOrDefault();
                //if (companyUser != null && !string.IsNullOrEmpty(companyUser.Email))
                //{
                //    EmailEventModel eventModel = new EmailEventModel()
                //    {
                //        EventName = meetingEntity.MeetingPurpose, //mail subject also
                //        EventDescription = meetingEntity.Agenda,
                //        Location = companyUser?.Location?.Name ?? string.Empty, //companyUId > User.Location
                //        StartDateTime = meetingEntity.ScheduledOn,
                //        EndDateTime = meetingEntity.ScheduledOn.AddHours(2),
                //        Addess = companyUser?.Location?.Company?.Address ?? string.Empty,
                //        Summary = meetingEntity.MeetingPurpose,
                //        MailFrom = _appSettings.MailFrom,
                //        MailTo = _appSettings.MailFrom//companyUser?.Email ?? string.Empty
                //    };
                //    _helperService.SendInvitationMailWithAttachments(eventModel);
                //}
                //#endregion

                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return meetingEntity.Id;
        }

        public async Task<Guid?> EditMeeting(MeetingDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.Meetings.Include(x => x.MeetingCompanyAttendees).Include(x => x.MeetingClientAttendees)
                    .FirstOrDefaultAsync(f => f.Id == model.Id);

                if (originalEntity != null)
                {
                    model.CreatedById = originalEntity.CreatedById;
                    model.CreatedOn = originalEntity.CreatedOn;
                    model.UpdatedOn = DateTime.Now;

                    if (model.MeetingClientAttendeesIds != null && model.MeetingClientAttendeesIds.Count > 0)
                    {
                        model.MeetingClientAttendeesIds = model.MeetingClientAttendeesIds.Distinct().ToList();
                        //remove existing records
                        _dbContext.MeetingClientAttendees.RemoveRange(originalEntity.MeetingClientAttendees);
                        //save in MeetingClientAttens table using foreach
                        foreach (var attendeesId in model.MeetingClientAttendeesIds)
                        {
                            MeetingClientAttendee meetingClient = new MeetingClientAttendee()
                            {

                                Id = Guid.NewGuid(),
                                MeetingId = originalEntity.Id,
                                CreatedById = originalEntity.CreatedById,
                                ClientEmployeeId = attendeesId,
                            };
                            await _dbContext.MeetingClientAttendees.AddAsync(meetingClient);
                        }
                    }
                    if (model.MeetingCompanyAttendeesIds != null && model.MeetingCompanyAttendeesIds.Count > 0)
                    {
                        model.MeetingCompanyAttendeesIds = model.MeetingCompanyAttendeesIds.Distinct().ToList();
                        //remove existing records
                        _dbContext.MeetingCompanyAttendees.RemoveRange(originalEntity.MeetingCompanyAttendees);
                        //save in MeetingClientAttens table using foreach
                        foreach (var attendeesId in model.MeetingCompanyAttendeesIds)
                        {

                            MeetingCompanyAttendee meetingCompany = new MeetingCompanyAttendee()
                            {
                                Id = Guid.NewGuid(),
                                MeetingId = originalEntity.Id,
                                CompanyUserId = model.CompanyUserId,
                                CreatedById = originalEntity.CreatedById,
                            };

                            await _dbContext.MeetingCompanyAttendees.AddAsync(meetingCompany);
                        }
                    }
                    _dbContext.Entry(originalEntity).CurrentValues.SetValues(model);
                    await _dbContext.SaveChangesAsync();
                    id = model.Id;
                }
                transaction.Commit();
            }
            return id;
        }


        public async Task<MeetingDTO> GetMeetingById(Guid id)
        {

            var result = await (from x in _dbContext.Meetings.Include(x => x.CompanyUser).Include(x => x.ClientEmployee)
                               .ThenInclude(x => x.ClientBusinessUnit).Include(x => x.CompanyUser).
               Include(x => x.Client).ThenInclude(x => x.ClientGroup).Include(x => x.MeetingClientAttendees).ThenInclude(x => x.ClientEmployee)
                                where x.Id == id
                                select new MeetingDTO()
                                {
                                    Id = x.Id,
                                    CompanyId = x.CompanyId,
                                    ClientBusinessUnitId = x.ClientBusinessUnitId,
                                    ClientId = x.ClientId,
                                    VisitedOn = x.VisitedOn,
                                    VisitSummary = x.VisitSummary,
                                    MeetingStatus = x.MeetingStatus,
                                    VisitedCompanyUserId = x.VisitedCompanyUserId,
                                    VisitedCompanyUser = x.VisitedCompanyUser == null ? null : new() { Name = x.VisitedCompanyUser.Name },
                                    VisitedClientEmployeeId = x.VisitedClientEmployeeId,
                                    VisitedClientEmployee = x.VisitedClientEmployee == null ? null : new() { Name = x.VisitedClientEmployee.Name },
                                    VisitedClientBusinessUnitId = x.VisitedClientBusinessUnitId,
                                    Client = x.Client == null ? null : new()
                                    {
                                        Name = x.Client.Name,
                                        CIFNo = x.Client.CIFNo,
                                        ClientGroup = x.Client.ClientGroup == null ? null : new ClientGroupDTO()
                                        { GroupName = x.Client.ClientGroup.GroupName, GroupCIFNo = x.Client.ClientGroup.GroupCIFNo }
                                    },
                                    ScheduledOn = x.ScheduledOn,
                                    ScheduledEnd = x.ScheduledOn.AddMinutes(60),
                                    MeetingNo = x.MeetingNo,
                                    Agenda = x.Agenda,
                                    ClientEmployeeId = x.ClientEmployeeId,
                                    ClientEmployee = x.ClientEmployee == null ? null : new()
                                    {
                                        Name = x.ClientEmployee.Name,
                                        Mobile = x.ClientEmployee.Mobile,
                                        clientBusinessUnit = x.ClientEmployee.ClientBusinessUnit == null ? null : new ClientBusinessUnitDTO()
                                        { Id = x.ClientEmployee.ClientBusinessUnit.Id }
                                    },
                                    MeetingPurpose = x.MeetingPurpose,
                                    CompanyUserId = x.CompanyUserId,
                                    MeetingClientAttendeesIds = x.MeetingClientAttendees.Count() == 0 ?
                                    new List<Guid>() : x.MeetingClientAttendees.Select(y => y.ClientEmployeeId).ToList(),
                                    MeetingCompanyAttendeesIds = x.MeetingCompanyAttendees.Count() == 0 ?
                                    new List<Guid>() : x.MeetingCompanyAttendees.Select(y => y.CompanyUserId).ToList(),
                                    CompanyUser = x.CompanyUser == null ? null : new() { Name = x.CompanyUser.Name },

                                    SelectedMeetingClientAttendees = x.MeetingClientAttendees.Count() == 0 ?
                                    new List<MultiSelectDTO>() : x.MeetingClientAttendees.Select(y => new MultiSelectDTO()
                                    {
                                        label = y.ClientEmployee.Name,
                                        value = y.ClientEmployeeId.ToString().ToLower()
                                    }).ToList(),
                                    SelectedMeetingCompanyAttendees = x.MeetingCompanyAttendees.Count() == 0 ?
                                    new List<MultiSelectDTO>() : x.MeetingCompanyAttendees.Select(y => new MultiSelectDTO()
                                    {
                                        label = y.CompanyUser.Name,
                                        value = y.CompanyUserId.ToString().ToLower()
                                    }).ToList(),
                                }).FirstOrDefaultAsync();
            return result;
        }


        public async Task<List<MeetingDTO>> SearchMeetings(SearchMeetingDTO model)
        {
            var reportingToUserIds = _dbContext.Users.Where(x => x.ReportingToUserId == model.ReportingToUserId).Select(x => x.Id).ToList();
            var MeetingIds = _dbContext.MeetingCompanyAttendees.Where(x => x.CompanyUserId == model.MeetingCompanyAttendeesIds).Select(x => x.MeetingId).Distinct().ToList();

            var result = await (from x in _dbContext.Meetings
                .Include(x => x.CompanyUser)
                .Include(x => x.ClientBusinessUnit)
                .Include(x => x.ClientEmployee)
                .Include(x => x.Client)
                .Where(x => x.MeetingStatusId == 0 && ((model.CompanyId == null || x.CompanyId == model.CompanyId.Value)
                    && (model.CompanyUserId == null || x.CompanyUserId == model.CompanyUserId)  // OWN
                    && (model.ReportingToUserId == null || reportingToUserIds.Contains(x.CompanyUserId)) // SUBORDINATE
                     && (model.MeetingCompanyAttendeesIds == null || MeetingIds.Contains(x.Id)))) //PARTICIPANTS

                                select new MeetingDTO
                                {
                                    Id = x.Id,
                                    CompanyId = x.CompanyId,
                                    ClientId = x.ClientId,
                                    ClientBusinessUnitId = x.ClientBusinessUnitId,
                                    ClientBusinessUnit = x.ClientBusinessUnit == null ? null : new ClientBusinessUnitDTO
                                    {
                                        Name = x.ClientBusinessUnit.Name,
                                    },
                                    MeetingStatus = x.MeetingStatus,
                                    Client = x.Client == null ? null : new ClientDTO
                                    {
                                        Name = x.Client.Name
                                    },
                                    ScheduledOn = x.ScheduledOn,
                                    ScheduledEnd = x.ScheduledOn.AddMinutes(60),
                                    MeetingNo = x.MeetingNo,
                                    // MeetingStatus = x.MeetingStatus,
                                    Agenda = x.Agenda,
                                    ClientEmployeeId = x.ClientEmployeeId,
                                    ClientEmployee = x.ClientEmployee == null ? null : new ClientEmployeeDTO
                                    {
                                        Name = x.ClientEmployee.Name,
                                        Mobile = x.ClientEmployee.Mobile
                                    },
                                    MeetingPurpose = x.MeetingPurpose,
                                    CompanyUserId = x.CompanyUserId,
                                    CompanyUser = x.CompanyUser == null ? null : new UserDTO
                                    {
                                        Name = x.CompanyUser.Name
                                    }
                                }).OrderByDescending(x => x.ScheduledOn).ToListAsync();

            return result;
        }
        public List<SelectListDTO> GetuserSelectList(Guid ReportingToUserId)
        {
            var reportingToUserIds = _dbContext.Users.Where(x => x.ReportingToUserId == ReportingToUserId ||x.Id == ReportingToUserId).Select(x => x.Id).ToList();
            var result = _dbContext.Users.Include(x => x.Designation).Where(x=>x.Name == RoleEnum.CompanyUser || reportingToUserIds.Contains(x.Id) ).OrderBy(x => x.Name).Select(x => new SelectListDTO()
            {
                Value = x.Id.ToString().ToLower(),
                Text = x.Name + " ( " + x.Designation.Name + " )"
            }).ToList();
            return result;
        }
   

        public async Task<List<MeetingDTO>> GetMeetingsByStatus(string MeetingStatus)
        {
            var res = await _dbContext.Meetings.Include(x => x.Client).Include(x => x.CompanyUser)
                .Include(x => x.ClientEmployee). Where(x => x.MeetingStatus == MeetingStatus).ToListAsync();
            var result = res.Select(x => new MeetingDTO()
            {
                Id = x.Id,
                CompanyId = x.CompanyId,
                ClientId = x.ClientId,
                CancelRemark= x.CancelRemark,
                Client = x.Client == null ? null : new ClientDTO()
                {
                    Name = x.Client.Name
                },
                ScheduledOn = x.ScheduledOn,
                ScheduledEnd = x.ScheduledOn.AddMinutes(60),
                MeetingNo = x.MeetingNo,
                MeetingStatus = x.MeetingStatus,
        
                Agenda = x.Agenda,
                ClientEmployeeId = x.ClientEmployeeId,

                ClientEmployee = x.ClientEmployee == null ? null : new ClientEmployeeDTO()
                {
                    Name = x.ClientEmployee.Name,

                    Mobile = x.ClientEmployee.Mobile

                },
                MeetingStatusId = x.MeetingStatusId,
                MeetingPurpose = x.MeetingPurpose,
                CompanyUserId = x.CompanyUserId,
                CompanyUser = x.CompanyUser == null ? null : new UserDTO()
                {
                    Name = x.CompanyUser.Name
                }
            }).ToList();
            return result;
        }
        public async Task<bool> Deletemeeting(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.Meetings.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.Meetings.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }
                transaction.Commit();
            }
            return isSuccess;
        }
        public async Task<List<MeetingDTO>> GetMeetingsByClientIdOrClientBusinessUnitId(Guid? clientId, Guid? clientBusinessUnitId)
        {

            var res = await _dbContext.Meetings.Include(x => x.CompanyUser).Include(x => x.ClientBusinessUnit).Include(x => x.ClientEmployee).
                Include(x => x.Client)
                .Where(x => ((clientId == null || x.ClientId == clientId) && (clientBusinessUnitId == null || x.ClientBusinessUnitId == clientBusinessUnitId))).ToListAsync();

            var result = res.Select(x => new MeetingDTO()
            {
                Id = x.Id,
                CompanyId = x.CompanyId,
                ClientId = x.ClientId,
               
                Client = x.Client == null ? null : new ClientDTO()
                {
                    Name = x.Client.Name
                },
                ScheduledOn = x.ScheduledOn,
                ScheduledEnd = x.ScheduledOn.AddMinutes(60),
                MeetingNo = x.MeetingNo,
                // MeetingStatus = x.MeetingStatus,

                Agenda = x.Agenda,
                ClientEmployeeId = x.ClientEmployeeId,

                ClientEmployee = x.ClientEmployee == null ? null : new ClientEmployeeDTO()
                {
                    Name = x.ClientEmployee.Name,

                    Mobile = x.ClientEmployee.Mobile

                },

                MeetingPurpose = x.MeetingPurpose,
                CompanyUserId = x.CompanyUserId,
                CompanyUser = x.CompanyUser == null ? null : new UserDTO()
                {
                    Name = x.CompanyUser.Name
                }

            }).ToList();
            return result;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<Guid?> MeetingCancellation(RemarkMeetingDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.Meetings.FirstOrDefaultAsync(f => f.Id == model.MeetingId);

                if (originalEntity != null)
                {
                    originalEntity.CancelRemark = model.CancelRemark;
                    originalEntity.MeetingStatusId = (short)MeetingStatusEnum.Cancelled;
                    originalEntity.UpdatedOn = DateTime.Now;
                    originalEntity.UpdatedById = model.UpdatedById;
                    //  _dbContext.Entry().CurrentValues.SetValues(model);
                   //dbContext.Entry(originalEntity).CurrentValues.SetValues(model);
                    await _dbContext.SaveChangesAsync();
                    id = model.MeetingId;
                }
                transaction.Commit();
            }
            return id;
        }


        #endregion

    }
}
