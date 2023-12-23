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
    public class BusinessUnitService : IBusinessUnitService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion
        #region Constructor
        public BusinessUnitService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddBusinessUnit(BusinessUnitDTO model)
        {
            BusinessUnit entity = new BusinessUnit();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<BusinessUnit>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditBusinessUnit(BusinessUnitDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.BusinessUnits.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsBusinessUnitExists(string name, Guid id)
        {
            bool isExists = _dbContext.BusinessUnits.Count(m => m.Name == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<BusinessUnitDTO>> GetAllBusinessUnit()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.BusinessUnits.ToListAsync();
                return entities.Select(x => _mapper.Map<BusinessUnitDTO>(x)).OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<bool> DeleteBusinessUnit(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.BusinessUnits.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.BusinessUnits.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<BusinessUnitDTO> GetBusinessUnitById(Guid id)
        {
            var entity = await _dbContext.BusinessUnits.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<BusinessUnitDTO>(entity);
        }

        public List<SelectListDTO> GetBusinessUnitSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.BusinessUnits.Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }

        public async Task<List<BusinessUnitDTO>> GetBusinessUnitByCompanyId(Guid companyId)
        {
            var res = await _dbContext.BusinessUnits.Include(x => x.Company).Include(x => x.Parent)
                .Where(x => x.CompanyId == companyId).ToListAsync();

            var result = res.Select(x => new BusinessUnitDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                CompanyId = x.CompanyId,
                Active = x.Active,
                ParentId = x.ParentId,
                Parent = x.Parent == null ? null : new BusinessUnitDTO()
                {
                    Name = x.Parent.Name
                }

            }).OrderBy(x => x.Name).ToList();
            return result;
        }

        public async Task<List<BusinessUnit>> GetParentBusinessUnit(Guid? id)
        {
            var result = await _dbContext.BusinessUnits.Where(x => x.Id != id).ToListAsync();
            return result;
        }

        public async Task<List<SelectListDTO>> GetParentBusinessUnitSelectList(Guid? id, Guid companyId)
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.BusinessUnits.Where(x => x.Id != id && x.CompanyId == companyId).Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
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
