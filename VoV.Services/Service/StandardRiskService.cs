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
    public class StandardRiskService : IStandardRiskService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        #endregion

        #region Constructor
        public StandardRiskService(VoVDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        #endregion

        #region Method
        public async Task<Guid> AddStandardRisk(StandardRiskDTO model)
        {
            StandardRisk entity = new StandardRisk();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<StandardRisk>(model);
                entity.CreatedOn = DateTime.Now;
                entity.Active = true;
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditStandardRisk(StandardRiskDTO model)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.StandardRisks.FirstOrDefaultAsync(f => f.Id == model.Id);

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
        public bool IsStandardRiskExists(string name, Guid id)
        {
            bool isExists = _dbContext.StandardRisks.Count(m => m.Name == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<StandardRiskDTO>> GetAllStandardRisks()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                {
                    var res = await _dbContext.StandardRisks.Include(x => x.BusinessSegment).ToListAsync();
                    var result = res.Select(x => new StandardRiskDTO()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Sequence = x.Sequence,
                        Active = x.Active,
                        BusinessSegmentId = x.BusinessSegmentId,
                        BusinessSegment = x.BusinessSegment == null ? null : new BusinessSegmentDTO()
                        {
                            Id=x.BusinessSegment.Id,
                            Name = x.BusinessSegment.Name
                        },
                    }).OrderBy(x => x.Name).ToList();
                    return result;
                }
            }
        }

                public async Task<bool> DeleteStandardRisk(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.StandardRisks.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.StandardRisks.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<StandardRiskDTO> GetStandardRiskById(Guid id)
        {
            var entity = await _dbContext.StandardRisks.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<StandardRiskDTO>(entity);
        }
        public List<SelectListDTO> GetStandardRiskSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.StandardRisks.Select(x => new SelectListDTO()
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