using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class ProductRepair : IProductRepair
    {
        #region IProductRepair Member

        public int Insert(ProductRepairInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into ProductRepair (AppCode,UserId,OrgId,ProductId,RecordDate,LastUpdatedDate)
			            values
						(@AppCode,@UserId,@OrgId,@ProductId,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@AppCode",SqlDbType.Char,10), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@OrgId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.AppCode;
            parms[1].Value = model.UserId;
            parms[2].Value = model.OrgId;
            parms[3].Value = model.ProductId;
            parms[4].Value = model.RecordDate;
            parms[5].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(ProductRepairInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into ProductRepair (Id,AppCode,UserId,OrgId,ProductId,RecordDate,LastUpdatedDate)
			            values
						(@Id,@AppCode,@UserId,@OrgId,@ProductId,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,10), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@OrgId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.OrgId;
            parms[4].Value = model.ProductId;
            parms[5].Value = model.RecordDate;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(ProductRepairInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update ProductRepair set AppCode = @AppCode,UserId = @UserId,OrgId = @OrgId,ProductId = @ProductId,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,10), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@OrgId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@ProductId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.OrgId;
            parms[4].Value = model.ProductId;
            parms[5].Value = model.RecordDate;
            parms[6].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from ProductRepair where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public bool DeleteBatch(IList<object> list)
        {
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from ProductRepair where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public ProductRepairInfo GetModel(Guid id)
        {
            ProductRepairInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,AppCode,UserId,OrgId,ProductId,RecordDate,LastUpdatedDate 
			            from ProductRepair
						where Id = @Id ");
            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = id;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new ProductRepairInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.OrgId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.ProductId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.RecordDate = reader.IsDBNull(5) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(5);
                        model.LastUpdatedDate = reader.IsDBNull(6) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(6);
                    }
                }
            }

            return model;
        }

        public IList<ProductRepairInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from ProductRepair ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<ProductRepairInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,AppCode,UserId,OrgId,ProductId,RecordDate,LastUpdatedDate
					  from ProductRepair ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ProductRepairInfo> list = new List<ProductRepairInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductRepairInfo model = new ProductRepairInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.OrgId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.ProductId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.RecordDate = reader.IsDBNull(6) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(6);
                        model.LastUpdatedDate = reader.IsDBNull(7) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ProductRepairInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,AppCode,UserId,OrgId,ProductId,RecordDate,LastUpdatedDate
					   from ProductRepair ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ProductRepairInfo> list = new List<ProductRepairInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductRepairInfo model = new ProductRepairInfo();
                        model.Id = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.AppCode = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.UserId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.OrgId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.ProductId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.RecordDate = reader.IsDBNull(6) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(6);
                        model.LastUpdatedDate = reader.IsDBNull(7) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ProductRepairInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select Id,AppCode,UserId,OrgId,ProductId,RecordDate,LastUpdatedDate
                        from ProductRepair ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by LastUpdatedDate desc ");

            IList<ProductRepairInfo> list = new List<ProductRepairInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductRepairInfo model = new ProductRepairInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.OrgId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.ProductId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.RecordDate = reader.IsDBNull(5) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(5);
                        model.LastUpdatedDate = reader.IsDBNull(6) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ProductRepairInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select Id,AppCode,UserId,OrgId,ProductId,RecordDate,LastUpdatedDate 
			            from ProductRepair
					    order by LastUpdatedDate desc ");

            IList<ProductRepairInfo> list = new List<ProductRepairInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductRepairInfo model = new ProductRepairInfo();
                        model.Id = reader.IsDBNull(0) ? Guid.Empty : reader.GetGuid(0);
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.OrgId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.ProductId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.RecordDate = reader.IsDBNull(5) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(5);
                        model.LastUpdatedDate = reader.IsDBNull(6) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(6);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
