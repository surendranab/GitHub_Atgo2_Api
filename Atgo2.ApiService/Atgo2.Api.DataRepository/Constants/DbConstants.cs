using System;
using System.Collections.Generic;
using System.Text;

namespace Atgo2.Api.DataRepository.Constants
{
    public class CommonDbOperations
    {        
        public const string Delete = "_Delete";
        public const string Insert = "_Insert";
        public const string Update = "_Update";
        public const string Get = "_Get";
        public const string DeleteAll = "_DeleteAll";

        public const string GetById = "_GetById";
        public const string GetAll = "_GetAll";
        public const string GetByName = "_GetByName";
        public const string Select = "_Select";
    }

    public class TableNames
    {
        public const string User = "User";
        public const string Role = "Role";
        public const string PermissionSet = "PermissionSet";
        public const string UserRoles = "UserRoles";
        public const string UserPermissions = "UserPermissions";
    }

    public class CoreDbProcedures
    {
        public const string GetPasswordHash = "_GetPasswordHash";
        public const string GetSecurityStamp = "_GetSecurityStamp";
        public const string GetUserByEmail = "_GetUserByEmail";
        public const string GetUserId = "_GetUserId";
        public const string GetUserByName = "_GetUserName";
        public const string SetPasswordHash = "_SetPasswordHash";
        public const string LockUserAccount = "_LockUserAccount";
        public const string UpdateAccessFailedCount = "_UpdateAccessFailedCount";
        public const string GetAccessFailedCount = "_GetAccessFailedCount";
        public const string CheckPassword = "_CheckPassword";
        public const string CheckPasswordExpired = "_GetPasswordExpiryDate";
        public const string FindByUniqueId = "_FindByUniqueId";
        public const string GetByUserAuthorization = "_GetByUserAuthorization";
    }
}
