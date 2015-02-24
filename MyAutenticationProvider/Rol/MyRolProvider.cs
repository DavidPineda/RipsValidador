using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using RipsValidadorDao.ConnectionDB.AutenticationProvider;
using System.Data;

namespace MyAutenticationProvider.Rol
{
    class MyRolProvider : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            Insertar i = new Insertar();
            foreach (string usuario in usernames)
            {
                foreach (string rol in roleNames)
                {
                    try
                    {
                        i.agregarUsuarioArol(usuario, rol);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            Consulta c = new Consulta();
            try
            {
                DataTable objDtRoles = c.consultarRoles(); 
                if (objDtRoles == null)
                {
                    return new string[0];
                }
                string[] roles = new string[objDtRoles.Rows.Count];
                int i = 0;
                foreach (DataRow objDrRow in objDtRoles.Rows)
                {
                    //string cadena = objDrRow["value"] + ":" + objDrRow["text"] + ":" + objDrRow["descripcion"];
                    string cadena = objDrRow["text"].ToString();
                    roles[i] = cadena;
                    i++;
                }
                return roles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override string[] GetRolesForUser(string username)
        {
            Consulta c = new Consulta();
            try
            {
                DataTable roles = c.consultarRolesXUsuario(username);
                if (roles == null)
                {
                    return new string[0];
                }
                int i = 0;
                string[] returnRoles = new string[roles.Rows.Count];
                foreach (DataRow row in roles.Rows)
                {
                    returnRoles[i] = row["text"].ToString(); 
                    i++;
                }
                return returnRoles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            Consulta c = new Consulta();
            return c.consultarDatosXrolYusuario(username, roleName).Rows.Count > 0;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
