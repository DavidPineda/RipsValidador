using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class DetalleGrupoDependiente
    {
        #region "Propiedades"
        public EncabezadoGrupoVarDependiente encabezadoGrupo { get; set; }
        public int idGrupo { get; set; }
        public EstadoParametrizacion estado { get; set; }
        public string descripcion { get; set; }
        public VariableDependiente varDependiente { get; set; }
        #endregion

        #region "Constructores"
        public DetalleGrupoDependiente()
        {
            this.idGrupo = 0;
            this.descripcion = string.Empty;
            this.estado = null;
            this.varDependiente = null;
            this.encabezadoGrupo = null;
        }

        public DetalleGrupoDependiente(int idGrupo)
        {
            this.idGrupo = idGrupo;
            this.descripcion = string.Empty;
            this.estado = null;
            this.varDependiente = null;
            this.encabezadoGrupo = null;
        }

        public DetalleGrupoDependiente(int idGrupo, string descripcion)
        {
            this.idGrupo = idGrupo;
            this.descripcion = descripcion;
            this.estado = null;
            this.varDependiente = null;
            this.encabezadoGrupo = null;
        }

        public DetalleGrupoDependiente(int idGrupo, string descripcion, EstadoParametrizacion estado, VariableDependiente varDependiente, 
            EncabezadoGrupoVarDependiente encabezadoGrupo)
        {
            this.idGrupo = idGrupo;
            this.descripcion = descripcion;
            this.estado = estado;
            this.varDependiente = varDependiente;
            this.encabezadoGrupo = encabezadoGrupo;
        }
        #endregion

        #region "Metodos"
        public void tableToDetalleGrupoDependiente(System.Data.DataRow row)
        {
            if (row != null)
            {
                try
                {
                    this.idGrupo = Convert.ToInt32(row["id_grupo"]);
                    this.descripcion = row["descripcion"].ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void tableToDetalleGrupoDependiente(System.Data.DataRow row, EstadoParametrizacion estado, VariableDependiente varDependiente,
            EncabezadoGrupoVarDependiente encabezadoGrupo)
        {
            if (row != null)
            {
                try
                {
                    this.idGrupo = Convert.ToInt32(row["id_grupo"]);
                    this.descripcion = row["descripcion"].ToString();
                    this.estado = estado;
                    this.varDependiente = varDependiente;
                    this.encabezadoGrupo = encabezadoGrupo;
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
