using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;
using System.Data;
using RipsValidadorDao.Model;

namespace RipsValidadorDao.ConnectionDB.AutenticationProvider
{
    public class Insertar
    {
        public DataLayer.clsDataServices objDatos {get; set;} 

        public Insertar()
        {
            this.objDatos = new DataLayer.clsDataServices();
        }

        /// <summary>
        /// Permite la creacion de un usuario en el sistema
        /// </summary>
        /// <param name="usuario">Objeto usuario a crear</param>
        /// <exception cref="Exception">Se retorna Error si no se puede crear el registro en la base de datos</exception>
        public void crearUsuario(Usuario usuario, int usuarioCrea)
        {
            objDatos.AddGenericParameter("@nomUsuario", DbType.String, ParameterDirection.Input, usuario.nomUsuario);
            objDatos.AddGenericParameter("@contrasena", DbType.String, ParameterDirection.Input, usuario.contrasena);
            objDatos.AddGenericParameter("@numIntentosLogeo", DbType.Int16, ParameterDirection.Input, usuario.numIntentosLogeo);
            objDatos.AddGenericParameter("@codIps", DbType.String, ParameterDirection.Input, usuario.codIps);
            objDatos.AddGenericParameter("@codSedeIps", DbType.String, ParameterDirection.Input, usuario.codSedeIps);
            objDatos.AddGenericParameter("@codRegional", DbType.String, ParameterDirection.Input, usuario.codRegional);
            objDatos.AddGenericParameter("@email", DbType.String, ParameterDirection.Input, usuario.email);
            objDatos.AddGenericParameter("@estado", DbType.Boolean, ParameterDirection.Input, usuario.estado);
            objDatos.AddGenericParameter("@idUsuarioCrea", DbType.Int32, ParameterDirection.Input, usuarioCrea);
            try
            {
                objDatos.ExecuteStoredProcedure("P_RIPS_INSERTAR_USUARIO", ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Agrega usuario al rol especificado
        /// </summary>
        /// <param name="idUsuario">Nombre del usuario que se desea agregar al rol</param>
        /// <param name="idRol">Nombre del rol al que se desea asignar el usuario</param>
        public void agregarUsuarioArol(string nomUsuario, string nomRol)
        {
            objDatos.AddGenericParameter("@parNomUsuario", DbType.String, ParameterDirection.Input, nomUsuario);
            objDatos.AddGenericParameter("@parNomRol", DbType.String, ParameterDirection.Input, nomRol);
            try
            {
                objDatos.ExecuteStoredProcedure("P_RIPS_ADICIONAR_USUARIO_A_ROL", ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
