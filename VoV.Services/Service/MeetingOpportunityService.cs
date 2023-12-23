using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class MeetingOpportunityService : IMeetingOpportunityService
    {

        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public MeetingOpportunityService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #endregion

        #region Method
        public async Task<Guid> AddMeetingOpportunity(MeetingOpportunityDTO model)
        {
            MeetingOpportunity entity = new MeetingOpportunity();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<MeetingOpportunity>(model);
                entity.CreatedOn = DateTime.Now;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditMeetingOpportunity(MeetingOpportunityDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.MeetingOpportunities.FirstOrDefaultAsync(f => f.Id == model.Id);

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

        public async Task<bool> DeleteMeetingOpportunity(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.MeetingOpportunities.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.MeetingOpportunities.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<MeetingOpportunityDTO> GetMeetingOpportunityById(Guid id)
        {
            var entity = await _dbContext.MeetingOpportunities.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<MeetingOpportunityDTO>(entity);
        }

        public async Task<List<MeetingOpportunityDTO>> GetMeetingOpportunityByMeetingId(Guid meetingId)
        {
            var res = await _dbContext.MeetingOpportunities
                .Include(x => x.Meeting)
                .Include(x => x.AssignedToUser)
                .Include(x => x.CompanyOpportunity)
                .Where(x => x.MeetingId == meetingId).ToListAsync();
            var result = res.Select(x => new MeetingOpportunityDTO()
            {
                Id = x.Id,
                MeetingId = x.MeetingId,
                AssignedToUserId=x.AssignedToUserId,
                IsCritical = x.IsCritical,
               
                Remarks = x.Remarks,
                Responsibility = x.Responsibility,
                DeadLine = x.DeadLine,
               ActionDetails = x.ActionDetails,
                ActionRequired = x.ActionRequired,

                CompanyOpportunityId = x.CompanyOpportunityId,
                CompanyOpportunity = x.CompanyOpportunity == null ? null : new CompanyOpportunityDTO()
                {
                    Name=x.CompanyOpportunity.Name,
                },
                AssignedToUser=x.AssignedToUser == null ? null : new UserDTO()
                {
                    UserName=x.AssignedToUser.UserName,
                }
            }).ToList();
            return result;
        }

        //public List<spTestResult> spTest()
        //{
        //    List<spTestResult>? result = _customDbContext.spTest.FromSqlRaw("exec spTest @sequence={0}", 0).AsEnumerable().ToList();
        //    return result;
        //}


        public async Task<List<MeetingOpportunityDTO>> GetPendingMeetingOpportunityByClientIdOrClientBusinessUnitId(Guid? clientId, Guid? clientBusinessUnitId)
        {

            var res = await _dbContext.MeetingOpportunities.Include(x => x.Meeting)
                .Include(x => x.CompanyOpportunity)
                .Where(x => ((clientId == null || x.Meeting.ClientId == clientId) &&
                (clientBusinessUnitId == null || x.Meeting.ClientBusinessUnitId == clientBusinessUnitId))).ToListAsync();


            var result = res.Select(x => new MeetingOpportunityDTO()
            {
                Id = x.Id,
                
                CompanyOpportunityId = x.CompanyOpportunityId,
                CompanyOpportunity = x.CompanyOpportunity == null ? null : new CompanyOpportunityDTO()
                {
                    Name = x.CompanyOpportunity.Name,
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


            }).OrderBy(x => x.CompanyOpportunity.Name).ToList();
            return result;
        }

        public async Task<List<MeetingOpportunityDTO>> GetAllMeetingOpportunity()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var res = await _dbContext.MeetingOpportunities.Include(x => x.Meeting).ThenInclude(x => x.Client)
                       .Include(x => x.Meeting).ThenInclude(x => x.VisitedClientEmployee)
                    .Include(x => x.Meeting).ThenInclude(x => x.VisitedCompanyUser)
                    .Include(x => x.CompanyOpportunity).Include(x => x.AssignedToUser)
                    .Where(x => x.OpportunityStatus == "P").ToListAsync();
                var result = res.Select(x => new MeetingOpportunityDTO()
                {
                    Id = x.Id,
                    MeetingId = x.MeetingId,
                   
                   AssignedToUserId = x.AssignedToUserId,
                    CompanyOpportunityId = x.CompanyOpportunityId,
                    IsCritical = x.IsCritical,
                    ActionDetails = x.ActionDetails,
                    ActionRequired = x.ActionRequired,
                    Remarks = x.Remarks,
                    Responsibility = x.Responsibility,
                    DeadLine = x.DeadLine,
                    Meeting = x.Meeting == null ? null : new MeetingDTO()
                    {
                        MeetingNo = x.Meeting.MeetingNo,
                        //ScheduledOn = x.Meeting.ScheduledOn,
                        MeetingPurpose = x.Meeting.MeetingPurpose,
                        Agenda = x.Meeting.Agenda,
                        VisitedOn = x.Meeting.VisitedOn,
                        VisitSummary = x.Meeting.VisitSummary,

                        Client = x.Meeting.Client == null ? null : new ClientDTO()
                        {
                            Name = x.Meeting.Client.Name
                        },
                        

                        VisitedClientEmployee = x.Meeting.VisitedClientEmployee == null ? null : new ClientEmployeeDTO()
                        {
                            Name = x.Meeting.VisitedClientEmployee.Name
                        },

                        VisitedCompanyUser = x.Meeting.VisitedCompanyUser == null ? null : new UserDTO()
                        {
                            UserName = x.Meeting.VisitedCompanyUser.UserName
                        },

                    },

                    AssignedToUser = x.AssignedToUser == null ? null : new UserDTO()
                    {
                        UserName = x.AssignedToUser.UserName
                    },
                    CompanyOpportunity = x.CompanyOpportunity == null ? null : new CompanyOpportunityDTO()
                    {
                        Name = x.CompanyOpportunity.Name,
                    }
                }).OrderBy(x => x.Meeting.MeetingNo).ToList();
                return result;
            }
        }

        public void Dispose()
        {
            _dbContext.Dispose();

        }

        #endregion
    }
}
