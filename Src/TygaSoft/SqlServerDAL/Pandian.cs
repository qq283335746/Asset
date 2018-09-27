using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;
using TygaSoft.SysUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class Pandian
    {
        #region IPandian Member

        public int UpdateIsDown(object Id)
        {
            var cmdText = @"update Pandian Set IsDown = @IsDown,Status = " + (int)EnumPandianStatus.进行中 + " where Id = @Id ";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@IsDown",SqlDbType.Bit)
                                   };
            parms[0].Value = Id;
            parms[1].Value = true;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, parms);
        }

        public int[] GetTotal()
        {
            var cmdText = string.Format(@"select count(1) as Total from Pandian  
                union all select count(1) as Total from Pandian where Status = {0}
                union all select count(1) as Total from Pandian where Status = {1} or Status = {2}
                ", (int)EnumStatus.完成, (int)EnumStatus.未完成, (int)EnumStatus.新建);

            var list = new List<int>(3);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetInt32(0));
                    }
                }
            }

            return list.ToArray();
        }

        public IList<PandianInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(250);
            sb.AppendFormat(@"select count(*) from Pandian pd
                        left join {0}aspnet_Users u on u.UserId = pd.UserId
                      ", GlobalConfig.Dbo);
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PandianInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.AppendFormat(@"select * from(select row_number() over(order by pd.LastUpdatedDate desc) as RowNumber,
			          pd.Id,pd.AppCode,pd.UserId,pd.DepmtId,pd.Named,pd.TotalQty,pd.IsDown,pd.Sort,pd.Remark,pd.Status,pd.RecordDate,pd.LastUpdatedDate
                      ,u.UserName
					  from Pandian pd 
                      left join {0}aspnet_Users u on u.UserId = pd.UserId
                      ", GlobalConfig.Dbo);
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new PandianInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.DepmtId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Named = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.TotalQty = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                        model.IsDown = reader.IsDBNull(7) ? false : reader.GetBoolean(7);
                        model.Sort = reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                        model.Remark = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Status = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                        model.RecordDate = reader.IsDBNull(11) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(11);
                        model.LastUpdatedDate = reader.IsDBNull(12) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(12);
                        model.UserName = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.SCreateDate = model.RecordDate.ToString("yyyy-MM-dd");
                        model.StatusName = EnumHelper.GetName(typeof(EnumPandianStatus), model.Status);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public PandianInfo GetModelByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder(300);
            sb.AppendFormat(@"select pd.Id,pd.AppCode,pd.UserId,pd.DepmtId,pd.Named,pd.TotalQty,pd.IsDown,pd.Sort,pd.Remark,pd.Status,pd.RecordDate,pd.LastUpdatedDate
                          ,u.UserName
					      from Pandian pd 
                          left join {0}aspnet_Users u on u.UserId = pd.UserId
                          ", GlobalConfig.Dbo);
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            PandianInfo model = null;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    if (reader.Read())
                    {
                        model = new PandianInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Named = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        model.TotalQty = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                        model.IsDown = reader.IsDBNull(6) ? false : reader.GetBoolean(6);
                        model.Sort = reader.IsDBNull(7) ? 0 : reader.GetInt32(7);
                        model.Remark = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Status = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                        model.RecordDate = reader.IsDBNull(10) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(10);
                        model.LastUpdatedDate = reader.IsDBNull(11) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(11);
                        model.UserName = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.SCreateDate = model.RecordDate.ToString("yyyy-MM-dd");
                        model.StatusName = EnumHelper.GetName(typeof(EnumPandianStatus), model.Status);
                    }
                }
            }

            return model;
        }

        public bool IsExistChildren(Guid id)
        {
            var cmdText = @"select 1 from PandianAsset
                            where PandianId = @PandianId
                            ";
            var parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
            parm.Value = id;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, cmdText, parm);
            if (obj != null) return true;

            return false;
        }

        #endregion
    }
}
