using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class DatosEstructuraArchivo
    {
        #region "Propiedades"
        public int idValPermitido { get; set; }
        public EstructuraArchivo estructuraArchivo { get; set; }
        public string valor { get; set; }
        public string descripcion { get; set; }
        public TipoValor tipoValor { get; set; }
        #endregion

        #region "Constructores"
        public DatosEstructuraArchivo()
        {
            this.idValPermitido = 0;
            this.estructuraArchivo = null;
            this.valor = string.Empty;
            this.descripcion = string.Empty;
            this.tipoValor = null;
        }
        public DatosEstructuraArchivo(int idValPermitido, string valor, string descripcion)
        {
            this.idValPermitido = idValPermitido;
            this.estructuraArchivo = null;
            this.valor = valor;
            this.descripcion = descripcion;
            this.tipoValor = null;
        }
        public DatosEstructuraArchivo(int idValPermitido, string valor, string descripcion, EstructuraArchivo estructuraArchivo, TipoValor tipoValor)
        {
            this.idValPermitido = idValPermitido;
            this.estructuraArchivo = estructuraArchivo;
            this.valor = valor;
            this.descripcion = descripcion;
            this.tipoValor = tipoValor;
        }
        #endregion

        #region "Metodos"
        public void tableToDatosEstructuraArchivo(System.Data.DataRow myDataRow)
        {
            if (myDataRow != null)
            {
                try
                {
                    this.idValPermitido = int.Parse(myDataRow["ID_VAL_PERMITIDO"].ToString());
                    this.valor = (string)myDataRow["VALOR"];
                    this.descripcion = (string)myDataRow["DESCRIPCION"];
                }
                catch (InvalidCastException ex)
                {
                    throw ex;
                }
            }
        }
        public void tableToDatosEstructuraArchivo(System.Data.DataRow myDataRow, EstructuraArchivo estructuraArchivo, TipoValor tipoValor)
        {
            if (myDataRow != null)
            {
                try
                {
                    this.idValPermitido = int.Parse(myDataRow["ID_VAL_PERMITIDO"].ToString());
                    this.valor = (string)myDataRow["VALOR"];
                    this.descripcion = (string)myDataRow["DESCRIPCION"];
                    this.estructuraArchivo = estructuraArchivo;
                    this.tipoValor = tipoValor;
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
