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
    public class ClientGroupService : IClientGroupService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public ClientGroupService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddClientGroup(ClientGroupDTO model)
        {
            ClientGroup entity = new ClientGroup();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<ClientGroup>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditClientGroup(ClientGroupDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.ClientGroups.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsClientGroupExists(string name, Guid id)
        {
            bool isExists = _dbContext.ClientGroups.Count(m => m.GroupName == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<ClientGroupDTO>> GetAllClientGroups()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.ClientGroups.ToListAsync();
                return entities.Select(x => _mapper.Map<ClientGroupDTO>(x)).OrderBy(x => x.GroupName).ToList();
            }
        }

        public async Task<bool> DeleteClientGroup(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.ClientGroups.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.ClientGroups.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<ClientGroupDTO> GetClientGroupById(Guid id)
        {
            var entity = await _dbContext.ClientGroups.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ClientGroupDTO>(entity);
        }
        public List<SelectListDTO> GetClientGroupSelectListByCompanyId(Guid companyId)
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.ClientGroups.Where(x => x.CompanyId == companyId).Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.GroupName
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }

        public async Task<List<ClientGroupDTO>> GetClientGroupByCompanyId(Guid companyId)
        {
            var res = await _dbContext.ClientGroups.Include(x => x.Company)
                .Where(x => x.CompanyId == companyId).ToListAsync();

            List<ClientGroupDTO>? result = res.Select(x => new ClientGroupDTO()
            {
                Id = x.Id,
                GroupName = x.GroupName,
                GroupCIFNo = x.GroupCIFNo,
                CompanyId = x.CompanyId,
                Active = x.Active,
             

            }).OrderBy(x => x.GroupName).ToList();
            return result;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }


        #endregion
    }
}
