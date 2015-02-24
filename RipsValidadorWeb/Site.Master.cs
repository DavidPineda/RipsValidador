using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxPro;
using System.Configuration;

namespace RipsValidadorWeb
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Image1.ImageUrl = ConfigurationManager.AppSettings["IMAGEN_QDATA"];
        }

    public void validarSession()
    {
        HttpContext ctx = HttpContext.Current;
        HttpCookie cookie = ctx.Request.Cookies["ASP.NET_SessionId"];
        cookie.Value = String.Empty;
    }
  
    }
}
