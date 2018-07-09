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
    public class EmployeesRepository : BaseRepository, IEmployeesRepository
    {
        public List<Employees> GetList()
        {
            List<Employees> result = null;

            using (IDbConnection conn = Connection)
            {
                string sql = "select * from Employees";

                try
                {
                    conn.Open();

                    result = conn.Query<Employees>(sql).AsQueryable().ToList();
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

        public bool AddNewEmployees(Employees employees)
        {
            int result = 0;

            using (IDbConnection conn = Connection)
            {
                string sql = string.Format(@"INSERT INTO [dbo].[Employees]
                                               ([Email]
                                               ,[PhoneNumber]
                                               ,[RecordOrder]
                                               ,[RecordActive]
                                               ,[RecordDeleted]
                                               ,[CreatedBy]
                                               ,[Created]
                                               ,[LastUpdateBy]
                                               ,[LastUpdate]
                                               ,[Name])
                                         VALUES
                                               ('{0}'
                                               ,'{1}'
                                               ,0
                                               ,1
                                               ,0
                                               ,'{2}'
                                               ,GETDATE()
                                               ,'{3}'
                                               ,GETDATE()
                                               ,'{4}')", employees.Email, employees.PhoneNumber, employees.CreatedBy, employees.LastUpdateBy, employees.Name);

                try
                {
                    conn.Open();

                    result = conn.Execute(sql);
                    //db.Employees.Add(employees);
                    //db.SaveChangesAsync();
                    //return true;
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

            return result == 1 ? true : false;
        }
    }
}
