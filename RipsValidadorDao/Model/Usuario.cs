using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class Usuario
    {
        #region "Propiedades"
        public int idUsuario { get; set; }
        public string nomUsuario { get; set; }
        public string contrasena { get; set; }
        public Int16 numIntentosLogeo { get; set; }
        public string codIps { get; set; }
        public string codSedeIps { get; set; }
        public string codRegional { get; set; }
        public bool estado { get; set; }
        public string email { get; set; }
        #endregion

        #region "Constructores"
        public Usuario()
        {
            this.idUsuario = -1;
            this.nomUsuario = "";
            this.contrasena = "";
            this.numIntentosLogeo = 0;
            this.codIps = "";
            this.codSedeIps = "";
            this.codRegional = "";
            this.estado = false;
            this.email = "";
        }

        public Usuario(int idUsuario, string nomUsuario, string contrasena, Int16 numIntentosLogeo, string codIps, string codSedeIps, string codRegional, string email, bool estado)
        {
            this.idUsuario = idUsuario;
            this.nomUsuario = nomUsuario;
            this.contrasena = contrasena;
            this.numIntentosLogeo = numIntentosLogeo;
            this.codIps = codIps;
            this.codSedeIps = codSedeIps;
            this.codRegional = codRegional;
            this.email = email;
            this.estado = estado;
        }

        #endregion

        #region "Metodos"
        public void tableToUser(System.Data.DataRow myTable)
        {
            if (myTable == null)
            {
                this.idUsuario = -1;
                this.nomUsuario = "";
                this.contrasena = "";
                this.numIntentosLogeo = 0;
                this.codIps = "";
                this.codSedeIps = "";
                this.codRegional = "";
                this.estado = false;
                this.email = "";
            }
            else
            {
                this.idUsuario = (int)myTable["ID_USUARIO"];
                this.nomUsuario = (string)myTable["NOM_USUARIO"];
                this.contrasena = (string)myTable["CONTRASENA"];
                this.numIntentosLogeo = (Int16)myTable["NUM_INTENTOS_LOGUEO"];
                this.codIps = (string)myTable["COD_IPS"];
                this.codSedeIps = (string)myTable["COD_SEDE_IPS"];
                this.codRegional = (string)myTable["COD_REGIONAL"];
                this.email = (string)myTable["EMAIL"];
                this.estado = (bool)myTable["ESTADO"];
            }
        }
        #endregion
    }
}
