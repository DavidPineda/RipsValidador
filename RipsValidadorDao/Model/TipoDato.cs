using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class TipoDato
    {
        #region "Propiedades"
        public int codTipoDato { get; set; }
        public string descripcion { get; set; }
        #endregion

        #region "Constructores"
        public TipoDato()
        {
            this.codTipoDato = 0;
            this.descripcion = string.Empty;
        }
        public TipoDato(int codTipoDato, string descripcion)
        {
            this.codTipoDato = codTipoDato;
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
                    this.codTipoDato = int.Parse(myDataRow["value"].ToString());
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
