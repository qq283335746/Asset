using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class Staff
    {
        #region Staff Member

        public string GetUserRule(Guid userid) 
        {
            var staffOrgInfo = GetStaffOrgInfo(userid);
            if (staffOrgInfo == null) return null;
            var list = new List<string>();
            var orgs = new OrgDepmt().GetList(string.Format("and CHARINDEX('{0}',Step) > 0", staffOrgInfo.OrgId));
            if(orgs == null || orgs.Count == 0) return null;
            if (orgs.Count == 1) return orgs[0].Step.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];

            var uras = new List<UserRuleAnalyseInfo>();
            foreach (var item in orgs)
            {
                var arr = item.Step.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                uras.Add(new UserRuleAnalyseInfo { Items = arr.ToList(), TotalCount = arr.Length });
            }
            var q = uras.OrderByDescending(m => m.TotalCount);
            var qLast = q.Last();
            foreach (var item in q) 
            {
                item.Items.RemoveAt((item.Items.Count - 1));
                foreach (var currItem in item.Items) 
                {
                    if (!list.Contains(currItem) && !qLast.Items.Contains(currItem)) list.Add(currItem);
                }
            }
            foreach (var currItem in qLast.Items) 
            {
                if (!list.Contains(currItem)) list.Add(currItem);
            }

            //var arr = staffOrgInfo.OrgStep.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            //var index = arr.FindLastIndex(m => m.ToLower() == staffOrgInfo.OrgId.ToString().ToLower());
            //for (var i = 0; i <= index; i++)
            //{
            //    list.Add(arr[i]);
            //}
            return string.Join(",", list);
        }

        public Guid GetOrgId(Guid userid) 
        {
            return dal.GetOrgId(userid);
        }

        public StaffInfo GetStaffOrgInfo(Guid userid) 
        {
            return dal.GetStaffOrgInfo(userid);
        }

        public IList<StaffInfo> GetListByOrg(int pageIndex, int pageSize, out int totalRecords, object orgId, string sqlWhere, params SqlParameter[] cmdParms) 
        {
            return dal.GetListByOrg(pageIndex, pageSize, out totalRecords, orgId, sqlWhere, cmdParms);
        }

        public IList<StaffInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms) 
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public int DeleteStaff(Guid userid) 
        {
            return dal.DeleteStaff(userid);
        }

        public void InsertOrgStaff(StaffInfo model) 
        {
            dal.InsertOrgStaff(model);
        }

        #endregion
    }
}
