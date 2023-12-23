using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoV.Core.Enum;
using VoV.Core.Helpers;
using VoV.Data.Context;
using VoV.Data.DTOs;
using VoV.Data.Entities;
using VoV.Services.Interface;

namespace VoV.Services.Service
{
    public class CompanyService : ICompanyService
    {
        #region Properties
        private readonly VoVDbContext _dbContext;
        IMapper _mapper;
        private readonly IHelperService _helperService;
        #endregion

        #region Constructor
        public CompanyService(VoVDbContext dbContext,
            IMapper mapper,
            IHelperService helperService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _helperService = helperService;
        }
        #endregion

        #region Method
        public async Task<Guid> AddCompany(CompanyDTO model, IFormFileCollection httpRequestFiles)
        {
            Company entity = new Company();
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                entity = _mapper.Map<Company>(model);
                entity.Id = Guid.NewGuid();
                entity.CreatedOn = DateTime.Now;
                entity.Active = true;
                if (httpRequestFiles.Count > 0)
                {
                    var tuple = await _helperService.uploadHttpRequestFiles(entity.Id, httpRequestFiles, FileUploadEnum.CompanyLogo.ToString());
                    bool isFileUploaded = tuple.Item2;
                    if (isFileUploaded)
                    {
                        entity.Logo = tuple.Item1.Select(x => x.FileName).FirstOrDefault();
                    }
                }
                // entity.ADLoginYn = false;
                // entity.MobileIronYn = false;
                await _dbContext.Companies.AddAsync(entity);
                User SiteAdmin = new User()
                {
                    Id = Guid.NewGuid(),
                    Name = model.SiteAdminName,
                    EmpCode = model.SiteAdminEmpCode,
                    UserName = model.SiteAdminUserName,
                    RoleId = model.SiteAdminRoleId,
                    Mobile = model.SiteAdminMobile,
                    Active=true,
                    Email=model.Email,
                    Password= AesOperation.EncryptString(model.SiteAdminPassword),
                };
                await _dbContext.Users.AddAsync(SiteAdmin);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return entity.Id;
        }

        public async Task<Guid?> EditCompany(CompanyDTO model, IFormFileCollection httpRequestFiles)
        {
            Guid? id = null;

            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.Companies.FirstOrDefaultAsync(f => f.Id == model.Id);

                if (originalEntity != null)
                {
                    model.CreatedById = originalEntity.CreatedById;
                    model.CreatedOn = originalEntity.CreatedOn;
                    model.UpdatedOn = DateTime.Now;
                    if (httpRequestFiles.Count > 0)
                    {
                        var tuple = await _helperService.uploadHttpRequestFiles(model.Id, httpRequestFiles, FileUploadEnum.CompanyLogo.ToString());
                        bool isFileUploaded = tuple.Item2;
                        if (isFileUploaded)
                        {
                            model.Logo = tuple.Item1.Select(x => x.FileName).FirstOrDefault();
                        }
                    }
                    _dbContext.Entry(originalEntity).CurrentValues.SetValues(model);
                    await _dbContext.SaveChangesAsync();
                    id = model.Id;
                }
                transaction.Commit();
            }
            return id;
        }
        public bool IsCompanyExists(string name, Guid id)
        {
            bool isExists = _dbContext.Companies.Count(m => m.Name == name && m.Id != id) > 0;
            return isExists;
        }

        public async Task<List<CompanyDTO>> GetAllCompany()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var entities = await _dbContext.Companies.ToListAsync();
                return entities.Select(x => _mapper.Map<CompanyDTO>(x)).OrderBy(x => x.Name).ToList();
            }
        }

        public async Task<bool> DeleteCompany(Guid id)
        {
            bool isSuccess = false;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var data = await _dbContext.Companies.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (data != null)
                {
                    //Delete that record
                    _dbContext.Companies.Remove(data);

                    //Commit the transaction
                    await _dbContext.SaveChangesAsync();
                    isSuccess = true;
                }

                transaction.Commit();
            }
            return isSuccess;
        }

        public async Task<CompanyDTO> GetCompanyById(Guid id)
        {
            var entity = await _dbContext.Companies.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<CompanyDTO>(entity);
        }

        public async Task<Tuple<bool, string>> ActivateOrDeActivate(Guid id, Guid currentUserId)
        {
            var entity = await _dbContext.Companies.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (entity == null)
                return Tuple.Create(false, "Company not found.");

            entity.Active = !entity.Active;
            entity.UpdatedById = currentUserId;
            entity.UpdatedOn = DateTime.Now;
            _dbContext.SaveChanges();
            return Tuple.Create(true, "Successfully " + (entity.Active ? "activated" : "de-activated") + " company");
        }
        public List<SelectListDTO> GetCompanySelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.Companies.Select(x => new SelectListDTO()
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
