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
    public class ClientBusinessUnitService : IClientBusinessUnitService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public ClientBusinessUnitService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddClientBusinessUnit(ClientBusinessUnitDTO model)
        {
            ClientBusinessUnit entity = new ClientBusinessUnit();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<ClientBusinessUnit>(model);
                entity.CreatedOn = DateTime.Now;
                //entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditClientBusinessUnit(ClientBusinessUnitDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.ClientBusinessUnits.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsClientBusinessUnitExists(string name, Guid id)
        {
            bool isExists = _dbContext.ClientBusinessUnits.Count(m => m.Name == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<ClientBusinessUnitDTO>> GetAllClientBusinessUnits()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.ClientBusinessUnits.ToListAsync();
                return entities.Select(x => _mapper.Map<ClientBusinessUnitDTO>(x)).OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<bool> DeleteClientBusinessUnit(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.ClientBusinessUnits.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.ClientBusinessUnits.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<ClientBusinessUnitDTO> GetClientBusinessUnitById(Guid id)
        {
            var entity = await _dbContext.ClientBusinessUnits.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ClientBusinessUnitDTO>(entity);
        }
        public List<SelectListDTO> GetClientBusinessUnitSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.ClientBusinessUnits.Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }
        public List<SelectListDTO> GetBusinessSubSegmentSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.ClientBusinessUnits.Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }

        public List<SelectListDTO> GetSelectListByClientId(Guid clientId)
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.ClientBusinessUnits.Where(x =>  x.ClientId == clientId).Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }

        public async Task<List<ClientBusinessUnitDTO>> GetClientBusinessUnitByclientId(Guid clientId)
        {
            var res = await _dbContext.ClientBusinessUnits.Include(x => x.Client)
                .Include(x=>x.BusinessSegment).Include(x=>x.RoUser)
                .Where(x => x.ClientId == clientId).ToListAsync();

            var result = res.Select(x => new ClientBusinessUnitDTO()
            { Id = x.Id,
                BusinessSegmentId = x.BusinessSegmentId, 
                Name = x.Name, RoUserId = x.RoUserId,
                BusinessSegment = x.BusinessSegment == null ? null : new BusinessSegmentDTO() 
                { Name = x.BusinessSegment.Name }, 
                RoUser = x.RoUser == null ? null : new UserDTO() { Name = x.RoUser.Name } }).
                OrderBy(x => x.BusinessSegmentId).OrderBy(x =>x.Name).ToList();
            return result;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }


        #endregion
    }
}
