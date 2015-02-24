using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Diagnostics;
using ErrorsLog.LogErrores;

namespace RipsValidadorWeb.Clases
{
    public static class Logger
    {
        public static void generarLogError(string strError, StackFrame objTrama, Exception objException = null)
        {
            string path = ConfigurationManager.AppSettings["RUTA_LOG_ERROR"];
            Log.Nivel = Log.MessageTipe.sError;
            try{
                if (!(objException == null))
                {
                    Log.writeError(strError, objException, path, objTrama);
                }
                else {
                    Log.writeError(strError, path, objTrama);
                }
            }catch(Exception ex)
            {
                LogEvent.eventAdd("Integr@PYP4505", strError + " -- " + ex.Message, 5, 100);
            }
        }
    }
}