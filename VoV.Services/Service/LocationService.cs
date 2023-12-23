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
    public class LocationService : ILocationService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public LocationService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddLocation(LocationDTO model)
        {
            Location entity = new Location();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<Location>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditLocation(LocationDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.Locations.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsLocationExists(string name, Guid id)
        {
            bool isExists = _dbContext.Locations.Count(m => m.Name == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<LocationDTO>> GetAllLocations()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.Locations.ToListAsync();
                return entities.Select(x => _mapper.Map<LocationDTO>(x)).OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<bool> DeleteLocation(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.Locations.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.Locations.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<LocationDTO> GetLocationById(Guid id)
        {
            var entity = await _dbContext.Locations.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<LocationDTO>(entity);
        }
        public List<SelectListDTO> GetLocationSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.Locations.Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).ToList();
                return result;
            }
        }

        public async Task<List<LocationDTO>> GetLocationByCompanyId(Guid companyId)
        {
            var res = await _dbContext.Locations.Include(x => x.Company).Include(x => x.Parent)
                .Where(x => x.CompanyId == companyId).ToListAsync();

            var result = res.Select(x => new LocationDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                CompanyId = x.CompanyId,
                Active = x.Active,
                ParentId = x.ParentId,
                Parent = x.Parent == null ? null : new DesignationDTO()
                {
                    Name = x.Parent.Name
                }

            }).OrderBy(x => x.Name).ToList();
            return result;
        }
        public async Task<List<SelectListDTO>> GetParentLocationSelectList(Guid? id , Guid companyId)
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.Locations.Where(x => x.Id != id && x.CompanyId == companyId).Where(x => x.Id != id).Select(x => new SelectListDTO()
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
