using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class VariableDependiente
    {
        #region "Propiedades"
        public int idVariableDependiente { get; set; }
        public DatosEstructuraArchivo estructuraDep { get; set; }
        public DatosEstructuraArchivo estructuraCru { get; set; }
        public TipoComparacion tipoComparacionDep { get; set; }
        public TipoComparacion tipoComparacionCru { get; set; }
        public string otroValorDep { get; set; }
        public string otroValorCru { get; set; }
        public string mensajeError { get; set; }
        public Int16 estado { get; set; }
        #endregion

        #region "Constructores"
        public VariableDependiente()
        {
            this.idVariableDependiente = 0;
            this.estructuraCru = null;
            this.estructuraDep = null;
            this.tipoComparacionCru = null;
            this.tipoComparacionDep = null;
            this.otroValorDep = string.Empty;
            this.otroValorCru = string.Empty;
            this.mensajeError = string.Empty;
        }

        public VariableDependiente(int idVariableDependiente)
        {
            this.idVariableDependiente = idVariableDependiente;
            this.estructuraCru = null;
            this.estructuraDep = null;
            this.tipoComparacionCru = null;
            this.tipoComparacionDep = null;
            this.otroValorDep = string.Empty;
            this.otroValorCru = string.Empty;
            this.mensajeError = string.Empty;
            this.estado = 0;
        }
        public VariableDependiente(int idVariableDependiente, string mensajeError)
        {
            this.idVariableDependiente = idVariableDependiente;
            this.estructuraCru = null;
            this.estructuraDep = null;
            this.tipoComparacionCru = null;
            this.tipoComparacionDep = null;
            this.otroValorDep = string.Empty;
            this.otroValorCru = string.Empty;
            this.mensajeError = mensajeError;
            this.estado = 0;
        }
        public VariableDependiente(int idVariableDependiente, string mensajeError, Int16 estado)
        {
            this.idVariableDependiente = idVariableDependiente;
            this.estructuraCru = null;
            this.estructuraDep = null;
            this.tipoComparacionCru = null;
            this.tipoComparacionDep = null;
            this.otroValorDep = string.Empty;
            this.otroValorCru = string.Empty;
            this.mensajeError = mensajeError;
            this.estado = estado;
        }
        public VariableDependiente(int idVariableDependiente, string mensajeError, Int16 estado, TipoComparacion tipoComparacionDep, TipoComparacion tipoComparacionCru)
        {
            this.idVariableDependiente = idVariableDependiente;
            this.estructuraCru = null;
            this.estructuraDep = null;
            this.mensajeError = mensajeError;
            this.tipoComparacionCru = tipoComparacionCru;
            this.tipoComparacionDep = tipoComparacionDep;
            this.otroValorDep = string.Empty;
            this.otroValorCru = string.Empty;
            this.estado = estado;
        }
        public VariableDependiente(int idVariableDependiente, string mensajeError, Int16 estado, TipoComparacion tipoComparacionDep, TipoComparacion tipoComparacionCru,
            DatosEstructuraArchivo estructuraDep, DatosEstructuraArchivo estructuraCru)
        {
            this.idVariableDependiente = idVariableDependiente;
            this.estructuraCru = estructuraCru;
            this.estructuraDep = estructuraDep;
            this.mensajeError = mensajeError;
            this.tipoComparacionCru = tipoComparacionCru;
            this.tipoComparacionDep = tipoComparacionDep;
            this.otroValorDep = string.Empty;
            this.otroValorCru = string.Empty;
            this.estado = estado;
        }
        #endregion

        #region "Metodos"
        public void tableToVaribaleDependiente(System.Data.DataRow myDataRow)
        {
            if (myDataRow != null)
            {
                try
                {
                    this.idVariableDependiente = Convert.ToInt32(myDataRow["id_var_dependiente"]);
                    this.mensajeError = myDataRow["mensaje_error"].ToString();
                    this.estado = Convert.ToInt16(myDataRow["estado_var_dependiente"]);
                    this.otroValorDep = myDataRow["otro_valor_dep"].ToString();
                    this.otroValorCru = myDataRow["otro_valor_cru"].ToString();
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
