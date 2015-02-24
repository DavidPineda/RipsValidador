using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class ExtensionXarchivo
    {

        #region "Propiedades"
        public ExtensionArchivo extension { get; set; }
        public ParametrizacionArchivo archivo { get; set; }
        #endregion

        #region "Constructores"
        public ExtensionXarchivo()
        {
            this.archivo = null;
            this.extension = null;
        }

        public ExtensionXarchivo(ParametrizacionArchivo archivo, ExtensionArchivo extension)
        {
            this.archivo = archivo;
            this.extension = extension;
        }
        #endregion

    }
}
