using Microsoft.Extensions.Logging;
using Atgo2.Api.BusinessLayer.Interface;
using Atgo2.Api.DataRepository;
using Atgo2.Api.DataRepository.Repositories;
using Atgo2.Api.Entity;
using System;
using System.Threading.Tasks;

namespace Atgo2.Api.BusinessLayer
{
    public class UserService : BaseService<UserRepository, ApplicationUser>, IUserService
    {
        private readonly IDatabase<UserRepository> _user;

        /// <summary>
        /// Validate User
        /// </summary>
        /// <param name="req"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public async Task<dynamic> ValidateUser(LoginRequestViewModel req, int currentUserId)
        {
            //Logger.Log(new LogInformation
            //{
            //    Module = Constants.UserModule,
            //    UserId = currentUserId,
            //    Message = Constants.MethodInvokedMessage
            //});

            //var applicationUser = await FindByUniqueIdAsync(req.UserName, currentUserId);

            //if (applicationUser != null && string.Equals(applicationUser.Email, applicationUser.UniqueUserId, StringComparison.OrdinalIgnoreCase))
            //{
            //    if (applicationUser.IsAutomationUser)
            //    {
            //        return new { authenticated = false };
            //    }
            //    if (applicationUser.IsAutoLockEnable && applicationUser.LockoutEndDateUtc > DateTime.UtcNow)
            //    {
            //        return new { IsAutoLocked = true };
            //    }

            //    if (applicationUser.LockoutEnabled)
            //    {
            //        return new { LockoutEnabled = true };
            //    }

            //    DateTime? expires = DateTime.UtcNow.AddMinutes(30);
            //    var validatePassword = await CheckUserPassword(applicationUser.UserId, req.Password, applicationUser.UserId, applicationUser.TimeZoneId);

            //    var checkUsrLockState = await CheckIsUserLocked(applicationUser.UniqueUserId, applicationUser.UserId);

            //    if (checkUsrLockState.IsAutoLockEnable)
            //    {
            //        return new { IsAutoLocked = true };
            //    }

            //    if (checkUsrLockState.LockoutEnabled)
            //    {
            //        return new { LockoutEnabled = true };
            //    }

            //    if (applicationUser.IsActive == false)
            //    {
            //        return new { IsInActive = true };
            //    }

            //    if (validatePassword)
            //    {
            //        var isPasswordExpired = false;

            //        if (!applicationUser.IsAutomationUser)
            //        {
            //            isPasswordExpired = await CheckIfPasswordExpired(applicationUser.UniqueUserId, applicationUser.UserId);
            //        }
            //        if (isPasswordExpired)
            //        {
            //            return new { IsPasswordExpired = true, UserId = Encryption.EncryptString(applicationUser.UserId.ToString()) };
            //        }
            //        var permissions = await FindAccessForLoggedInUser(applicationUser.UserId);
            //        var roles = await FindRoleOfUser(applicationUser.UserId, applicationUser.UserId);

            //        var id = Guid.NewGuid();
            //        await InserUserLogin(id.ToString(), applicationUser.UserId, applicationUser.TimeZoneId);

            //        return new { authenticated = true, entityId = 1, token = id, tokenExpires = expires, Permission = permissions, Role = roles.AccessRights, EULA = applicationUser.IsEulaAccepted ?? false };
            //    }

            //    //if (applicationUser.IsAutomationUser && req.IsFromCoreo == false)
            //    //{
            //    //    var exception = await _exception.Repository.InsertException(0, 116, 117, "Incorrect Login", applicationUser.FirstName + " Entered Wrong Password", null, null);
            //    //}
            //}
            return new { authenticated = true };
        }

        //public async Task<ApplicationUser> FindByUniqueIdAsync(string uniqueUserId, int currentUserId)
        //{
        //    //_logger.Log(new LogInformation
        //    //{
        //    //    Module = Constants.UserModule,
        //    //    UserId = currentUserId,
        //    //    Message = Constants.MethodInvokedMessage
        //    //});

        //    try
        //    {
        //        var user = await _user.Repository.FindByUniqueIdAsync(uniqueUserId, currentUserId);
        //        return user;
        //    }
        //    catch (Exception exception)
        //    {
        //        //_logger.Log(new LogInformation
        //        //{
        //        //    Data = $"uniqueUserId: {uniqueUserId} currentUserId: {currentUserId}",
        //        //    Module = Constants.UserModule,
        //        //    UserId = currentUserId,
        //        //    Exception = exception,
        //        //    Message = Constants.ExceptionMessage
        //        //});
        //        throw;
        //    }
        //}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _user?.Dispose();
            }
        }
    }
}
