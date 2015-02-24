using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using RipsValidadorDao.Model;
using RipsValidadorDao.ConnectionDB.AutenticationProvider;
using System.Data;

namespace MyAutenticationProvider.User
{
    public class MyMembershipProvider
    {
        private Consulta c { get; set; }
        private Insertar i { get; set; }
        private Borrar b { get; set; }
        private Actualizar a { get; set; }

        public MyMembershipProvider()
        {
            this.c = new Consulta();
            this.i = new Insertar();
            this.b = new Borrar();
            this.a = new Actualizar();
        }

        /// <summary>
        /// Permite la creacion de un usuario en el sistema
        /// </summary>
        /// <param name="usuario">Objeto usuario a crear</param>
        /// <exception cref="Exception">Se retorna Error si no se puede crear el registro en la base de datos</exception>
        public void crearUsuario(Usuario usuario, int usuarioCrea) 
        {
            try
            {
                i.crearUsuario(usuario, usuarioCrea);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite eliminar un usuario del sistema
        /// </summary>
        /// <param name="usuario">Usuario que se desea eliminar</param>
        /// <param name="usuarioElimina">Usuario Administrador que elimina a otro usuario</param>
        /// <exception cref="Exception">Se retorna Error si no se puede eliminar el registro en la base de datos</exception>
        public void eliminarUsuario(Usuario usuarioEliminar, Usuario usuarioElimina)
        {
            try
            {
                b.eliminarUsuario(usuarioEliminar, usuarioElimina);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar los datos de un usuario del sistema
        /// </summary>
        /// <param name="nomUsuario">Nombre de usuario a consultar</param>
        /// <returns>Usuario encontrado</returns>
        public Usuario consultarUsuarioXnombre(string nomUsuario)
        {
            try
            {
                return c.consultarUsuarioXnombre(nomUsuario);
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
        public Usuario consultarUsarioAutenticacion(string nomUsuario, string password)
        {
            try
            {
                return c.consultarUsuarioAutenticacion(nomUsuario, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Actualizar los datos de un usuario
        /// </summary>
        /// <param name="usuario">Usuario con los datos actualizados</param>
        /// <param name="idUsuarioAct">Usuario que realiza la actualizacion</param>
        public void actualizarUsuario(Usuario usuario, int idUsuarioAct)
        {
            try
            {
                a.actualizarUsuario(usuario, idUsuarioAct);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Actualizar los datos de un usuario
        /// </summary>
        /// <param name="usuario">Usuario con los datos actualizados</param>
        /// <param name="idUsuarioAct">Usuario que realiza la actualizacion</param>
        /// <param name="idRol">Rol que se modifica al usuario</param>
        public void actualizarUsuario(Usuario usuario, int idUsuarioAct, int idRol)
        {
            try
            {
                a.actualizarUsuario(usuario, idUsuarioAct, idRol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retorna los roles del sistema
        /// </summary>
        /// <returns>Datatable con los roles del sistema</returns>
        public DataTable consultarRoles()
        {
            try
            {
                return c.consultarRoles();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}