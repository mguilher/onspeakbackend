using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using User.OnBoarding.Application.CommunicationModel.Request;
using User.OnBoarding.Application.CommunicationModel.Response;
using User.OnBoarding.Application.ExtensionMethod;
using User.OnBoarding.Publisher;
using User.OnBoarding.Repository;
using User.OnBoarding.ValueObject;
using Microsoft.Extensions.Logging;

namespace User.OnBoarding.Application.Notification
{
    public interface IUserSignUpNotification
    {
        Task<BaseResponse> Notify(UserSignUpRequest request);
    }

    public class UserSignUpNotification : IUserSignUpNotification
    {
        private readonly IUserRepository _repository;
        private readonly IAmazonPublisher _publisher;
        private readonly ILogger<UserSignUpNotification> _logger;

        public UserSignUpNotification(IUserRepository repository, IAmazonPublisher publisher, ILogger<UserSignUpNotification> logger)
        {
            _repository = repository;
            _publisher = publisher;
            _logger = logger;
        }

        public async Task<BaseResponse> Notify(UserSignUpRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var valueObject = request.ToUserValueSignUp();
                var signUpDoesNotExist = await _repository.SingUpInsert(valueObject);
                if (signUpDoesNotExist)
                    await _publisher.Execute(valueObject);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "UserSignUpNotification Error");
                response.ResponseType = ResponseTypeEnum.Error;
            }
            return response;
        }
    }



}
