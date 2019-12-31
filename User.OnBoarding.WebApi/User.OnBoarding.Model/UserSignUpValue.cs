using System;

namespace User.OnBoarding.ValueObject
{
    public class UserSignUpValue
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string UserDocument { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public DateTime SignUpDate { get; set; }
    }
}
