using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.UI;
#if Telerik
using Telerik.Web.UI;
#endif

namespace ControlesAsp.Panel
{
    public static class PanelASP
    {

        #if Telerik
        public static void limpiarPanel(ref System.Web.UI.WebControls.Panel myPanel)
        {
            if (myPanel == null)
            {
                throw new ArgumentNullException("El argumento myPanel no puede estar Nulo");
            }
            if (myPanel.HasControls())
            {
                foreach (Control c in myPanel.Controls)
                {
                    switch (c.GetType().FullName)
                    {
                        case "Telerik.Web.UI.RadTextBox":
                            ((RadTextBox)c).Text = string.Empty;
                            break;
                        case "Telerik.Web.UI.RadNumericTextBox":
                            ((RadNumericTextBox)c).Text = string.Empty;
                            break;
                        case "System.Web.UI.WebControls.CheckBox":
                            ((CheckBox)c).Checked = false;
                            break;
                        case "Telerik.Web.UI.RadDropDownList":
                            ((RadDropDownList)c).Items.Clear();
                            break;
                        case "Telerik.Web.UI.RadComboBox":
                            ((RadComboBox)c).Items.Clear();
                            break;
                        //case "System.Web.UI.WebControls.Label":
                        //    ((Label)c).Text = string.Empty;
                        //    break;
                        case "System.Web.UI.WebControls.TextBox":
                            ((TextBox)c).Text = string.Empty;
                            break;
                    }
                }
            }
        }
        #endif

    }
}
