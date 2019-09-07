using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class FeatureUser: IFeatureUser
    {
        private LiteDbContext _db;

        public FeatureUser()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public int Delete(Guid userId, Guid featureId)
        {
            _db.FeatureUsers.Delete(m => m.UserId.Equals(userId) && m.FeatureId.Equals(featureId));

            return 1;
        }

        public bool DeleteBatch(IList<object> list)
        {
            throw new NotImplementedException();
        }

        public IList<FeatureUserInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<FeatureUserInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<FeatureUserInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            FeatureUserInfo model = ConvertToModel.ToModel<FeatureUserInfo>(cmdParms);

            IEnumerable<FeatureUserInfo> datas = _db.FeatureUsers.FindAll();

            if (!model.UserId.Equals(Guid.Empty))
            {
                datas = datas.Where(m => m.UserId.Equals(model.UserId));
            }
            if (!string.IsNullOrEmpty(model.TypeName))
            {
                datas = datas.Where(m => m.TypeName.Equals(model.TypeName));
            }

            return datas.ToList();
        }

        public IList<FeatureUserInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public FeatureUserInfo GetModel(Guid userId, string typeName)
        {
            return _db.FeatureUsers.FindOne(m => m.UserId.Equals(userId) && m.TypeName.Equals(typeName));
        }

        public FeatureUserInfo GetModel(Guid userId, Guid featureId)
        {
            return _db.FeatureUsers.FindOne(m => m.UserId.Equals(userId) && m.FeatureId.Equals(featureId));
        }

        public int Insert(FeatureUserInfo model)
        {
            model.Id = Guid.NewGuid();
            _db.FeatureUsers.Insert(model);

            return 1;
        }

        public int Update(FeatureUserInfo model)
        {
            throw new NotImplementedException();
        }
    }
}
