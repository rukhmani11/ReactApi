//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Storage;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using VoV.Data.Context;
//using VoV.Data.DTOs;
//using VoV.Data.Entities;
//using VoV.Services.Interface;

//namespace VoV.Services.Service
//{
//    public class CompanyObservationService : ICompanyObservationService
//    {
//        #region Properties
//        private readonly VoVDbContext _dbContext;
//        IMapper _mapper;
//        #endregion

//        #region Constructor
//        public CompanyObservationService(VoVDbContext dbContext,
//            IMapper mapper)
//        {
//            _dbContext = dbContext;
//            _mapper = mapper;
//        }
//        #endregion

//        #region Method
//        public async Task<Guid> AddCompanyObservation(CompanyObservationDTO model)
//        {
//            CompanyObservation entity = new CompanyObservation();
//            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
//            {
//                entity = _mapper.Map<CompanyObservation>(model);
//                entity.CreatedOn = DateTime.Now;
//                entity.Active = true;
//                await _dbContext.AddAsync(entity);
//                await _dbContext.SaveChangesAsync();
//                transaction.Commit();
//            }
//            return entity.Id;
//        }

//        public async Task<Guid?> EditCompanyObservation(CompanyObservationDTO model)
//        {
//            Guid? id = null;

//            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
//            {
//                var originalEntity = await _dbContext.CompanyObservations.FirstOrDefaultAsync(f => f.Id == model.Id);

//                if (originalEntity != null)
//                {
//                    model.CreatedById = originalEntity.CreatedById;
//                    model.CreatedOn = originalEntity.CreatedOn;
//                    model.UpdatedOn = DateTime.Now;
//                    _dbContext.Entry(originalEntity).CurrentValues.SetValues(model);
//                    await _dbContext.SaveChangesAsync();
//                    id = model.Id;
//                }
//                transaction.Commit();
//            }
//            return id;
//        }
//        public bool IsCompanyObservationExists(string name, Guid id)
//        {
//            bool isExists = _dbContext.CompanyObservations.Count(m => m.Name == name && m.Id != id) > 0;
//            return isExists;
//        }

//        public async Task<List<CompanyObservationDTO>> GetAllCompanyObservations()
//        {
//            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
//            {
//                var entities = await _dbContext.CompanyObservations.ToListAsync();
//                return entities.Select(x => _mapper.Map<CompanyObservationDTO>(x)).OrderBy(x => x.Name).ToList();
//            }
//        }

//        public async Task<bool> DeleteCompanyObservation(Guid id)
//        {
//            bool isSuccess = false;
//            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
//            {
//                var data = await _dbContext.CompanyObservations.Where(f => f.Id == id).FirstOrDefaultAsync();
//                if (data != null)
//                {
//                    //Delete that record
//                    _dbContext.CompanyObservations.Remove(data);

//                    //Commit the transaction
//                    await _dbContext.SaveChangesAsync();
//                    isSuccess = true;
//                }

//                transaction.Commit();
//            }
//            return isSuccess;
//        }

//        public async Task<CompanyObservationDTO> GetCompanyObservationById(Guid id)
//        {
//            var entity = await _dbContext.CompanyObservations.Where(x => x.Id == id).FirstOrDefaultAsync();
//            return _mapper.Map<CompanyObservationDTO>(entity);
//        }
//        public List<SelectListDTO> GetCompanyObservationSelectList()
//        {
//            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
//            {
//                var result = _dbContext.CompanyObservations.Select(x => new SelectListDTO()
//                {
//                    Value = x.Id.ToString().ToLower(),
//                    Text = x.Name
//                }).OrderBy(x => x.Text).ToList();
//                return result;
//            }
//        }

//        public async Task<List<CompanyObservationDTO>> GetCompanyObservationByCompanyId(Guid companyId)
//        {
//            var res = await _dbContext.CompanyObservations.Include(x => x.Company).Include(x => x.BusinessSegment)
//                .Where(x => x.CompanyId == companyId).ToListAsync();

//            var result = res.Select(x => new CompanyObservationDTO()
//            {
//                Id = x.Id,
//                Name = x.Name,
//                Description = x.Description,
//                Sequence = x.Sequence,
//                Active = x.Active,
//                businessSegment = x.BusinessSegment == null ? null : new BusinessSegmentDTO()
//                {
//                    Name = x.BusinessSegment.Name
//                },

//            }).OrderBy(x => x.Name).OrderBy(x => x.businessSegment.Name).ToList();
//            return result;
//        }
//        public void Dispose()
//        {
//            _dbContext.Dispose();
//        }


//        #endregion
//    }
//}

