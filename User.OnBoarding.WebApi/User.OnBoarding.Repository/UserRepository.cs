using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using User.OnBoarding.ValueObject;

namespace User.OnBoarding.Repository
{
    public interface IUserRepository
    {
        Task<bool> SingUpInsert(UserSignUpValue value);
        Task<bool> Login(LoginValue values);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IRepositoryConfig _config;

        public UserRepository(IRepositoryConfig config)
        {
            _config = config;
        }

        public async Task<bool> UserExists(Guid userId)
        {
            var sql = "Select 1 from Student where StudentId = @StudentId";
            var conn = new SqlConnection(_config.ConnectionString);
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("StudentId", userId);
            conn.Open();
            var exists = Convert.ToBoolean(cmd.ExecuteScalar());
            conn.Close();
            return exists;
        }
        public async Task<bool> SingUpInsert(UserSignUpValue value)
        {
            if (await UserExists(value.UserId))
                return false;

            var sql = "insert Student values (@Id, @Name, @Document, @Email, @Password, @Active)";
            var conn = new SqlConnection(_config.ConnectionString);
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("Id", value.UserId);
            cmd.Parameters.AddWithValue("Name", value.UserName);
            cmd.Parameters.AddWithValue("Document", value.UserDocument);
            cmd.Parameters.AddWithValue("Email", value.Email);
            cmd.Parameters.AddWithValue("Password", value.Password);
            cmd.Parameters.AddWithValue("Active", 0);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return true;
        }

        public async Task<bool> Login(LoginValue values)
        {
            var sql = "select 1 from Student where email=@email and password=@password and Active=1";
            var conn = new SqlConnection(_config.ConnectionString);
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("email", values.Email);
            cmd.Parameters.AddWithValue("password", values.Password);
            conn.Open();
            var result = Convert.ToBoolean(cmd.ExecuteScalar());
            conn.Close();
            return result;
        }
    }
}
