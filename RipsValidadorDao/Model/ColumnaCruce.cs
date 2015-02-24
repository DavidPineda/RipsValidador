using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class ColumnaCruce
    {

        #region "Propiedades"
        public int id { get; set; }
        public string descripcion { get; set; }
        public string codigo { get; set; }
        public int estado { get; set; }
        #endregion

        #region "Constructores"
        public ColumnaCruce()
        {
            this.id = 0;
            this.descripcion = string.Empty;
            this.codigo = string.Empty;
            this.estado = 0;
        }

        public ColumnaCruce(int id, string descripcion, string codigo, int estado)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.codigo = codigo;
            this.estado = estado;
        }
        #endregion

        #region "Metodos"
        public void tableToColumnaCruce(System.Data.DataRow row)
        {
            if (row != null)
            {
                try
                {
                    this.id = int.Parse(row["value"].ToString());
                    this.descripcion = row["text"].ToString();
                    this.codigo = row["codigo"].ToString();
                    this.estado = int.Parse(row["estado"].ToString());
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
