using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipsValidadorDao.Model
{
    public class ArchivoDependiente
    {
        #region "Propiedades"
        public ParametrizacionArchivo archivo { get; set; }
        public ParametrizacionArchivo archivoDep { get; set; }
        #endregion

        #region "Constructores"
        public ArchivoDependiente()
        {
            this.archivo = null;
            this.archivoDep = null;
        }

        public ArchivoDependiente(ParametrizacionArchivo archivo, ParametrizacionArchivo archivoDep)
        {
            this.archivo = archivo;
            this.archivoDep = archivoDep;
        }
        #endregion
    }
}
