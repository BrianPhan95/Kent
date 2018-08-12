using Dapper;
using Kent.Entities.Model;
using Kent.Libary.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kent.Entities.Repositories
{
    public class HeaderTemplateRepository : BaseRepository, IHeaderTemplateRepository
    {
        public bool AddHeaderTemplate(HeaderTemplate headerTemplate)
        {
            using (var db = new KentEntities())
            {
                try
                {
                    db.HeaderTemplates.Add(headerTemplate);
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

        public bool UpdateHeaderTemplate(HeaderTemplate headerTemplate)
        {

            using (var db = new KentEntities())
            {
                try
                {
                    var template = db.HeaderTemplates.Where(d => d.ID == headerTemplate.ID).FirstOrDefault();
                    if (template == null)
                        return false;

                    template.Content = headerTemplate.Content;
                    template.IsDefaultTemplate = headerTemplate.IsDefaultTemplate;
                    template.LastUpdate = DateTime.Now;

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

        public List<HeaderTemplate> GetHeaderTemplates(string keyword)
        {
            List<HeaderTemplate> result = new List<HeaderTemplate>();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from HeaderTemplates where Name LIKE '%{0}%'";
                sql = string.Format(sql, keyword);

                try
                {
                    conn.Open();

                    result = conn.Query<HeaderTemplate>(sql).AsQueryable().ToList();
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

        public HeaderTemplate GetHeaderTemplateById(int id)
        {
            HeaderTemplate result = new HeaderTemplate();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from HeaderTemplates where Id = {0}";
                sql = string.Format(sql, id);

                try
                {
                    conn.Open();

                    result = conn.Query<HeaderTemplate>(sql).AsQueryable().FirstOrDefault();
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

        public HeaderTemplate GetHeaderTemplateDefault()
        {
            HeaderTemplate result = new HeaderTemplate();
            using (IDbConnection conn = Connection)
            {
                string sql = @"select * from HeaderTemplates
                                where IsDefaultTemplate = 1";

                try
                {
                    conn.Open();

                    result = conn.Query<HeaderTemplate>(sql).AsQueryable().FirstOrDefault();
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
