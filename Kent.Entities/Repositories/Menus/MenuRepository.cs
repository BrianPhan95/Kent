using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kent.Entities.Model;
using System.Data.SqlClient;
using Kent.Libary.Logger;
using System.Data;
using Dapper;
using Kent.Libary.Models;

namespace Kent.Entities.Repositories.Menus
{
    public class MenuRepository : BaseRepository, IMenuRepository
    {

        public Menu GetById(int id)
        {
            Menu result = new Menu();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from Menus where Id = {0}";
                sql = string.Format(sql, id);

                try
                {
                    conn.Open();

                    result = conn.Query<Menu>(sql).AsQueryable().FirstOrDefault();
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

        public List<Menu> GetList(RequestModel request)
        {
            List<Menu> result = new List<Menu>();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from Menus where Name LIKE '%{0}%'";
                sql = string.Format(sql, request.Keyword);

                try
                {
                    conn.Open();

                    result = conn.Query<Menu>(sql).AsQueryable().ToList();
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

        public ResponseModel Insert(Menu model)
        {
            var result = new ResponseModel();
            using (var db = new KentEntities())
            {
                try
                {
                    db.Menus.Add(model);
                    result.Success = db.SaveChanges() > 0 ? true : false;
                }
                catch (SqlException sqlEx)
                {
                    result.Message = sqlEx.Message;
                    Logger.ErrorException(sqlEx);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    Logger.ErrorException(ex);
                }
            }
            return result;
        }

        public ResponseModel Update(Menu model)
        {
            var result = new ResponseModel();
            using (var db = new KentEntities())
            {
                try
                {
                    var menu = db.Menus.Where(d => d.ID == model.ID).FirstOrDefault();
                    if (menu == null)
                        return result;

                    menu.Name = model.Name;
                    menu.Action = model.Action;
                    menu.Controller = model.Controller;
                    menu.Area = model.Area;
                    menu.Visible = model.Visible;
                    menu.LastUpdate = DateTime.Now;

                    result.Success = db.SaveChanges() > 0 ? true : false;
                }
                catch (SqlException sqlEx)
                {
                    result.Message = sqlEx.Message;
                    Logger.ErrorException(sqlEx);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    Logger.ErrorException(ex);
                }
            }

            return result;
        }

        public ResponseModel Delete(int id)
        {
            var result = new ResponseModel();
            using (IDbConnection conn = Connection)
            {
                string sql = "DELETE Menus WHERE ID = {0}";
                sql = string.Format(sql, id);
                try
                {
                    conn.Open();

                    var exec = conn.Execute(sql);
                    result.Success = exec == 1 ? true : false;
                }
                catch (SqlException sqlEx)
                {
                    result.Message = sqlEx.Message;
                    Logger.ErrorException(sqlEx);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    Logger.ErrorException(ex);
                }
            }

            return result;
        }

    }
}
