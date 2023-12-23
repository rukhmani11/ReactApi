using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using VoV.Services.Interface;

namespace VoV.Services.Service
{
    public class MeetingRiskService : IMeetingRiskService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        private readonly VoVCustomDBContext _customDbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public MeetingRiskService(VoVDbContext dbContext,
            VoVCustomDBContext customDbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _customDbContext = customDbContext;
        }
        #endregion

        #region Method
        public async Task<Guid> AddMeetingRisk(MeetingRiskDTO model)
        {
            MeetingRisk entity = new MeetingRisk();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<MeetingRisk>(model);
                entity.CreatedOn = DateTime.Now;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<List<MeetingRiskDTO>> GetAllMeetingRisk()
        {

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var res = await _dbContext.MeetingRisks.Include(x => x.Meeting).ThenInclude(x => x.Client)
                      .Include(x => x.Meeting).ThenInclude(x => x.VisitedClientEmployee)
                    .Include(x => x.Meeting).ThenInclude(x => x.VisitedCompanyUser)
                    .Include(x => x.CompanyRisk).Include(x => x.AssignedToUser)
                    .Where(x=>x.RiskStatus=="P" ).ToListAsync();
                var result = res.Select(x => new MeetingRiskDTO()
                {
                    Id=x.Id,
                    MeetingId = x.MeetingId,
                    CompanyRiskId = x.CompanyRiskId,
                    AssignedToUserId = x.AssignedToUserId,
                    IsCritical = x.IsCritical,
                    Remarks = x.Remarks,
                    Responsibility = x.Responsibility,
                    ActionRequired = x.ActionRequired,
                    ActionDetails = x. ActionDetails,
                    DeadLine = x.DeadLine,
                    Meeting = x.Meeting == null ? null : new MeetingDTO()
                    {
                        MeetingNo = x.Meeting.MeetingNo,
                        //ScheduledOn = x.Meeting.ScheduledOn,
                        MeetingPurpose = x.Meeting.MeetingPurpose,
                        Agenda = x.Meeting.Agenda,
                        Client = x.Meeting.Client == null ? null : new ClientDTO()
                        {
                            Name = x.Meeting.Client.Name
                        },
                        VisitedOn = x.Meeting.VisitedOn,
                        VisitSummary = x.Meeting.VisitSummary,

                        VisitedClientEmployee = x.Meeting.VisitedClientEmployee == null ? null : new ClientEmployeeDTO()
                        {
                           Name = x.Meeting.VisitedClientEmployee.Name
                        },
                      
                        VisitedCompanyUser = x.Meeting.VisitedCompanyUser == null ? null : new UserDTO()
                        {
                            UserName = x.Meeting.VisitedCompanyUser.UserName
                        },

                    },
                    CompanyRisk = x.CompanyRisk == null ? null : new CompanyRiskDTO()
                    {
                        Name = x.CompanyRisk.Name,
                    },

                    //AssignedToUser = x.AssignedToUser == null ? null : new UserDTO()
                    //{
                    //    UserName = x.AssignedToUser.UserName,
                    //},
                    AssignedToUser = x.AssignedToUser == null ? null : new UserDTO()
                    {
                        UserName = x. AssignedToUser.UserName,
                    }
                }).OrderBy(x=>x.Meeting.MeetingNo).ToList();
                    return result;
            }
        }

        //public async Task<List<MeetingRiskDTO>> GetCompanyRisk()
        //{

        //    using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
        //    {
        //        var res = await _dbContext.MeetingRisks.Include(x => x.Meeting).ThenInclude(x => x.MeetingNo).Include(x => x.CompanyRisk)
        //            .Where(x => x.Meeting?.MeetingNo).ToListAsync();
        //        var result = res.Select(x => new MeetingRiskDTO()
        //        {

        //            CompanyRiskId = x.CompanyRiskId,

        //            Meeting = x.Meeting == null ? null : new MeetingDTO()
        //            {
        //                MeetingNo = x.Meeting.MeetingNo,

        //            },
        //            CompanyRisk = x.CompanyRisk == null ? null : new CompanyRiskDTO()
        //            {
        //                Name = x.CompanyRisk.Name,
        //            },

        //        }).ToList();
        //        return result;
        //    }
        //}

        public async Task<Guid?> EditMeetingRisk(MeetingRiskDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.MeetingRisks.FirstOrDefaultAsync(f => f.Id == model.Id);

                if (originalEntity != null)
                {
                    model.CreatedById = originalEntity.CreatedById;
                    model.CreatedOn = originalEntity.CreatedOn;
                    model.UpdatedOn = DateTime.Now;
                    _dbContext.Entry(originalEntity).CurrentValues.SetValues(model);
                    await _dbContext.SaveChangesAsync();
                    id = model.Id;
                }
                transaction.Commit();
            }
            return id;
        }

        public async Task<bool> DeleteMeetingRisk(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.MeetingRisks.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.MeetingRisks.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<MeetingRiskDTO> GetMeetingRiskById(Guid id)
        {
            var entity = await _dbContext.MeetingRisks.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<MeetingRiskDTO>(entity);
        }

        //public List<SelectListDTO> GetMeetingRiskSelectList()
        //{
        //    using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
        //    {
        //        var result = _dbContext.MeetingRisks.Select(x => new SelectListDTO()
        //        {
        //            Value = x.Id.ToString().ToLower(),
        //            Text = x.Name
        //        }).ToList();
        //        return result;
        //    }
        //}

        //public async Task<List<MeetingRiskDTO>> GetMeetingRisksByMeetingId(Guid meetingId)
        //{
        //    var res = await _dbContext.MeetingRisks.Include(x => x.Meeting)
        //        .Where(x => x.MeetingId == meetingId).ToListAsync();

        //    var result = res.Select(x => _mapper.Map<MeetingRiskDTO>(x)).ToList();
        //    return result;
        //}


        public async Task<List<MeetingRiskDTO>> GetMeetingRisksByMeetingId(Guid meetingId)
        {
            var res = await _dbContext.MeetingRisks
                .Include(x => x.Meeting)
                .Include(x => x.CompanyRisk)
                .Include(x => x.AssignedToUser)
                .Where(x => x.MeetingId == meetingId).ToListAsync();

            var result = res.Select(x => new MeetingRiskDTO()
            {
                Id = x.Id,
                MeetingId = x.MeetingId,
                IsCritical = x.IsCritical,
                Remarks = x.Remarks,
                Responsibility = x.Responsibility,
                DeadLine = x.DeadLine,
                ActionDetails = x.ActionDetails,
                ActionRequired = x.ActionRequired,

                CompanyRiskId = x.CompanyRiskId,
                CompanyRisk = x.CompanyRisk == null ? null : new CompanyRiskDTO()
                {
                    Name = x.CompanyRisk.Name,
                },

                AssignedToUserId = x.AssignedToUserId,

                AssignedToUser = x.AssignedToUser == null ? null : new UserDTO()
                {
                    UserName = x.AssignedToUser.UserName
                },

            }).ToList();
            return result;
        }

        public List<spTestResult> spTest()
        {
            List<spTestResult>? result = _customDbContext.spTest.FromSqlRaw("exec spTest @sequence={0}", 0).AsEnumerable().ToList();
            return result;
        }


        public async Task<List<MeetingRiskDTO>> GetPendingMeetingRisksByClientIdOrClientBusinessUnitId(Guid? clientId, Guid? clientBusinessUnitId)
        {

            var res = await _dbContext.MeetingRisks.Include(x => x.Meeting)
                .Include(x => x.CompanyRisk)
                .Where(x => ((clientId == null || x.Meeting.ClientId == clientId) &&
                (clientBusinessUnitId == null || x.Meeting.ClientBusinessUnitId == clientBusinessUnitId))).ToListAsync();


            var result = res.Select(x => new MeetingRiskDTO()
            {
                Id = x.Id,
                CompanyRiskId = x.CompanyRiskId,
                CompanyRisk = x.CompanyRisk == null ? null : new CompanyRiskDTO()
                {
                    Name = x.CompanyRisk.Name,
                },
                Meeting = x.Meeting == null ? null : new MeetingDTO()
                {
                    MeetingNo = x.Meeting.MeetingNo,
                    ScheduledOn = x.Meeting.ScheduledOn,
                    MeetingPurpose = x.Meeting.MeetingPurpose,
                },
                IsCritical = x.IsCritical,
                Remarks = x.Remarks,
                AssignedToUserId = x.AssignedToUserId,
                Responsibility = x.Responsibility,
                DeadLine = x.DeadLine,
                DateOfClosing = x.DateOfClosing


            }).ToList();
            return result;
        }



        public void Dispose()
        {
            _dbContext.Dispose();
        }


        #endregion
    }
}
