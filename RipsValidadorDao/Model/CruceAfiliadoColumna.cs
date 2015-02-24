using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class CruceAfiliadoColumna
    {
        #region "Propiedades"
        public ColumnaCruce columnaCruce { get; set; }
        public CruceAfiliado cruceAfiliado { get; set; }
        public Int16 estado { get; set; }
        public string descEstado { get; set; }
        #endregion

        #region "Constructores"
        public CruceAfiliadoColumna()
        {
            this.columnaCruce = null;
            this.cruceAfiliado = null;
            this.estado = 0;
            this.descEstado = string.Empty;
        }

        public CruceAfiliadoColumna(ColumnaCruce c1, CruceAfiliado c2, Int16 estado)
        {
            this.columnaCruce = columnaCruce;
            this.cruceAfiliado = cruceAfiliado;
            this.estado = estado;
            this.descEstado = string.Empty;
        }
        #endregion

        #region "Metodos"
        public void tableToCruceAfiliadoColumna(System.Data.DataRow row)
        {
            if (row != null)
            {
                try
                {
                    this.estado = Convert.ToInt16(row["estado_cruce"]);
                    this.descEstado = row["desc_estado"].ToString();
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
