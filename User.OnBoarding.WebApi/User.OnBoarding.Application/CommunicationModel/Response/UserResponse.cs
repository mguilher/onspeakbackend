using System;
using System.Collections.Generic;
using System.Text;

namespace User.OnBoarding.Application.CommunicationModel.Response
{
    public class UserResponse : BaseResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserDocument { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public char Gender { get; set; }
        public bool Active { get; set; }
    }
}
