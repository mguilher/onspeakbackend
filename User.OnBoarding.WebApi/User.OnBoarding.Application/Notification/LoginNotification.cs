using System;
using System.Threading.Tasks;
using User.OnBoarding.Application.CommunicationModel.Request;
using User.OnBoarding.Application.CommunicationModel.Response;
using User.OnBoarding.Application.ExtensionMethod;
using User.OnBoarding.Publisher;
using User.OnBoarding.Repository;
using Microsoft.Extensions.Logging;

namespace User.OnBoarding.Application.Notification
{
    public interface ILoginNotification
    {
        Task<BaseResponse> Notify(LoginRequest request);
    }

    public class LoginNotification : ILoginNotification
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<LoginNotification> _logger;

        public LoginNotification(IUserRepository repository,ILogger<LoginNotification> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseResponse> Notify(LoginRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var valueObject = request.ToValueLogin();
                var userExists = await _repository.Login(valueObject);
                response.ResponseType = userExists ? ResponseTypeEnum.Info : ResponseTypeEnum.Error;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "UserSignUpNotification Error");
                response.ResponseType = ResponseTypeEnum.Error;
            }
            return response;
        }
    }



}
