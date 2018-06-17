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
        public List<Form> GetList(int typeID)
        {
            List<Form> result = null;

            using (IDbConnection conn = Connection)
            {
                string sql = "select * from Forms where FormTypeID = " + typeID;

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
            int result = 0 ;

            using (IDbConnection conn = Connection)
            {
                string sql = "INSERT INTO [dbo].[Forms] "
                            + "([FormTypeID]"
                            + ",[Data]"
                            + ",[RecordOrder]"
                            + ",[RecordActive]"
                            + ",[RecordDeleted]"
                            + ",[CreatedBy]"
                            + ",[Created]"
                            + ",[LastUpdateBy]"
                            + ",[LastUpdate])"
                            + " VALUES ("
                            + form.FormTypeID.ToString()  + ","
                            + "'"+ form.Data.ToString() + "',"
                            + "0,"
                            + "1,"
                            + "0,"
                            + "'system','"
                            + DateTime.Now.ToString() + "',"
                            + "'',"
                            + "'')"
                            + "SELECT @@IDENTITY";

                try
                {
                    conn.Open();

                    result = conn.Query<int>(sql).First();
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
