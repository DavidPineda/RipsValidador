using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace RipsValidadorWeb.Account
{
    public partial class AccountIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Image1.ImageUrl = ConfigurationManager.AppSettings["LOGO_EMPRESA"];
        }
    }
}