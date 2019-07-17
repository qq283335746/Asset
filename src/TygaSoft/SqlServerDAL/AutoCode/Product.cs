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
    public partial class Product : IProduct
    {
        #region IProduct Member

        public int Insert(ProductInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Product (AppCode,UserId,DepmtId,CategoryId,Coded,Named,Barcode,SpecModel,Qty,Price,Amount,MeterUnit,PieceQty,Pattr,SourceFrom,Supplier,BuyDate,EnableDate,UseDateLimit,UseDepmtId,UsePersonName,MgrDepmtId,StoragePlaceId,Remark,Status,Sort,IsDisable,RecordDate,LastUpdatedDate)
			            values
						(@AppCode,@UserId,@DepmtId,@CategoryId,@Coded,@Named,@Barcode,@SpecModel,@Qty,@Price,@Amount,@MeterUnit,@PieceQty,@Pattr,@SourceFrom,@Supplier,@BuyDate,@EnableDate,@UseDateLimit,@UseDepmtId,@UsePersonName,@MgrDepmtId,@StoragePlaceId,@Remark,@Status,@Sort,@IsDisable,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@AppCode",SqlDbType.Char,6), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@CategoryId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Coded",SqlDbType.VarChar,36), 
new SqlParameter("@Named",SqlDbType.NVarChar,256), 
new SqlParameter("@Barcode",SqlDbType.VarChar,36), 
new SqlParameter("@SpecModel",SqlDbType.NVarChar,256), 
new SqlParameter("@Qty",SqlDbType.Int), 
new SqlParameter("@Price",SqlDbType.Decimal), 
new SqlParameter("@Amount",SqlDbType.Decimal), 
new SqlParameter("@MeterUnit",SqlDbType.NVarChar,20), 
new SqlParameter("@PieceQty",SqlDbType.Int), 
new SqlParameter("@Pattr",SqlDbType.NVarChar,100), 
new SqlParameter("@SourceFrom",SqlDbType.NVarChar,256), 
new SqlParameter("@Supplier",SqlDbType.NVarChar,30), 
new SqlParameter("@BuyDate",SqlDbType.DateTime), 
new SqlParameter("@EnableDate",SqlDbType.VarChar,20), 
new SqlParameter("@UseDateLimit",SqlDbType.NVarChar,30), 
new SqlParameter("@UseDepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@UsePersonName",SqlDbType.NVarChar,10), 
new SqlParameter("@MgrDepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@StoragePlaceId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Remark",SqlDbType.NVarChar,300), 
new SqlParameter("@Status",SqlDbType.Int), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@IsDisable",SqlDbType.Bit), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.AppCode;
            parms[1].Value = model.UserId;
            parms[2].Value = model.DepmtId;
            parms[3].Value = model.CategoryId;
            parms[4].Value = model.Coded;
            parms[5].Value = model.Named;
            parms[6].Value = model.Barcode;
            parms[7].Value = model.SpecModel;
            parms[8].Value = model.Qty;
            parms[9].Value = model.Price;
            parms[10].Value = model.Amount;
            parms[11].Value = model.MeterUnit;
            parms[12].Value = model.PieceQty;
            parms[13].Value = model.Pattr;
            parms[14].Value = model.SourceFrom;
            parms[15].Value = model.Supplier;
            parms[16].Value = model.BuyDate;
            parms[17].Value = model.EnableDate;
            parms[18].Value = model.UseDateLimit;
            parms[19].Value = model.UseDepmtId;
            parms[20].Value = model.UsePersonName;
            parms[21].Value = model.MgrDepmtId;
            parms[22].Value = model.StoragePlaceId;
            parms[23].Value = model.Remark;
            parms[24].Value = model.Status;
            parms[25].Value = model.Sort;
            parms[26].Value = model.IsDisable;
            parms[27].Value = model.RecordDate;
            parms[28].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int InsertByOutput(ProductInfo model)
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"insert into Product (Id,AppCode,UserId,DepmtId,CategoryId,Coded,Named,Barcode,SpecModel,Qty,Price,Amount,MeterUnit,PieceQty,Pattr,SourceFrom,Supplier,BuyDate,EnableDate,UseDateLimit,UseDepmtId,UsePersonName,MgrDepmtId,StoragePlaceId,Remark,Status,Sort,IsDisable,RecordDate,LastUpdatedDate)
			            values
						(@Id,@AppCode,@UserId,@DepmtId,@CategoryId,@Coded,@Named,@Barcode,@SpecModel,@Qty,@Price,@Amount,@MeterUnit,@PieceQty,@Pattr,@SourceFrom,@Supplier,@BuyDate,@EnableDate,@UseDateLimit,@UseDepmtId,@UsePersonName,@MgrDepmtId,@StoragePlaceId,@Remark,@Status,@Sort,@IsDisable,@RecordDate,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,6), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@CategoryId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Coded",SqlDbType.VarChar,36), 
new SqlParameter("@Named",SqlDbType.NVarChar,256), 
new SqlParameter("@Barcode",SqlDbType.VarChar,36), 
new SqlParameter("@SpecModel",SqlDbType.NVarChar,256), 
new SqlParameter("@Qty",SqlDbType.Int), 
new SqlParameter("@Price",SqlDbType.Decimal), 
new SqlParameter("@Amount",SqlDbType.Decimal), 
new SqlParameter("@MeterUnit",SqlDbType.NVarChar,20), 
new SqlParameter("@PieceQty",SqlDbType.Int), 
new SqlParameter("@Pattr",SqlDbType.NVarChar,100), 
new SqlParameter("@SourceFrom",SqlDbType.NVarChar,256), 
new SqlParameter("@Supplier",SqlDbType.NVarChar,30), 
new SqlParameter("@BuyDate",SqlDbType.DateTime), 
new SqlParameter("@EnableDate",SqlDbType.VarChar,20), 
new SqlParameter("@UseDateLimit",SqlDbType.NVarChar,30), 
new SqlParameter("@UseDepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@UsePersonName",SqlDbType.NVarChar,10), 
new SqlParameter("@MgrDepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@StoragePlaceId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Remark",SqlDbType.NVarChar,300), 
new SqlParameter("@Status",SqlDbType.Int), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@IsDisable",SqlDbType.Bit), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.DepmtId;
            parms[4].Value = model.CategoryId;
            parms[5].Value = model.Coded;
            parms[6].Value = model.Named;
            parms[7].Value = model.Barcode;
            parms[8].Value = model.SpecModel;
            parms[9].Value = model.Qty;
            parms[10].Value = model.Price;
            parms[11].Value = model.Amount;
            parms[12].Value = model.MeterUnit;
            parms[13].Value = model.PieceQty;
            parms[14].Value = model.Pattr;
            parms[15].Value = model.SourceFrom;
            parms[16].Value = model.Supplier;
            parms[17].Value = model.BuyDate;
            parms[18].Value = model.EnableDate;
            parms[19].Value = model.UseDateLimit;
            parms[20].Value = model.UseDepmtId;
            parms[21].Value = model.UsePersonName;
            parms[22].Value = model.MgrDepmtId;
            parms[23].Value = model.StoragePlaceId;
            parms[24].Value = model.Remark;
            parms[25].Value = model.Status;
            parms[26].Value = model.Sort;
            parms[27].Value = model.IsDisable;
            parms[28].Value = model.RecordDate;
            parms[29].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(ProductInfo model)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"update Product set AppCode = @AppCode,UserId = @UserId,DepmtId = @DepmtId,CategoryId = @CategoryId,Coded = @Coded,Named = @Named,Barcode = @Barcode,SpecModel = @SpecModel,Qty = @Qty,Price = @Price,Amount = @Amount,MeterUnit = @MeterUnit,PieceQty = @PieceQty,Pattr = @Pattr,SourceFrom = @SourceFrom,Supplier = @Supplier,BuyDate = @BuyDate,EnableDate = @EnableDate,UseDateLimit = @UseDateLimit,UseDepmtId = @UseDepmtId,UsePersonName = @UsePersonName,MgrDepmtId = @MgrDepmtId,StoragePlaceId = @StoragePlaceId,Remark = @Remark,Status = @Status,Sort = @Sort,IsDisable = @IsDisable,RecordDate = @RecordDate,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier), 
new SqlParameter("@AppCode",SqlDbType.Char,6), 
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@DepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@CategoryId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Coded",SqlDbType.VarChar,36), 
new SqlParameter("@Named",SqlDbType.NVarChar,256), 
new SqlParameter("@Barcode",SqlDbType.VarChar,36), 
new SqlParameter("@SpecModel",SqlDbType.NVarChar,256), 
new SqlParameter("@Qty",SqlDbType.Int), 
new SqlParameter("@Price",SqlDbType.Decimal), 
new SqlParameter("@Amount",SqlDbType.Decimal), 
new SqlParameter("@MeterUnit",SqlDbType.NVarChar,20), 
new SqlParameter("@PieceQty",SqlDbType.Int), 
new SqlParameter("@Pattr",SqlDbType.NVarChar,100), 
new SqlParameter("@SourceFrom",SqlDbType.NVarChar,256), 
new SqlParameter("@Supplier",SqlDbType.NVarChar,30), 
new SqlParameter("@BuyDate",SqlDbType.DateTime), 
new SqlParameter("@EnableDate",SqlDbType.VarChar,20), 
new SqlParameter("@UseDateLimit",SqlDbType.NVarChar,30), 
new SqlParameter("@UseDepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@UsePersonName",SqlDbType.NVarChar,10), 
new SqlParameter("@MgrDepmtId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@StoragePlaceId",SqlDbType.UniqueIdentifier), 
new SqlParameter("@Remark",SqlDbType.NVarChar,300), 
new SqlParameter("@Status",SqlDbType.Int), 
new SqlParameter("@Sort",SqlDbType.Int), 
new SqlParameter("@IsDisable",SqlDbType.Bit), 
new SqlParameter("@RecordDate",SqlDbType.DateTime), 
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.AppCode;
            parms[2].Value = model.UserId;
            parms[3].Value = model.DepmtId;
            parms[4].Value = model.CategoryId;
            parms[5].Value = model.Coded;
            parms[6].Value = model.Named;
            parms[7].Value = model.Barcode;
            parms[8].Value = model.SpecModel;
            parms[9].Value = model.Qty;
            parms[10].Value = model.Price;
            parms[11].Value = model.Amount;
            parms[12].Value = model.MeterUnit;
            parms[13].Value = model.PieceQty;
            parms[14].Value = model.Pattr;
            parms[15].Value = model.SourceFrom;
            parms[16].Value = model.Supplier;
            parms[17].Value = model.BuyDate;
            parms[18].Value = model.EnableDate;
            parms[19].Value = model.UseDateLimit;
            parms[20].Value = model.UseDepmtId;
            parms[21].Value = model.UsePersonName;
            parms[22].Value = model.MgrDepmtId;
            parms[23].Value = model.StoragePlaceId;
            parms[24].Value = model.Remark;
            parms[25].Value = model.Status;
            parms[26].Value = model.Sort;
            parms[27].Value = model.IsDisable;
            parms[28].Value = model.RecordDate;
            parms[29].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Product where Id = @Id ");
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
                sb.Append(@"delete from Product where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public ProductInfo GetModel(Guid id)
        {
            ProductInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 AppCode,UserId,DepmtId,Id,CategoryId,Coded,Named,Barcode,SpecModel,Qty,Price,Amount,MeterUnit,PieceQty,Pattr,SourceFrom,Supplier,BuyDate,EnableDate,UseDateLimit,UseDepmtId,UsePersonName,MgrDepmtId,StoragePlaceId,Remark,Status,Sort,IsDisable,RecordDate,LastUpdatedDate 
			            from Product
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
                        model = new ProductInfo();
                        model.AppCode = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.DepmtId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.Id = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.CategoryId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Coded = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Named = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Barcode = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.SpecModel = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Qty = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                        model.Price = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10);
                        model.Amount = reader.IsDBNull(11) ? 0 : reader.GetDecimal(11);
                        model.MeterUnit = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        model.PieceQty = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                        model.Pattr = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.SourceFrom = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.Supplier = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.BuyDate = reader.IsDBNull(17) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(17);
                        model.EnableDate = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                        model.UseDateLimit = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                        model.UseDepmtId = reader.IsDBNull(20) ? Guid.Empty : reader.GetGuid(20);
                        model.UsePersonName = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
                        model.MgrDepmtId = reader.IsDBNull(22) ? Guid.Empty : reader.GetGuid(22);
                        model.StoragePlaceId = reader.IsDBNull(23) ? Guid.Empty : reader.GetGuid(23);
                        model.Remark = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                        model.Status = reader.IsDBNull(25) ? 0 : reader.GetInt32(25);
                        model.Sort = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                        model.IsDisable = reader.IsDBNull(27) ? false : reader.GetBoolean(27);
                        model.RecordDate = reader.IsDBNull(28) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(28);
                        model.LastUpdatedDate = reader.IsDBNull(29) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(29);
                    }
                }
            }

            return model;
        }

        public IList<ProductInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from Product ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<ProductInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Status,Sort) as RowNumber,
			          AppCode,UserId,DepmtId,Id,CategoryId,Coded,Named,Barcode,SpecModel,Qty,Price,Amount,MeterUnit,PieceQty,Pattr,SourceFrom,Supplier,BuyDate,EnableDate,UseDateLimit,UseDepmtId,UsePersonName,MgrDepmtId,StoragePlaceId,Remark,Status,Sort,IsDisable,RecordDate,LastUpdatedDate
					  from Product ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ProductInfo> list = new List<ProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductInfo model = new ProductInfo();
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Id = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.CategoryId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.Coded = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Named = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.Barcode = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.SpecModel = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Qty = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                        model.Price = reader.IsDBNull(11) ? 0 : reader.GetDecimal(11);
                        model.Amount = reader.IsDBNull(12) ? 0 : reader.GetDecimal(12);
                        model.MeterUnit = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.PieceQty = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                        model.Pattr = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.SourceFrom = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.Supplier = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                        model.BuyDate = reader.IsDBNull(18) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(18);
                        model.EnableDate = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                        model.UseDateLimit = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
                        model.UseDepmtId = reader.IsDBNull(21) ? Guid.Empty : reader.GetGuid(21);
                        model.UsePersonName = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                        model.MgrDepmtId = reader.IsDBNull(23) ? Guid.Empty : reader.GetGuid(23);
                        model.StoragePlaceId = reader.IsDBNull(24) ? Guid.Empty : reader.GetGuid(24);
                        model.Remark = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                        model.Status = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                        model.Sort = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
                        model.IsDisable = reader.IsDBNull(28) ? false : reader.GetBoolean(28);
                        model.RecordDate = reader.IsDBNull(29) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(29);
                        model.LastUpdatedDate = reader.IsDBNull(30) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(30);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ProductInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Status,Sort) as RowNumber,
			           AppCode,UserId,DepmtId,Id,CategoryId,Coded,Named,Barcode,SpecModel,Qty,Price,Amount,MeterUnit,PieceQty,Pattr,SourceFrom,Supplier,BuyDate,EnableDate,UseDateLimit,UseDepmtId,UsePersonName,MgrDepmtId,StoragePlaceId,Remark,Status,Sort,IsDisable,RecordDate,LastUpdatedDate
					   from Product ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<ProductInfo> list = new List<ProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductInfo model = new ProductInfo();
                        model.AppCode = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.UserId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.DepmtId = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.Id = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.CategoryId = reader.IsDBNull(5) ? Guid.Empty : reader.GetGuid(5);
                        model.Coded = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Named = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.Barcode = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.SpecModel = reader.IsDBNull(9) ? string.Empty : reader.GetString(9);
                        model.Qty = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                        model.Price = reader.IsDBNull(11) ? 0 : reader.GetDecimal(11);
                        model.Amount = reader.IsDBNull(12) ? 0 : reader.GetDecimal(12);
                        model.MeterUnit = reader.IsDBNull(13) ? string.Empty : reader.GetString(13);
                        model.PieceQty = reader.IsDBNull(14) ? 0 : reader.GetInt32(14);
                        model.Pattr = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.SourceFrom = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.Supplier = reader.IsDBNull(17) ? string.Empty : reader.GetString(17);
                        model.BuyDate = reader.IsDBNull(18) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(18);
                        model.EnableDate = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                        model.UseDateLimit = reader.IsDBNull(20) ? string.Empty : reader.GetString(20);
                        model.UseDepmtId = reader.IsDBNull(21) ? Guid.Empty : reader.GetGuid(21);
                        model.UsePersonName = reader.IsDBNull(22) ? string.Empty : reader.GetString(22);
                        model.MgrDepmtId = reader.IsDBNull(23) ? Guid.Empty : reader.GetGuid(23);
                        model.StoragePlaceId = reader.IsDBNull(24) ? Guid.Empty : reader.GetGuid(24);
                        model.Remark = reader.IsDBNull(25) ? string.Empty : reader.GetString(25);
                        model.Status = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                        model.Sort = reader.IsDBNull(27) ? 0 : reader.GetInt32(27);
                        model.IsDisable = reader.IsDBNull(28) ? false : reader.GetBoolean(28);
                        model.RecordDate = reader.IsDBNull(29) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(29);
                        model.LastUpdatedDate = reader.IsDBNull(30) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(30);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ProductInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select AppCode,UserId,DepmtId,Id,CategoryId,Coded,Named,Barcode,SpecModel,Qty,Price,Amount,MeterUnit,PieceQty,Pattr,SourceFrom,Supplier,BuyDate,EnableDate,UseDateLimit,UseDepmtId,UsePersonName,MgrDepmtId,StoragePlaceId,Remark,Status,Sort,IsDisable,RecordDate,LastUpdatedDate
                        from Product ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Status,Sort ");

            IList<ProductInfo> list = new List<ProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductInfo model = new ProductInfo();
                        model.AppCode = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.DepmtId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.Id = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.CategoryId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Coded = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Named = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Barcode = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.SpecModel = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Qty = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                        model.Price = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10);
                        model.Amount = reader.IsDBNull(11) ? 0 : reader.GetDecimal(11);
                        model.MeterUnit = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        model.PieceQty = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                        model.Pattr = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.SourceFrom = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.Supplier = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.BuyDate = reader.IsDBNull(17) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(17);
                        model.EnableDate = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                        model.UseDateLimit = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                        model.UseDepmtId = reader.IsDBNull(20) ? Guid.Empty : reader.GetGuid(20);
                        model.UsePersonName = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
                        model.MgrDepmtId = reader.IsDBNull(22) ? Guid.Empty : reader.GetGuid(22);
                        model.StoragePlaceId = reader.IsDBNull(23) ? Guid.Empty : reader.GetGuid(23);
                        model.Remark = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                        model.Status = reader.IsDBNull(25) ? 0 : reader.GetInt32(25);
                        model.Sort = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                        model.IsDisable = reader.IsDBNull(27) ? false : reader.GetBoolean(27);
                        model.RecordDate = reader.IsDBNull(28) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(28);
                        model.LastUpdatedDate = reader.IsDBNull(29) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(29);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<ProductInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select AppCode,UserId,DepmtId,Id,CategoryId,Coded,Named,Barcode,SpecModel,Qty,Price,Amount,MeterUnit,PieceQty,Pattr,SourceFrom,Supplier,BuyDate,EnableDate,UseDateLimit,UseDepmtId,UsePersonName,MgrDepmtId,StoragePlaceId,Remark,Status,Sort,IsDisable,RecordDate,LastUpdatedDate 
			            from Product
					    order by Status,Sort ");

            IList<ProductInfo> list = new List<ProductInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetDbConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProductInfo model = new ProductInfo();
                        model.AppCode = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        model.UserId = reader.IsDBNull(1) ? Guid.Empty : reader.GetGuid(1);
                        model.DepmtId = reader.IsDBNull(2) ? Guid.Empty : reader.GetGuid(2);
                        model.Id = reader.IsDBNull(3) ? Guid.Empty : reader.GetGuid(3);
                        model.CategoryId = reader.IsDBNull(4) ? Guid.Empty : reader.GetGuid(4);
                        model.Coded = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                        model.Named = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                        model.Barcode = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                        model.SpecModel = reader.IsDBNull(8) ? string.Empty : reader.GetString(8);
                        model.Qty = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                        model.Price = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10);
                        model.Amount = reader.IsDBNull(11) ? 0 : reader.GetDecimal(11);
                        model.MeterUnit = reader.IsDBNull(12) ? string.Empty : reader.GetString(12);
                        model.PieceQty = reader.IsDBNull(13) ? 0 : reader.GetInt32(13);
                        model.Pattr = reader.IsDBNull(14) ? string.Empty : reader.GetString(14);
                        model.SourceFrom = reader.IsDBNull(15) ? string.Empty : reader.GetString(15);
                        model.Supplier = reader.IsDBNull(16) ? string.Empty : reader.GetString(16);
                        model.BuyDate = reader.IsDBNull(17) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(17);
                        model.EnableDate = reader.IsDBNull(18) ? string.Empty : reader.GetString(18);
                        model.UseDateLimit = reader.IsDBNull(19) ? string.Empty : reader.GetString(19);
                        model.UseDepmtId = reader.IsDBNull(20) ? Guid.Empty : reader.GetGuid(20);
                        model.UsePersonName = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
                        model.MgrDepmtId = reader.IsDBNull(22) ? Guid.Empty : reader.GetGuid(22);
                        model.StoragePlaceId = reader.IsDBNull(23) ? Guid.Empty : reader.GetGuid(23);
                        model.Remark = reader.IsDBNull(24) ? string.Empty : reader.GetString(24);
                        model.Status = reader.IsDBNull(25) ? 0 : reader.GetInt32(25);
                        model.Sort = reader.IsDBNull(26) ? 0 : reader.GetInt32(26);
                        model.IsDisable = reader.IsDBNull(27) ? false : reader.GetBoolean(27);
                        model.RecordDate = reader.IsDBNull(28) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(28);
                        model.LastUpdatedDate = reader.IsDBNull(29) ? DateTime.Parse("1754-01-01") : reader.GetDateTime(29);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
