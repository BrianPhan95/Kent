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

namespace Kent.Entities.Repositories.Questions
{
    public class QuestionRepository : BaseRepository, IQuestionRepository
    {
        #region Base
        public ResponseModel Delete(int id)
        {
            var result = new ResponseModel();
            using (IDbConnection conn = Connection)
            {
                string sql = "DELETE Questions WHERE ID = {0}";
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

        public Question GetById(int id)
        {
            Question result = new Question();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from Questions where Id = {0}";
                sql = string.Format(sql, id);

                try
                {
                    conn.Open();

                    result = conn.Query<Question>(sql).AsQueryable().FirstOrDefault();
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

        public List<Question> GetList(RequestModel request)
        {
            List<Question> result = new List<Question>();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from Questions where Name LIKE '%{0}%'";
                sql = string.Format(sql, request.Keyword);

                try
                {
                    conn.Open();

                    result = conn.Query<Question>(sql).AsQueryable().ToList();
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

        public ResponseModel Insert(Question model)
        {
            var result = new ResponseModel();
            using (var db = new KentEntities())
            {
                try
                {
                    db.Questions.Add(model);
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

        public ResponseModel Update(Question model)
        {
            var result = new ResponseModel();
            using (var db = new KentEntities())
            {
                try
                {
                    var question = db.Questions.Where(d => d.ID == model.ID).FirstOrDefault();
                    if (question == null)
                        return result;

                    question.QuestionString = model.QuestionString;
                    question.SelectedAnswers = model.SelectedAnswers;
                    question.Answer = model.Answer;
                    question.LastUpdate = DateTime.Now;

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
