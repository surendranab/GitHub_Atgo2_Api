using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Atgo2.Api.BusinessLayer;
using Atgo2.Api.Entity;
using Atgo2.Api.CrossCuttingLayer.Logging.Interfaces;
using Atgo2.Api.CrossCuttingLayer.Logging.Model;
using Atgo2.Api.Entity.Interface;
using Atgo2.Api.CoreApi;
using Atgo2.Api.CrossCuttingLayer.Caching.Interfaces;

namespace Atgo2.Api.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : ApiController
    {
        private readonly IServiceLogger _logger;
        private readonly IServices<UserService> _user;
        private readonly IUserContextAccessor _userContextAccessor;

        public UserController(IServices<UserService> user, IServiceLogger logger, Authorize authorize, ICacheService cacheService, AppSettings appsettings)
        {           
            _user = user;
            _logger = logger;
        }

        //_userContextAccessor
        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPost("login")]   
        public async Task<dynamic> Post([FromBody]LoginRequestViewModel req)
        {
            _logger.Log(new LogInformation
            {
                Module = CrossCuttingLayer.Logging.Constants.TokenModule,
                UserId = _userContextAccessor.UserId,
                Message = CrossCuttingLayer.Logging.Constants.MethodInvokedMessage
            });
            
            var applicationUser = await _user.Service.ValidateUser(req, 1);

            //await _user.Service.ValidateUser(req, 1); //_userContextAccessor.UserId
            return applicationUser;
        }        
    }
}