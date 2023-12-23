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
    public class MeetingObservationAndOtherMatterService : IMeetingObservationAndOtherMatterService
    {


        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public MeetingObservationAndOtherMatterService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        #endregion

        #region Method
        public async Task<Guid> AddMeetingObservationAndOtherMatter(MeetingObservationAndOtherMatterDTO model)
        {
            MeetingObservationAndOtherMatter entity = new MeetingObservationAndOtherMatter();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<MeetingObservationAndOtherMatter>(model);
                entity.CreatedOn = DateTime.Now;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditMeetingObservationAndOtherMatter(MeetingObservationAndOtherMatterDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.MeetingObservationAndOtherMatters.FirstOrDefaultAsync(f => f.Id == model.Id);

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

        public async Task<bool> DeleteMeetingObservationAndOtherMatter(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.MeetingObservationAndOtherMatters.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.MeetingObservationAndOtherMatters.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }


        public async Task<MeetingObservationAndOtherMatterDTO> GetMeetingObservationAndOtherMatterById(Guid id)
        {
            var entity = await _dbContext.MeetingObservationAndOtherMatters.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<MeetingObservationAndOtherMatterDTO>(entity);
        }


        public async Task<List<MeetingObservationAndOtherMatterDTO>> GetMeetingObservationAndOtherMatterByMeetingId(Guid meetingId)
        {
            var res = await _dbContext.MeetingObservationAndOtherMatters
                .Include(x => x.Meeting)/*.Include(x => x.CompanyObservation)*/.Include(x => x.AssignedToUser)
                .Where(x => x.MeetingId == meetingId).ToListAsync();
            var result = res.Select(x => new MeetingObservationAndOtherMatterDTO()
            {
                Id = x.Id,
                MeetingId = x.MeetingId,
                CompanyObservation = x.CompanyObservation,
                AssignedToUserId = x.AssignedToUserId,
                IsCritical = x.IsCritical,
                Remarks = x.Remarks,
                Responsibility = x.Responsibility,
                DeadLine = x.DeadLine,
                ActionDetails = x.ActionDetails,
                ActionRequired = x.ActionRequired,
                AssignedToUser = x.ActionRequired == null ? null : new UserDTO()
                {
                    UserName = x.AssignedToUser.UserName,
                },
                //CompanyObservation = x.CompanyObservation == null ? null : new CompanyObservationDTO()
                //{
                //    Name = x.CompanyObservation.Name,
                //}
            }).ToList();

            return result;
        }


        public async Task<List<MeetingObservationAndOtherMatterDTO>> GetPendingMeetingObservationAndOtherMatterByClientIdOrClientBusinessUnitId(Guid? clientId, Guid? clientBusinessUnitId)
        {

            var res = await _dbContext.MeetingObservationAndOtherMatters.Include(x => x.Meeting)
                //.Include(x => x.CompanyObservation)
                .Where(x => ((clientId == null || x.Meeting.ClientId == clientId) &&
                (clientBusinessUnitId == null || x.Meeting.ClientBusinessUnitId == clientBusinessUnitId))).ToListAsync();


            var result = res.Select(x => new MeetingObservationAndOtherMatterDTO()
            {
                Id = x.Id,
                CompanyObservation = x.CompanyObservation,
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

        public async Task<List<MeetingObservationAndOtherMatterDTO>> GetAllMeetingObservationAndOtherMatter()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var res = await _dbContext.MeetingObservationAndOtherMatters.Include(x => x.Meeting).ThenInclude(x => x.Client)
                       .Include(x => x.Meeting).ThenInclude(x => x.VisitedClientEmployee)
                    .Include(x => x.Meeting).ThenInclude(x => x.VisitedCompanyUser)
                   /* .Include(x => x.CompanyObservation)*/.Include(x => x.AssignedToUser)
                    .Where(x => x.ObservationStatus == "P").ToListAsync();
                var result = res.Select(x => new MeetingObservationAndOtherMatterDTO()
                {
                    Id = x.Id,
                    MeetingId = x.MeetingId,
                    CompanyObservation = x.CompanyObservation,
                    AssignedToUserId = x.AssignedToUserId,
                    Remarks = x.Remarks,
                    ActionRequired = x. ActionRequired,
                    ActionDetails = x. ActionDetails,
                   // ActionDetails = x.ActionDetails,
                    IsCritical = x.IsCritical,
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
                        UserName = x.AssignedToUser.UserName,
                    },
                    //CompanyObservation = x.CompanyObservation == null ? null : new CompanyObservationDTO()
                    //{
                    //    Name = x.CompanyObservation.Name,
                    //}

                }).ToList();
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
