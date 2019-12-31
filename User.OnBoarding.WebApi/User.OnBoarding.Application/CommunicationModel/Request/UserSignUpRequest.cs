using System.Collections.Generic;
using System.Text;

namespace User.OnBoarding.Application.CommunicationModel.Request
{
    public class UserSignUpRequest
    {
        public string UserName { get; set; }
        public string UserDocument { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
