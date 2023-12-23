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
    public class CompanyOpportunityService : ICompanyOpportunityService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        private readonly VoVCustomDBContext _customDBContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public CompanyOpportunityService(VoVDbContext dbContext,
            VoVCustomDBContext customDBContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _customDBContext = customDBContext;
        }
        #endregion

        #region Method
        public async Task<Guid> AddCompanyOpportunity(CompanyOpportunityDTO model)
        {
            CompanyOpportunity entity = new CompanyOpportunity();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<CompanyOpportunity>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditCompanyOpportunity(CompanyOpportunityDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.CompanyOpportunities.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsCompanyOpportunityExists(string name, Guid id)
        {
            bool isExists = _dbContext.CompanyOpportunities.Count(m => m.Name == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<CompanyOpportunityDTO>> GetAllCompanyOpportunitys()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.CompanyOpportunities.ToListAsync();
                return entities.Select(x => _mapper.Map<CompanyOpportunityDTO>(x)).ToList();
            }
        }

        public async Task<bool> DeleteCompanyOpportunity(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.CompanyOpportunities.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.CompanyOpportunities.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<CompanyOpportunityDTO> GetCompanyOpportunityById(Guid id)
        {
            var entity = await _dbContext.CompanyOpportunities.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<CompanyOpportunityDTO>(entity);
        }
        public List<SelectListDTO> GetCompanyOpportunitySelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.CompanyOpportunities.Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }
        public async Task<List<CompanyOpportunityDTO>> GetCompanyOpportunitiesByCompanyId(Guid companyId)
        {
            var res = await _dbContext.CompanyOpportunities.Include(x => x.Company).Include(x => x.BusinessSegment)
                .Where(x => x.CompanyId == companyId).ToListAsync();

            var result = res.Select(x => new CompanyOpportunityDTO()
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

        public async Task<List<spGetCompanyOpportunitiesOfClientEmployeeResult>> GetCompanyOpportunitiesByClientEmployeeId(Guid clientEmployeeId)
        {
            var result = _customDBContext.spGetCompanyOpportunitiesOfClientEmployee.FromSqlRaw("exec spGetCompanyOpportunitiesOfClientEmployee @ClientEmployeeId={0}", clientEmployeeId).AsEnumerable().ToList();
            return result;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }


        #endregion
    }
}
