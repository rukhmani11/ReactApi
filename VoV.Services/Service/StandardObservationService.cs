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
//    public class StandardObservationService : IStandardObservationService
//    {
//        #region Properties
//        private readonly VoVDbContext _dbContext;
//        IMapper _mapper;
//        #endregion

//        #region Constructor
//        public StandardObservationService(VoVDbContext dbContext,
//            IMapper mapper)
//        {
//            _dbContext = dbContext;
//            _mapper = mapper;
//        }
//        #endregion

//        #region Method
//        public async Task<Guid> AddStandardObservation(StandardObservationDTO model)
//        {
//            StandardObservation entity = new StandardObservation();
//            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
//            {
//                entity = _mapper.Map<StandardObservation>(model);
//                entity.CreatedOn = DateTime.Now;
//                entity.Active = true;
//                await _dbContext.AddAsync(entity);
//                await _dbContext.SaveChangesAsync();
//                transaction.Commit();
//            }
//            return entity.Id;
//        }

//        public async Task<Guid?> EditStandardObservation(StandardObservationDTO model)
//        {
//            Guid? id = null;

//            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
//            {
//                var originalEntity = await _dbContext.StandardObservations.FirstOrDefaultAsync(f => f.Id == model.Id);

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
//        public bool IsStandardObservationExists(string name, Guid id)
//        {
//            bool isExists = _dbContext.StandardObservations.Count(m => m.Name == name && m.Id != id) > 0;
//            return isExists;
//        }

//        public async Task<List<StandardObservationDTO>> GetAllStandardObservations()
//        {
//            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
//            {
//                var res = await _dbContext.StandardObservations.Include(x => x.BusinessSegment).ToListAsync();
//                var result = res.Select(x => new StandardObservationDTO()
//                {
//                    Id = x.Id,
//                    Name = x.Name,
//                    Description = x.Description,
//                    Sequence = x.Sequence,
//                    Active = x.Active,
//                    BusinessSegmentId = x.BusinessSegmentId,
//                    BusinessSegment = x.BusinessSegment == null ? null : new BusinessSegmentDTO()
//                    {
//                        Name = x.BusinessSegment.Name
//                    },
//                }).OrderBy(x =>x.BusinessSegment.Name).ThenBy(x => x.Name).ToList();
//                return result;
//            }
//        }

//        public async Task<bool> DeleteStandardObservation(Guid id)
//        {
//            bool isSuccess = false;
//            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
//            {
//                var data = await _dbContext.StandardObservations.Where(f => f.Id == id).FirstOrDefaultAsync();
//                if (data != null)
//                {
//                    //Delete that record
//                    _dbContext.StandardObservations.Remove(data);

//                    //Commit the transaction
//                    await _dbContext.SaveChangesAsync();
//                    isSuccess = true;
//                }

//                transaction.Commit();
//            }
//            return isSuccess;
//        }

//        public async Task<StandardObservationDTO> GetStandardObservationById(Guid id)
//        {
//            var entity = await _dbContext.StandardObservations.Where(x => x.Id == id).FirstOrDefaultAsync();
//            return _mapper.Map<StandardObservationDTO>(entity);
//        }
//        public List<SelectListDTO> GetStandardObservationSelectList()
//        {
//            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
//            {
//                var result = _dbContext.StandardObservations.Select(x => new SelectListDTO()
//                {
//                    Value = x.Id.ToString().ToLower(),
//                    Text = x.Name
//                }).OrderBy(x => x.Text).ToList();
//                return result;
//            }
//        }
//        public void Dispose()
//        {
//            _dbContext.Dispose();
//        }


//        #endregion
//    }
//}

