using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Ionic.Zip;
using System.IO;

namespace MailManaged.Zip
{
    public class ManagedZip
    {
        #region "procedimientos"

        /// <summary>
        /// Función que se utiliza para comprimir una carpeta o un archivo
        /// </summary>
        /// <param name="path"> Ruta fisica del archivo o carpeta a comprimir </param>
        /// <param name="fileOrForlder"> Indica si lo que se desea comprimir es un Archivo o una carpeta </param>
        /// <returns> True si la operación se completo sin problemas </returns>
        /// <remarks>
        /// <list> Creado Agosto 14 de 2013 - Ing david pineda </list>
        /// </remarks>
        public static void generarZip(string path, bool fileOrForlder)
        {
            ZipFile zip = new ZipFile();
            try
            {
                if (fileOrForlder)
                {
                    //Si es un Archivo
                    using (zip)
                    {
                        zip.AddFile(path);
                        string path_zip = path.Replace(Path.GetFileName(path), Path.Combine(Path.GetFileNameWithoutExtension(path), ".zip"));
                        if (System.IO.File.Exists(path_zip))
                        {
                            System.IO.File.Delete(path_zip);
                        }
                        zip.Save(path_zip);
                    }
                }
                else
                {
                    //Si es un Folder - Carpeta
                    using (zip)
                    {
                        zip.AddDirectory(path);
                        string path_zip = path + ".zip";
                        if (System.IO.File.Exists(path_zip))
                        {
                            System.IO.File.Delete(path_zip);
                        }
                        zip.Save(path_zip);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Se agrega la el Dispose para asegurar que se liberen recursos
                zip.Dispose();
            }
        }

        #endregion
    }
}
