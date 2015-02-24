using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RipsValidadorWeb.UserControls
{
    public partial class ExtensionAdd : System.Web.UI.UserControl
    {
        #region "Propiedades"
        private object _dataItem = null;

        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }
        #endregion

        #region "Eventos"
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///          Required method for Designer support - do not modify
        ///          the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DataBinding += new System.EventHandler(this.ExtensionAdd_DataBinding);
        }
        protected void ExtensionAdd_DataBinding(object sender, System.EventArgs e)
        {
            habilitarCampos();
        }
 
        #endregion

        #region "Metodos"
        private void habilitarCampos()
        {
            if (!(DataItem is Telerik.Web.UI.GridInsertionObject))
            {
                this.txtExtension.Enabled = false;
            }
        }
        #endregion
    }
}