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
    public class EmailQueueRepository : BaseRepository, IEmailQueueRepository
    {
        //private readonly KentEntities kentEntities;
        //public EmailQueueRepository()
        //{
        //    kentEntities = new KentEntities();
        //}

        public bool SaveEmailToQueue(EmailQueue email)
        {
            using (var db = new KentEntities())
            {
                try
                {
                    db.EmailQueues.Add(email);
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

        public EmailQueue GetEmailQueueByID(int id)
        {
            EmailQueue result = null;

            using (IDbConnection conn = Connection)
            {
                string sql = string.Format("select * from EmailQueues where ID = {0}", id);

                try
                {
                    conn.Open();

                    result = conn.Query<EmailQueue>(sql).AsQueryable().FirstOrDefault();
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

        public int CreateEmailAsync(EmailQueue newEmail)
        {
            using (var db = new KentEntities())
            {
                try
                {
                    db.EmailQueues.Add(newEmail);
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
