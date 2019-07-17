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
    public partial class ContentType
    {
        #region ContentType Member

        public string GetTreeJson()
        {
            StringBuilder jsonAppend = new StringBuilder();
            var list = dal.GetList().ToList<ContentTypeInfo>();
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

        private void CreateTreeJson(IEnumerable<ContentTypeInfo> q, object parentId, ContentTypeInfo rootNode, ref StringBuilder jsonAppend)
        {
            jsonAppend.Append("[");
            var qChild = q.Where(x => x.ParentId.Equals(parentId));
            if (qChild != null && qChild.Count() > 0)
            {
                int index = 0;
                foreach (var model in qChild)
                {
                    var state = (model.Id == rootNode.Id) ? "open" : "closed";
                    var sText = model.ParentId.Equals(Guid.Empty) ? model.Named : string.Format("{0}（{1}）", model.Coded, model.Named);
                    jsonAppend.AppendFormat(@"{{""id"":""{0}"",""text"":""{1}"",""state"":""{2}"",""attributes"":{3}", model.Id, sText, state, JsonConvert.SerializeObject(model));
                    if (q.Any(r => r.ParentId.Equals(model.Id)))
                    {
                        jsonAppend.Append(",\"children\":");
                        CreateTreeJson(q, model.Id, rootNode, ref jsonAppend);
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
