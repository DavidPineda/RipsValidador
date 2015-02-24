using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class FormatoFecha
    {
        #region "Propiedades"
        public int codFormatoFecha { get; set; }
        public string descripcion { get; set; }
        #endregion

        #region "Constructores"
        public FormatoFecha()
        {
            this.codFormatoFecha = 0;
            this.descripcion = string.Empty;
        }
        public FormatoFecha(int codFormatoFecha, string descripcion)
        {
            this.codFormatoFecha = codFormatoFecha;
            this.descripcion = descripcion;
        }
        #endregion

        #region "Metodos"
        public void tableToFormatoFecha(System.Data.DataRow myDataRow)
        {
            if (myDataRow != null)
            {
                try
                {
                    this.codFormatoFecha = int.Parse(myDataRow["value"].ToString());
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
