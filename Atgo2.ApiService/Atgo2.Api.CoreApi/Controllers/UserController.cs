using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Atgo2.Api.BusinessLayer;
using Atgo2.Api.Entity;

namespace Atgo2.Api.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : ApiController
    {
        private readonly IServices<UserService> _user;

        public UserController(IServices<UserService> user)
        {           
            _user = user;
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
            //_logger.Log(new LogInformation
            //{
            //    Module = CrossCuttingLayer.Logging.Constants.TokenModule,
            //    UserId = _userContextAccessor.UserId,
            //    Message = CrossCuttingLayer.Logging.Constants.MethodInvokedMessage
            //});

            var userServ = new UserService();
            var applicationUser = await userServ.ValidateUser(req, 1);

            //await _user.Service.ValidateUser(req, 1); //_userContextAccessor.UserId
            return applicationUser;
        }        
    }
}