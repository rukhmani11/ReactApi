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
    public class CompanyRiskService : ICompanyRiskService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        private readonly VoVCustomDBContext _customDBContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public CompanyRiskService(VoVDbContext dbContext,
            VoVCustomDBContext customDBContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _customDBContext = customDBContext;
        }
        #endregion

        #region Method
        public async Task<Guid> AddCompanyRisk(CompanyRiskDTO model)
        {
            CompanyRisk entity = new CompanyRisk();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<CompanyRisk>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditCompanyRisk(CompanyRiskDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.CompanyRisks.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsCompanyRiskExists(string name, Guid id)
        {
            bool isExists = _dbContext.CompanyRisks.Count(m => m.Name == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<CompanyRiskDTO>> GetAllCompanyRisks()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.CompanyRisks.ToListAsync();
                return entities.Select(x => _mapper.Map<CompanyRiskDTO>(x)).ToList();
            }
        }

        public async Task<bool> DeleteCompanyRisk(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.CompanyRisks.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.CompanyRisks.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<CompanyRiskDTO> GetCompanyRiskById(Guid id)
        {
            var entity = await _dbContext.CompanyRisks.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<CompanyRiskDTO>(entity);
        }
        public List<SelectListDTO> GetCompanyRiskSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.CompanyRisks.Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }
        public async Task<List<CompanyRiskDTO>> GetCompanyRisksByCompanyId(Guid companyId)
        {
            var res = await _dbContext.CompanyRisks.Include(x => x.Company).Include(x => x.BusinessSegment)
                .Where(x => x.CompanyId == companyId).ToListAsync();

            var result = res.Select(x => new CompanyRiskDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Sequence = x.Sequence,
                Active = x.Active,
                businessSegment = x.BusinessSegment == null ? null : new BusinessSegmentDTO()
                {
                    Name = x.BusinessSegment.Name
                },

            }).OrderBy(x => x.Name).OrderBy(x => x.businessSegment.Name).ToList();
            return result;
        }

        public async Task<List<spGetCompanyRisksOfClientEmployeeResult>> GetCompanyRisksByClientEmployeeId(Guid clientEmployeeId)
        {
            var result = _customDBContext.spGetCompanyRisksOfClientEmployee.FromSqlRaw("exec spGetCompanyRisksOfClientEmployee @ClientEmployeeId={0}", clientEmployeeId).AsEnumerable().ToList();
            return result;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }


        #endregion
    }
}

