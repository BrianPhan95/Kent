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
    public class FooterTemplateRepository : BaseRepository, IFooterTemplateRepository
    {
        public bool AddFooterTemplate(FooterTemplate headerTemplate)
        {
            using (var db = new KentEntities())
            {
                try
                {
                    db.FooterTemplates.Add(headerTemplate);
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

        public bool UpdateFooterTemplate(FooterTemplate headerTemplate)
        {

            using (var db = new KentEntities())
            {
                try
                {
                    var template = db.FooterTemplates.Where(d => d.ID == headerTemplate.ID).FirstOrDefault();
                    if (template == null)
                        return false;

                    template.Content = headerTemplate.Content;
                    template.ContentEnglish = headerTemplate.ContentEnglish;
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

        public List<FooterTemplate> GetFooterTemplates(string keyword)
        {
            List<FooterTemplate> result = new List<FooterTemplate>();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from FooterTemplates where Name LIKE '%{0}%'";
                sql = string.Format(sql, keyword);

                try
                {
                    conn.Open();

                    result = conn.Query<FooterTemplate>(sql).AsQueryable().ToList();
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

        public FooterTemplate GetFooterTemplateById(int id)
        {
            FooterTemplate result = new FooterTemplate();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from FooterTemplates where Id = {0}";
                sql = string.Format(sql, id);

                try
                {
                    conn.Open();

                    result = conn.Query<FooterTemplate>(sql).AsQueryable().FirstOrDefault();
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

        public FooterTemplate GetFooterTemplateDefault()
        {
            FooterTemplate result = new FooterTemplate();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from FooterTemplates where IsDefaultTemplate = 1";

                try
                {
                    conn.Open();

                    result = conn.Query<FooterTemplate>(sql).AsQueryable().FirstOrDefault();
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
        public bool DeleteFooterTemplate(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "DELETE FooterTemplates WHERE ID = {0}";
                sql = string.Format(sql, id);
                try
                {
                    conn.Open();

                    var result = conn.Execute(sql);
                    return result == 1 ? true : false;
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

            return false;
        }
    }
}
