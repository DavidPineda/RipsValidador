using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class EstructuraArchivo
    {
        #region "Propiedades"
        public ParametrizacionArchivo parametrizacionArchivo { get; set; }
        public int numeroColumna { get; set; }
        public string nombreColumna { get; set; }
        public string descripcion { get; set; }
        public int longitud { get; set; }
        public int longitudMaxima { get; set; }
        public bool valorRequerido { get; set; }
        public bool validar { get; set; }
        public TipoDato tipoDato { get; set; }
        public EstadoParametrizacion estadoParametrizacion { get; set; }
        public Single rangoIni { get; set; }
        public Single rangoFin { get; set; }
        public FormatoFecha formatoFecha { get; set; }
        #endregion

        #region "Constructores"
        public EstructuraArchivo()
        {
            this.parametrizacionArchivo = null;
            this.numeroColumna = 0;
            this.nombreColumna = string.Empty;
            this.descripcion = string.Empty;
            this.longitud = 0;
            this.longitudMaxima = 0;
            this.valorRequerido = false;
            this.validar = false;
            this.tipoDato = null;
            this.estadoParametrizacion = null;
            this.rangoIni = -1;
            this.rangoFin = -1;
            this.formatoFecha = null;
        }
        public EstructuraArchivo(int numeroColumna, string nombreColumna, string descripcion, int longitud, int longitudMaxima, bool valorRequerido,
            bool validar, Single rangoIni, Single rangoFin)
        {
            this.parametrizacionArchivo = null;
            this.numeroColumna = numeroColumna;
            this.nombreColumna = nombreColumna;
            this.descripcion = descripcion;
            this.longitud = longitud;
            this.longitudMaxima = longitudMaxima;
            this.valorRequerido = valorRequerido;
            this.validar = validar;
            this.tipoDato = null;
            this.estadoParametrizacion = null;
            this.rangoIni = rangoIni;
            this.rangoFin = rangoFin;
            this.formatoFecha = null;
        }
        #endregion

        #region "Metodos"
        public void tableToEstructuraArchivo(System.Data.DataRow myDataRow)
        {
            if (myDataRow != null)
            {
                try
                {
                    this.numeroColumna = (int)myDataRow["NUMERO_COLUMNA"];
                    this.nombreColumna = (string)myDataRow["NOMBRE_COLUMNA"];
                    this.descripcion = (string)myDataRow["DESCRIPCION"];
                    this.longitud = (int)myDataRow["LONGITUD"];
                    this.longitudMaxima = (int)myDataRow["LONGITUD_MAX"];
                    this.valorRequerido = (bool)myDataRow["VALOR_REQUERIDO"];
                    this.validar = (bool)myDataRow["VALIDAR"];
                    if (Convert.ToInt32(myDataRow["RANGO_INI"]) != -1)
                    {
                        this.rangoIni = Single.Parse(myDataRow["RANGO_INI"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("es-CO"));
                    }
                    if (Convert.ToInt32(myDataRow["RANGO_FIN"]) != -1)
                    {
                        this.rangoFin = Single.Parse(myDataRow["RANGO_FIN"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("es-CO"));
                    }                
                }
                catch (InvalidCastException ex)
                {
                    throw ex;
                }
            }
        }

        public void tableToEstructuraArchivo(System.Data.DataRow myDataRow, ParametrizacionArchivo codArchivo, TipoDato tipoDato, 
            EstadoParametrizacion estadoParametrizacion, FormatoFecha formatoFecha)
        {
            if (myDataRow != null)
            {
                try
                {
                    this.numeroColumna = int.Parse(myDataRow["NUMERO_COLUMNA"].ToString());
                    this.nombreColumna = (string)myDataRow["NOMBRE_COLUMNA"];
                    this.descripcion = (string)myDataRow["DESCRIPCION"];
                    this.longitud = int.Parse(myDataRow["LONGITUD"].ToString());
                    this.longitudMaxima = int.Parse(myDataRow["LONGITUD_MAX"].ToString());
                    this.valorRequerido = (bool)myDataRow["VALOR_REQUERIDO"];
                    this.validar = (bool)myDataRow["VALIDAR"];
                    this.rangoIni = Single.Parse(myDataRow["RANGO_INI"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                    this.rangoFin = Single.Parse(myDataRow["RANGO_FIN"].ToString(), System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                    this.parametrizacionArchivo = codArchivo;
                    this.tipoDato = tipoDato;
                    this.estadoParametrizacion = estadoParametrizacion;
                    this.formatoFecha = formatoFecha;
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
