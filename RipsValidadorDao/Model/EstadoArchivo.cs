using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class EstadoArchivo
    {
        #region "Propiedades"
        public Int16 codEstdoArchivo { get; set; }
        public string descripcion { get; set; }
        #endregion

        #region "Constructores"
        public EstadoArchivo()
        {
            this.codEstdoArchivo = 0;
            this.descripcion = string.Empty;
        }

        public EstadoArchivo(Int16 codEstdoArchivo, string descripcion)
        {
            this.codEstdoArchivo = codEstdoArchivo;
            this.descripcion = descripcion;
        }
        #endregion

        #region "Metodos"
        public void tableToEstadoArchivo(System.Data.DataRow row)
        {
            if (row != null)
            {
                try
                {
                    this.codEstdoArchivo = Convert.ToInt16(row["value"]);
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
