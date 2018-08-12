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
    public class FormRepository : BaseRepository, IFormRepository
    {
        public List<Form> GetList(int typeID, string keyword)
        {
            List<Form> result = null;

            using (IDbConnection conn = Connection)
            {
                string getFormDatas = "select * from Forms where FormTypeID = {0} AND Data LIKE '%{1}%'";
                string sql = string.Format(getFormDatas, typeID, keyword);

                try
                {
                    conn.Open();

                    result = conn.Query<Form>(sql).AsQueryable().ToList();
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

        public int SaveFormData(Form form)
        {
            using (var db = new KentEntities())
            {
                try
                {
                    db.Forms.Add(form);
                    return db.SaveChanges();
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

            return 0;
        }
    }
}
