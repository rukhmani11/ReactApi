using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public class ClientService : IClientService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion
        #region Constructor
        public ClientService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion
        #region Method
        public async Task<Guid> AddClient(ClientDTO model)
        {
            Client entity = new Client();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<Client>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditClient(ClientDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.Clients.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsClientExists(string name, Guid id)
        {
            bool isExists = _dbContext.Clients.Count(m => m.Name == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<ClientDTO>> GetAllClient()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.Clients.ToListAsync();
                return entities.Select(x => _mapper.Map<ClientDTO>(x)).OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<bool> DeleteClient(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.Clients.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.Clients.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<ClientDTO> GetClientbyId(Guid id)
        {
            var entity = await _dbContext.Clients.Include(x=>x.ClientGroup).
                Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<ClientDTO>(entity);
        }

        public async Task<ClientDTO> GetClientbyIdCIF( string CIFNo)
        {
            var entity = await _dbContext.Clients.Include(x => x.ClientGroup).
                Where(x =>  x.CIFNo == CIFNo).FirstOrDefaultAsync();
            return _mapper.Map<ClientDTO>(entity);
        }
        public List<SelectListDTO> GetClientSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.Clients.Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }
        //public async Task<List<ClientDTO>> GetClientsByCompanyId(Guid companyId)
        //{
        //    var res = await _dbContext.Clients.Include(x => x.Company).Include(x => x.ClientGroup)
        //        .Where(x => x.CompanyId == companyId).ToListAsync();

        //    var result = res.Select(x => new ClientDTO()
    //        {
    //            Id = x.Id,
    //            Name = x.Name,
    //            CIFNo = x.CIFNo,
    //            CompanyId = x.CompanyId,
    //            Active = x.Active,
    //            ClientGroupId = x.ClientGroupId,
    //            VisitingFrequencyInMonth = x.VisitingFrequencyInMonth,
    //            ClientGroup = x.ClientGroup == null ? null : new ClientGroupDTO()
    //    {
    //        GroupName = x.ClientGroup.GroupName,
    //                GroupCIFNo = x.ClientGroup.GroupCIFNo,
    //            }
    //    //CurrencyCode = x.CurrencyCode,


    //}).ToList();
    //        return result;
    //    }
public async Task<List<ClientDTO>> GetClientsByCompanyId(Guid companyId)
        {
            var res = await _dbContext.Clients.Include(x => x.Company).Include(x => x.ClientGroup)
                .Where(x => x.CompanyId == companyId).ToListAsync();

            var result = res.Select(x => new ClientDTO()
            {
                
                Id = x.Id,
                Name = x.Name,
                CIFNo = x.CIFNo,
                CompanyId = x.CompanyId,
                Active = x.Active,
                ClientGroupId = x.ClientGroupId,
                VisitingFrequencyInMonth = x.VisitingFrequencyInMonth,
                ClientGroup = x.ClientGroup == null ? null : new ClientGroupDTO()
        {
            GroupName = x.ClientGroup.GroupName,
                    GroupCIFNo = x.ClientGroup.GroupCIFNo,
                }
        //CurrencyCode = x.CurrencyCode,


    }).OrderBy(x => x.Name).ToList();
            return result;
        }


        public async Task<ClientsTitleDTO> GetPageTitle(ClientsTitleDTO model)
        {
            ClientsTitleDTO result = new ClientsTitleDTO();
          
            if (model.ClientId != null && model.ClientId != Guid.Empty)
            {
                var client = _dbContext.Clients.Where(x => x.Id == model.ClientId).Select(x=>x.Name).FirstOrDefault();
                if (client != null)
                {
                    result.Name = client;
                }
            }
            return result;
        }
        
        public void Dispose()
        {
            _dbContext.Dispose();
        }


        #endregion

    }
}


