using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class EstadoProgramacion
    {
        #region "Propiedades"
        public Int16 codEstadoCargue { get; set; }
        public string descripcion { get; set; }
        #endregion

        #region "Constructores"
        public EstadoProgramacion()
        {
            this.codEstadoCargue = 0;
            this.descripcion = string.Empty;
        }

        public EstadoProgramacion(Int16 codEstadoCargue, string descripcion)
        {
            this.codEstadoCargue = codEstadoCargue;
            this.descripcion = descripcion;
        }
        #endregion

        #region "Metodos"
        public void tableToEstadoCargue(System.Data.DataRow row)
        {
            if (row != null)
            {
                try
                {
                    this.codEstadoCargue = Convert.ToInt16(row["value"]);
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
