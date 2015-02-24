using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class ResultadoValidacionDetalle
    {
        #region "Propiedades"
        public ArchivoCargado archivo { get; set; }
        public int cantRegTotales { get; set; }
        public int cantRegValidos { get; set; }
        public int cantRegError { get; set; }
        public string rutaArchivoError { get; set; }
        #endregion

        #region "Constructores"
        public ResultadoValidacionDetalle()
        {
            this.archivo = null;
            this.cantRegTotales = 0;
            this.cantRegValidos = 0;
            this.cantRegError = 0;
            this.rutaArchivoError = string.Empty;
        }

        public ResultadoValidacionDetalle(ArchivoCargado archivo, int cantRegTotales, int cantRegValidos,
                                            int cantRegError, string rutaArchivoError)
        {
            this.archivo = archivo;
            this.cantRegTotales = cantRegTotales;
            this.cantRegValidos = cantRegValidos;
            this.cantRegError = cantRegError;
            this.rutaArchivoError = rutaArchivoError;
        }
        #endregion

        #region "Metodos"
        public void tableToResultadoValidacionDetalle(System.Data.DataRow row)
        {
            if (row != null)
            {
                try
                {
                    this.cantRegTotales = Convert.ToInt32(row["cant_reg_totales"]);
                    this.cantRegValidos = Convert.ToInt32(row["cant_reg_validos"]);
                    this.cantRegError = Convert.ToInt32(row["cant_reg_error"]);
                    this.rutaArchivoError = row["ruta_archivo_error"].ToString();
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
