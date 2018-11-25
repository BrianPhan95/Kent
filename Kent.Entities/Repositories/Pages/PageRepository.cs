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
    public class PageRepository : BaseRepository, IPageRepository
    {
        public bool AddPage(Page page)
        {
            using (var db = new KentEntities())
            {
                try
                {
                    db.Pages.Add(page);
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

        public Page GetPageById(int id)
        {
            Page result = new Page();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from Pages where Id = {0}";
                sql = string.Format(sql, id);

                try
                {
                    conn.Open();

                    result = conn.Query<Page>(sql).AsQueryable().FirstOrDefault();
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

        public List<Page> GetPages(string keyword)
        {
            List<Page> result = new List<Page>();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from Pages where Title LIKE '%{0}%'";
                sql = string.Format(sql, keyword);

                try
                {
                    conn.Open();

                    result = conn.Query<Page>(sql).AsQueryable().ToList();
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

        public Page GetPageByFriendlyUrl(string url)
        {
            Page result = new Page();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from Pages where FriendlyUrl = '{0}'";
                if (string.IsNullOrEmpty(url))
                {
                    sql = "select * from Pages where IsHomePage = 1";
                }
                else
                {
                    sql = string.Format(sql, url);
                }

                try
                {
                    conn.Open();

                    result = conn.Query<Page>(sql).AsQueryable().FirstOrDefault();
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

        public bool UpdatePage(Page page)
        {

            using (var db = new KentEntities())
            {
                try
                {
                    var pageData = db.Pages.Where(d => d.ID == page.ID).FirstOrDefault();
                    if (pageData == null)
                        return false;


                    pageData.Title = page.Title;
                    pageData.FriendlyUrl = page.FriendlyUrl;
                    pageData.Status = page.Status;
                    pageData.Content = page.Content;
                    pageData.IsHomePage = page.IsHomePage;

                    pageData.TitleEnglish = page.TitleEnglish;
                    pageData.FriendlyUrlEnglish = page.FriendlyUrlEnglish;
                    pageData.ContentEnglish = page.ContentEnglish;

                    pageData.FooterTemplateId = page.FooterTemplateId;
                    pageData.HeaderTemplateId = page.HeaderTemplateId;
                    pageData.LastUpdateBy = page.LastUpdateBy;
                    pageData.LastUpdate = DateTime.Now;
                    pageData.RecordOrder = page.RecordOrder;
                    pageData.RecordActive = page.RecordActive;
                    pageData.RecordDeleted = page.RecordDeleted;

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

        public bool DeletePage(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "DELETE Pages WHERE ID = {0}";
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
