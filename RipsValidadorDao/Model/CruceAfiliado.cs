using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class CruceAfiliado
    {

        #region "Propiedades"
        public int id { get; set; }
        public string descripcion { get; set; }
        public int prioridad { get; set; }
        public Int16 estado { get; set; }
        public string descEstado { get; set; }
        #endregion

        #region "Constructores"
        public CruceAfiliado()
        {
            this.id = 0;
            this.descripcion = string.Empty;
            this.prioridad = 0;
            this.estado = 0;
            this.descEstado = string.Empty;
        }

        public CruceAfiliado(int id)
        {
            this.id = id;
            this.descripcion = string.Empty;
            this.prioridad = 0;
            this.estado = 0;
            this.descEstado = string.Empty;
        }
        public CruceAfiliado(int id, string descripcion, int prioridad, Int16 estado)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.prioridad = prioridad;
            this.estado = estado;
            this.descEstado = string.Empty;
        }
        public CruceAfiliado(int id, string descripcion, int prioridad, Int16 estado, string descEstado)
        {
            this.id = id;
            this.descripcion = descripcion;
            this.prioridad = prioridad;
            this.estado = estado;
            this.descEstado = string.Empty;
            this.descEstado = descEstado;
        }
        #endregion

        #region "Metodos"
        public void tableToEstadoCruces(System.Data.DataRow row)
        {
            if (row != null)
            {
                try
                {
                    this.id = Convert.ToInt32(row["id"]);
                    this.descripcion = row["descripcion"].ToString();
                    this.prioridad = Convert.ToInt32(row["prioridad"]);
                    this.estado = Convert.ToInt16(row["estado"]);
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
