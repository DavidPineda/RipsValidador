using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class TipoComparacion
    {
        #region "Propiedades"
        public int codOperadorLogico { get; set; }
        public string descripcion { get; set; }
        public string signo { get; set; }

        #endregion

        #region "Constructores"
        public TipoComparacion()
        {
            this.codOperadorLogico = 0;
            this.descripcion = string.Empty;
            this.signo = string.Empty;
        }

        public TipoComparacion(int codOperadorLogico, string descripcion, string simbolo)
        {
            this.codOperadorLogico = codOperadorLogico;
            this.descripcion = descripcion;
            this.signo = simbolo;
        }
        #endregion

        #region "Metodos"
        public void tableToTipoComparacion(System.Data.DataRow myDataRow)
        {
            if (myDataRow != null)
            {
                try
                {
                    this.codOperadorLogico = Convert.ToInt16(myDataRow["value"]);
                    this.descripcion = (string)myDataRow["text"];
                    this.signo = (string)myDataRow["signo"];
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
