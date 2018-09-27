using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using TygaSoft.Model;
using TygaSoft.BLL;

namespace TygaSoft.Web.Manages
{
    public partial class PrintBarcodeTemplate : System.Web.UI.Page
    {
        //private bool isPrint = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) Bind();
        }

        private void Bind()
        {
            var key = Request.QueryString["key"];
            switch (key)
            {
                case "PrintTemplate-PrintTest":
                    if (!string.IsNullOrWhiteSpace(Request.QueryString["barcodeTemplateId"]))
                    {
                        var barcodeTemplateId = Guid.Empty;
                        if (Guid.TryParse(Request.QueryString["barcodeTemplateId"], out barcodeTemplateId))
                        {
                            var bll = new BarcodeTemplate();
                            var barcodeTemplateInfo = bll.GetModel(barcodeTemplateId);
                            if (barcodeTemplateInfo != null) ltrHtml.Text = barcodeTemplateInfo.Html;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}