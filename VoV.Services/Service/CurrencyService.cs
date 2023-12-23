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
    public class CurrencyService : ICurrencyService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public CurrencyService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<string> AddCurrency(CurrencyDTO model)
        {
            Currency entity = new Currency();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<Currency>(model);
                entity.CreatedOn = DateTime.Now;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Code;
        }

        public async Task<string?> EditCurrency(CurrencyDTO model)
        {
            string? currencyCode = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.Currencies.FirstOrDefaultAsync(f => f.Code == model.Code);

                if (originalEntity != null)
                {
                    model.CreatedById = originalEntity.CreatedById;
                    model.CreatedOn = originalEntity.CreatedOn;
                    model.UpdatedOn = DateTime.Now;
                    _dbContext.Entry(originalEntity).CurrentValues.SetValues(model);
                    await _dbContext.SaveChangesAsync();
                    currencyCode = model.Code;
                }
                transaction.Commit();
            }
            return currencyCode;
        }

        public bool IsCurrencyExists(string currencyName, string currencyCode)
        {
            bool isExists = _dbContext.Currencies.Count(m => m.Name == currencyName && m.Code != currencyCode) > 0;
            return isExists;
        }

        public async Task<List<CurrencyDTO>> GetAllCurrencys()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.Currencies.ToListAsync();
                return entities.Select(x => _mapper.Map<CurrencyDTO>(x)).OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<bool> DeleteCurrency(string currencyCode)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.Currencies.Where(f => f.Code == currencyCode).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.Currencies.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }
                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<CurrencyDTO> GetCurrencyBycurrencyCode(string currencyCode)
        {
            var entity = await _dbContext.Currencies.Where(x => x.Code == currencyCode).FirstOrDefaultAsync();
            return _mapper.Map<CurrencyDTO>(entity);
        }

        public List<SelectListDTO> GetCurrencySelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.Currencies.Select(x => new SelectListDTO()
                {
                    Value = x.Code.ToString(),
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