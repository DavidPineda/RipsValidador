using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class TipoValor
    {
        #region "Propiedades"
        public int codTipoValor { get; set; }
        public string descripcion { get; set; }
        #endregion

        #region "Constructores"
        public TipoValor()
        {
            this.codTipoValor = 0;
            this.descripcion = string.Empty;
        }

        public TipoValor(int codTipoArchivo, string descripcion)
        {
            this.codTipoValor = codTipoArchivo;
            this.descripcion = descripcion;
        }
        #endregion

        #region "Metodos"
        public void tableToTipoDato(System.Data.DataRow myDataRow)
        {
            if (myDataRow != null)
            {
                try
                {
                    this.codTipoValor = int.Parse(myDataRow["value"].ToString());
                    this.descripcion = (string)myDataRow["text"];
                }
                catch (InvalidCastException ex)
                {
                    throw ex;
                }
            }
        }
        #endregion
    }
}
