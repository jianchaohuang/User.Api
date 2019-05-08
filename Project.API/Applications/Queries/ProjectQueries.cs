using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;

namespace Project.API.Applications.Queries
{
    public class ProjectQueries : IProjectQueries
    {
        private readonly string _conStr;
        public ProjectQueries(string conStr)
        {
            _conStr = conStr;
         }

        public Task<dynamic> GetProjectByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        //public async Task<dynamic> GetProjectByUserId(int userId)
        //{
        //    var sql = "select * from Projects where userId=@userId";
        //    using (var connection=new MySqlConnection(_conStr))
        //    {
        //        connection.Open();
        //        var result = await connection.QueryAsync<dynamic>(sql,new { userId});
        //        return result;
        //    }
        //}

        public Task<dynamic> GetProjectDetail(int userId, int projectId)
        {
            throw new NotImplementedException();
        }
    }
}
