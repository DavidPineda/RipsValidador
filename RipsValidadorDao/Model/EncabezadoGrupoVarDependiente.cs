using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class EncabezadoGrupoVarDependiente
    {
        #region "Propiedades"
        public int idEncabezadoGrupo { get; set; }
        public EstructuraArchivo datosArchivo { get; set; }
        public string descripcion { get; set; }
        public EstadoParametrizacion estado { get; set; }
        #endregion

        #region "Constructores"
        public EncabezadoGrupoVarDependiente()
        {
            this.idEncabezadoGrupo = 0;
            this.datosArchivo = null;
            this.descripcion = string.Empty;
            this.estado = null;
        }
        public EncabezadoGrupoVarDependiente(int idEncabezadoGrupo)
        {
            this.idEncabezadoGrupo = idEncabezadoGrupo;
            this.datosArchivo = null;
            this.descripcion = string.Empty;
            this.estado = null;
        }
        public EncabezadoGrupoVarDependiente(int idEncabezadoGrupo, EstructuraArchivo datosArchivo, string descripcion, EstadoParametrizacion estado)
        {
            this.idEncabezadoGrupo = idEncabezadoGrupo;
            this.datosArchivo = datosArchivo;
            this.descripcion = descripcion;
            this.estado = estado;
        }        
        #endregion

        #region "Metodos"
        public void tableToEncabezadoGrupoVarDependiente(System.Data.DataRow myDataRow)
        {
            if (myDataRow != null)
            {
                try
                {
                    this.idEncabezadoGrupo = Convert.ToInt32(myDataRow["id_enc_grupo"]);
                    this.descripcion = myDataRow["descripcion"].ToString();                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public void tableToEncabezadoGrupoVarDependiente(System.Data.DataRow myDataRow, EstructuraArchivo datosArchivo, EstadoParametrizacion estado)
        {
            if (myDataRow != null)
            {
                try
                {
                    this.idEncabezadoGrupo = Convert.ToInt32(myDataRow["id_enc_grupo"]);
                    this.descripcion = myDataRow["descripcion"].ToString();
                    this.estado = estado;
                    this.datosArchivo = datosArchivo;
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
