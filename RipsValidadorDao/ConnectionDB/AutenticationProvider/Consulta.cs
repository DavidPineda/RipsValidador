using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using RipsValidadorDao.Model;
using System.Data;

namespace RipsValidadorDao.ConnectionDB.AutenticationProvider
{
    public class Consulta
    {
        private DataLayer.clsDataServices objDatos { get; set; }

        public Consulta()
        {
            this.objDatos = new DataLayer.clsDataServices();
        }

        /// <summary>
        /// Consultar los datos de un usuario por nombre de usuario
        /// </summary>
        /// <param name="nomUsuario">Nombre del usuario a consultar</param>
        /// <returns></returns>
        public Usuario consultarUsuarioXnombre(string nomUsuario)
        {
            Usuario user = new Usuario();
            try
            {
                objDatos.AddGenericParameter("@nomUsuario", DbType.String, ParameterDirection.Input, nomUsuario);
                user.tableToUser((System.Data.DataRow)objDatos.ExecuteStoredProcedure("P_RIPS_CONSULTAR_USUARIO_X_NOMBRE", ReturnType.DatarowType));
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta todos los roles del sistema
        /// </summary>
        /// <returns>Retorna todos los roles del sistema en un datatable</returns>
        public DataTable consultarRoles()
        {
            try
            {
                return (DataTable)objDatos.ExecuteStoredProcedure("P_RIPS_CONSULTAR_ROLES", ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar un usuario por nombre y password
        /// </summary>
        /// <param name="nomUsuario">Nombre del usuario a consultar</param>
        /// <param name="password">Password del usuario a consultar</param>
        /// <returns></returns>
        public Usuario consultarUsuarioAutenticacion(string nomUsuario, string password)
        {
            Usuario user = new Usuario();
            try
            {
                objDatos.AddGenericParameter("@nomUsuario", DbType.String, ParameterDirection.Input, nomUsuario);
                objDatos.AddGenericParameter("@parContrasena", DbType.String, ParameterDirection.Input, password);
                user.tableToUser((System.Data.DataRow)objDatos.ExecuteStoredProcedure("P_RIPS_CONSULTAR_USUARIO_X_NOMBRE", ReturnType.DatarowType));
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consultar todos los roles de un usuario en el sistema
        /// </summary>
        /// <param name="nomUsuario">Nombre de usuario del cual se consulta</param>
        /// <returns>Datatable con todos los roles que tiene un usuario</returns>
        public DataTable consultarRolesXUsuario(string nomUsuario)
        {
            try
            {
                objDatos.AddGenericParameter("@parNomUsuario", DbType.String, ParameterDirection.Input, nomUsuario);
                return (DataTable)objDatos.ExecuteStoredProcedure("P_RIPS_CONSULTAR_ROL_X_USUARIO", ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta los datos de un usario con un rol especifico
        /// </summary>
        /// <param name="nomUsuario">Nombre del usuario</param>
        /// <param name="nomRol">Nombre del rol del usuario</param>
        /// <returns>DataTable Con el resultado de la consulta</returns>
        public DataTable consultarDatosXrolYusuario(string nomUsuario, string nomRol)
        {
            objDatos.AddGenericParameter("@parNomUsuario", DbType.String, ParameterDirection.Input, nomUsuario);
            objDatos.AddGenericParameter("@parNomRol", DbType.String, ParameterDirection.Input, nomRol);
            try
            {
                return (DataTable)objDatos.ExecuteStoredProcedure("P_RIPS_CONSULTAR_ROL_X_USUARIO", ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
