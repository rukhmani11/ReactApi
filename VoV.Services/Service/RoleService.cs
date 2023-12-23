using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Core.Enum;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using VoV.Services.Interface;

namespace VoV.Services.Service
{
    public class RoleService : IRoleService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public RoleService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddRole(RoleDTO model)
        {
            Role entity = new Role();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<Role>(model);
                entity.CreatedOn = DateTime.Now;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }
        public async Task<Guid?> EditRole(RoleDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.Roles.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsRoleExists(string name, Guid id)
        {
            bool isExists = _dbContext.Roles.Count(m => m.Name == name && m.Id != id) > 0;
            return isExists;
        }
        public async Task<List<RoleDTO>> GetAllRoles()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.Roles.ToListAsync();
                return entities.Select(x => _mapper.Map<RoleDTO>(x)).OrderBy(x => x.Name).ToList();
            }
        }
        public async Task<bool> DeleteRole(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.Roles.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.Roles.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }
        public async Task<RoleDTO> GetRoleById(Guid id)
        {
            var entity = await _dbContext.Roles.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<RoleDTO>(entity);
        }


        public List<SelectListDTO> GetRoleSelectList(string currentUserRole)
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                List<SelectListDTO> result = new List<SelectListDTO>();
                if (currentUserRole.Equals(RoleEnum.SiteAdmin))
                {
                    result = _dbContext.Roles.Where(x => x.Name == RoleEnum.SiteAdmin || x.Name == RoleEnum.CompanyAdmin)
                        .Select(x => new SelectListDTO()
                        {
                            Value = x.Id.ToString().ToLower(),
                            Text = x.Name
                        }).OrderBy(x => x.Text).ToList();
                }
                else
                {
                    result = _dbContext.Roles.Where(x=>x.Name!=RoleEnum.SiteAdmin)
                        .Select(x => new SelectListDTO()
                    {
                        Value = x.Id.ToString().ToLower(),
                        Text = x.Name
                    }).OrderBy(x => x.Text).ToList();
                }
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
