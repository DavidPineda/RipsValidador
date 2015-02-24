using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class ProgramacionArchivo
    {
        #region "Propiedades"
        public int idProgramacion { get; set; }
        public DateTime fechaProgramacion { get; set; }
        public DateTime periodoCobro { get; set; }
        public string estadoProceso { get; set; }
        public Regional regional { get; set; }
        public EstadoProgramacion estado { get; set; }
        public TipoContrato contrato { get; set; }
        public Usuario usuario { get; set; }
        #endregion

        #region "Constructores"
        public ProgramacionArchivo()
        {
            this.idProgramacion = 0;
            this.fechaProgramacion = System.DateTime.Now;
            this.periodoCobro = System.DateTime.Now;
            this.estadoProceso = string.Empty;
            this.regional = null;
            this.estado = null;
            this.contrato = null;
            this.usuario = null;
        }

        public ProgramacionArchivo(int idProgramacion, DateTime fechaProgramacion, DateTime periodoCobro, string estadoProceso)
        {
            this.idProgramacion = idProgramacion;
            this.fechaProgramacion = fechaProgramacion;
            this.periodoCobro = periodoCobro;
            this.estadoProceso = estadoProceso;
            this.regional = null;
            this.estado = null;
            this.contrato = null;
            this.usuario = null;
        }
        #endregion

        #region "Metodos"
        public void tableToProgramacionArchivo(System.Data.DataRow row)
        {
            if (row != null)
            {
                try
                {
                    this.idProgramacion = Convert.ToInt32(row["id_programacion"]);
                    this.fechaProgramacion = Convert.ToDateTime(row["fecha_programacion"]);
                    this.periodoCobro = Convert.ToDateTime(row["periodo_cobro"]);
                    this.estadoProceso = row["estado_proceso"].ToString();
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
