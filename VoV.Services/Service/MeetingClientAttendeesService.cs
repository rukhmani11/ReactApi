using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using VoV.Services.Interface;

namespace VoV.Services.Service
{
    public class MeetingClientAttendeesService : IMeetingClientAttendeesService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion
        #region Constructor
        public MeetingClientAttendeesService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion
        #region Method
        public async Task<Guid> AddMeetingClientAttendees(MeetingClientAttendeesDTO model)
        {
            MeetingClientAttendee entity = new MeetingClientAttendee();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<MeetingClientAttendee>(model);
                entity.CreatedOn = DateTime.Now;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<List<MeetingClientAttendeesDTO>> GetClientAttendeesByMeetingId(Guid MeetingId)
        {
            var res = await _dbContext.MeetingClientAttendees
                .Include(x => x.Meeting)
                .Include(x => x.ClientEmployee)
                .ThenInclude(x => x.ClientBusinessUnit)
                .Where(x => x.MeetingId == MeetingId).ToListAsync();

            var result = res.Select(x => new MeetingClientAttendeesDTO()
            {
                Id = x.Id,
                MeetingId = x.MeetingId,
                ClientEmployeeId = x.ClientEmployeeId,

                ClientEmployee = x.ClientEmployee == null ? null : new ClientEmployeeDTO()
                {
                    Name = x.ClientEmployee.Name,
                    ClientBusinessUnitId = x.ClientEmployee.ClientBusinessUnitId,
                    Designation = x.ClientEmployee.Designation,
                    Department = x.ClientEmployee.Department,
                    Location = x.ClientEmployee.Location,
                    
                    clientBusinessUnit = x.ClientEmployee.ClientBusinessUnit == null ? null : new   ()
                    {
                        Name = x.ClientEmployee.ClientBusinessUnit.Name,
                    }
                },

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
