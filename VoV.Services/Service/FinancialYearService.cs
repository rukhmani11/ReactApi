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
    public class FinancialYearService : IFinancialYearService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public FinancialYearService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddFinancialYear(FinancialYearDTO model)
        {
            FinancialYear entity = new FinancialYear();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<FinancialYear>(model);
                entity.CreatedOn = DateTime.Now;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditFinancialYear(FinancialYearDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.FinancialYears.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsFinancialYearExists(string name, Guid id)
        {
            bool isExists = _dbContext.FinancialYears.Count(m => m.Abbr == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<FinancialYearDTO>> GetAllFinancialYears()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.FinancialYears.ToListAsync();
                return entities.Select(x => _mapper.Map<FinancialYearDTO>(x)).OrderBy(x => x.FromDate).ToList();
            }
        }

        public async Task<bool> DeleteFinancialYear(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.FinancialYears.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.FinancialYears.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<FinancialYearDTO> GetFinancialYearById(Guid id)
        {
            var entity = await _dbContext.FinancialYears.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<FinancialYearDTO>(entity);
        }
        public List<SelectListDTO> GetFinancialYearSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.FinancialYears.Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Abbr
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }
        public DateTime? GetFinancialYearFromToDate()
        {
            var latestFinancialYear = _dbContext.FinancialYears.OrderByDescending(x => x.ToDate).FirstOrDefault();
            if (latestFinancialYear == null)
                return null;
            else
                return latestFinancialYear.ToDate;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }


        #endregion
    }
}
