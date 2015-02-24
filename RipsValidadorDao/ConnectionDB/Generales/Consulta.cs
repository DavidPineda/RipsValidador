using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using RipsValidadorDao.Model;
namespace RipsValidadorDao.ConnectionDB.Generales
{
    public class Consulta
    {
        private DataLayer.clsDataServices objDataLayer { get; set; }

        public Consulta()
        {
            objDataLayer = new DataLayer.clsDataServices();
        }

        /// <summary>
        /// Permite Consultar los datos de la IPS por Nit
        /// </summary>
        /// <param name="nitIps">Nit de la Ips A consultar</param>
        /// <returns>DataTable con los datos encontrados</returns>
        public DataTable consultarIpsXnit(string nitIps) 
        {
            try
            {
                objDataLayer.AddGenericParameter("@parNitIps", DbType.String, ParameterDirection.Input, nitIps);
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_IPS", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar una IPS por razon Social
        /// </summary>
        /// <param name="nombre">Razon Social de la IPS</param>
        /// <returns>DataTable con los datos encontrados</returns>
        public DataTable consultarIpsXnombre(string nombre)
        {
            try
            {
                objDataLayer.AddGenericParameter("@parNombreIps", DbType.String, ParameterDirection.Input, nombre);
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_IPS", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar las sedes de una IPS
        /// </summary>
        /// <param name="codIps">(Opciona) Codigo de la IPS</param>
        /// <returns>Datos de la sedes encontradas</returns>
        public DataTable consultarSedesIPSxCodIps(string codIps = null)
        {
            try
            {
                objDataLayer.AddGenericParameter("@parCodIps", DbType.String, ParameterDirection.Input, codIps);
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_SEDES_IPS", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar Todas las Regionales
        /// </summary>
        /// <param name="codRegional">(Opcional) Código de regional a consultar</param>
        /// <returns>DataTable con los datos encontrados de la consulta</returns>
        public DataTable consultarRegionales(string codRegional = null)
        {
            try
            {
                objDataLayer.AddGenericParameter("@parCodRegional", DbType.String, ParameterDirection.Input, codRegional);
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_REGIONALES", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar una regional en especifico
        /// </summary>
        /// <param name="codRegional">Código de regional a consultar</param>
        /// <returns>Objeto con los datos encontrados de la consulta</returns>
        public Regional consultarRegionalesOBJ(string codRegional)
        {
            objDataLayer.AddGenericParameter("@parCodRegional", DbType.String, ParameterDirection.Input, codRegional);
            DataRow row;
            Regional r = new Regional();
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_REGIONALES", DataLayer.ReturnType.DatarowType);
                if (row != null)
                {
                    r.tableToRegional(row);
                    return r;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar los tipos de contrato de la APP
        /// </summary>
        /// <param name="codTipoContrato">(Opcional) Codigo de tipo de contrato</param>
        /// <returns>Datatable con los datos de la consulta</returns>
        public DataTable consultarTipoContrato(string codTipoContrato = "")
        {
            objDataLayer.AddGenericParameter("@cod_contrato", DbType.String, ParameterDirection.Input, codTipoContrato);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_TIPO_CONTRATO", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoContrato consultarTipoContratoOBJ(string codTipoContrato)
        {
            TipoContrato t = new TipoContrato();
            try
            {
                DataRow row = consultarTipoContrato(codTipoContrato).Rows[0];
                if (row != null)
                {
                    t.tableToTipoContrato(row);
                    return t;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar los datos de archivos Parametrizados
        /// </summary>
        /// <param name="codArchivo">(Opcional) codigo del archivo</param>
        /// <returns>DataTable con los datos encontrados de la consulta</returns>
        public DataTable consultarArchivosParametrizados()
        {
            objDataLayer.AddGenericParameter("@tipoOperacion", DbType.Int16, ParameterDirection.Input, 1);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_PARAMETRIZACION_ARCHIVO", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar los datos de un archivo parametrizado
        /// </summary>
        /// <param name="codArchivo">Codigo del archivo</param>
        /// <returns>Archivo Parametrizado</returns>
        public ParametrizacionArchivo consultarArchivoParametrizado(string codArchivo)
        {
            objDataLayer.AddGenericParameter("@tipoOperacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, codArchivo);
            try
            {
                ParametrizacionArchivo p = new ParametrizacionArchivo();
                p.TableToParametrizacionArchivo((DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_PARAMETRIZACION_ARCHIVO", DataLayer.ReturnType.DatarowType));
                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar la estructura de archivos por codigo del archivo
        /// </summary>
        /// <param name="codArchivo">Codigo del archivo</param>
        /// <returns>DataTable con los datos de la consulta</returns>
        public DataTable consultarEstructuraArchivo(string codArchivo)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int32, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, codArchivo);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_ESTRUCTURA_ARCHIVO", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar la estructura de archivos por codigo del archivo
        /// </summary>
        /// <param name="codArchivo">Codigo del archivo</param>
        /// <returns>Objeto EstructuraArchivo con los datos de la consulta</returns>
        public EstructuraArchivo consultarEstructuraArchivo(string codArchivo, int numColumna)
        {
            EstructuraArchivo e = new EstructuraArchivo();
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int32, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, codArchivo);
            objDataLayer.AddGenericParameter("@numero_columna", DbType.Int32, ParameterDirection.Input, numColumna);
            DataRow row;
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_ESTRUCTURA_ARCHIVO", DataLayer.ReturnType.DatarowType);
                if (row != null) { 
                    e.tableToEstructuraArchivo(row);
                    e.tipoDato = consultarTipoDatoOBJ(int.Parse(row["COD_TIPO_DATO"].ToString()));
                    e.formatoFecha = consultarFormatoFechaOBJ(int.Parse(row["COD_FORMATO_FECHA"].ToString()));
                    e.estadoParametrizacion = consultarEstadoParametrizacionOBJ(int.Parse(row["COD_ESTADO"].ToString()));
                    e.parametrizacionArchivo = consultarArchivoParametrizado(row["COD_ARCHIVO"].ToString());
                }
                else
                {
                    e.tipoDato = new TipoDato();
                    e.formatoFecha = new FormatoFecha();
                    e.estadoParametrizacion = new EstadoParametrizacion();
                    e.parametrizacionArchivo = consultarArchivoParametrizado(codArchivo);
                }
                return e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite consultar los tipos de datos del sistema
        /// </summary>
        /// <param name="codTipoDato">(OPcional) Codigo del tipo de dato</param>
        /// <returns>DataTable con los datos encontrados de la consulta</returns>
        public DataTable consultarTipoDato(int codTipoDato = 0)
        {
            objDataLayer.AddGenericParameter("@tipo_dato", DbType.Int16, ParameterDirection.Input, codTipoDato);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_TIPO_DATO", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite consultar los tipos de datos del sistema
        /// </summary>
        /// <param name="codTipoDato">Codigo del tipo de dato</param>
        /// <returns>Objeto TipoDato con los datos encontrados de la consulta</returns>
        public TipoDato consultarTipoDatoOBJ(int codTipoDato)
        {
            objDataLayer.AddGenericParameter("@tipo_dato", DbType.Int16, ParameterDirection.Input, codTipoDato);
            TipoDato t = new TipoDato();
            DataRow row;
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_TIPO_DATO", DataLayer.ReturnType.DatarowType);
                if (row != null)
                {
                    t.tableToTipoDato(row);
                    return t;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar los formatos de fecha aceptados en el sistema
        /// </summary>
        /// <param name="codFormatoFecha">(Opcional) Código del formato de fecha</param>
        /// <returns>DataTable con los datos encontrados de la consulta</returns>
        public DataTable consultarFormatoFecha(int codFormatoFecha = 0)
        {
            objDataLayer.AddGenericParameter("@cod_formato_fecha", DbType.Int16, ParameterDirection.Input, codFormatoFecha);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_FORMATO_FECHA", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar los formatos de fecha aceptados en el sistema
        /// </summary>
        /// <param name="codFormatoFecha">(Opcional) Código del formato de fecha</param>
        /// <returns>Objeto FormatoFecha con los datos encontrados de la consulta</returns>
        public FormatoFecha consultarFormatoFechaOBJ(int codFormatoFecha)
        {
            FormatoFecha f = new FormatoFecha();
            objDataLayer.AddGenericParameter("@cod_formato_fecha", DbType.Int16, ParameterDirection.Input, codFormatoFecha);
            DataRow row;
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_FORMATO_FECHA", DataLayer.ReturnType.DatarowType);
                if (row != null)
                {
                    f.tableToFormatoFecha(row);
                    return f;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable consultarEstadoProgramacion(Int16 codEstadoCargue = 0)
        {
            objDataLayer.AddGenericParameter("@cod_estado", DbType.Int16, ParameterDirection.Input, codEstadoCargue);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_ESTADO_PROGRAMACION_ARCHVO", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstadoProgramacion consultarEstadoProgramacionOBJ(Int16 codEstadoCargue)
        {
            EstadoProgramacion e = new EstadoProgramacion();
            try
            {
                DataRow row = consultarEstadoProgramacion(codEstadoCargue).Rows[0];
                if (row != null)
                {
                    e.tableToEstadoCargue(row);
                    return e;
                }
                return null;
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

        public DataTable consultarEstadoArchivo(Int16 codEstadoArchivo = 0)
        {
            objDataLayer.AddGenericParameter("@codEstadoArchivo", DbType.Int16, ParameterDirection.Input, codEstadoArchivo);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_ESTADO_ARCHIVO", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstadoArchivo consultarEstadoArchivoOBJ(Int16 codEstadoArchivo)
        {
            EstadoArchivo e = new EstadoArchivo();
            try
            {
                DataRow row = consultarEstadoArchivo(codEstadoArchivo).Rows[0];
                if (row != null)
                {
                    e.tableToEstadoArchivo(row);
                    return e;
                }
                return null;
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

        /// <summary>
        /// Permite consultar los valores aceptados parametrizados por la aplicacion
        /// </summary>
        /// <param name="e">Estructura de archivo del cual consultar</param>
        /// <returns>Consulta con los datos obtenidos</returns>
        public DataTable consultarValoresAceptados(EstructuraArchivo e)
        {
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, e.parametrizacionArchivo.codArchivo);
            objDataLayer.AddGenericParameter("@num_columna", DbType.Int32, ParameterDirection.Input, e.numeroColumna);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_VALORES_ACEPTADOS", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar los tipos de comparaciones para campos dependientes
        /// </summary>
        /// <param name="codComparacion">Codigo de Comparacion</param>
        /// <returns>Datatable con todos los datos obtenidos de la columna</returns>
        public DataTable consultarTipoComparacion(int codComparacion = 0)
        {
            objDataLayer.AddGenericParameter("@cod_comparacion", DbType.Int16, ParameterDirection.Input, codComparacion);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_TIPO_COMPARACION", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar los tipos de comparaciones para campos dependientes
        /// </summary>
        /// <param name="codComparacion">Codigo de Comparacion</param>
        /// <returns>Objeto TipoComparacion con todos los datos obtenidos de la columna</returns>
        public TipoComparacion consultarTipoComparacionOBJ(int codComparacion)
        {
            TipoComparacion t = new TipoComparacion();
            objDataLayer.AddGenericParameter("@cod_comparacion", DbType.Int16, ParameterDirection.Input, codComparacion);
            DataRow row;
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_TIPO_COMPARACION", DataLayer.ReturnType.DatarowType);
                if (row != null)
                {
                    t.tableToTipoComparacion(row);
                    return t;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar los tipos de comparaciones para campos dependientes
        /// </summary>
        /// <param name="codTipoValor">Codigo de Comparacion(Opcional)</param>
        /// <returns>Datatable con todos los datos obtenidos de la columna</returns>
        public DataTable consultarTipoValor(int codTipoValor = 0)
        {
            objDataLayer.AddGenericParameter("@codTipoValor", DbType.Int16, ParameterDirection.Input, codTipoValor);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_TIPO_VALOR", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar los tipos de comparaciones para campos dependientes
        /// </summary>
        /// <param name="codTipoValor"></param>
        /// <param name="codTipoValor">Codigo de Comparacion</param>
        /// <returns>Objeto TipoValor con todos los datos obtenidos de la columna</returns>
        public TipoValor consultarTipoValorOBJ(int codTipoValor)
        {
            TipoValor t = new TipoValor();
            objDataLayer.AddGenericParameter("@codTipoValor", DbType.Int16, ParameterDirection.Input, codTipoValor);
            DataRow row;
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_TIPO_VALOR", DataLayer.ReturnType.DatarowType);
                if (row != null)
                {
                    t.tableToTipoDato(row);
                    return t;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar El estado de parametrizacion de la estructura del archivo
        /// </summary>
        /// <param name="codEstado">(Opcional) Codigo del estado de parametrizacion</param>
        /// <returns>DataTable con los datos encontrados de la consulta</returns>
        public DataTable consultarEstadoParametrizacion(int codEstado = 0)
        {
            objDataLayer.AddGenericParameter("@cod_estado", DbType.Int16, ParameterDirection.Input, codEstado);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_ESTADO_PARAMETRIZACION", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar El estado de parametrizacion de la estructura del archivo
        /// </summary>
        /// <param name="codEstado">(Opcional) Codigo del estado de parametrizacion</param>
        /// <returns>Objeto EstadoParametrizacion con los datos encontrados de la consulta</returns>
        public EstadoParametrizacion consultarEstadoParametrizacionOBJ(int codEstado)
        {
            objDataLayer.AddGenericParameter("@cod_estado", DbType.Int16, ParameterDirection.Input, codEstado);
            EstadoParametrizacion e = new EstadoParametrizacion();
            DataRow row;
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_ESTADO_PARAMETRIZACION", DataLayer.ReturnType.DatarowType);
                if (row != null)
                {
                    e.tableToEstadoParametrizacion(row);
                    return e;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar Los datos de Estructura de los archivos
        /// </summary>
        /// <param name="codArchivo">Codigo del archivo a consultar</param>
        /// <param name="numeroColumna">Numero de columna del archivo que se consultar</param>
        /// <returns>DataTable con los datos recuperados de la consulta</returns>
        public DataTable consultarDatosEstructuraArchivo(string codArchivo, int numeroColumna)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int32, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, codArchivo);
            objDataLayer.AddGenericParameter("@numero_columna", DbType.Int32, ParameterDirection.Input, numeroColumna);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_DATOS_ESTRUCTURA_ARCHIVO", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Consultar Los datos de Estructura de los archivos
        /// </summary>
        /// <param name="idValPermitido">Id del valor parametrizado a consultar</param>
        /// <param name="codArchivo">Codigo del archivo a consultar</param>
        /// <param name="numeroColumna">Numero de columna del archivo que se consultar</param>
        /// <returns>Objeto DatosEstructuraArchivo con los datos recuperados de la consulta</returns>
        public DatosEstructuraArchivo consultarDatosEstructuraArchivoOBJ(int idValPermitido, string codArchivo, int numeroColumna)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int32, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, codArchivo);
            objDataLayer.AddGenericParameter("@numero_columna", DbType.Int32, ParameterDirection.Input, numeroColumna);
            objDataLayer.AddGenericParameter("@id_val_permitido", DbType.Int32, ParameterDirection.Input, idValPermitido);
            DatosEstructuraArchivo d = new DatosEstructuraArchivo();
            DataRow row;
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_DATOS_ESTRUCTURA_ARCHIVO", DataLayer.ReturnType.DatarowType);
                if(row != null){
                    d.tableToDatosEstructuraArchivo(row);
                    d.estructuraArchivo = consultarEstructuraArchivo(codArchivo, numeroColumna);
                    d.tipoValor = consultarTipoValorOBJ(Convert.ToInt16(row["cod_tipo_valor"]));
                    return d;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable consultarVariablesDependientes(string codArchivoDep, int numeroColumnaDep)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@cod_archivo_dep", DbType.String, ParameterDirection.Input, codArchivoDep);
            objDataLayer.AddGenericParameter("@num_columna_dep", DbType.Int32, ParameterDirection.Input, numeroColumnaDep);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_VARIABLES_DEPENDIENTES", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VariableDependiente consultarVariablesDependientesOBJ(int idValorDependiente)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@id_valor_dependiente", DbType.Int32, ParameterDirection.Input, idValorDependiente);
            VariableDependiente v = new VariableDependiente();
            DataRow row = null;
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_VARIABLES_DEPENDIENTES", DataLayer.ReturnType.DatarowType);
                if (row != null)
                {
                    v.tableToVaribaleDependiente(row);
                    int idValPermitidoDep = Convert.ToInt32(row["id_val_permitido_dep"]);
                    int idValPermitidoCru = Convert.ToInt32(row["id_val_permitido_cru"]);
                    if (idValPermitidoDep > 0)
                    {
                        v.estructuraDep = consultarDatosEstructuraArchivoOBJ(Convert.ToInt32(row["id_val_permitido_dep"]), 
                            row["cod_archivo_dep"].ToString(), Convert.ToInt32(row["num_columna_dep"]));
                    }
                    else
                    {
                        DatosEstructuraArchivo d1 = new DatosEstructuraArchivo();
                        d1.estructuraArchivo = consultarEstructuraArchivo(row["cod_archivo_dep"].ToString(), Convert.ToInt32(row["num_columna_dep"]));
                        d1.idValPermitido = idValPermitidoDep;
                        v.estructuraDep = d1;
                    }
                    if (idValPermitidoCru > 0)
                    {
                        v.estructuraCru = consultarDatosEstructuraArchivoOBJ(Convert.ToInt32(row["id_val_permitido_cru"]),
                            row["cod_archivo_cru"].ToString(), Convert.ToInt32(row["num_columna_cru"]));
                    }else
                    {
                        DatosEstructuraArchivo d2 = new DatosEstructuraArchivo();
                        d2.estructuraArchivo = consultarEstructuraArchivo(row["cod_archivo_cru"].ToString(), Convert.ToInt32(row["num_columna_cru"]));
                        d2.idValPermitido = idValPermitidoCru;
                        v.estructuraCru = d2;
                    }
                    v.tipoComparacionDep = consultarTipoComparacionOBJ(Convert.ToInt16(row["tipo_comparacion_dep"]));
                    v.tipoComparacionCru = consultarTipoComparacionOBJ(Convert.ToInt16(row["tipo_comparacion_cru"]));
                    return v;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable consultarEncabezadoGruposDependencias(string codArchivo, int numColumna)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, codArchivo);
            objDataLayer.AddGenericParameter("@num_columna", DbType.Int32, ParameterDirection.Input, numColumna);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_ENC_GRUPO_VARIABLE_DEPENDIENTE", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EncabezadoGrupoVarDependiente consultarEncabezadoGruposDependenciasOBJ(int idEncabezadoGrupo)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@id_enc_grupo", DbType.String, ParameterDirection.Input, idEncabezadoGrupo);
            DataRow row;
            EncabezadoGrupoVarDependiente e = new EncabezadoGrupoVarDependiente();
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_ENC_GRUPO_VARIABLE_DEPENDIENTE", DataLayer.ReturnType.DatarowType);
                if (row != null)
                {
                    e.tableToEncabezadoGrupoVarDependiente(row);
                    e.datosArchivo = consultarEstructuraArchivo(row["cod_archivo"].ToString(), Convert.ToInt32(row["num_columna"]));
                    e.estado = consultarEstadoParametrizacionOBJ(Convert.ToInt16(row["cod_estado"]));
                    return e;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable consultarDetalleGrupoDependencia(int idEncabezado){
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@id_enc_grupo", DbType.Int32, ParameterDirection.Input, idEncabezado);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_DET_GRUPO_VARIABLE_DEPENDIENTE", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DetalleGrupoDependiente consultarDetalleGrupoDependenciaOBJ(int idGrupo)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@id_grupo", DbType.Int32, ParameterDirection.Input, idGrupo);
            DataRow row = null;
            DetalleGrupoDependiente d = new DetalleGrupoDependiente();
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_DET_GRUPO_VARIABLE_DEPENDIENTE", DataLayer.ReturnType.DatarowType);
                if (row != null)
                {
                    d.tableToDetalleGrupoDependiente(row);
                    d.encabezadoGrupo = consultarEncabezadoGruposDependenciasOBJ(Convert.ToInt32(row["id_enc_grupo"]));
                    d.estado = consultarEstadoParametrizacionOBJ(Convert.ToInt32(row["cod_estado"]));
                    d.varDependiente = consultarVariablesDependientesOBJ(Convert.ToInt32(row["id_var_dependiente"]));
                    return d;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable consultarExtensiones()
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_EXTENSION_ARCHIVO", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExtensionArchivo consultarExtensionesOBJ(Int16 idExtension)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@id_extension", DbType.Int16, ParameterDirection.Input, idExtension);
            DataRow row = null;
            ExtensionArchivo e = new ExtensionArchivo();
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_EXTENSION_ARCHIVO", DataLayer.ReturnType.DatarowType);
                if (row != null)
                {
                    e.tableToExtensionArchivo(row);
                    return e;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable consultarExtensionXarchivo(string codArchivo = "", int idExtension = 0)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@id_extension", DbType.Int16, ParameterDirection.Input, idExtension);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, codArchivo);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_EXTENSION_X_ARCHIVO", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ExtensionXarchivo consultarExtensionXarchivoOBJ(string codArchivo, int idExtension)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@id_extension", DbType.Int16, ParameterDirection.Input, idExtension);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, codArchivo);
            DataRow row = null;
            ExtensionXarchivo e = new ExtensionXarchivo();
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_EXTENSION_X_ARCHIVO", DataLayer.ReturnType.DatarowType);
                if (row != null)
                {
                    e.archivo = consultarArchivoParametrizado(row["cod_archivo"].ToString());
                    e.extension = consultarExtensionesOBJ(Convert.ToInt16(row["id_extension"]));
                    return e;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable consultarArchivosDependientes(string codArchivo = "")
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, codArchivo);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_ARCHIVOS_DEPENDIENTES", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArchivoDependiente consultarArchivosDependientesOBJ(string codArchivo, string codArchivoDep)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, codArchivo);
            objDataLayer.AddGenericParameter("@cod_archivo_dep", DbType.String, ParameterDirection.Input, codArchivoDep);
            DataRow row;
            ArchivoDependiente a = new ArchivoDependiente();
            try
            {
                row = (DataRow)objDataLayer.ExecuteStoredProcedure("P_RIPS_ARCHIVOS_DEPENDIENTES", DataLayer.ReturnType.DatarowType);
                if (row != null)
                {
                    a.archivo = consultarArchivoParametrizado(row["cod_archivo"].ToString());
                    a.archivoDep = consultarArchivoParametrizado(row["cod_archivo_dep"].ToString());
                    return a;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable consultarProgramacionArchivo(int idProgramacion = 0)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@id_programacion", DbType.Int32, ParameterDirection.Input, idProgramacion);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_PROGRAMACION_ARCHIVO", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable consultarProgramacionArchivo(string codIps, Int16 estado, string fecIni, string fecFin)
        {
            objDataLayer.AddGenericParameter("@cod_ips", DbType.String, ParameterDirection.Input, codIps);
            objDataLayer.AddGenericParameter("@fecha_inicio", DbType.String, ParameterDirection.Input, fecIni);
            objDataLayer.AddGenericParameter("@fecha_fin", DbType.String, ParameterDirection.Input, fecFin);
            objDataLayer.AddGenericParameter("@id_estado_carga", DbType.Int16, ParameterDirection.Input, estado);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_PROGRAMACION_FILTRO", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProgramacionArchivo consultarProgramacionArchivoOBJ(int idProgramacion)
        {
            ProgramacionArchivo p = new ProgramacionArchivo();
            AutenticationProvider.Consulta c = new AutenticationProvider.Consulta();
            try
            {
                DataRow row = consultarProgramacionArchivo(idProgramacion).Rows[0];
                if (row != null)
                {
                    p.tableToProgramacionArchivo(row);
                    p.regional = consultarRegionalesOBJ(row["cod_regional"].ToString());
                    p.estado = consultarEstadoProgramacionOBJ(Convert.ToInt16(row["estado_programacion"]));
                    p.contrato = consultarTipoContratoOBJ(row["cod_tipo_contrato"].ToString());
                    p.usuario = c.consultarUsuarioXnombre(row["nom_usuario"].ToString());
                    return p;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable consultarArchivoCargado(int idProgramacion = 0, int consecutivo = 0)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@id_programacion", DbType.Int32, ParameterDirection.Input, idProgramacion);
            objDataLayer.AddGenericParameter("@consecutivo", DbType.Int32, ParameterDirection.Input, consecutivo);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_ARCHIVO_CARGADO", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ArchivoCargado consultarArchivoCargadoOBJ(int idProgramacion, int consecutivo)
        {
            ArchivoCargado a = new ArchivoCargado();
            DataTable objDtDatos = null;
            try
            {
                objDtDatos = consultarArchivoCargado(idProgramacion, consecutivo);
                if (objDtDatos != null && objDtDatos.Rows.Count > 0)
                {
                    DataRow row = objDtDatos.Rows[0];
                    if (row != null)
                    {
                        a.tableToArchivoCargado(row);
                        a.programacion = consultarProgramacionArchivoOBJ(Convert.ToInt32(row["id_programacion"]));
                        a.archivo = consultarArchivoParametrizado(row["cod_archivo"].ToString());
                        a.estadoArchivo = consultarEstadoArchivoOBJ(Convert.ToInt16(row["cod_estado_archivo"]));
                        return a;
                    }
                }
                return null;
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

        public DataTable consultarRazonSocialIpsXNit(string nitIps)
        {
            objDataLayer.AddGenericParameter("@nit_ips", DbType.String, ParameterDirection.Input, nitIps);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_NOMBRES_IPS", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable consultarRazonSocialIpsXNombre(string razonSocial)
        {
            objDataLayer.AddGenericParameter("@nom_eps", DbType.String, ParameterDirection.Input, razonSocial);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_NOMBRES_IPS", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable consultarResultadoValidacion(int id_programacion = 0)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@id_programacion", DbType.Int32, ParameterDirection.Input, id_programacion);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_RESULTADO_VALIDACION", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultadoValidacion consultarResultadoValidacionOBJ(int id_programacion)
        {
            ResultadoValidacion r = new ResultadoValidacion();
            DataTable objDtDatos = null;
            try
            {
                objDtDatos = consultarResultadoValidacion(id_programacion);
                if (objDtDatos != null)
                {
                    if (objDtDatos.Rows.Count > 0)
                    {
                        r.tableToResultadoValidacion(objDtDatos.Rows[0]);
                        r.programacion = consultarProgramacionArchivoOBJ(id_programacion);
                        return r;
                    }
                }
                return null;
            }
            catch(InvalidCastException ex){
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable consultarResultadoValidacionDetalle(int id_programacion = 0, int consecutivo = 0)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@id_programacion", DbType.Int32, ParameterDirection.Input, id_programacion);
            objDataLayer.AddGenericParameter("@consecutivo", DbType.Int32, ParameterDirection.Input, consecutivo);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_RESULTADO_VALIDACION_DETALLE", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResultadoValidacionDetalle consultarResultadoValidacionDetalleOBJ(int id_programacion, int consecutivo)
        {
            ResultadoValidacionDetalle r = new ResultadoValidacionDetalle();
            try
            {
                DataTable objDtDatos = consultarResultadoValidacionDetalle(id_programacion, consecutivo);
                if (objDtDatos != null)
                {
                    if (objDtDatos.Rows.Count > 0)
                    {
                        r.tableToResultadoValidacionDetalle(objDtDatos.Rows[0]);
                        r.archivo = consultarArchivoCargadoOBJ(id_programacion, consecutivo);
                        return r;
                    }
                }
                return null;
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

        public DataTable consultarErroresProcesoValidacion(int idProgramacion, int consecutivo)
        {
            objDataLayer.AddGenericParameter("@id_programacion", DbType.Int32, ParameterDirection.Input, idProgramacion);
            objDataLayer.AddGenericParameter("@consecutivo", DbType.Int32, ParameterDirection.Input, consecutivo);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_ERROR_VALIDACION", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ColumnaCruce consultarColumnaCruceOBJ(int idColumna)
        {
            ColumnaCruce c = new ColumnaCruce();
            try
            {
                DataTable objDtDatos = consultarColumnaCruce(idColumna);
                if (objDtDatos != null)
                {
                    if (objDtDatos.Rows.Count > 0)
                    {
                        c.tableToColumnaCruce(objDtDatos.Rows[0]);
                        return c;
                    }
                }
                return null;
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

        public DataTable consultarColumnaCruce(int idColumna)
        {
            objDataLayer.AddGenericParameter("@id_columna", DbType.Int32, ParameterDirection.Input, idColumna);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CONSULTAR_COLUMNAS_CRUCE", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CruceAfiliado consultarCruceAfiliadoOBJ(int idCruce)
        {
            CruceAfiliado e = new CruceAfiliado();
            try
            {
                DataTable objDtDatos = consultarCruceAfiliado(idCruce);
                if (objDtDatos != null)
                {
                    if (objDtDatos.Rows.Count > 0)
                    {
                        e.tableToEstadoCruces(objDtDatos.Rows[0]);
                        return e;
                    }
                }
                return null;
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

        public DataTable consultarCruceAfiliado(int idCruce)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@id", DbType.Int32, ParameterDirection.Input, idCruce);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CRUCE_AFILIADO", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CruceAfiliadoColumna consultarCruceAfiliadoColumnaOBJ(int idCruce, int idColumna)
        {
            CruceAfiliadoColumna c = new CruceAfiliadoColumna();
            try
            {
                DataTable objDtDatos = consultarCruceAfiliadoColumna(idCruce, idColumna);
                if (objDtDatos != null)
                {
                    if (objDtDatos.Rows.Count > 0)
                    {
                        c.columnaCruce = consultarColumnaCruceOBJ(idColumna);
                        c.cruceAfiliado = consultarCruceAfiliadoOBJ(idCruce);
                        c.tableToCruceAfiliadoColumna(objDtDatos.Rows[0]);
                        return c;
                    }
                }
                return null;
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

        public DataTable consultarCruceAfiliadoColumna(int idCruce, int idColumna)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, 1);
            objDataLayer.AddGenericParameter("@id_cruce_afiliado", DbType.Int32, ParameterDirection.Input, idCruce);
            objDataLayer.AddGenericParameter("@id_cruce_columna", DbType.Int32, ParameterDirection.Input, idColumna);
            try
            {
                return (DataTable)objDataLayer.ExecuteStoredProcedure("P_RIPS_CRUCE_AFILIADO_COLUMNA", DataLayer.ReturnType.DatatableType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
