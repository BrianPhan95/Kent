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

namespace Kent.Entities.Repositories.QuestionSections
{
    public class QuestionSectionRepository : BaseRepository, IQuestionSectionRepository
    {
        #region Base
        public ResponseModel Delete(int id)
        {
            var result = new ResponseModel();
            using (IDbConnection conn = Connection)
            {
                string sql = "DELETE QuestionSections WHERE ID = {0}";
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

        public QuestionSection GetById(int id)
        {
            QuestionSection result = new QuestionSection();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from QuestionSections where Id = {0}";
                sql = string.Format(sql, id);

                try
                {
                    conn.Open();

                    result = conn.Query<QuestionSection>(sql).AsQueryable().FirstOrDefault();
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

        public List<QuestionSection> GetList(RequestModel request)
        {
            List<QuestionSection> result = new List<QuestionSection>();
            using (IDbConnection conn = Connection)
            {
                string sql = "select * from QuestionSections where Name LIKE '%{0}%'";
                sql = string.Format(sql, request.Keyword);

                try
                {
                    conn.Open();

                    result = conn.Query<QuestionSection>(sql).AsQueryable().ToList();
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

        public ResponseModel Insert(QuestionSection model)
        {
            var result = new ResponseModel();
            using (var db = new KentEntities())
            {
                try
                {
                    db.QuestionSections.Add(model);
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

        public ResponseModel Update(QuestionSection model)
        {
            var result = new ResponseModel();
            using (var db = new KentEntities())
            {
                try
                {
                    var questionSection = db.QuestionSections.Where(d => d.ID == model.ID).FirstOrDefault();
                    if (questionSection == null)
                        return result;

                    questionSection.Description = model.Description;
                    questionSection.SectionType = model.SectionType;
                    questionSection.QuestionKitID = model.QuestionKitID;
                    questionSection.LastUpdate = DateTime.Now;

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
