using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using VoV.Services.Interface;

namespace VoV.Services.Service
{
    public class ClientEmployeeService : IClientEmployeeService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public ClientEmployeeService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddClientEmployee(ClientEmployeeDTO model)
        {
            ClientEmployee entity = new ClientEmployee();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<ClientEmployee>(model);
                entity.CreatedOn = DateTime.Now;
                //entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditClientEmployee(ClientEmployeeDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.ClientEmployees.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsClientEmployeeExists(string name, Guid id)
        {
            bool isExists = _dbContext.ClientEmployees.Count(m => m.Name == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<ClientEmployeeDTO>> GetAllClientEmployee()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.ClientEmployees.ToListAsync();
                return entities.Select(x => _mapper.Map<ClientEmployeeDTO>(x)).OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<bool> DeleteClientEmployee(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.ClientEmployees.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.ClientEmployees.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<ClientEmployeeDTO> GetClientEmployeeById(Guid id)
        {
            var entity = await _dbContext.ClientEmployees.Where(x => x.Id == id).Include(t => t.ClientBusinessUnit).FirstOrDefaultAsync();
            return _mapper.Map<ClientEmployeeDTO>(entity);
        }
        public List<SelectListDTO> GetClientEmployeeSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.ClientEmployees.OrderBy(t => t.Name).Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }

        public List<SelectListDTO> GetSelectClientEmployeeByclientId(Guid clientId)
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.ClientEmployees.Where(x => x.ClientId == clientId).OrderBy(t => t.Name).Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }
        public async Task<List<ClientEmployeeDTO>> GetClientEmployeeByclientId(Guid clientId)
        {
            var res = await _dbContext.ClientEmployees.Include(x => x.Client).Include(x => x.ClientBusinessUnit)
                .Where(x => x.ClientId == clientId).ToListAsync();

            var result = res.Select(x => new ClientEmployeeDTO()
            {
                Id=x.Id,
                CreatedById=x.CreatedById,
                ClientId=x.ClientId,
                ClientBusinessUnitId=x.ClientBusinessUnitId,
                Name=x.Name,
                Mobile = x.Mobile,
                Email = x.Email,
                Department = x.Department,
                Designation = x.Designation,
                Location = x.Location,
                 clientBusinessUnit = x.ClientBusinessUnit == null ? null : new ClientBusinessUnitDTO()
                {
                    Name = x.ClientBusinessUnit.Name
                },

            }).OrderBy(x => x.Name).ToList();
            return result;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }


        #endregion
    }
}