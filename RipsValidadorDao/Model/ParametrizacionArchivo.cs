using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class ParametrizacionArchivo
    {
        #region "Propiedades"
        public string codArchivo { get; set; }
        public string descripcion { get; set; }
        public int cantColumnas { get; set; }
        public string separador { get; set; }
        public int tamMaximoCargue { get; set; }
        public string rutaCargueArchivo { get; set; }
        public bool isCargueObligatorio { get; set; }
        public int lngMaximaNombre { get; set; }
        public int lngMinimaNombre { get; set; }

        #endregion

        #region "Constructores"
        public ParametrizacionArchivo()
        {
            this.codArchivo = string.Empty;
            this.descripcion = string.Empty;
            this.cantColumnas = 0;
            this.separador = string.Empty;
            this.tamMaximoCargue = 0;
            this.rutaCargueArchivo = string.Empty;
            this.isCargueObligatorio = true;
            this.lngMaximaNombre = 0;
            this.lngMinimaNombre = 0;
        }

        public ParametrizacionArchivo(string codArchivo, string descripcion, int cantColumnas, string separador, int tamMaximoCargue, string rutaCargueArchivo,
            bool isCargueObligatorio, int lngMaximaNombre, int lngMinimaNombre)
        {
            this.codArchivo = codArchivo;
            this.descripcion = descripcion;
            this.cantColumnas = cantColumnas;
            this.separador = separador;
            this.tamMaximoCargue = tamMaximoCargue;
            this.rutaCargueArchivo = rutaCargueArchivo;
            this.isCargueObligatorio = isCargueObligatorio;
            this.lngMaximaNombre = lngMaximaNombre;
            this.lngMinimaNombre = lngMinimaNombre;
        }
        #endregion

        #region "Metodos"
        public void TableToParametrizacionArchivo(System.Data.DataRow myDataRow)
        {
            if(myDataRow != null)
            try
            {
                this.codArchivo = (string)myDataRow["COD_ARCHIVO"];
                this.descripcion = (string)myDataRow["DESCRIPCION"];
                this.cantColumnas = (int)myDataRow["CANT_COLUMNAS"];
                this.separador = (string)myDataRow["SEPARADOR"];
                this.tamMaximoCargue = (int)myDataRow["TAM_MAX_CARGUE"];
                this.rutaCargueArchivo = (string)myDataRow["RUTA_CARGUE_ARCHIVO"];
                this.isCargueObligatorio = (bool)myDataRow["CARGUE_OBLIGATORIO"];
                this.lngMaximaNombre = (int)myDataRow["LONG_MAX_NOM_ARCHIVO"];
                this.lngMinimaNombre = (int)myDataRow["LONG_MIN_NOM_ARCHIVO"];
            }
            catch (InvalidCastException ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
