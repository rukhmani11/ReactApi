using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
    public class UserService : IUserService
    {
        #region Properties
        private VoVDbContext _dbContext;
        private readonly ApplicationSettings _appSettings;
        IMapper _mapper;

        #endregion

        #region Constructor
        public UserService(VoVDbContext dbContext,
             IOptions<ApplicationSettings> appSettings
            , IMapper mapper
            )
        {
            this._dbContext = dbContext;
            this._appSettings = appSettings.Value;
            this._mapper = mapper;

        }
        #endregion

        #region Methods

        public bool ValidateBasicAuthHeader(string authHeader)
        {
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Basic"))
            {
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                int seperatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, seperatorIndex);
                var password = usernamePassword.Substring(seperatorIndex + 1);
                if (username == "VoVUser" && password.Equals("t4z5pzMVVf5r8RchkdFDEwW39p3graUBSRP0=", StringComparison.Ordinal))
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }
        public async Task<Tuple<bool, string>> Register(UserDTO model ,string currenUser)
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var userNameExists = this.IsUserNameExists(model.UserName, model.Id);
                if (userNameExists)
                {
                    return Tuple.Create(false, "UserName already exists.");
                }

                model.Id = Guid.NewGuid();
                var userEmailExists = this.IsEmailExists(model.Email, model.Id);
                if (userEmailExists)
                {
                    return Tuple.Create(false, "Email already exists with another account.");
                }

                var userMobile = this.IsMobileNoExists(model.Mobile, model.Id);
                if (userMobile)
                {
                    return Tuple.Create(false, "Mobile number already exists with another account.");
                }

                if (currenUser.Equals(RoleEnum.CompanyAdmin)||currenUser.Equals(RoleEnum.CompanyDomainUser))
                {


                    User entity = _mapper.Map<User>(model);
                    entity.Id = Guid.NewGuid();
                    entity.Password = AesOperation.EncryptString(model.Password);
                    entity.CreatedOn = DateTime.UtcNow;
                    entity.CreatedById = entity.Id;
                    _dbContext.Users.Add(entity);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                else if (currenUser.Equals(RoleEnum.SiteAdmin))
                {
                    
                    User entity = _mapper.Map<User>(model);
                    entity.Id = Guid.NewGuid();
                    entity.CompanyId = null;
                    entity.ReportingToUserId = null;
                    entity.Password = AesOperation.EncryptString(model.Password);
                    entity.CreatedOn = DateTime.UtcNow;
                    entity.CreatedById = entity.Id;
                    _dbContext.Users.Add(entity);
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                }
            }
            return Tuple.Create(true, "User Registration completed.");
        }

        public async Task<Tuple<bool, string, UserDTO?>> Login(LoginDTO model)
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                UserDTO? userModel = null;
                User userEntity = null;
                string lightThemeHexCode = CustomConstants.themeLightHexCode, darkThemeHexCode = CustomConstants.themeDarkHexCode;
                string decryptedUserName = string.Empty, decryptedEntityPassword = string.Empty, decryptedModelPassword = string.Empty;
                try
                {
                    decryptedUserName = AesOperation.DecryptString(model.UserName);
                    userEntity = _dbContext.Users.Where(x => x.UserName == decryptedUserName)
                        .Include(x => x.Company)
                        .Include(x => x.Role).FirstOrDefault();
                    decryptedModelPassword = AesOperation.DecryptString(model.Password);

                    if (userEntity == null)
                        return Tuple.Create(false, "Incorrect username.", userModel);

                    #region AppSetting
                    var appSetting = _dbContext.AppSettings.Include(x => x.Company).FirstOrDefault();
                    if (appSetting != null)
                    {
                        //if (appSetting.CompanyId != userEntity.CompanyId)
                        //    return Tuple.Create(false, "Cannot Login, User doesn't belongs to this company.", userModel);
                        lightThemeHexCode = appSetting.Company.ThemeLightHexCode ?? CustomConstants.themeLightHexCode;
                        darkThemeHexCode = appSetting.Company.ThemeDarkHexCode ?? CustomConstants.themeDarkHexCode;
                    }
                    #endregion

                    if (!userEntity.Active)
                        return Tuple.Create(false, "Cannot Login, Account is inactive.", userModel);

                    decryptedEntityPassword = AesOperation.DecryptString(userEntity.Password);
                }
                catch (Exception ex)
                {
                    return Tuple.Create(false, "Incorrect username/password.", userModel);
                }

                if (decryptedEntityPassword != decryptedModelPassword)
                    return Tuple.Create(false, "Incorrect password.", userModel);

                //userModel = user.ToModel();
                userModel = new UserDTO(); //comment this i we uncomment abouve line
                userModel.Password = null;
                userModel.Name = userEntity.Name;
                userModel.Id = userEntity.Id;
                userModel.RoleName = userEntity.Role?.Name;
                userModel.RoleId = userEntity.RoleId;
                userModel.Active = userEntity.Active;
                userModel.CompanyId = userEntity.CompanyId;
                userModel.ReportingToUserId = userEntity.ReportingToUserId;
                if (userEntity.Company != null)
                {
                    userModel.Company = new CompanyDTO()
                    {
                        Name = userEntity.Company?.Name ?? string.Empty,
                        ThemeLightHexCode = userEntity.Company?.ThemeLightHexCode ?? lightThemeHexCode,
                        ThemeDarkHexCode = userEntity.Company?.ThemeDarkHexCode ?? darkThemeHexCode,
                        Logo = userEntity.Company?.Logo ?? null,
                    };
                }

                userModel.UserName = userEntity.UserName;

                #region Claims

                bool generateToken = false;
                //If Token not expired then send that token
                if (string.IsNullOrEmpty(userEntity.JwtToken))
                {
                    generateToken = true;
                }
                else
                {
                    var jwt = new JwtSecurityTokenHandler().ReadJwtToken(userEntity.JwtToken);
                    string strExpiryDate = jwt.Claims.First(c => c.Type == "exp").Value;
                    if (string.IsNullOrEmpty(strExpiryDate))
                    {
                        generateToken = true;
                    }
                    else
                    {
                        long expDate = Convert.ToInt64(strExpiryDate);
                        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(expDate);
                        DateTime tokenExpiryDate = dateTimeOffset.UtcDateTime;
                        //make token before 3 hours of expiry
                        if (DateTime.UtcNow >= tokenExpiryDate.AddHours(-3))
                        {
                            generateToken = true;
                        }
                        else
                        {
                            //everything is success;
                            _dbContext.SaveChanges();
                            transaction.Commit();
                        }
                    }
                    //return Tuple.Create(true, "Success", userModel);
                }

                if (generateToken)
                {
                    // var roleIds = userEntity.UserRoles.Count > 0 ? string.Join(",", userEntity.UserRoles.Select(x => x.RoleId).ToList()) : string.Empty;
                    var authClaims = new List<Claim>
                    {
                        new Claim ("Id", userEntity.Id.ToString()),
                        new Claim (ClaimTypes.Email, userEntity.Email),
                        new Claim ("UserName", userEntity.UserName),
                        new Claim ("RoleId",userEntity.RoleId.ToString() ),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    //foreach (var userrole in _dbContext.UserRoles.Where(x => x.Id == userEntity.Id).Include(x => x.Role).ToList())
                    //{
                    authClaims.Add(new Claim(ClaimTypes.Role, userEntity.Role?.Name));
                    //}
                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JwtSecret));

                    DateTime expiresOn = DateTime.UtcNow.AddMonths(1);
                    var securityToken = new JwtSecurityToken(
                        issuer: _appSettings.JwtValidIssuer,
                        audience: _appSettings.JwtValidAudience,
                        expires: expiresOn,
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                        );

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.WriteToken(securityToken);
                    //Update Token and Validation Date
                    userEntity.JwtToken = token;
                    userEntity.JwtTokenExpiresOn = expiresOn;
                    await _dbContext.SaveChangesAsync();
                    transaction.Commit();
                }
                //set token here
                userModel.JwtToken = userEntity.JwtToken;
                userModel.JwtTokenExpiryDate = userEntity.JwtTokenExpiresOn.Value.ToString(CustomConstants.dateFormatToDisplay);
                return Tuple.Create(true, "Success", userModel);
                #endregion
            }
        }

        //public async Task<Tuple<bool, string>> ChangePassword(ChangePasswordModel model)
        //{
        //    var user = await _userManager.FindByIdAsync(model.Id);
        //    if (user == null)
        //    {
        //        return Tuple.Create(false, "User doesn't exists.");
        //    }
        //    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        //    if (result.Succeeded)
        //    {
        //        return Tuple.Create(true, "Password changed successfully for '" + user.FirstName + " " + user.LastName + "'.");
        //    }
        //    else
        //    {
        //        return Tuple.Create(false, "Password not changed: " + string.Join(", ", result.Errors.Select(x => x.Description)));
        //    }
        //}

        //public async Task<Tuple<bool, string>> ForgetPassword(ChangePasswordModel model)
        //{
        //    var user = await _userManager.FindByIdAsync(model.Id);
        //    if (user == null)
        //    {
        //        return Tuple.Create(false, "User doesn't exists.");
        //    }
        //    var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        //    if (result.Succeeded)
        //    {
        //        return Tuple.Create(true, "Password changed successfully for '" + user.FirstName + " " + user.LastName + "'.");
        //    }
        //    else
        //    {
        //        return Tuple.Create(false, "Password not changed: " + string.Join(", ", result.Errors.Select(x => x.Description)));
        //    }
        //}


        public async Task<Tuple<bool, string>> UpdateUser(UserDTO model)
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                User originalEntity = _dbContext.Users.Where(x => x.Id == model.Id).Include(x => x.Role).FirstOrDefault();
                if (originalEntity == null)
                {
                    return Tuple.Create(false, "User doesn't exists.");
                }
                var userNameExists = this.IsUserNameExists(model.UserName, model.Id);
                if (userNameExists)
                {
                    return Tuple.Create(false, "UserName already exists.");
                }

                var userEmailExists = this.IsEmailExists(model.Email, model.Id);
                if (userEmailExists)
                {
                    return Tuple.Create(false, "Email already exists with another account.");
                }

                var userPhoneExists = this.IsMobileNoExists(model.Mobile, model.Id);
                if (userPhoneExists)
                {
                    return Tuple.Create(false, "PhoneNumber already exists with another account.");
                }

                User entity = _mapper.Map<User>(model);
                if (!string.IsNullOrEmpty(model.UserName))
                    entity.UserName = model.UserName;
                else
                    entity.UserName = originalEntity.UserName;

                entity.CreatedById = originalEntity.CreatedById;
                entity.CreatedOn = originalEntity.CreatedOn;
                entity.JwtToken = null;
                entity.JwtTokenExpiresOn = null;
                entity.Password = originalEntity.Password;
                entity.UpdatedOn = DateTime.Now;
                bool tokenNeedsToRegenerate = false;
                if (model.RoleId != entity.RoleId)
                {
                    tokenNeedsToRegenerate = true;
                }
                if (tokenNeedsToRegenerate)
                {
                    entity.JwtToken = null;
                    entity.JwtTokenExpiresOn = null;
                }
                _dbContext.Entry(originalEntity).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();
            }
            return Tuple.Create(true, "User updated successfully.");
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            var data = _dbContext.Users.Where(x => x.Id == id).FirstOrDefault();
            bool isSuccess = false;
            if (data != null)
            {
                //Delete that record
                _dbContext.Users.Remove(data);

                //Commit the transaction
                await _dbContext.SaveChangesAsync();
                isSuccess = true;
            }
            transaction.Commit();
            return isSuccess;
        }

        public async Task<List<UserDTO>> GetAllUser()
        {
            var result = await (from u in _dbContext.Users.Include(x => x.Role)
                                select new UserDTO()
                                {
                                    Id = u.Id,
                                    Name = u.Name,
                                    UserName = u.UserName,
                                    CreatedOn = u.CreatedOn,
                                    Email = u.Email,
                                    Mobile = u.Mobile,
                                    Active = u.Active,
                                    RoleName = u.Role == null ? null : u.Role.Name
                                }).OrderBy(x => x.Name).ToListAsync();
            return result;
        }

        public async Task<List<UserDTO>> GetUsersByCompanyId(Guid companyId,string currentUserRoles)
        {
            List<UserDTO> result = new List<UserDTO>();
            if (currentUserRoles.Equals(RoleEnum.SiteAdmin) )
            {
                 result = await (from u in _dbContext.Users
                                .Include(x => x.Company)
                                .Include(x => x.Designation)
                                .Include(x => x.BusinessUnit)
                                .Include(x => x.Location)
                                .Include(x => x.Role)
                                    where u.Role.Name == RoleEnum.SiteAdmin || u.Role.Name == RoleEnum.CompanyAdmin

                                 select new UserDTO()
                                    {
                                        Id = u.Id,
                                        Name = u.Name,
                                        UserName = u.UserName,
                                        CreatedOn = u.CreatedOn,
                                        Email = u.Email,
                                        EmpCode = u.EmpCode,
                                        Mobile = u.Mobile,
                                        Active = u.Active,
                                        RoleId = u.RoleId,
                                        RoleName = u.Role == null ? null : u.Role.Name,
                                        CompanyId = u.CompanyId,
                                        Company = u.Company == null ? null : new CompanyDTO()
                                        {
                                            Name = u.Company.Name
                                        },
                                        DesignationId = u.DesignationId,
                                        Designation = u.Designation == null ? null : new DesignationDTO()
                                        {
                                            Name = u.Designation.Name
                                        },
                                        LocationId = u.LocationId,
                                        Location = u.Location == null ? null : new LocationDTO()
                                        {
                                            Name = u.Location.Name
                                        },
                                        BusinessUnitId = u.BusinessUnitId,
                                        BusinessUnit = u.BusinessUnit == null ? null : new BusinessUnitDTO()
                                        {
                                            Name = u.BusinessUnit.Name
                                        },
                                    }).OrderBy(x => x.Name).ToListAsync();
            }
            else if(currentUserRoles.Equals(RoleEnum.CompanyAdmin))
            {
                 result = await (from u in _dbContext.Users
                              .Include(x => x.Company)
                              .Include(x => x.Designation)
                              .Include(x => x.BusinessUnit)
                              .Include(x => x.Location)
                              .Include(x => x.Role)
                              .Include(x => x.ReportingToUser)
                                 where u.CompanyId == companyId && u.Role.Name != RoleEnum.SiteAdmin
                                 select new UserDTO()
                                    {
                                        Id = u.Id,
                                        Name = u.Name,
                                        UserName = u.UserName,
                                        ReportingToUserId= u.ReportingToUserId,
                                        CreatedOn = u.CreatedOn,
                                        Email = u.Email,
                                        EmpCode = u.EmpCode,
                                        Mobile = u.Mobile,
                                        Active = u.Active,
                                        RoleId = u.RoleId,
                                        RoleName = u.Role == null ? null : u.Role.Name,
                                        CompanyId = u.CompanyId,
                                        DesignationId = u.DesignationId,
                                        Designation = u.Designation == null ? null : new DesignationDTO()
                                        {
                                            Name = u.Designation.Name
                                        },
                                        LocationId = u.LocationId,
                                        Location = u.Location == null ? null : new LocationDTO()
                                        {
                                            Name = u.Location.Name
                                        },
                                        BusinessUnitId = u.BusinessUnitId,
                                        BusinessUnit = u.BusinessUnit == null ? null : new BusinessUnitDTO()
                                        {
                                            Name = u.BusinessUnit.Name
                                        },
                                        ReportingToUser = u.ReportingToUser == null ? null : new UserDTO()
                                        {
                                            Name = u.ReportingToUser.Name
                                        }
                                    }).OrderBy(x => x.Name).ToListAsync();
            }
            return result;
        }

        public async Task<UserDTO> GetUserById(Guid id)
        {
            // var result = await _dbContext.User.Where(x => x.Id == id).FirstOrDefaultAsync();
            var entity = await _dbContext.Users.Where(x => x.Id == id)
                .Include(x => x.Role).FirstOrDefaultAsync();
            var result = _mapper.Map<UserDTO>(entity);
            result.RoleName = entity.Role?.Name;
            return result;
        }

        public async Task<List<SelectListDTO>> GetUserSelectList()
        {
            var result = await _dbContext.Users.OrderBy(t => t.Name)
               .Select
               (x => new SelectListDTO()
               {
                   Value = x.Id.ToString().ToLower(),
                   Text = x.Name
               }).OrderBy(x => x.Text).ToListAsync();
            return result;
        }
        public List<SelectListDTO> GetUsersSelectList()
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.Users.OrderBy(t => t.Name).Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name
                }).OrderBy(x => x.Text).ToList();
                return result;
            }
        }


        //public async Task<List<SelectListDTO>> GetReportingUserSelectList(Guid? userId)
        //{
        //    var appSetting = _dbContext.AppSettings.FirstOrDefault();
        //    var result = await _dbContext.Users.Where(x => appSetting == null || x.CompanyId == appSetting.CompanyId && x.Id != userId).OrderBy(t => t.Name)
        //       .Select
        //       (x => new SelectListDTO()
        //       {
        //           Value = x.Id.ToString().ToLower(),
        //           Text = x.Name
        //       }).ToListAsync();
        //    return result;
        //}

        public async Task<List<SelectListDTO>> GetReportingUserSelectList(Guid? Id)
        {
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var result = _dbContext.Users.Include(x=>x.Designation).Where(x => x.Id != Id).OrderBy(x=>x.Name).Select(x => new SelectListDTO()
                {
                    Value = x.Id.ToString().ToLower(),
                    Text = x.Name + " ( " + x.Designation.Name + " )"
                }).ToList();
                return result;
            }
        }
        public bool IsUserNameExists(string userName, Guid? id)
        {
            bool isExists = _dbContext.Users.Count(e => e.UserName == userName && e.Id != id) > 0;
            return isExists;
        }

        public bool IsEmailExists(string email, Guid? id)
        {
            bool isExists = _dbContext.Users.Count(e => e.Email == email && e.Id != id) > 0;
            return isExists;
        }

        public bool IsMobileNoExists(string mobile, Guid? id)
        {
            bool isExists = _dbContext.Users.Count(e => e.Mobile == mobile && e.Id != id) > 0;
            return isExists;
        }

        public async Task<AppSettingDTO> GetAppSetting()
        {
            var appSetting = await _dbContext.AppSettings.Include(x => x.Company).FirstOrDefaultAsync();
            AppSettingDTO model = _mapper.Map<AppSettingDTO>(appSetting);
            if (model != null)
            {
                model.CompanyName = appSetting.Company?.Name ?? string.Empty;
                model.ThemeDarkHexCode = appSetting.Company?.ThemeDarkHexCode ?? CustomConstants.themeDarkHexCode;
                model.ThemeLightHexCode = appSetting.Company?.ThemeLightHexCode ?? CustomConstants.themeLightHexCode;
            }
            return model;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<Tuple<bool, string>> ChangePassword(ChangePasswordDTO model)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            User originalEntity = _dbContext.Users.Where(x => x.Id == model.Id).FirstOrDefault();
            if (originalEntity == null)
            {
                return Tuple.Create(false, "User not found.");
            }

            originalEntity.Password = AesOperation.EncryptString(model.Password);
            originalEntity.UpdatedOn = DateTime.Now;
            originalEntity.UpdatedById = model.UpdatedById;
            await _dbContext.SaveChangesAsync();
            transaction.Commit();
            return Tuple.Create(true, "User Password updated successfully.");
        }


        public async Task<Guid?> EditUser(UserDTO model)
        {
            Guid? id = null;
            using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
            {
                var originalEntity = await _dbContext.Users.FirstOrDefaultAsync(f => f.Id == model.Id);

                if (originalEntity != null)
                {
                    model.Password = AesOperation.EncryptString(model.Password);
                    model.CreatedById = originalEntity.CreatedById;
                    model.CreatedOn = originalEntity.CreatedOn;
                    model.JwtToken = null;
                    model.JwtTokenExpiresOn = null;
                    model.UpdatedOn = DateTime.Now;
                    _dbContext.Entry(originalEntity).CurrentValues.SetValues(model);
                    await _dbContext.SaveChangesAsync();
                    id = model.Id;
                }
                transaction.Commit();
            }
            return id;
        }


        #endregion
    }
}
