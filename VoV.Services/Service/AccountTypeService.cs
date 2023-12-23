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
    public class AccountTypeService : IAccountTypeService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public AccountTypeService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddAccountType(AccountTypeDTO model)
        {
            AccountType entity = new AccountType();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<AccountType>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditAccountType(AccountTypeDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.AccountTypes.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsAccountTypeExists(string name, Guid id)
        {
            bool isExists = _dbContext.AccountTypes.Count(m => m.Name == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<AccountTypeDTO>> GetAllAccountTypes()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.AccountTypes.ToListAsync();
                return entities.Select(x => _mapper.Map<AccountTypeDTO>(x)).OrderBy(x =>x.Name).ToList();
            }
        }

        public async Task<bool> DeleteAccountType(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.AccountTypes.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.AccountTypes.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<AccountTypeDTO> GetAccountTypeById(Guid id)
        {
            var entity = await _dbContext.AccountTypes.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<AccountTypeDTO>(entity);
        }
        public List<SelectListDTO> GetAccountTypeSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.AccountTypes.Select(x => new SelectListDTO()
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
