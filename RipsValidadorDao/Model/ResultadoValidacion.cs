using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class ResultadoValidacion
    {
        #region "Propiedades"
        public ProgramacionArchivo programacion { get; set; }
        public DateTime fecProcesoIni { get; set; }
        public DateTime fecProcesoFin { get; set; }
        public int cantArchivosError { get; set; }
        public int cantArchivosProcesados { get; set; }
        public int cantArchivosValidos { get; set; }
        #endregion

        #region "Constructor"
        public ResultadoValidacion()
        {
            this.programacion = null;
            this.fecProcesoIni = System.DateTime.Now;
            this.fecProcesoFin = System.DateTime.Now;
            this.cantArchivosError = 0;
            this.cantArchivosProcesados = 0;
            this.cantArchivosValidos = 0;
        }

        public ResultadoValidacion(ProgramacionArchivo programacion, DateTime fecProcesoIni, DateTime fecProcesoFin,
                                    int cantArchivosError, int cantArchivosProcesados, int cantArchivosValidos)
        {
            this.programacion = programacion;
            this.fecProcesoIni = fecProcesoIni;
            this.fecProcesoFin = fecProcesoFin;
            this.cantArchivosError = cantArchivosError;
            this.cantArchivosProcesados = cantArchivosProcesados;
            this.cantArchivosValidos = cantArchivosValidos;
        }

        #endregion

        #region "Metodos"

        public void tableToResultadoValidacion(System.Data.DataRow row)
        {
            if (row != null)
            {
                try
                {
                    this.fecProcesoIni = DateTime.Parse(row["fecha_proceso_ini"].ToString());
                    this.fecProcesoFin = DateTime.Parse(row["fecha_proceso_fin"].ToString());
                    this.cantArchivosError = Convert.ToInt32(row["cant_arch_error"]);
                    this.cantArchivosProcesados = Convert.ToInt32(row["cant_arch_procesados"]);
                    this.cantArchivosValidos = Convert.ToInt32(row["cant_arch_validos"]);
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
