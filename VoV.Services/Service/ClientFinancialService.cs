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
    public class ClientFinancialService : IClientFinancialService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public ClientFinancialService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddClientFinancial(ClientFinancialDTO model)
        {
            ClientFinancial entity = new ClientFinancial();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<ClientFinancial>(model);
                entity.CreatedOn = DateTime.Now;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditClientFinancial(ClientFinancialDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.ClientFinancials.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsClientFinancialExists(Guid Clientid, Guid FinancialYearId, Guid id)
        {
            bool isExists = _dbContext.ClientFinancials.Count(m => m.FinancialYearId == FinancialYearId && m.Id != id && m.ClientId != Clientid) > 0;
            return isExists;
        }

        public async Task<List<ClientFinancialDTO>> GetAllClientFinancials()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.ClientFinancials.ToListAsync();
                return entities.Select(x => _mapper.Map<ClientFinancialDTO>(x)).ToList();
            }
        }

        public async Task<bool> DeleteClientFinancial(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.ClientFinancials.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.ClientFinancials.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<ClientFinancialDTO> GetClientFinancialById(Guid id)
        {
            var entity = await _dbContext.ClientFinancials.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ClientFinancialDTO>(entity);
        }
        public List<SelectListDTO> GetClientFinancialSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.ClientFinancials.Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.CurrencyCode
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }

        public async Task<List<ClientFinancialDTO>> GetClientFinancialByclientId(Guid clientId)
        {
            var res = await _dbContext.ClientFinancials.Include(x => x.Client)
                .Include(x=>x.Currency)
                .Include(x=>x.FinancialYear)
                .Where(x => x.ClientId == clientId).ToListAsync();

            var result = res.Select(x => new ClientFinancialDTO()
            {
                Id=x.Id,
                FinancialYearId=x.FinancialYearId,
                CurrencyCode = x.CurrencyCode,
                Turnover = x.Turnover,
                Profit = x.Profit,
                Currency=x.Currency==null ? null : new CurrencyDTO()
                {
                    Name= x.Currency.Name
                },
                FinancialYear=x.FinancialYear==null ? null : new FinancialYearDTO()
                {
                    Abbr=x.FinancialYear.Abbr
                },
            }).OrderBy(x => x.CurrencyCode).ThenBy(x =>x. Currency.Name).ToList();
            return result;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }


        #endregion
    }
}
