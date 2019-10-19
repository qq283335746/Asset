using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace Yibi.LiteDAL
{
    public class Staff : IStaff
    {
        private LiteDbContext _db;

        public Staff()
        {
            _db = new LiteDbContext(ConnectionHelper.ConnectionString);
        }

        public IList<StaffInfo> GetListByOrg(int pageIndex, int pageSize, out int totalRecords, object orgId, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var queryModel = ConvertToModel.ToModel<LiteQueryModel>(cmdParms);

            IEnumerable<StaffInfo> staffs = null;

            if (!string.IsNullOrEmpty(queryModel.Keyword))
            {
                staffs = _db.Staffs.Find(m => (m.Coded + m.Phone).Contains(queryModel.Keyword));
            }
            else
            {
                staffs = _db.Staffs.FindAll();
            }
            if (staffs == null)
            {
                totalRecords = 0;
                return null;
            }

            totalRecords = staffs.Count();

            var datas = staffs.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return datas.ToList();
        }


        public int Delete(Guid userId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBatch(IList<object> list)
        {
            throw new NotImplementedException();
        }

        public int DeleteStaff(Guid userid)
        {
            var staffs = _db.Staffs.Find(m => m.UserId.Equals(userid));
            if (staffs == null) return 0;

            foreach(var item in staffs)
            {
                _db.Staffs.Delete(item.Id);
            }

            var userInOrgs = _db.UserInOrg.Find(m => m.UserId.Equals(userid));
            if (userInOrgs == null) return 1;

            foreach(var item in userInOrgs)
            {
                _db.UserInOrg.Delete(m=>m.UserId.Equals(item.UserId) && m.OrgId.Equals(item.OrgId));
            }

            return 1;
        }

        public IList<StaffInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<StaffInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<StaffInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            throw new NotImplementedException();
        }

        public IList<StaffInfo> GetList()
        {
            throw new NotImplementedException();
        }

        public IList<StaffInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var queryModel = ConvertToModel.ToModel<LiteQueryModel>(cmdParms);

            IEnumerable<StaffInfo> staffs = null;

            if (!string.IsNullOrEmpty(queryModel.Keyword))
            {
                staffs = _db.Staffs.Find(m => (m.Coded + m.Phone).Contains(queryModel.Keyword));
            }
            else
            {
                staffs = _db.Staffs.FindAll();
            }
            if (staffs == null)
            {
                totalRecords = 0;
                return null;
            }

            totalRecords = staffs.Count();

            var datas = staffs.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return datas.ToList();
        }

        public StaffInfo GetModel(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Guid GetOrgId(Guid userid)
        {
            var userInOrgInfo = _db.UserInOrg.FindOne(m => m.UserId.Equals(userid));
            if (userInOrgInfo == null) return Guid.Empty;

            return userInOrgInfo.OrgId;
        }

        public StaffInfo GetStaffOrgInfo(Guid userid)
        {
            var staffInfo = _db.Staffs.FindOne(m => m.UserId.Equals(userid));
            if (staffInfo == null) return null;

            return staffInfo;
        }

        public int Insert(StaffInfo model)
        {
            if (model.Id.Equals(Guid.Empty)) model.Id = Guid.NewGuid();

            _db.Staffs.Insert(model);

            return 1;
        }

        public void InsertOrgStaff(StaffInfo model)
        {
            Insert(model);
            var userInOrgInfo = new UserInOrgInfo(model.AppCode, model.UserId, model.OrgId);
            _db.UserInOrg.Insert(userInOrgInfo);
        }

        public int Update(StaffInfo model)
        {
            var oldInfo = _db.Staffs.FindOne(m => m.UserId.Equals(model.UserId));
            if(oldInfo != null)
            {
                oldInfo.AppCode = model.AppCode;
                oldInfo.Coded = model.Coded;
                oldInfo.Named = model.Named;
                oldInfo.Phone = model.Phone;
                oldInfo.Sort = model.Sort;
                oldInfo.Remark = model.Remark;
                oldInfo.RecordDate = model.RecordDate;
                oldInfo.LastUpdatedDate = model.LastUpdatedDate;

                return _db.Staffs.Update(oldInfo) ? 1 : 0;
            }

            return 0;
        }
    }
}
