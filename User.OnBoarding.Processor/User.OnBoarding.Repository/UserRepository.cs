using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace User.OnBoarding.Repository
{
    public interface IUserRepository
    {
        void UpdateUserStatus(Guid userId, bool enableAccount);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IRepositoryConfiguration _config;

        public UserRepository(IRepositoryConfiguration config)
        {
            _config = config;
        }

        public void UpdateUserStatus(Guid userId, bool enableAccount)
        {

            var sql = "Update Student Set Active = @Active where StudentId = @Id";
            var conn = new SqlConnection(_config.ConnectionString);
            var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("Id", userId);
            cmd.Parameters.AddWithValue("Active", enableAccount);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

    }
}
