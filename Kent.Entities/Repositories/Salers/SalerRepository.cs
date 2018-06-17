using Kent.Entities.Model;
using Kent.Libary.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Repositories
{
    public class SalerRepository : BaseRepository, ISalerRepository
    {
        public List<Saler> GetList()
        {
            List<Saler> result = null;

            using (IDbConnection conn = Connection)
            {
                string sql = "select * from Salers";

                try
                {
                    conn.Open();

                    result = conn.Query<Saler>(sql).AsQueryable().ToList();
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
    }
}
