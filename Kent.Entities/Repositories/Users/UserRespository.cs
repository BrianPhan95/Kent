using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kent.Entities.Model;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using Kent.Libary.Logger;

namespace Kent.Entities.Repositories
{
    public class UserRespository : BaseRepository, IUserRespository
    {
        public User GetUserDetails(string email, string password)
        {
            User result = null;

            using (IDbConnection conn = Connection)
            {
                string getUserStr = @"SELECT * FROM Users
                                        WHERE Email = '{0}'
                                        AND Password = '{1}'";
                string sql = string.Format(getUserStr, email, password);

                try
                {
                    conn.Open();

                    result = conn.Query<User>(sql).AsQueryable().FirstOrDefault();
                }
                catch (SqlException sqlEx)
                {
                    Logger.ErrorException(sqlEx);
                }
                catch (Exception ex)
                {
                    Logger.ErrorException(ex);
                }
                finally
                {
                    if (conn != null && conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }

            return result;
        }

        public bool UpdateLastLoginTime(int userId)
        {
            using (var db = new KentEntities())
            {
                try
                {
                    var user = db.Users.Where(d => d.UserTypeId == userId).FirstOrDefault();
                    user.LastLogin = DateTime.Now;

                    return db.SaveChanges() > 0 ? true : false;
                }
                catch (SqlException sqlEx)
                {
                    Logger.ErrorException(sqlEx);
                }
                catch (Exception ex)
                {
                    Logger.ErrorException(ex);
                }
            }

            return false;
        }
    }
}
