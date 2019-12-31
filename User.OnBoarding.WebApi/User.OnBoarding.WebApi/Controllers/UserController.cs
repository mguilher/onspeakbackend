using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.OnBoarding.Application.CommunicationModel.Request;
using User.OnBoarding.Application.CommunicationModel.Response;
using User.OnBoarding.Application.Notification;

namespace User.OnBoarding.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserSignUpNotification _userSignUp;
        private readonly ILoginNotification _userLogin;

        public UserController(IUserSignUpNotification userSignUp,
                             ILoginNotification userLogin)
        {
            _userSignUp = userSignUp;
            _userLogin = userLogin;
        }



        [HttpPost("SignUp")]
        public async Task<BaseResponse> SignUp([FromBody]UserSignUpRequest request)
        {
            return await _userSignUp.Notify(request);
        }

        [HttpPost("Login")]
        public async Task<BaseResponse> Login([FromBody]LoginRequest request)
        {
            return await _userLogin.Notify(request);
        }
    }
}
