using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kent.Entities.Model;
using Kent.Libary.Models;
using System.Data.SqlClient;
using Kent.Libary.Logger;
using System.Data;
using Dapper;

namespace Kent.Entities.Repositories.QuestionKits
{
    public class QuestionKitRepository : BaseRepository, IQuestionKitRepository
    {
        #region Base
        public ResponseModel Delete(int id)
        {
            var result = new ResponseModel();
            using (IDbConnection conn = Connection)
            {
                string sql = "DELETE QuestionKits WHERE ID = {0}";
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

        public QuestionKit GetById(int id)
        {
            QuestionKit result = new QuestionKit();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from QuestionKits where Id = {0}";
                sql = string.Format(sql, id);

                try
                {
                    conn.Open();

                    result = conn.Query<QuestionKit>(sql).AsQueryable().FirstOrDefault();
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

        public List<QuestionKit> GetList(RequestModel request)
        {
            List<QuestionKit> result = new List<QuestionKit>();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from QuestionKits where Name LIKE '%{0}%'";
                sql = string.Format(sql, request.Keyword);

                try
                {
                    conn.Open();

                    result = conn.Query<QuestionKit>(sql).AsQueryable().ToList();
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

        public ResponseModel Insert(QuestionKit model)
        {
            var result = new ResponseModel();
            using (var db = new KentEntities())
            {
                try
                {
                    db.QuestionKits.Add(model);
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

        public ResponseModel Update(QuestionKit model)
        {
            var result = new ResponseModel();
            using (var db = new KentEntities())
            {
                try
                {
                    var questionKit = db.QuestionKits.Where(d => d.ID == model.ID).FirstOrDefault();
                    if (questionKit == null)
                        return result;

                    questionKit.Name = model.Name;
                    questionKit.Description = model.Description;
                    questionKit.LastUpdate = DateTime.Now;

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
        #endregion
    }
}
