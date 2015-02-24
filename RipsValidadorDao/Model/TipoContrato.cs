using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class TipoContrato
    {
        #region "Propiedades"
        public string codTipoContrato { get; set; }
        public string descripcion { get; set; }
        #endregion

        #region "Constructores"
        public TipoContrato()
        {
            this.codTipoContrato = string.Empty;
            this.descripcion = string.Empty;
        }

        public TipoContrato(string codTipoContrato, string descripcion)
        {
            this.codTipoContrato = codTipoContrato;
            this.descripcion = descripcion;
        }
        #endregion

        #region "Metodos"
        public void tableToTipoContrato(System.Data.DataRow row)
        {
            if (row != null)
            {
                try
                {
                    this.codTipoContrato = row["value"].ToString();
                    this.descripcion = row["text"].ToString();
                }
                catch (InvalidCastException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion
    }
}
