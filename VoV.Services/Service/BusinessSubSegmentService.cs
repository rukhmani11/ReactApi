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
using VoV.Data.Migrations;
using VoV.Services.Interface;

namespace VoV.Services.Service
{
    public class BusinessSubSegmentService : IBusinessSubSegmentService
    {

        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public BusinessSubSegmentService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddBusinessSubSegment(BusinessSubSegmentDTO model)
        {
            BusinessSubSegment entity = new BusinessSubSegment();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<BusinessSubSegment>(model);
                entity.CreatedOn = DateTime.Now;
              //  entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;

        }
        public List<SelectListDTO> GetBusinessSubSegmentSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.BusinessSubSegments.Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }

        public async Task<Guid?> EditBusinessSubSegment(BusinessSubSegmentDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.BusinessSubSegments.FirstOrDefaultAsync(f => f.Id == model.Id);

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

        public async Task<List<BusinessSubSegmentDTO>> GetAllBusinessSubSegment()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.BusinessSubSegments.ToListAsync();
                return entities.Select(x => _mapper.Map<BusinessSubSegmentDTO>(x)).OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<bool> DeleteBusinessSubSegment(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.BusinessSubSegments.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.BusinessSubSegments.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }
        public bool IsBusinessSubSegmentExists( string name, Guid id, Guid businessSegmentId)
        {

            bool isExists = _dbContext.BusinessSubSegments.Include(x => x.BusinessSegment).Count(m => m.Name == name && m.Id != id && m.BusinessSegmentId == businessSegmentId) > 0;
            return isExists;
        }

        public async Task<BusinessSubSegmentDTO> GetBusinessSubSegmentById(Guid id)
        {
            var entity = await _dbContext.BusinessSubSegments.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<BusinessSubSegmentDTO>(entity);
        }




        public void Dispose()
        {
            _dbContext.Dispose();
        }

       //public async Task<List<BusinessSubSegmentDTO>> GetbusinessSubSegmentBybusinessSegmentId(Guid businessSegmentId)
       // {

       //     var res = await _dbContext.BusinessSubSegments.Include(x => x.BusinessSegment)
       //         .Where(x => x.BusinessSegmentId == businessSegmentId).ToListAsync();

       //     var result = res.Select(x => new BusinessSubSegmentDTO()    
       //     {
       //         Id = x.Id,
       //         Active= x.Active,
       //         Name = x.Name

       //     }).OrderBy(x => x.Name).ToList();
       //     return result;
       // }
        public List<SelectListDTO> GetbusinessSubSegmentBybusinessSegmentId(Guid businessSegmentId)
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.BusinessSubSegments.Where(x => x.BusinessSegmentId == businessSegmentId).OrderBy(t => t.Name).Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }


        #endregion
    }
}