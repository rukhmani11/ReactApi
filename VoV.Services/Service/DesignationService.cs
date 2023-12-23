using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using VoV.Services.Interface;

namespace VoV.Services.Service
{
    public class DesignationService : IDesignationService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public DesignationService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddDesignation(DesignationDTO model)
        {
            Designation entity = new Designation();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<Designation>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditDesignation(DesignationDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.Designations.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsDesignationExists(string name, Guid id)
        {
            bool isExists = _dbContext.Designations.Count(m => m.Name == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<DesignationDTO>> GetAllDesignations()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.Designations.ToListAsync();
                return entities.Select(x => _mapper.Map<DesignationDTO>(x)).OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<bool> DeleteDesignation(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.Designations.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.Designations.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<DesignationDTO> GetDesignationById(Guid id)
        {
            var entity = await _dbContext.Designations.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<DesignationDTO>(entity);
        }
        public List<SelectListDTO> GetDesignationSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.Designations.Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }

        public async Task<List<DesignationDTO>> GetDesignationByCompanyId(Guid companyId)
        {
            var res = await _dbContext.Designations.Include(x => x.Company).Include(x=>x.Parent)
                .Where(x => x.CompanyId == companyId).ToListAsync();

            List<DesignationDTO>? result = res.Select(x => new DesignationDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                CompanyId = x.CompanyId,
                Active = x.Active,
                ParentId = x.ParentId,
                Parent= x.Parent==null?null:new DesignationDTO()
                {
                    Name=x.Parent.Name
                }

            }).OrderBy(x => x.Name).ToList();
            return result;
        }

        public async Task<List<Designation>> GetParentDesignations(Guid? id)
        {
            var result = await _dbContext.Designations.Where(x => x.Id != id).ToListAsync();
            return result;
        }

        public async Task<List<SelectListDTO>> GetParentDesignationsSelectList(Guid? id, Guid companyId)
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.Designations.Where(x => x.Id != id && x.CompanyId == companyId ).Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
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
