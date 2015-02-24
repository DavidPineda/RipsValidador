using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{

    public class ArchivoCargado
    {
        #region "Propiedades"
        public ProgramacionArchivo programacion { get; set; }
        public int consecutivo { get; set; }
        public ParametrizacionArchivo archivo { get; set; }
        public string nombreArchivo { get; set; }
        public string rutaArchivo { get; set; }
        public EstadoArchivo estadoArchivo { get; set; }
        public string estadoProceso { get; set; }
        #endregion

        #region "Contructores"
        public ArchivoCargado()
        {
            this.programacion = null;
            this.consecutivo = 0;
            this.archivo = null;
            this.nombreArchivo = string.Empty;
            this.rutaArchivo = string.Empty;
            this.estadoArchivo = null;
            this.estadoProceso = string.Empty;
        }

        public ArchivoCargado(int consecutivo, string nombreArchivo, string rutaArchivo, string estadoProceso)
        {
            this.programacion = null;
            this.consecutivo = consecutivo;
            this.archivo = null;
            this.nombreArchivo = nombreArchivo;
            this.rutaArchivo = rutaArchivo;
            this.estadoArchivo = null;
            this.estadoProceso = estadoProceso;
        }
        #endregion

        #region "Metodos"
        public void tableToArchivoCargado(System.Data.DataRow row)
        {
            if (row != null)
            {
                try
                {
                    this.consecutivo = Convert.ToInt32(row["consecutivo"]);
                    this.nombreArchivo = row["nombre_archivo"].ToString();
                    this.rutaArchivo = row["ruta_archivo"].ToString();
                    this.estadoProceso = row["estado_archivo"].ToString();
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
