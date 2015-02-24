using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class Regional
    {
        #region "Propiedades"
        public string codRegional { get; set; }
        public string descripcion { get; set; }
        #endregion

        #region "Constructores"
        public Regional()
        {
            this.codRegional = string.Empty;
            this.descripcion = string.Empty;
        }

        public Regional(string codRegional, string descripcion)
        {
            this.codRegional = codRegional;
            this.descripcion = descripcion;
        }
        #endregion

        #region "Metodos"
        public void tableToRegional(System.Data.DataRow row)
        {
            if (row != null)
            {
                try
                {
                    this.codRegional = row["value"].ToString();
                    this.descripcion = row["text"].ToString();
                }
                catch (InvalidCastException ex)
                {
                    throw ex;
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
