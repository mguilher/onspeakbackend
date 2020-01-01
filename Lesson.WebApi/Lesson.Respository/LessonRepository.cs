using Lesson.Repository;
using Lesson.ValueObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson.Respository
{
    public interface ILessonRepository
    {
        Task<List<LessonValue>> GetLessons();
        Task<List<LessonValue>> GetLessons(int teacherId);
        Task<List<LessonValue>> GetLessons(string value);
        Task<List<TeacherValue>> GetAllTeachers();
    }

    public class LessonRepository : ILessonRepository
    {
        private readonly IRepositoryConfig _config;
        public LessonRepository(IRepositoryConfig config)
        {
            _config = config;
        }

        public async Task<List<TeacherValue>> GetAllTeachers()
        {
            var result = new List<TeacherValue>();
            var sql = "Select t.TeacherId, t.TeacherName From Teacher t ";
            var reader = PrepareDataReader(sql);
            while (reader.Read())
            {
                result.Add(new TeacherValue 
                {
                    TeacherId = Convert.ToInt32(reader["TeacherId"]),
                    TeacherName = reader["TeacherName"].ToString()
                });
            }
            reader.Close();

            return result;
        }

        public async Task<List<LessonValue>> GetLessons()
        {
            var result = new List<LessonValue>();
            var sql = @"
                        Select top 10 t.TeacherId, t.TeacherName, t.TeacherImageUrl, T.Country,
	                           l.LessonId, l.LessonTitle, l.LessonImageUrl, d.LessonVideoUrl, d.LessonDescription
                        From Teacher t inner join Lesson l
		                        on t.TeacherId = l.TeacherId
	                         inner join LessonDetail d
		                        on l.LessonId = d.LessonId

                        ";

            var reader = PrepareDataReader(sql);
            while (reader.Read())
            {
                result.Add(LessonBuild(reader));
            }
            reader.Close();

            return result;
        }

        public async Task<List<LessonValue>> GetLessons(int teacherId)
        {
            var result = new List<LessonValue>();
            var sql = @"
                        Select t.TeacherId, t.TeacherName, t.TeacherImageUrl, T.Country,
	                           l.LessonId, l.LessonTitle, l.LessonImageUrl, d.LessonVideoUrl, d.LessonDescription
                        From Teacher t inner join Lesson l
		                        on t.TeacherId = l.TeacherId
	                         inner join LessonDetail d
		                        on l.LessonId = d.LessonId
                        where t.TeacherId = @teacherId 
                        ";

            var reader = PrepareDataReader(sql, new SqlParameter("teacherId", teacherId));
            while (reader.Read())
            {
                result.Add(LessonBuild(reader));
            }
            reader.Close();

            return result;
        }

        public async Task<List<LessonValue>> GetLessons(string value)
        {
            var result = new List<LessonValue>();
            var sql = @"
                        Select t.TeacherId, t.TeacherName, t.TeacherImageUrl, T.Country,
	                           l.LessonId, l.LessonTitle, l.LessonImageUrl, d.LessonVideoUrl, d.LessonDescription
                        From Teacher t inner join Lesson l
		                        on t.TeacherId = l.TeacherId
	                         inner join LessonDetail d
		                        on l.LessonId = d.LessonId
                        where t.TeacherName like '%' + @value + '%' or  l.LessonTitle like '%' +@value+ '%' 
                        ";

            var reader = PrepareDataReader(sql, new SqlParameter("value", value));
            while (reader.Read())
            {
                result.Add(LessonBuild(reader));
            }
            reader.Close();

            return result;
        }

        private LessonValue LessonBuild(IDataReader reader)
        => new LessonValue
                {
                    LessonId = Convert.ToInt32(reader["LessonId"]),
                    LessonDescription = reader["LessonDescription"].ToString(),
                    LessonImageUrl = reader["LessonImageUrl"].ToString(),
                    LessonTitle = reader["LessonTitle"].ToString(),
                    LessonVideoUrl = reader["LessonVideoUrl"].ToString(),
                    TeacherCountry = reader["Country"].ToString(),
                    TeacherId = Convert.ToInt32(reader["TeacherId"]),
                    TeacherImageUrl = reader["TeacherImageUrl"].ToString(),
                    TeacherName = reader["TeacherName"].ToString()
                };

        private IDataReader PrepareDataReader(string sql, params IDbDataParameter[] parameters)
        {
            var con = new SqlConnection(_config.ConnectionString);
            var cmd = new SqlCommand(sql, con);
            foreach(var param in parameters)
                cmd.Parameters.Add(param);

            con.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

    }

}
