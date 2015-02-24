using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class EstadoParametrizacion
    {
        #region "Propiedades"
        public int codEstado { get; set; }
        public string descripcion { get; set; }
        #endregion

        #region "Constructores"
        public EstadoParametrizacion()
        {
            this.codEstado = 0;
            this.descripcion = string.Empty;
        }

        public EstadoParametrizacion(int codEstado, string descripcion)
        {
            this.codEstado = codEstado;
            this.descripcion = descripcion;
        }
        #endregion

        #region "Metodos"
        public void tableToEstadoParametrizacion(System.Data.DataRow myDataRow)
        {
            if (myDataRow != null)
            {
                try
                {
                    this.codEstado = int.Parse(myDataRow["value"].ToString());
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
