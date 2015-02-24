using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RipsValidadorDao.Model;
using System.Data;
using DataLayer;

namespace RipsValidadorDao.ConnectionDB.AutenticationProvider
{
    public class Borrar
    {
        private DataLayer.clsDataServices objDatos { get; set; }

        public Borrar()
        {
            objDatos = new clsDataServices();
        }

        /// <summary>
        /// Permite eliminar un usuario del sistema
        /// </summary>
        /// <param name="usuario">Usuario que se desea eliminar</param>
        /// <param name="usuarioElimina">Usuario que realiza la eliminacion</param>
        /// <exception cref="Exception">Se retorna Error si no se puede eliminar el registro en la base de datos</exception>
        public void eliminarUsuario(Usuario usuarioEliminar, Usuario usuarioElimina)
        {
            objDatos.AddGenericParameter("@parIdUsuarioD", DbType.Int32, ParameterDirection.Input, usuarioEliminar.idUsuario);
            objDatos.AddGenericParameter("@parIdUserDelete", DbType.Int32, ParameterDirection.Input, usuarioElimina.idUsuario);
            try
            {
                objDatos.ExecuteStoredProcedure("P_RIPS_ELIMINAR_USUARIO", ReturnType.NothingType);
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }
    }
}
