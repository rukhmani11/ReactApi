using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Services.Interface;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VoV.Services.Service
{
    public class BusinessSegmentService : IBusinessSegmentService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public BusinessSegmentService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddBusinessSegment(BusinessSegmentDTO model)
        {
            BusinessSegment entity = new BusinessSegment();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<BusinessSegment>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditBusinessSegment(BusinessSegmentDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.BusinessSegments.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsBusinessSegmentExists(string code, Guid id)
        {
            bool isExists = _dbContext.BusinessSegments.Count(m => m.Code == code && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<BusinessSegmentDTO>> GetAllBusinessSegments()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.BusinessSegments.ToListAsync();
                return entities.Select(x => _mapper.Map<BusinessSegmentDTO>(x)).OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<bool> DeleteBusinessSegment(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.BusinessSegments.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.BusinessSegments.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<BusinessSegmentDTO> GetBusinessSegmentById(Guid id)
        {
            var entity = await _dbContext.BusinessSegments.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<BusinessSegmentDTO>(entity);
        }
        public List<SelectListDTO> GetBusinessSegmentSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.BusinessSegments.Select(x => new SelectListDTO()
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
