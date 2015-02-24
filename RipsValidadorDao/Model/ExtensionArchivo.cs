using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class ExtensionArchivo
    {
        #region "Propiedades"
        public Int16 idExtension { get; set; }
        public string extension { get; set; }
        public string descripcion { get; set; }
        #endregion

        #region "Constructores"
        public ExtensionArchivo()
        {
            this.idExtension = 0;
            this.extension = string.Empty;
            this.descripcion = string.Empty;
        }

        public ExtensionArchivo(Int16 idExtension)
        {
            this.idExtension = idExtension;
            this.extension = string.Empty;
            this.descripcion = string.Empty;
        }
        public ExtensionArchivo(Int16 idExtension, string extension, string descripcion)
        {
            this.idExtension = idExtension;
            this.extension = extension;
            this.descripcion = descripcion;
        }
        #endregion

        #region "Metodos"
        public void tableToExtensionArchivo(System.Data.DataRow row)
        {
            if (row != null)
            {
                try
                {
                    this.idExtension = Convert.ToInt16(row["value"]);
                    this.extension = row["text"].ToString();
                    this.descripcion = row["descripcion"].ToString();
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
