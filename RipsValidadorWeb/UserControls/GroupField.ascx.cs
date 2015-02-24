using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RipsValidadorDao.ConnectionDB.Generales;
using ControlesAsp.DropDownListControl;
using RipsValidadorWeb.Clases;

namespace RipsValidadorWeb.UserControls
{
    public partial class GroupField : System.Web.UI.UserControl
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
            this.DataBinding += new System.EventHandler(this.DataStructFile_DataBinding);
        }
        protected void DataStructFile_DataBinding(object sender, System.EventArgs e)
        {
            cargarEstados();
        }
        #endregion

        #region "Metodos"

        private void cargarEstados() 
        {
            Consulta c = new Consulta();
            try
            {
                DropDownListASP.llenarDropDownList(c.consultarEstadoParametrizacion(), "value", "text", ref this.ddlEstado);
                if (!(DataItem is Telerik.Web.UI.GridInsertionObject))
                {
                    DropDownListASP.selectIndexByValue(ref this.ddlEstado, Convert.ToString(((System.Data.DataRowView)DataItem).Row.ItemArray[3]));
                }
                else
                {
                    DropDownListASP.selectIndexByValue(ref this.ddlEstado, "1");
                }
            }
            catch (Exception ex)
            {
                Logger.generarLogError(ex.Message, new System.Diagnostics.StackFrame(true), ex);
            }
        }
        #endregion

    }
}