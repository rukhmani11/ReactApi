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
    public class MeetingCompanyAttendeesService : IMeetingCompanyAttendeesService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public MeetingCompanyAttendeesService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddMeetingCompanyAttendees(MeetingCompanyAttendeesDTO model)
        {
            MeetingCompanyAttendee entity = new MeetingCompanyAttendee();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<MeetingCompanyAttendee>(model);
                entity.CreatedOn = DateTime.Now;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }


        public async Task<List<MeetingCompanyAttendeesDTO>> GetCompanyAttendeesByMeetingId(Guid MeetingId)
        {
            var res = await _dbContext.MeetingCompanyAttendees.Include(x => x.Meeting).Include(x => x.CompanyUser).ThenInclude(x => x.Designation).Include(x => x.CompanyUser).ThenInclude(x => x.BusinessUnit)
                .Include(x => x.CompanyUser).ThenInclude(x => x.BusinessUnit)
                .Include(x => x.CompanyUser).ThenInclude(x => x.Location)
                .Where(x => x.MeetingId == MeetingId).ToListAsync();

            var result = res.Select(x => new MeetingCompanyAttendeesDTO()
            {
                Id = x.Id,
                MeetingId = x.MeetingId,
                CompanyUserId = x.CompanyUserId,
                CompanyUser = x.CompanyUser == null ? null : new UserDTO()
                {
                    Name = x.CompanyUser.Name,
                    Designation = x.CompanyUser.Designation == null ? null : new DesignationDTO()
                    {
                        Name = x.CompanyUser.Designation.Name,
                    },

                    BusinessUnit = x.CompanyUser.BusinessUnit == null ? null : new BusinessUnitDTO()
                    {
                        Name = x.CompanyUser.BusinessUnit.Name,
                    },

                      Location = x.CompanyUser.Location == null ? null : new LocationDTO()
                      {
                          Name = x.CompanyUser.Location.Name,
                      },
                }


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
