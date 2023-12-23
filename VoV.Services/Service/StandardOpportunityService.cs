using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class StandardOpportunityService : IStandardOpportunityService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public StandardOpportunityService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddStandardOpportunity(StandardOpportunityDTO model)
        {
            StandardOpportunity entity = new StandardOpportunity();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<StandardOpportunity>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditStandardOpportunity(StandardOpportunityDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.StandardOpportunities.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsStandardOpportunityExists(string name, Guid id)
        {
            bool isExists = _dbContext.StandardOpportunities.Count(m => m.Name == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<StandardOpportunityDTO>> GetAllStandardOpportunities()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var res = await _dbContext.StandardOpportunities.Include(x => x.BusinessSegment).ToListAsync();
                var result = res.Select(x => new StandardOpportunityDTO()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Sequence = x.Sequence,
                    Active = x.Active,
                    BusinessSegmentId = x.BusinessSegmentId,
                    BusinessSegment = x.BusinessSegment == null ? null : new BusinessSegmentDTO()
                    {
                        Name = x.BusinessSegment.Name
                    },
                }).OrderBy(x => x.Name).ToList();
                return result;
            }
    }
        

        public async Task<bool> DeleteStandardOpportunity(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.StandardOpportunities.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.StandardOpportunities.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<StandardOpportunityDTO> GetStandardOpportunityById(Guid id)
        {
            var entity = await _dbContext.StandardOpportunities.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<StandardOpportunityDTO>(entity);
        }
        public List<SelectListDTO> GetStandardOpportunitySelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.StandardOpportunities.Select(x => new SelectListDTO()
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