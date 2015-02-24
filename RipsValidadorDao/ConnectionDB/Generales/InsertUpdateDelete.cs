using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RipsValidadorDao.Model;
using System.Data;

namespace RipsValidadorDao.ConnectionDB.Generales
{
    public class InsertUpdateDelete
    {
        public DataLayer.clsDataServices objDataLayer { get; set; }
        public Usuario u { get; set; }

        public InsertUpdateDelete(Usuario u)
        {
            objDataLayer = new DataLayer.clsDataServices();
            this.u = u;
        }

        /// <summary>
        /// Permite manejar Insert, Update o Delete sobre la tabla RIPS_PARAMETRIZACION_ARCHIVO
        /// </summary>
        /// <param name="p">Archivo Parametrizado</param>
        /// <param name="codOperacion">Código de la Operacion a ejecutar 2) Insert - 3) Update - 4) Delete</param>
        public void IUDarchivoParametrizado(ParametrizacionArchivo p, Int16 codOperacion)
        {
            objDataLayer.AddGenericParameter("@tipoOperacion", DbType.Int16, ParameterDirection.Input, codOperacion);
            objDataLayer.AddGenericParameter("@idUsuario", DbType.Int32, ParameterDirection.Input, u.idUsuario);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, p.codArchivo);
            objDataLayer.AddGenericParameter("@descripcion", DbType.String, ParameterDirection.Input, p.descripcion);
            objDataLayer.AddGenericParameter("@cant_columnas", DbType.Int32, ParameterDirection.Input, p.cantColumnas);
            objDataLayer.AddGenericParameter("@separador", DbType.String, ParameterDirection.Input, p.separador);
            objDataLayer.AddGenericParameter("@tam_max_cargue", DbType.Int32, ParameterDirection.Input, p.tamMaximoCargue);
            objDataLayer.AddGenericParameter("@ruta_cargue_archivo", DbType.String, ParameterDirection.Input, p.rutaCargueArchivo);
            objDataLayer.AddGenericParameter("@cargue_obligatorio", DbType.Boolean, ParameterDirection.Input, p.isCargueObligatorio);
            objDataLayer.AddGenericParameter("@long_max_nom_archivo", DbType.Int32, ParameterDirection.Input, p.lngMaximaNombre);
            objDataLayer.AddGenericParameter("@long_min_nom_archivo", DbType.Int32, ParameterDirection.Input, p.lngMinimaNombre);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_PARAMETRIZACION_ARCHIVO", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Manejar Insert, Update o Delete sobre la Tabla RIPS_ESTRUCTURA_ARCHIVO
        /// </summary>
        /// <param name="e">Estructura Parametrizada</param>
        /// <param name="codOperacion">Código de la Operacion a ejecutar 2) Insert - 3) Update - 4) Delete</param>
        public void IUDestructuraArchivo(EstructuraArchivo e, Int16 codOperacion)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, codOperacion);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, e.parametrizacionArchivo.codArchivo);
            objDataLayer.AddGenericParameter("@idusuario", DbType.Int32, ParameterDirection.Input, u.idUsuario);
            objDataLayer.AddGenericParameter("@numero_columna", DbType.Int32 , ParameterDirection.Input, e.numeroColumna);
            objDataLayer.AddGenericParameter("@nombre_columna", DbType.String, ParameterDirection.Input, e.nombreColumna);
            objDataLayer.AddGenericParameter("@descripcion", DbType.String, ParameterDirection.Input, e.descripcion);
            objDataLayer.AddGenericParameter("@longitud", DbType.Int32, ParameterDirection.Input, e.longitud);
            objDataLayer.AddGenericParameter("@longitud_max", DbType.Int32, ParameterDirection.Input, e.longitudMaxima);
            objDataLayer.AddGenericParameter("@valor_requerido", DbType.Boolean, ParameterDirection.Input, e.valorRequerido);
            objDataLayer.AddGenericParameter("@validar", DbType.Boolean, ParameterDirection.Input, e.validar);
            objDataLayer.AddGenericParameter("@cod_tipo_dato", DbType.Int16, ParameterDirection.Input, e.tipoDato.codTipoDato);
            objDataLayer.AddGenericParameter("@cod_estado", DbType.Int16, ParameterDirection.Input, e.estadoParametrizacion.codEstado);
            if (e.rangoIni != -1)
            {
                objDataLayer.AddGenericParameter("@rango_ini", DbType.Single, ParameterDirection.Input, e.rangoIni);
            }
            if (e.rangoFin != -1)
            {
                objDataLayer.AddGenericParameter("@rango_fin", DbType.Single, ParameterDirection.Input, e.rangoFin);
            }            
            if (e.formatoFecha != null && e.formatoFecha.codFormatoFecha != -1)
            {
                objDataLayer.AddGenericParameter("@cod_formato_fecha", DbType.Int16, ParameterDirection.Input, e.formatoFecha.codFormatoFecha);
            }            
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_ESTRUCTURA_ARCHIVO", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite Manejar Insert, Update o Delete sobre la tabla RIPS_DATOS_ESTRUCTURA_ARCHIVO
        /// </summary>
        /// <param name="d">Datos de estructura parametrizado</param>
        /// <param name="codOperacion">Código de la Operacion a ejecutar 2) Insert - 3) Update - 4) Delete</param>
        public void IUDdatosEstructuraAcrchivo(DatosEstructuraArchivo d, Int16 codOperacion)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, codOperacion);
            objDataLayer.AddGenericParameter("@id_val_permitido", DbType.Int32, ParameterDirection.Input,d.idValPermitido);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input,d.estructuraArchivo.parametrizacionArchivo.codArchivo);
            objDataLayer.AddGenericParameter("@numero_columna", DbType.Int32, ParameterDirection.Input,d.estructuraArchivo.numeroColumna);
            objDataLayer.AddGenericParameter("@cod_tipo_valor", DbType.String, ParameterDirection.Input,d.tipoValor.codTipoValor);
            objDataLayer.AddGenericParameter("@valor", DbType.String, ParameterDirection.Input,d.valor);
            objDataLayer.AddGenericParameter("@descripcion", DbType.String, ParameterDirection.Input,d.descripcion);
            objDataLayer.AddGenericParameter("@idUsuario", DbType.String, ParameterDirection.Input,u.idUsuario);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_DATOS_ESTRUCTURA_ARCHIVO", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Permite realizar el CRUD de la tabla RIPS_VARIABLES_DEPENDIENTES
        /// </summary>
        /// <param name="v">Objeto con los datos ha trabajar</param>
        /// <param name="codOperacion">Código de la Operacion a ejecutar 2) Insert - 3) Update - 4) Delete</param>
        public void IUDvariablesDependientes(VariableDependiente v, Int16 codOperacion)
        {           
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, codOperacion);
            objDataLayer.AddGenericParameter("@id_valor_dependiente", DbType.Int32, ParameterDirection.Input, v.idVariableDependiente);
            objDataLayer.AddGenericParameter("@cod_archivo_dep", DbType.String, ParameterDirection.Input, v.estructuraDep.estructuraArchivo.parametrizacionArchivo.codArchivo);
            objDataLayer.AddGenericParameter("@cod_archivo_cru", DbType.String, ParameterDirection.Input, v.estructuraCru.estructuraArchivo.parametrizacionArchivo.codArchivo);
            objDataLayer.AddGenericParameter("@num_columna_dep", DbType.Int32, ParameterDirection.Input, v.estructuraDep.estructuraArchivo.numeroColumna);
            objDataLayer.AddGenericParameter("@num_columna_cru", DbType.Int32, ParameterDirection.Input, v.estructuraCru.estructuraArchivo.numeroColumna);
            objDataLayer.AddGenericParameter("@id_val_permitido_dep", DbType.Int32, ParameterDirection.Input, v.estructuraDep.idValPermitido);
            objDataLayer.AddGenericParameter("@id_val_permitido_cru", DbType.Int32, ParameterDirection.Input, v.estructuraCru.idValPermitido);
            objDataLayer.AddGenericParameter("@tipo_comparacion_dep", DbType.Int16, ParameterDirection.Input, v.tipoComparacionDep.codOperadorLogico);
            objDataLayer.AddGenericParameter("@tipo_comparacion_cru", DbType.Int16, ParameterDirection.Input, v.tipoComparacionCru.codOperadorLogico);
            objDataLayer.AddGenericParameter("@otro_valor_dep", DbType.String, ParameterDirection.Input, v.otroValorDep);
            objDataLayer.AddGenericParameter("@otro_valor_cru", DbType.String, ParameterDirection.Input, v.otroValorCru);
            objDataLayer.AddGenericParameter("@mensaje_error", DbType.String, ParameterDirection.Input, v.mensajeError);
            objDataLayer.AddGenericParameter("@cod_estado", DbType.Int16, ParameterDirection.Input, v.estado);
            objDataLayer.AddGenericParameter("@id_usuario", DbType.Int32, ParameterDirection.Input, u.idUsuario);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_VARIABLES_DEPENDIENTES", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IUDencGrupoVariableDependiente(EncabezadoGrupoVarDependiente g, Int16 codOperacion)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, codOperacion);
            objDataLayer.AddGenericParameter("@id_enc_grupo", DbType.Int32, ParameterDirection.Input, g.idEncabezadoGrupo);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, g.datosArchivo.parametrizacionArchivo.codArchivo);
            objDataLayer.AddGenericParameter("@num_columna", DbType.Int16, ParameterDirection.Input, g.datosArchivo.numeroColumna);
            objDataLayer.AddGenericParameter("@descripcion", DbType.String, ParameterDirection.Input, g.descripcion);
            objDataLayer.AddGenericParameter("@cod_estado", DbType.Int16, ParameterDirection.Input, g.estado.codEstado);
            objDataLayer.AddGenericParameter("@id_usuario", DbType.Int32, ParameterDirection.Input, u.idUsuario);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_ENC_GRUPO_VARIABLE_DEPENDIENTE", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IUDdetGrupoVariableDependiente(DetalleGrupoDependiente d, Int16 codOperacion)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, codOperacion);
            objDataLayer.AddGenericParameter("@id_enc_grupo", DbType.Int32, ParameterDirection.Input, d.encabezadoGrupo.idEncabezadoGrupo);
            objDataLayer.AddGenericParameter("@id_grupo", DbType.Int32, ParameterDirection.Input, d.idGrupo);
            objDataLayer.AddGenericParameter("@id_var_dependiente", DbType.Int32, ParameterDirection.Input, d.varDependiente.idVariableDependiente);
            objDataLayer.AddGenericParameter("@estado", DbType.Int16, ParameterDirection.Input, d.estado.codEstado);
            objDataLayer.AddGenericParameter("@descripcion", DbType.String, ParameterDirection.Input, d.descripcion);
            objDataLayer.AddGenericParameter("@id_usuario", DbType.Int32, ParameterDirection.Input, u.idUsuario);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_DET_GRUPO_VARIABLE_DEPENDIENTE", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IUDextensionArchivo(ExtensionArchivo e, Int16 codOperacion)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion",DbType.Int16, ParameterDirection.Input, codOperacion);
            objDataLayer.AddGenericParameter("@id_extension",DbType.Int16, ParameterDirection.Input, e.idExtension);
            objDataLayer.AddGenericParameter("@extension",DbType.String, ParameterDirection.Input, e.extension);
            objDataLayer.AddGenericParameter("@descripcion",DbType.String, ParameterDirection.Input, e.descripcion);
            objDataLayer.AddGenericParameter("@id_usuario",DbType.Int32, ParameterDirection.Input, u.idUsuario);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_EXTENSION_ARCHIVO", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IUDextensionXarchivo(ExtensionXarchivo e, Int16 codOperacion)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, codOperacion);
            objDataLayer.AddGenericParameter("@id_extension", DbType.Int16, ParameterDirection.Input, e.extension.idExtension);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, e.archivo.codArchivo);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_EXTENSION_X_ARCHIVO", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IUDarchivoDependiente(ArchivoDependiente a1, Int16 codOperacion)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, codOperacion);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, a1.archivo.codArchivo);
            objDataLayer.AddGenericParameter("@cod_archivo_dep", DbType.String, ParameterDirection.Input, a1.archivoDep.codArchivo);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_ARCHIVOS_DEPENDIENTES", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int IUDprogramacionArchivo(ProgramacionArchivo p, Int16 codOperacion)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, codOperacion);
            objDataLayer.AddGenericParameter("@id_programacion", DbType.Int32, ParameterDirection.Input, p.idProgramacion);
            objDataLayer.AddGenericParameter("@fecha_programacion", DbType.DateTime, ParameterDirection.Input, p.fechaProgramacion);
            objDataLayer.AddGenericParameter("@periodo_cobro", DbType.DateTime, ParameterDirection.Input, p.periodoCobro);
            objDataLayer.AddGenericParameter("@cod_regional", DbType.String, ParameterDirection.Input, p.regional.codRegional);
            objDataLayer.AddGenericParameter("@estado_programacion", DbType.Int16, ParameterDirection.Input, p.estado.codEstadoCargue);
            objDataLayer.AddGenericParameter("@cod_tipo_contrato", DbType.String, ParameterDirection.Input, p.contrato.codTipoContrato);
            objDataLayer.AddGenericParameter("@estado_proceso", DbType.String, ParameterDirection.Input, p.estadoProceso);
            objDataLayer.AddGenericParameter("@id_usuario", DbType.Int32, ParameterDirection.Input, p.usuario.idUsuario);
            try
            {
                return Convert.ToInt32((objDataLayer.ExecuteStoredProcedure("P_RIPS_PROGRAMACION_ARCHIVO", DataLayer.ReturnType.DatarowType) as DataRow)["retorno"]);
            }
            catch(Exception ex){
                throw ex;
            }
        }

        public void IUDcargarArchivo(ArchivoCargado a, Int16 codOperacion)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, codOperacion);
            objDataLayer.AddGenericParameter("@id_programacion", DbType.Int32, ParameterDirection.Input, a.programacion.idProgramacion);
            objDataLayer.AddGenericParameter("@consecutivo", DbType.Int32, ParameterDirection.Input, a.consecutivo);
            objDataLayer.AddGenericParameter("@cod_archivo", DbType.String, ParameterDirection.Input, a.archivo.codArchivo);
            objDataLayer.AddGenericParameter("@nombre_archivo", DbType.String, ParameterDirection.Input, a.nombreArchivo);
            objDataLayer.AddGenericParameter("@ruta_archivo", DbType.String, ParameterDirection.Input, a.rutaArchivo);
            objDataLayer.AddGenericParameter("@cod_estado_archivo", DbType.Int16, ParameterDirection.Input, a.estadoArchivo.codEstdoArchivo);
            objDataLayer.AddGenericParameter("@estado_archivo", DbType.String, ParameterDirection.Input, a.estadoProceso);
            objDataLayer.AddGenericParameter("@id_usuario", DbType.Int32, ParameterDirection.Input, u.idUsuario);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_ARCHIVO_CARGADO", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IUDcruceAfiliado(CruceAfiliado c, Int16 codOperacion)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, codOperacion);
            objDataLayer.AddGenericParameter("@id", DbType.Int32, ParameterDirection.Input, c.id);
            objDataLayer.AddGenericParameter("@descripcion", DbType.String, ParameterDirection.Input, c.descripcion);
            objDataLayer.AddGenericParameter("@prioridad", DbType.Int32, ParameterDirection.Input, c.prioridad);
            objDataLayer.AddGenericParameter("@estado", DbType.Int16, ParameterDirection.Input, c.estado);
            objDataLayer.AddGenericParameter("@id_usuario", DbType.Int32, ParameterDirection.Input, u.idUsuario);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_CRUCE_AFILIADO", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void IUDcruceAfiliadoColumna(CruceAfiliadoColumna c, Int16 codOperacion)
        {
            objDataLayer.AddGenericParameter("@tipo_operacion", DbType.Int16, ParameterDirection.Input, codOperacion);
            objDataLayer.AddGenericParameter("@id_cruce_afiliado", DbType.Int16, ParameterDirection.Input, c.cruceAfiliado.id);
            objDataLayer.AddGenericParameter("@id_cruce_columna", DbType.Int16, ParameterDirection.Input, c.columnaCruce.id);
            objDataLayer.AddGenericParameter("@estado", DbType.Int16, ParameterDirection.Input, c.estado);
            objDataLayer.AddGenericParameter("@id_usuario", DbType.Int16, ParameterDirection.Input, u.idUsuario);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_CRUCE_AFILIADO_COLUMNA", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void borrarDatosProgramacion(int idProgramacion, int idUsuario, bool isReprogramar = false)
        {
            objDataLayer.AddGenericParameter("@id_programacion", DbType.Int32, ParameterDirection.Input, idProgramacion);
            objDataLayer.AddGenericParameter("@id_usuario", DbType.Int32, ParameterDirection.Input, idUsuario);
            objDataLayer.AddGenericParameter("@isReprogramar", DbType.Boolean, ParameterDirection.Input, isReprogramar);
            try
            {
                objDataLayer.ExecuteStoredProcedure("P_RIPS_ELIMINAR_DATOS_PROGRAMACION", DataLayer.ReturnType.NothingType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
