using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class Category
    {
        #region Category Member

        public DataTable GetExportData(string sqlWhere, params SqlParameter[] cmdParms)
        {
            var dt = new DataTable();
            dt.Columns.Add("资产分类编码", typeof(string));
            dt.Columns.Add("资产分类名称", typeof(string));
            dt.Columns.Add("所属上级分类编码", typeof(string));
            dt.Columns.Add("所属上级分类", typeof(string));
            dt.Columns.Add("排序", typeof(string));
            var categories = GetList(sqlWhere, cmdParms);
            var rootNode = categories.FirstOrDefault(m => m.ParentId.Equals(Guid.Empty));
            if (rootNode == null) return dt;
            CreateTreeData(categories, rootNode.Id, rootNode, ref dt);

            return dt;
        }

        public void CreateTreeData(IEnumerable<CategoryInfo> q, object parentId, CategoryInfo node, ref DataTable dt)
        {
            var parentNode = q.FirstOrDefault(m => m.Id.Equals(parentId));
            var qChild = q.Where(x => x.ParentId.Equals(parentId));
            if (qChild != null && qChild.Count() > 0)
            {
                foreach (var item in qChild)
                {
                    var itemArr = new Dictionary<string, string>();
                    itemArr.Add("资产分类编码", item.Coded);
                    itemArr.Add("资产分类名称", item.Named);
                    itemArr.Add("所属上级分类编码", parentNode != null ? parentNode.Coded.Replace("All", ""):"");
                    itemArr.Add("所属上级分类", parentNode != null ? parentNode.Named:"");
                    itemArr.Add("排序", item.Sort.ToString());

                    DataRow dr = dt.NewRow();
                    foreach (var kv in itemArr)
                    {
                        dr[kv.Key] = kv.Value;
                    }
                    dt.Rows.Add(dr);
                    if (q.Any(r => r.ParentId.Equals(item.Id)))
                    {
                        CreateTreeData(q, item.Id, node, ref dt);
                    }
                }
            }
        }

        public void GetStep(IList<CategoryInfo> categories, Guid id,ref List<Guid> ids) 
        {
            if (categories == null || categories.Count == 0) return;
            var node = categories.FirstOrDefault(m => m.Id.Equals(id));
            if (node == null) return;
            if (!ids.Contains(node.Id)) ids.Add(node.Id);
            var parentNode = categories.FirstOrDefault(m => m.Id.Equals(node.ParentId));
            if (parentNode == null) return;
            if (!ids.Contains(node.Id)) ids.Add(parentNode.Id);
            GetStep(categories, parentNode.Id, ref ids);
        }

        public CategoryInfo GetModel(string code, string name) 
        {
            return dal.GetModel(code, name);
        }

        public string GetTreeJson()
        {
            var jsonAppend = new StringBuilder();
            var list = dal.GetList().ToList<CategoryInfo>();
            if (list != null && list.Count > 0)
            {
                var rootNode = list.FirstOrDefault(m => m.ParentId == Guid.Empty);
                CreateTreeJson(list, Guid.Empty, rootNode, ref jsonAppend);
            }
            else
            {
                jsonAppend.Append("[{\"id\":\"" + Guid.Empty + "\",\"text\":\"请选择\",\"state\":\"closed\",\"attributes\":{\"parentId\":\"" + Guid.Empty + "\",\"parentName\":\"请选择\"}}]");
            }

            return jsonAppend.ToString();
        }

        private void CreateTreeJson(IEnumerable<CategoryInfo> q, object parentId, CategoryInfo node, ref StringBuilder jsonAppend)
        {
            jsonAppend.Append("[");
            var qChild = q.Where(x => x.ParentId.Equals(parentId));
            if (qChild != null && qChild.Count() > 0)
            {
                int index = 0;
                foreach (var model in qChild)
                {
                    var state = (model.Id == node.Id) ? "open" : "closed";
                    var sText = model.ParentId.Equals(Guid.Empty) ? model.Named : string.Format("{0}（{1}）", model.Coded, model.Named);
                    jsonAppend.AppendFormat(@"{{""id"":""{0}"",""text"":""{1}"",""state"":""{2}"",""attributes"":{3}", model.Id, sText, state, JsonConvert.SerializeObject(model));
                    if (q.Any(r => r.ParentId.Equals(model.Id)))
                    {
                        jsonAppend.Append(",\"children\":");
                        CreateTreeJson(q, model.Id, node, ref jsonAppend);
                    }
                    jsonAppend.Append("}");
                    if (index < qChild.Count() - 1) jsonAppend.Append(",");
                    index++;
                }
            }
            jsonAppend.Append("]");
        }

        public bool IsExistCode(string code, Guid Id)
        {
            return dal.IsExistCode(code, Id);
        }

        public bool IsExistChild(Guid Id)
        {
            return dal.IsExistChild(Id);
        }

        #endregion
    }
}
