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
    public class ClientAccountService : IClientAccountService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public ClientAccountService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddClientAccount(ClientAccountDTO model)
        {
            ClientAccount entity = new ClientAccount();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<ClientAccount>(model);
                entity.CreatedOn = DateTime.Now;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditClientAccount(ClientAccountDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.ClientAccounts.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsClientAccountExists(string name, Guid id)
        {
            bool isExists = _dbContext.ClientAccounts.Count(m => m.AccountNo == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<ClientAccountDTO>> GetAllClientAccount()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.ClientAccounts.ToListAsync();
                return entities.Select(x => _mapper.Map<ClientAccountDTO>(x)).OrderBy(x => x.AccountTypeId).ToList();
            }
        }

        public async Task<bool> DeleteClientAccount(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.ClientAccounts.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.ClientAccounts.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<ClientAccountDTO> GetClientAccountById(Guid id)
        {
            var entity = await _dbContext.ClientAccounts.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ClientAccountDTO>(entity);
            //solve this open git
        }
        public List<SelectListDTO> GetClientAccountSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.ClientAccounts.Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.AccountNo
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }

        public async Task<List<ClientAccountDTO>> GetClientAccountByClientId(Guid clientId)
        {
            var res = await _dbContext.ClientAccounts.Include(x=>x.AccountType).Include(x=>x.Currency)
                .Where(x => x.ClientId == clientId).ToListAsync();

            List<ClientAccountDTO>? result = res.Select(x => new ClientAccountDTO()
            {
                Id= x.Id,
                ClientId = x.ClientId,
                AccountNo = x.AccountNo,
                BalanceAsOn = x.BalanceAsOn,
                CurrencyCode = x.CurrencyCode,
                Balance = x.Balance,
                AccountTypeId = x.AccountTypeId,
                accountType = x.AccountType == null ? null : new AccountTypeDTO()
                {
                    Name = x.AccountType.Name
                },
                Currency = x.Currency == null ? null : new CurrencyDTO()
                {
                    Name = x.Currency.Name
                },

            }).OrderBy(x => x.accountType.Name).ToList();
            return result;
        }

       

       
        public void Dispose()
        {
            _dbContext.Dispose();
        }
        #endregion
    }
}
