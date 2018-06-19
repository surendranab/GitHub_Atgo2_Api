using Atgo2.Api.BusinessLayer;
using Atgo2.Api.BusinessLayer.Services;
using Atgo2.Api.Entity.Interface;
using System.Threading.Tasks;


namespace Atgo2.Api.CoreApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Authorize
    {
        private readonly IServices<RoleService> _roleService;
        private readonly IUserContextAccessor _userContextAccessor;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleService"></param>
        /// <param name="userContextAccessor"></param>
        public Authorize(IServices<RoleService> roleService, IUserContextAccessor userContextAccessor)
        {
            _roleService = roleService;
            _userContextAccessor = userContextAccessor;
        }

        /// <summary>
        /// 
        /// </summary>
        public Authorize()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="permission"></param>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        public virtual async Task<bool> IsAuthorized(string type, string permission, int currentUserId)
        {
            //type = type == Constants.Constants.Group ? Constants.Constants.Groups : type;
            //type = type == Constants.Constants.Location ? Constants.Constants.Locations : type;
            //type = type == Constants.Constants.BedToLocation ? Constants.Constants.Beds : type;
            //type = type == Constants.Constants.Patient ? Constants.Constants.Patients : type;
            //type = type == Constants.Constants.User ? Constants.Constants.Users : type;
            //type = type == "Role" ? Constants.Constants.UserRole : type;
            //type = type == "User Access" ? Constants.Constants.UserAccess : type;
            //type = type == Constants.Constants.Pathway ? Constants.Constants.Pathway : type;
            //type = type == Constants.Constants.MilestoneBranch ? Constants.Constants.MilestoneBranch : type;
            //type = type == Constants.Constants.Milestones ? Constants.Constants.Milestones : type;
            //type = type == "CohortModel" ? "Cohorts" : type;
            //type = type == "Roster" ? "Rosters" : type;

            var roles = await _roleService.Service.IsRoleAuthorized(currentUserId, type, permission);

            if (roles.Count > 0)
            {
                return true;
            }
            await _roleService.Service.LogNotAuthorizedEvent(currentUserId, type, permission);
            return false;
        }
    }
}