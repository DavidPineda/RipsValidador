using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ControlesAsp.DropDownListControl;
using RipsValidadorDao.ConnectionDB.Generales;

namespace RipsValidadorWeb.UserControls
{
    public partial class CruceAfiliadoDet : System.Web.UI.UserControl
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
            this.DataBinding += new System.EventHandler(this.CruceAfiliadoDet_DataBinding);
        }

        protected void CruceAfiliadoDet_DataBinding(object sender, System.EventArgs e)
        {
            cargarCampos();
        }
        #endregion

        #region "Metodos"
        private void cargarCampos()
        {
            cargarColumnas();
            if (!(DataItem is Telerik.Web.UI.GridInsertionObject))
            {
                try
                {
                    DropDownListASP.selectIndexByValue(ref this.ddlEstado, Convert.ToString(((System.Data.DataRowView)DataItem).Row.ItemArray[2]));
                    DropDownListASP.selectIndexByValue(ref this.ddlColumna, Convert.ToString(((System.Data.DataRowView)DataItem).Row.ItemArray[1]));
                    this.ddlColumna.Enabled = false;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void cargarColumnas()
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarColumnaCruce(0), "value", "text", ref this.ddlColumna);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}