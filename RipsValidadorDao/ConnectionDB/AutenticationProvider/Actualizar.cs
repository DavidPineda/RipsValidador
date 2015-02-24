using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RipsValidadorDao.Model;
using System.Data;

namespace RipsValidadorDao.ConnectionDB.AutenticationProvider
{
    public class Actualizar
    {

        private DataLayer.clsDataServices objDataLayer { get; set; }

        public Actualizar()
        {
            this.objDataLayer = new DataLayer.clsDataServices();
        }

        /// <summary>
        /// Permite Actualizar los datos de un Usuario en el sistema
        /// </summary>
        /// <param name="usuario">Usuario a actualizar</param>
        /// <param name="idUsuarioAct">Id del usuario del sistema que realiza la actualizacion</param>
        public void actualizarUsuario(Usuario usuario, int idUsuarioAct) 
        {
            objDataLayer.AddGenericParameter("@idUsuario", DbType.Int32, ParameterDirection.Input, usuario.idUsuario);
            objDataLayer.AddGenericParameter("@nomUsuario", DbType.String, ParameterDirection.Input, usuario.nomUsuario);
            objDataLayer.AddGenericParameter("@contrasena", DbType.String, ParameterDirection.Input, usuario.contrasena);
            objDataLayer.AddGenericParameter("@numIntentosLogeo", DbType.Int16, ParameterDirection.Input, usuario.numIntentosLogeo);
            objDataLayer.AddGenericParameter("@codIps", DbType.String, ParameterDirection.Input, usuario.codIps);
            objDataLayer.AddGenericParameter("@codSedeIps", DbType.String, ParameterDirection.Input, usuario.codSedeIps);
            objDataLayer.AddGenericParameter("@codRegional", DbType.String, ParameterDirection.Input, usuario.codRegional);
            objDataLayer.AddGenericParameter("@email", DbType.String, ParameterDirection.Input, usuario.email);
            objDataLayer.AddGenericParameter("@estado", DbType.Boolean, ParameterDirection.Input, usuario.estado);
            objDataLayer.AddGenericParameter("@id_usuarioAct", DbType.Int32, ParameterDirection.Input, idUsuarioAct);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_ACTUALIZAR_USUARIO", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Actualizar los datos de un Usuario en el sistema
        /// </summary>
        /// <param name="usuario">Usuario a actualizar</param>
        /// <param name="idUsuarioAct">Id del usuario del sistema que realiza la actualizacion</param>
        /// <param name="idRol">Id del rol que se modifica al usuario</param>
        public void actualizarUsuario(Usuario usuario, int idUsuarioAct, int idRol)
        {
            objDataLayer.AddGenericParameter("@idUsuario", DbType.Int32, ParameterDirection.Input, usuario.idUsuario);
            objDataLayer.AddGenericParameter("@nomUsuario", DbType.String, ParameterDirection.Input, usuario.nomUsuario);
            objDataLayer.AddGenericParameter("@contrasena", DbType.String, ParameterDirection.Input, usuario.contrasena);
            objDataLayer.AddGenericParameter("@numIntentosLogeo", DbType.Int16, ParameterDirection.Input, usuario.numIntentosLogeo);
            objDataLayer.AddGenericParameter("@codIps", DbType.String, ParameterDirection.Input, usuario.codIps);
            objDataLayer.AddGenericParameter("@codSedeIps", DbType.String, ParameterDirection.Input, usuario.codSedeIps);
            objDataLayer.AddGenericParameter("@codRegional", DbType.String, ParameterDirection.Input, usuario.codRegional);
            objDataLayer.AddGenericParameter("@email", DbType.String, ParameterDirection.Input, usuario.email);
            objDataLayer.AddGenericParameter("@estado", DbType.Boolean, ParameterDirection.Input, usuario.estado);
            objDataLayer.AddGenericParameter("@id_usuarioAct", DbType.Int32, ParameterDirection.Input, idUsuarioAct);
            objDataLayer.AddGenericParameter("@idRol", DbType.Int32, ParameterDirection.Input, idRol);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_ACTUALIZAR_USUARIO", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
