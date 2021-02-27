Imports Microsoft.VisualBasic

Public Class Credito
    Public Sub New(
   ByVal PR_KT_SUBPROD As String,
   ByVal PR_CA_REF1_PARENTE As String,
   ByVal PR_CD_CP As String,
   ByVal PR_CA_REF1_NOMBRE As String,
   ByVal PR_CA_REF4_DOM As String,
   ByVal PR_CA_REF4_PARENTE As String,
   ByVal PR_KT_VISITADOR As String,
   ByVal PR_KT_AGENCIA As String,
   ByVal PR_KT_CODIGOVISITA As String,
   ByVal PR_CA_REF2_DOM As String,
   ByVal PR_CA_REF2_PARENTE As String,
   ByVal PR_CA_REF4_NOMBRE As String,
   ByVal PR_KT_EXPEDIENTE As String,
   ByVal PR_KT_USUARIO As String,
   ByVal PR_CD_TEL_CASA As String,
   ByVal PR_CA_REF3_NOMBRE As String,
   ByVal PR_CF_PERI_CREDI As String,
   ByVal PR_KT_ESTATUS As String,
   ByVal PR_KT_CODRELEV As String,
   ByVal PR_KT_RESULTADOV As String,
   ByVal PR_CD_RFC As String,
   ByVal PR_CD_EDO As String,
   ByVal PR_CD_LADA_CEL As String,
   ByVal PR_CA_CONVENIO As String,
   ByVal PR_KT_FILA As String,
   ByVal PR_KT_PRODUCTO As String,
   ByVal PR_KT_RESULTADO As String,
   ByVal PR_CD_DOM As String,
   ByVal PR_CD_LADA_CASA As String,
   ByVal PR_CA_BANCO As String,
   ByVal PR_CA_CLABE As String,
   ByVal PR_CA_REF2_NOMBRE As String,
   ByVal PR_KT_CREDITO As String,
   ByVal PR_CD_COLONIA As String,
   ByVal PR_CD_TEL_CEL As String,
   ByVal PR_CA_PRODUCTOS As String,
   ByVal PR_CA_REF3_DOM As String,
   ByVal PR_CA_REF3_PARENTE As String,
   ByVal PR_CF_ESTATUS As String,
   ByVal PR_CA_EMPRESA As String,
   ByVal PR_CA_EMP_COL As String,
   ByVal PR_CA_EMP_POBLACION As String,
   ByVal PR_CA_EMP_EDO As String,
   ByVal PR_CA_REF1_DOM As String,
   ByVal PR_KT_UASIGNADO As String,
   ByVal PR_KT_RESULTADORELEV As String,
   ByVal PR_CD_NOMBRE As String,
   ByVal PR_CD_POBLACION As String,
   ByVal PR_CA_ESTATUS_ACT As String,
   ByVal PR_CA_EMP_DOM As String,
   ByVal PR_CA_EMP_CP As String,
   ByVal PR_KT_CODIGO As String,
   ByVal PR_KT_MONTOPP As String,
   ByVal PR_CA_EMP_TEL_OFF As String,
   ByVal PR_CF_NO_PAGOS As String,
   ByVal PR_CF_IVA_SIG_PAGO As String,
   ByVal PR_CF_MOROSIDAD As String,
   ByVal PR_KT_NOGESTIONES As String,
   ByVal PR_CA_REF1_LADA As String,
   ByVal PR_CA_REF1_TEL As String,
   ByVal PR_CA_REF2_TEL As String,
   ByVal PR_CA_REF4_LADA As String,
   ByVal PR_CF_SALDO_ACORT As String,
   ByVal PR_CF_INT_PAGO As String,
   ByVal PR_CF_NO_PAGOS_FUT As String,
   ByVal PR_CF_MTO_PRESTAMO As String,
   ByVal PR_CF_SALDO As String,
   ByVal PR_CF_CAP_SIG_PAGO As String,
   ByVal PR_CF_CAT As String,
   ByVal PR_CF_IVA_APER As String,
   ByVal PR_CF_TASA_COMISION As String,
   ByVal PR_CF_SALDO_VEN As String,
   ByVal PR_KT_NOVISITAS As String,
   ByVal PR_KT_MTOUP As String,
   ByVal PR_CA_CREDITOS As String,
   ByVal PR_CF_TASA_MOR As String,
   ByVal PR_CA_EMP_TEL_LADA As String,
   ByVal PR_CA_EMP_TEL_EXT As String,
   ByVal PR_CA_REF4_TEL As String,
   ByVal PR_CF_CAPITAL_CONDO As String,
   ByVal PR_CF_MONTO_DEV As String,
   ByVal PR_KT_MODAL As String,
   ByVal PR_CF_CAPITAL_PAGO As String,
   ByVal PR_CF_IMP_RET As String,
   ByVal PR_CF_NO_PAGOS_VEN As String,
   ByVal PR_CF_SALDO_PAGARE As String,
   ByVal PR_CF_CAPITAL As String,
   ByVal PR_CF_INACTIVIDAD As String,
   ByVal PR_CA_REF2_LADA As String,
   ByVal PR_CA_REF3_TEL As String,
   ByVal PR_CF_MONTO As String,
   ByVal PR_CF_MONTO_SIG_PAGO As String,
   ByVal PR_CF_INT_SIG_PAGO As String,
   ByVal PR_CF_SALDO_TOT As String,
   ByVal PR_CF_SALDO_CARTERA As String,
   ByVal PR_KT_SECFILA As String,
   ByVal PR_CA_REF3_LADA As String,
   ByVal PR_CF_PLAZO As String,
   ByVal PR_CF_TASA_ORD As String,
   ByVal PR_CF_COMIS_APER As String,
   ByVal PR_CF_MONTO_ULTIMO_PAG As String,
   ByVal PR_KT_DTEFILA As String,
   ByVal PR_KT_DTERESULTADORELEV As String,
   ByVal PR_KT_DTEPCONTACTO As String,
   ByVal PR_KT_DTEVISITA As String,
   ByVal PR_KT_DTECARGAINI As String,
   ByVal PR_CF_PERI_CORTE As String,
   ByVal PR_KT_DTEUP As String,
   ByVal PR_CF_FECHA_DISP As String,
   ByVal PR_CF_FECHA_SIG_PAGO As String,
   ByVal PR_KT_DTERETIRO As String,
   ByVal PR_KT_DTEGESTION As String,
   ByVal PR_CF_FECHA_ULTIMO_PAG As String,
   ByVal PR_KT_DTEPP As String,
   ByVal PR_KT_DTECARGA As String,
   ByVal PR_KT_DTEASIGNA As String,
   ByVal VI_ALERTA_GESTION As String,
   ByVal VI_DIAS_ALERTA_GESTION As String,
   ByVal PR_KT_CUANTASCUENTASFILA As String,
   ByVal PR_KT_CUENTATRABAJADAFILA As String,
   ByVal Avisos As String,
   ByVal PR_KT_UASIGNADOV As String
   )
        Me._PR_KT_SUBPROD = PR_KT_SUBPROD
        Me._PR_CA_REF1_PARENTE = PR_CA_REF1_PARENTE
        Me._PR_CD_CP = PR_CD_CP
        Me._PR_CA_REF1_NOMBRE = PR_CA_REF1_NOMBRE
        Me._PR_CA_REF4_DOM = PR_CA_REF4_DOM
        Me._PR_CA_REF4_PARENTE = PR_CA_REF4_PARENTE
        Me._PR_KT_VISITADOR = PR_KT_VISITADOR
        Me._PR_KT_AGENCIA = PR_KT_AGENCIA
        Me._PR_KT_CODIGOVISITA = PR_KT_CODIGOVISITA
        Me._PR_CA_REF2_DOM = PR_CA_REF2_DOM
        Me._PR_CA_REF2_PARENTE = PR_CA_REF2_PARENTE
        Me._PR_CA_REF4_NOMBRE = PR_CA_REF4_NOMBRE
        Me._PR_KT_EXPEDIENTE = PR_KT_EXPEDIENTE
        Me._PR_KT_USUARIO = PR_KT_USUARIO
        Me._PR_CD_TEL_CASA = PR_CD_TEL_CASA
        Me._PR_CA_REF3_NOMBRE = PR_CA_REF3_NOMBRE
        Me._PR_CF_PERI_CREDI = PR_CF_PERI_CREDI
        Me._PR_KT_ESTATUS = PR_KT_ESTATUS
        Me._PR_KT_CODRELEV = PR_KT_CODRELEV
        Me._PR_KT_RESULTADOV = PR_KT_RESULTADOV
        Me._PR_CD_RFC = PR_CD_RFC
        Me._PR_CD_EDO = PR_CD_EDO
        Me._PR_CD_LADA_CEL = PR_CD_LADA_CEL
        Me._PR_CA_CONVENIO = PR_CA_CONVENIO
        Me._PR_KT_FILA = PR_KT_FILA
        Me._PR_KT_PRODUCTO = PR_KT_PRODUCTO
        Me._PR_KT_RESULTADO = PR_KT_RESULTADO
        Me._PR_CD_DOM = PR_CD_DOM
        Me._PR_CD_LADA_CASA = PR_CD_LADA_CASA
        Me._PR_CA_BANCO = PR_CA_BANCO
        Me._PR_CA_CLABE = PR_CA_CLABE
        Me._PR_CA_REF2_NOMBRE = PR_CA_REF2_NOMBRE
        Me._PR_KT_CREDITO = PR_KT_CREDITO
        Me._PR_CD_COLONIA = PR_CD_COLONIA
        Me._PR_CD_TEL_CEL = PR_CD_TEL_CEL
        Me._PR_CA_PRODUCTOS = PR_CA_PRODUCTOS
        Me._PR_CA_REF3_DOM = PR_CA_REF3_DOM
        Me._PR_CA_REF3_PARENTE = PR_CA_REF3_PARENTE
        Me._PR_CF_ESTATUS = PR_CF_ESTATUS
        Me._PR_CA_EMPRESA = PR_CA_EMPRESA
        Me._PR_CA_EMP_COL = PR_CA_EMP_COL
        Me._PR_CA_EMP_POBLACION = PR_CA_EMP_POBLACION
        Me._PR_CA_EMP_EDO = PR_CA_EMP_EDO
        Me._PR_CA_REF1_DOM = PR_CA_REF1_DOM
        Me._PR_KT_UASIGNADO = PR_KT_UASIGNADO
        Me._PR_KT_RESULTADORELEV = PR_KT_RESULTADORELEV
        Me._PR_CD_NOMBRE = PR_CD_NOMBRE
        Me._PR_CD_POBLACION = PR_CD_POBLACION
        Me._PR_CA_ESTATUS_ACT = PR_CA_ESTATUS_ACT
        Me._PR_CA_EMP_DOM = PR_CA_EMP_DOM
        Me._PR_CA_EMP_CP = PR_CA_EMP_CP
        Me._PR_KT_CODIGO = PR_KT_CODIGO
        Me._PR_KT_MONTOPP = PR_KT_MONTOPP
        Me._PR_CA_EMP_TEL_OFF = PR_CA_EMP_TEL_OFF
        Me._PR_CF_NO_PAGOS = PR_CF_NO_PAGOS
        Me._PR_CF_IVA_SIG_PAGO = PR_CF_IVA_SIG_PAGO
        Me._PR_CF_MOROSIDAD = PR_CF_MOROSIDAD
        Me._PR_KT_NOGESTIONES = PR_KT_NOGESTIONES
        Me._PR_CA_REF1_LADA = PR_CA_REF1_LADA
        Me._PR_CA_REF1_TEL = PR_CA_REF1_TEL
        Me._PR_CA_REF2_TEL = PR_CA_REF2_TEL
        Me._PR_CA_REF4_LADA = PR_CA_REF4_LADA
        Me._PR_CF_SALDO_ACORT = PR_CF_SALDO_ACORT
        Me._PR_CF_INT_PAGO = PR_CF_INT_PAGO
        Me._PR_CF_NO_PAGOS_FUT = PR_CF_NO_PAGOS_FUT
        Me._PR_CF_MTO_PRESTAMO = PR_CF_MTO_PRESTAMO
        Me._PR_CF_SALDO = PR_CF_SALDO
        Me._PR_CF_CAP_SIG_PAGO = PR_CF_CAP_SIG_PAGO
        Me._PR_CF_CAT = PR_CF_CAT
        Me._PR_CF_IVA_APER = PR_CF_IVA_APER
        Me._PR_CF_TASA_COMISION = PR_CF_TASA_COMISION
        Me._PR_CF_SALDO_VEN = PR_CF_SALDO_VEN
        Me._PR_KT_NOVISITAS = PR_KT_NOVISITAS
        Me._PR_KT_MTOUP = PR_KT_MTOUP
        Me._PR_CA_CREDITOS = PR_CA_CREDITOS
        Me._PR_CF_TASA_MOR = PR_CF_TASA_MOR
        Me._PR_CA_EMP_TEL_LADA = PR_CA_EMP_TEL_LADA
        Me._PR_CA_EMP_TEL_EXT = PR_CA_EMP_TEL_EXT
        Me._PR_CA_REF4_TEL = PR_CA_REF4_TEL
        Me._PR_CF_CAPITAL_CONDO = PR_CF_CAPITAL_CONDO
        Me._PR_CF_MONTO_DEV = PR_CF_MONTO_DEV
        Me._PR_KT_MODAL = PR_KT_MODAL
        Me._PR_CF_CAPITAL_PAGO = PR_CF_CAPITAL_PAGO
        Me._PR_CF_IMP_RET = PR_CF_IMP_RET
        Me._PR_CF_NO_PAGOS_VEN = PR_CF_NO_PAGOS_VEN
        Me._PR_CF_SALDO_PAGARE = PR_CF_SALDO_PAGARE
        Me._PR_CF_CAPITAL = PR_CF_CAPITAL
        Me._PR_CF_INACTIVIDAD = PR_CF_INACTIVIDAD
        Me._PR_CA_REF2_LADA = PR_CA_REF2_LADA
        Me._PR_CA_REF3_TEL = PR_CA_REF3_TEL
        Me._PR_CF_MONTO = PR_CF_MONTO
        Me._PR_CF_MONTO_SIG_PAGO = PR_CF_MONTO_SIG_PAGO
        Me._PR_CF_INT_SIG_PAGO = PR_CF_INT_SIG_PAGO
        Me._PR_CF_SALDO_TOT = PR_CF_SALDO_TOT
        Me._PR_CF_SALDO_CARTERA = PR_CF_SALDO_CARTERA
        Me._PR_KT_SECFILA = PR_KT_SECFILA
        Me._PR_CA_REF3_LADA = PR_CA_REF3_LADA
        Me._PR_CF_PLAZO = PR_CF_PLAZO
        Me._PR_CF_TASA_ORD = PR_CF_TASA_ORD
        Me._PR_CF_COMIS_APER = PR_CF_COMIS_APER
        Me._PR_CF_MONTO_ULTIMO_PAG = PR_CF_MONTO_ULTIMO_PAG
        Me._PR_KT_DTEFILA = PR_KT_DTEFILA
        Me._PR_KT_DTERESULTADORELEV = PR_KT_DTERESULTADORELEV
        Me._PR_KT_DTEPCONTACTO = PR_KT_DTEPCONTACTO
        Me._PR_KT_DTEVISITA = PR_KT_DTEVISITA
        Me._PR_KT_DTECARGAINI = PR_KT_DTECARGAINI
        Me._PR_CF_PERI_CORTE = PR_CF_PERI_CORTE
        Me._PR_KT_DTEUP = PR_KT_DTEUP
        Me._PR_CF_FECHA_DISP = PR_CF_FECHA_DISP
        Me._PR_CF_FECHA_SIG_PAGO = PR_CF_FECHA_SIG_PAGO
        Me._PR_KT_DTERETIRO = PR_KT_DTERETIRO
        Me._PR_KT_DTEGESTION = PR_KT_DTEGESTION
        Me._PR_CF_FECHA_ULTIMO_PAG = PR_CF_FECHA_ULTIMO_PAG
        Me._PR_KT_DTEPP = PR_KT_DTEPP
        Me._PR_KT_DTECARGA = PR_KT_DTECARGA
        Me._PR_KT_DTEASIGNA = PR_KT_DTEASIGNA
        Me._VI_ALERTA_GESTION = VI_ALERTA_GESTION
        Me._VI_DIAS_ALERTA_GESTION = VI_DIAS_ALERTA_GESTION
        Me._PR_KT_CUANTASCUENTASFILA = PR_KT_CUANTASCUENTASFILA
        Me._PR_KT_CUENTATRABAJADAFILA = PR_KT_CUENTATRABAJADAFILA
        Me._Avisos = Avisos
        Me._PR_KT_UASIGNADOV = PR_KT_UASIGNADOV
    End Sub



    Private _PR_KT_SUBPROD As String
    Property PR_KT_SUBPROD() As String
        Get
            Return _PR_KT_SUBPROD
        End Get
        Set(ByVal value As String)
            _PR_KT_SUBPROD = value
        End Set
    End Property

    Private _PR_CA_REF1_PARENTE As String
    Property PR_CA_REF1_PARENTE() As String
        Get
            Return _PR_CA_REF1_PARENTE
        End Get
        Set(ByVal value As String)
            _PR_CA_REF1_PARENTE = value
        End Set
    End Property

    Private _PR_CD_CP As String
    Property PR_CD_CP() As String
        Get
            Return _PR_CD_CP
        End Get
        Set(ByVal value As String)
            _PR_CD_CP = value
        End Set
    End Property

    Private _PR_CA_REF1_NOMBRE As String
    Property PR_CA_REF1_NOMBRE() As String
        Get
            Return _PR_CA_REF1_NOMBRE
        End Get
        Set(ByVal value As String)
            _PR_CA_REF1_NOMBRE = value
        End Set
    End Property

    Private _PR_CA_REF4_DOM As String
    Property PR_CA_REF4_DOM() As String
        Get
            Return _PR_CA_REF4_DOM
        End Get
        Set(ByVal value As String)
            _PR_CA_REF4_DOM = value
        End Set
    End Property

    Private _PR_CA_REF4_PARENTE As String
    Property PR_CA_REF4_PARENTE() As String
        Get
            Return _PR_CA_REF4_PARENTE
        End Get
        Set(ByVal value As String)
            _PR_CA_REF4_PARENTE = value
        End Set
    End Property

    Private _PR_KT_VISITADOR As String
    Property PR_KT_VISITADOR() As String
        Get
            Return _PR_KT_VISITADOR
        End Get
        Set(ByVal value As String)
            _PR_KT_VISITADOR = value
        End Set
    End Property

    Private _PR_KT_AGENCIA As String
    Property PR_KT_AGENCIA() As String
        Get
            Return _PR_KT_AGENCIA
        End Get
        Set(ByVal value As String)
            _PR_KT_AGENCIA = value
        End Set
    End Property

    Private _PR_KT_CODIGOVISITA As String
    Property PR_KT_CODIGOVISITA() As String
        Get
            Return _PR_KT_CODIGOVISITA
        End Get
        Set(ByVal value As String)
            _PR_KT_CODIGOVISITA = value
        End Set
    End Property

    Private _PR_CA_REF2_DOM As String
    Property PR_CA_REF2_DOM() As String
        Get
            Return _PR_CA_REF2_DOM
        End Get
        Set(ByVal value As String)
            _PR_CA_REF2_DOM = value
        End Set
    End Property

    Private _PR_CA_REF2_PARENTE As String
    Property PR_CA_REF2_PARENTE() As String
        Get
            Return _PR_CA_REF2_PARENTE
        End Get
        Set(ByVal value As String)
            _PR_CA_REF2_PARENTE = value
        End Set
    End Property

    Private _PR_CA_REF4_NOMBRE As String
    Property PR_CA_REF4_NOMBRE() As String
        Get
            Return _PR_CA_REF4_NOMBRE
        End Get
        Set(ByVal value As String)
            _PR_CA_REF4_NOMBRE = value
        End Set
    End Property

    Private _PR_KT_EXPEDIENTE As String
    Property PR_KT_EXPEDIENTE() As String
        Get
            Return _PR_KT_EXPEDIENTE
        End Get
        Set(ByVal value As String)
            _PR_KT_EXPEDIENTE = value
        End Set
    End Property

    Private _PR_KT_USUARIO As String
    Property PR_KT_USUARIO() As String
        Get
            Return _PR_KT_USUARIO
        End Get
        Set(ByVal value As String)
            _PR_KT_USUARIO = value
        End Set
    End Property

    Private _PR_CD_TEL_CASA As String
    Property PR_CD_TEL_CASA() As String
        Get
            Return _PR_CD_TEL_CASA
        End Get
        Set(ByVal value As String)
            _PR_CD_TEL_CASA = value
        End Set
    End Property

    Private _PR_CA_REF3_NOMBRE As String
    Property PR_CA_REF3_NOMBRE() As String
        Get
            Return _PR_CA_REF3_NOMBRE
        End Get
        Set(ByVal value As String)
            _PR_CA_REF3_NOMBRE = value
        End Set
    End Property

    Private _PR_CF_PERI_CREDI As String
    Property PR_CF_PERI_CREDI() As String
        Get
            Return _PR_CF_PERI_CREDI
        End Get
        Set(ByVal value As String)
            _PR_CF_PERI_CREDI = value
        End Set
    End Property

    Private _PR_KT_ESTATUS As String
    Property PR_KT_ESTATUS() As String
        Get
            Return _PR_KT_ESTATUS
        End Get
        Set(ByVal value As String)
            _PR_KT_ESTATUS = value
        End Set
    End Property

    Private _PR_KT_CODRELEV As String
    Property PR_KT_CODRELEV() As String
        Get
            Return _PR_KT_CODRELEV
        End Get
        Set(ByVal value As String)
            _PR_KT_CODRELEV = value
        End Set
    End Property

    Private _PR_KT_RESULTADOV As String
    Property PR_KT_RESULTADOV() As String
        Get
            Return _PR_KT_RESULTADOV
        End Get
        Set(ByVal value As String)
            _PR_KT_RESULTADOV = value
        End Set
    End Property

    Private _PR_CD_RFC As String
    Property PR_CD_RFC() As String
        Get
            Return _PR_CD_RFC
        End Get
        Set(ByVal value As String)
            _PR_CD_RFC = value
        End Set
    End Property

    Private _PR_CD_EDO As String
    Property PR_CD_EDO() As String
        Get
            Return _PR_CD_EDO
        End Get
        Set(ByVal value As String)
            _PR_CD_EDO = value
        End Set
    End Property

    Private _PR_CD_LADA_CEL As String
    Property PR_CD_LADA_CEL() As String
        Get
            Return _PR_CD_LADA_CEL
        End Get
        Set(ByVal value As String)
            _PR_CD_LADA_CEL = value
        End Set
    End Property

    Private _PR_CA_CONVENIO As String
    Property PR_CA_CONVENIO() As String
        Get
            Return _PR_CA_CONVENIO
        End Get
        Set(ByVal value As String)
            _PR_CA_CONVENIO = value
        End Set
    End Property

    Private _PR_KT_FILA As String
    Property PR_KT_FILA() As String
        Get
            Return _PR_KT_FILA
        End Get
        Set(ByVal value As String)
            _PR_KT_FILA = value
        End Set
    End Property

    Private _PR_KT_PRODUCTO As String
    Property PR_KT_PRODUCTO() As String
        Get
            Return _PR_KT_PRODUCTO
        End Get
        Set(ByVal value As String)
            _PR_KT_PRODUCTO = value
        End Set
    End Property

    Private _PR_KT_RESULTADO As String
    Property PR_KT_RESULTADO() As String
        Get
            Return _PR_KT_RESULTADO
        End Get
        Set(ByVal value As String)
            _PR_KT_RESULTADO = value
        End Set
    End Property

    Private _PR_CD_DOM As String
    Property PR_CD_DOM() As String
        Get
            Return _PR_CD_DOM
        End Get
        Set(ByVal value As String)
            _PR_CD_DOM = value
        End Set
    End Property

    Private _PR_CD_LADA_CASA As String
    Property PR_CD_LADA_CASA() As String
        Get
            Return _PR_CD_LADA_CASA
        End Get
        Set(ByVal value As String)
            _PR_CD_LADA_CASA = value
        End Set
    End Property

    Private _PR_CA_BANCO As String
    Property PR_CA_BANCO() As String
        Get
            Return _PR_CA_BANCO
        End Get
        Set(ByVal value As String)
            _PR_CA_BANCO = value
        End Set
    End Property

    Private _PR_CA_CLABE As String
    Property PR_CA_CLABE() As String
        Get
            Return _PR_CA_CLABE
        End Get
        Set(ByVal value As String)
            _PR_CA_CLABE = value
        End Set
    End Property

    Private _PR_CA_REF2_NOMBRE As String
    Property PR_CA_REF2_NOMBRE() As String
        Get
            Return _PR_CA_REF2_NOMBRE
        End Get
        Set(ByVal value As String)
            _PR_CA_REF2_NOMBRE = value
        End Set
    End Property

    Private _PR_KT_CREDITO As String
    Property PR_KT_CREDITO() As String
        Get
            Return _PR_KT_CREDITO
        End Get
        Set(ByVal value As String)
            _PR_KT_CREDITO = value
        End Set
    End Property

    Private _PR_CD_COLONIA As String
    Property PR_CD_COLONIA() As String
        Get
            Return _PR_CD_COLONIA
        End Get
        Set(ByVal value As String)
            _PR_CD_COLONIA = value
        End Set
    End Property

    Private _PR_CD_TEL_CEL As String
    Property PR_CD_TEL_CEL() As String
        Get
            Return _PR_CD_TEL_CEL
        End Get
        Set(ByVal value As String)
            _PR_CD_TEL_CEL = value
        End Set
    End Property

    Private _PR_CA_PRODUCTOS As String
    Property PR_CA_PRODUCTOS() As String
        Get
            Return _PR_CA_PRODUCTOS
        End Get
        Set(ByVal value As String)
            _PR_CA_PRODUCTOS = value
        End Set
    End Property

    Private _PR_CA_REF3_DOM As String
    Property PR_CA_REF3_DOM() As String
        Get
            Return _PR_CA_REF3_DOM
        End Get
        Set(ByVal value As String)
            _PR_CA_REF3_DOM = value
        End Set
    End Property

    Private _PR_CA_REF3_PARENTE As String
    Property PR_CA_REF3_PARENTE() As String
        Get
            Return _PR_CA_REF3_PARENTE
        End Get
        Set(ByVal value As String)
            _PR_CA_REF3_PARENTE = value
        End Set
    End Property

    Private _PR_CF_ESTATUS As String
    Property PR_CF_ESTATUS() As String
        Get
            Return _PR_CF_ESTATUS
        End Get
        Set(ByVal value As String)
            _PR_CF_ESTATUS = value
        End Set
    End Property

    Private _PR_CA_EMPRESA As String
    Property PR_CA_EMPRESA() As String
        Get
            Return _PR_CA_EMPRESA
        End Get
        Set(ByVal value As String)
            _PR_CA_EMPRESA = value
        End Set
    End Property

    Private _PR_CA_EMP_COL As String
    Property PR_CA_EMP_COL() As String
        Get
            Return _PR_CA_EMP_COL
        End Get
        Set(ByVal value As String)
            _PR_CA_EMP_COL = value
        End Set
    End Property

    Private _PR_CA_EMP_POBLACION As String
    Property PR_CA_EMP_POBLACION() As String
        Get
            Return _PR_CA_EMP_POBLACION
        End Get
        Set(ByVal value As String)
            _PR_CA_EMP_POBLACION = value
        End Set
    End Property

    Private _PR_CA_EMP_EDO As String
    Property PR_CA_EMP_EDO() As String
        Get
            Return _PR_CA_EMP_EDO
        End Get
        Set(ByVal value As String)
            _PR_CA_EMP_EDO = value
        End Set
    End Property

    Private _PR_CA_REF1_DOM As String
    Property PR_CA_REF1_DOM() As String
        Get
            Return _PR_CA_REF1_DOM
        End Get
        Set(ByVal value As String)
            _PR_CA_REF1_DOM = value
        End Set
    End Property

    Private _PR_KT_UASIGNADO As String
    Property PR_KT_UASIGNADO() As String
        Get
            Return _PR_KT_UASIGNADO
        End Get
        Set(ByVal value As String)
            _PR_KT_UASIGNADO = value
        End Set
    End Property

    Private _PR_KT_RESULTADORELEV As String
    Property PR_KT_RESULTADORELEV() As String
        Get
            Return _PR_KT_RESULTADORELEV
        End Get
        Set(ByVal value As String)
            _PR_KT_RESULTADORELEV = value
        End Set
    End Property

    Private _PR_CD_NOMBRE As String
    Property PR_CD_NOMBRE() As String
        Get
            Return _PR_CD_NOMBRE
        End Get
        Set(ByVal value As String)
            _PR_CD_NOMBRE = value
        End Set
    End Property

    Private _PR_CD_POBLACION As String
    Property PR_CD_POBLACION() As String
        Get
            Return _PR_CD_POBLACION
        End Get
        Set(ByVal value As String)
            _PR_CD_POBLACION = value
        End Set
    End Property

    Private _PR_CA_ESTATUS_ACT As String
    Property PR_CA_ESTATUS_ACT() As String
        Get
            Return _PR_CA_ESTATUS_ACT
        End Get
        Set(ByVal value As String)
            _PR_CA_ESTATUS_ACT = value
        End Set
    End Property

    Private _PR_CA_EMP_DOM As String
    Property PR_CA_EMP_DOM() As String
        Get
            Return _PR_CA_EMP_DOM
        End Get
        Set(ByVal value As String)
            _PR_CA_EMP_DOM = value
        End Set
    End Property

    Private _PR_CA_EMP_CP As String
    Property PR_CA_EMP_CP() As String
        Get
            Return _PR_CA_EMP_CP
        End Get
        Set(ByVal value As String)
            _PR_CA_EMP_CP = value
        End Set
    End Property

    Private _PR_KT_CODIGO As String
    Property PR_KT_CODIGO() As String
        Get
            Return _PR_KT_CODIGO
        End Get
        Set(ByVal value As String)
            _PR_KT_CODIGO = value
        End Set
    End Property

    Private _PR_KT_MONTOPP As String
    Property PR_KT_MONTOPP() As String
        Get
            Return _PR_KT_MONTOPP
        End Get
        Set(ByVal value As String)
            _PR_KT_MONTOPP = value
        End Set
    End Property

    Private _PR_CA_EMP_TEL_OFF As String
    Property PR_CA_EMP_TEL_OFF() As String
        Get
            Return _PR_CA_EMP_TEL_OFF
        End Get
        Set(ByVal value As String)
            _PR_CA_EMP_TEL_OFF = value
        End Set
    End Property

    Private _PR_CF_NO_PAGOS As String
    Property PR_CF_NO_PAGOS() As String
        Get
            Return _PR_CF_NO_PAGOS
        End Get
        Set(ByVal value As String)
            _PR_CF_NO_PAGOS = value
        End Set
    End Property

    Private _PR_CF_IVA_SIG_PAGO As String
    Property PR_CF_IVA_SIG_PAGO() As String
        Get
            Return _PR_CF_IVA_SIG_PAGO
        End Get
        Set(ByVal value As String)
            _PR_CF_IVA_SIG_PAGO = value
        End Set
    End Property

    Private _PR_CF_MOROSIDAD As String
    Property PR_CF_MOROSIDAD() As String
        Get
            Return _PR_CF_MOROSIDAD
        End Get
        Set(ByVal value As String)
            _PR_CF_MOROSIDAD = value
        End Set
    End Property

    Private _PR_KT_NOGESTIONES As String
    Property PR_KT_NOGESTIONES() As String
        Get
            Return _PR_KT_NOGESTIONES
        End Get
        Set(ByVal value As String)
            _PR_KT_NOGESTIONES = value
        End Set
    End Property

    Private _PR_CA_REF1_LADA As String
    Property PR_CA_REF1_LADA() As String
        Get
            Return _PR_CA_REF1_LADA
        End Get
        Set(ByVal value As String)
            _PR_CA_REF1_LADA = value
        End Set
    End Property

    Private _PR_CA_REF1_TEL As String
    Property PR_CA_REF1_TEL() As String
        Get
            Return _PR_CA_REF1_TEL
        End Get
        Set(ByVal value As String)
            _PR_CA_REF1_TEL = value
        End Set
    End Property

    Private _PR_CA_REF2_TEL As String
    Property PR_CA_REF2_TEL() As String
        Get
            Return _PR_CA_REF2_TEL
        End Get
        Set(ByVal value As String)
            _PR_CA_REF2_TEL = value
        End Set
    End Property

    Private _PR_CA_REF4_LADA As String
    Property PR_CA_REF4_LADA() As String
        Get
            Return _PR_CA_REF4_LADA
        End Get
        Set(ByVal value As String)
            _PR_CA_REF4_LADA = value
        End Set
    End Property

    Private _PR_CF_SALDO_ACORT As String
    Property PR_CF_SALDO_ACORT() As String
        Get
            Return _PR_CF_SALDO_ACORT
        End Get
        Set(ByVal value As String)
            _PR_CF_SALDO_ACORT = value
        End Set
    End Property

    Private _PR_CF_INT_PAGO As String
    Property PR_CF_INT_PAGO() As String
        Get
            Return _PR_CF_INT_PAGO
        End Get
        Set(ByVal value As String)
            _PR_CF_INT_PAGO = value
        End Set
    End Property

    Private _PR_CF_NO_PAGOS_FUT As String
    Property PR_CF_NO_PAGOS_FUT() As String
        Get
            Return _PR_CF_NO_PAGOS_FUT
        End Get
        Set(ByVal value As String)
            _PR_CF_NO_PAGOS_FUT = value
        End Set
    End Property

    Private _PR_CF_MTO_PRESTAMO As String
    Property PR_CF_MTO_PRESTAMO() As String
        Get
            Return _PR_CF_MTO_PRESTAMO
        End Get
        Set(ByVal value As String)
            _PR_CF_MTO_PRESTAMO = value
        End Set
    End Property

    Private _PR_CF_SALDO As String
    Property PR_CF_SALDO() As String
        Get
            Return _PR_CF_SALDO
        End Get
        Set(ByVal value As String)
            _PR_CF_SALDO = value
        End Set
    End Property

    Private _PR_CF_CAP_SIG_PAGO As String
    Property PR_CF_CAP_SIG_PAGO() As String
        Get
            Return _PR_CF_CAP_SIG_PAGO
        End Get
        Set(ByVal value As String)
            _PR_CF_CAP_SIG_PAGO = value
        End Set
    End Property

    Private _PR_CF_CAT As String
    Property PR_CF_CAT() As String
        Get
            Return _PR_CF_CAT
        End Get
        Set(ByVal value As String)
            _PR_CF_CAT = value
        End Set
    End Property

    Private _PR_CF_IVA_APER As String
    Property PR_CF_IVA_APER() As String
        Get
            Return _PR_CF_IVA_APER
        End Get
        Set(ByVal value As String)
            _PR_CF_IVA_APER = value
        End Set
    End Property

    Private _PR_CF_TASA_COMISION As String
    Property PR_CF_TASA_COMISION() As String
        Get
            Return _PR_CF_TASA_COMISION
        End Get
        Set(ByVal value As String)
            _PR_CF_TASA_COMISION = value
        End Set
    End Property

    Private _PR_CF_SALDO_VEN As String
    Property PR_CF_SALDO_VEN() As String
        Get
            Return _PR_CF_SALDO_VEN
        End Get
        Set(ByVal value As String)
            _PR_CF_SALDO_VEN = value
        End Set
    End Property

    Private _PR_KT_NOVISITAS As String
    Property PR_KT_NOVISITAS() As String
        Get
            Return _PR_KT_NOVISITAS
        End Get
        Set(ByVal value As String)
            _PR_KT_NOVISITAS = value
        End Set
    End Property

    Private _PR_KT_MTOUP As String
    Property PR_KT_MTOUP() As String
        Get
            Return _PR_KT_MTOUP
        End Get
        Set(ByVal value As String)
            _PR_KT_MTOUP = value
        End Set
    End Property

    Private _PR_CA_CREDITOS As String
    Property PR_CA_CREDITOS() As String
        Get
            Return _PR_CA_CREDITOS
        End Get
        Set(ByVal value As String)
            _PR_CA_CREDITOS = value
        End Set
    End Property

    Private _PR_CF_TASA_MOR As String
    Property PR_CF_TASA_MOR() As String
        Get
            Return _PR_CF_TASA_MOR
        End Get
        Set(ByVal value As String)
            _PR_CF_TASA_MOR = value
        End Set
    End Property

    Private _PR_CA_EMP_TEL_LADA As String
    Property PR_CA_EMP_TEL_LADA() As String
        Get
            Return _PR_CA_EMP_TEL_LADA
        End Get
        Set(ByVal value As String)
            _PR_CA_EMP_TEL_LADA = value
        End Set
    End Property

    Private _PR_CA_EMP_TEL_EXT As String
    Property PR_CA_EMP_TEL_EXT() As String
        Get
            Return _PR_CA_EMP_TEL_EXT
        End Get
        Set(ByVal value As String)
            _PR_CA_EMP_TEL_EXT = value
        End Set
    End Property

    Private _PR_CA_REF4_TEL As String
    Property PR_CA_REF4_TEL() As String
        Get
            Return _PR_CA_REF4_TEL
        End Get
        Set(ByVal value As String)
            _PR_CA_REF4_TEL = value
        End Set
    End Property

    Private _PR_CF_CAPITAL_CONDO As String
    Property PR_CF_CAPITAL_CONDO() As String
        Get
            Return _PR_CF_CAPITAL_CONDO
        End Get
        Set(ByVal value As String)
            _PR_CF_CAPITAL_CONDO = value
        End Set
    End Property

    Private _PR_CF_MONTO_DEV As String
    Property PR_CF_MONTO_DEV() As String
        Get
            Return _PR_CF_MONTO_DEV
        End Get
        Set(ByVal value As String)
            _PR_CF_MONTO_DEV = value
        End Set
    End Property

    Private _PR_KT_MODAL As String
    Property PR_KT_MODAL() As String
        Get
            Return _PR_KT_MODAL
        End Get
        Set(ByVal value As String)
            _PR_KT_MODAL = value
        End Set
    End Property

    Private _PR_CF_CAPITAL_PAGO As String
    Property PR_CF_CAPITAL_PAGO() As String
        Get
            Return _PR_CF_CAPITAL_PAGO
        End Get
        Set(ByVal value As String)
            _PR_CF_CAPITAL_PAGO = value
        End Set
    End Property

    Private _PR_CF_IMP_RET As String
    Property PR_CF_IMP_RET() As String
        Get
            Return _PR_CF_IMP_RET
        End Get
        Set(ByVal value As String)
            _PR_CF_IMP_RET = value
        End Set
    End Property

    Private _PR_CF_NO_PAGOS_VEN As String
    Property PR_CF_NO_PAGOS_VEN() As String
        Get
            Return _PR_CF_NO_PAGOS_VEN
        End Get
        Set(ByVal value As String)
            _PR_CF_NO_PAGOS_VEN = value
        End Set
    End Property

    Private _PR_CF_SALDO_PAGARE As String
    Property PR_CF_SALDO_PAGARE() As String
        Get
            Return _PR_CF_SALDO_PAGARE
        End Get
        Set(ByVal value As String)
            _PR_CF_SALDO_PAGARE = value
        End Set
    End Property

    Private _PR_CF_CAPITAL As String
    Property PR_CF_CAPITAL() As String
        Get
            Return _PR_CF_CAPITAL
        End Get
        Set(ByVal value As String)
            _PR_CF_CAPITAL = value
        End Set
    End Property

    Private _PR_CF_INACTIVIDAD As String
    Property PR_CF_INACTIVIDAD() As String
        Get
            Return _PR_CF_INACTIVIDAD
        End Get
        Set(ByVal value As String)
            _PR_CF_INACTIVIDAD = value
        End Set
    End Property

    Private _PR_CA_REF2_LADA As String
    Property PR_CA_REF2_LADA() As String
        Get
            Return _PR_CA_REF2_LADA
        End Get
        Set(ByVal value As String)
            _PR_CA_REF2_LADA = value
        End Set
    End Property

    Private _PR_CA_REF3_TEL As String
    Property PR_CA_REF3_TEL() As String
        Get
            Return _PR_CA_REF3_TEL
        End Get
        Set(ByVal value As String)
            _PR_CA_REF3_TEL = value
        End Set
    End Property

    Private _PR_CF_MONTO As String
    Property PR_CF_MONTO() As String
        Get
            Return _PR_CF_MONTO
        End Get
        Set(ByVal value As String)
            _PR_CF_MONTO = value
        End Set
    End Property

    Private _PR_CF_MONTO_SIG_PAGO As String
    Property PR_CF_MONTO_SIG_PAGO() As String
        Get
            Return _PR_CF_MONTO_SIG_PAGO
        End Get
        Set(ByVal value As String)
            _PR_CF_MONTO_SIG_PAGO = value
        End Set
    End Property

    Private _PR_CF_INT_SIG_PAGO As String
    Property PR_CF_INT_SIG_PAGO() As String
        Get
            Return _PR_CF_INT_SIG_PAGO
        End Get
        Set(ByVal value As String)
            _PR_CF_INT_SIG_PAGO = value
        End Set
    End Property

    Private _PR_CF_SALDO_TOT As String
    Property PR_CF_SALDO_TOT() As String
        Get
            Return _PR_CF_SALDO_TOT
        End Get
        Set(ByVal value As String)
            _PR_CF_SALDO_TOT = value
        End Set
    End Property

    Private _PR_CF_SALDO_CARTERA As String
    Property PR_CF_SALDO_CARTERA() As String
        Get
            Return _PR_CF_SALDO_CARTERA
        End Get
        Set(ByVal value As String)
            _PR_CF_SALDO_CARTERA = value
        End Set
    End Property

    Private _PR_KT_SECFILA As String
    Property PR_KT_SECFILA() As String
        Get
            Return _PR_KT_SECFILA
        End Get
        Set(ByVal value As String)
            _PR_KT_SECFILA = value
        End Set
    End Property

    Private _PR_CA_REF3_LADA As String
    Property PR_CA_REF3_LADA() As String
        Get
            Return _PR_CA_REF3_LADA
        End Get
        Set(ByVal value As String)
            _PR_CA_REF3_LADA = value
        End Set
    End Property

    Private _PR_CF_PLAZO As String
    Property PR_CF_PLAZO() As String
        Get
            Return _PR_CF_PLAZO
        End Get
        Set(ByVal value As String)
            _PR_CF_PLAZO = value
        End Set
    End Property

    Private _PR_CF_TASA_ORD As String
    Property PR_CF_TASA_ORD() As String
        Get
            Return _PR_CF_TASA_ORD
        End Get
        Set(ByVal value As String)
            _PR_CF_TASA_ORD = value
        End Set
    End Property

    Private _PR_CF_COMIS_APER As String
    Property PR_CF_COMIS_APER() As String
        Get
            Return _PR_CF_COMIS_APER
        End Get
        Set(ByVal value As String)
            _PR_CF_COMIS_APER = value
        End Set
    End Property

    Private _PR_CF_MONTO_ULTIMO_PAG As String
    Property PR_CF_MONTO_ULTIMO_PAG() As String
        Get
            Return _PR_CF_MONTO_ULTIMO_PAG
        End Get
        Set(ByVal value As String)
            _PR_CF_MONTO_ULTIMO_PAG = value
        End Set
    End Property

    Private _PR_KT_DTEFILA As String
    Property PR_KT_DTEFILA() As String
        Get
            Return _PR_KT_DTEFILA
        End Get
        Set(ByVal value As String)
            _PR_KT_DTEFILA = value
        End Set
    End Property

    Private _PR_KT_DTERESULTADORELEV As String
    Property PR_KT_DTERESULTADORELEV() As String
        Get
            Return _PR_KT_DTERESULTADORELEV
        End Get
        Set(ByVal value As String)
            _PR_KT_DTERESULTADORELEV = value
        End Set
    End Property

    Private _PR_KT_DTEPCONTACTO As String
    Property PR_KT_DTEPCONTACTO() As String
        Get
            Return _PR_KT_DTEPCONTACTO
        End Get
        Set(ByVal value As String)
            _PR_KT_DTEPCONTACTO = value
        End Set
    End Property

    Private _PR_KT_DTEVISITA As String
    Property PR_KT_DTEVISITA() As String
        Get
            Return _PR_KT_DTEVISITA
        End Get
        Set(ByVal value As String)
            _PR_KT_DTEVISITA = value
        End Set
    End Property

    Private _PR_KT_DTECARGAINI As String
    Property PR_KT_DTECARGAINI() As String
        Get
            Return _PR_KT_DTECARGAINI
        End Get
        Set(ByVal value As String)
            _PR_KT_DTECARGAINI = value
        End Set
    End Property

    Private _PR_CF_PERI_CORTE As String
    Property PR_CF_PERI_CORTE() As String
        Get
            Return _PR_CF_PERI_CORTE
        End Get
        Set(ByVal value As String)
            _PR_CF_PERI_CORTE = value
        End Set
    End Property

    Private _PR_KT_DTEUP As String
    Property PR_KT_DTEUP() As String
        Get
            Return _PR_KT_DTEUP
        End Get
        Set(ByVal value As String)
            _PR_KT_DTEUP = value
        End Set
    End Property

    Private _PR_CF_FECHA_DISP As String
    Property PR_CF_FECHA_DISP() As String
        Get
            Return _PR_CF_FECHA_DISP
        End Get
        Set(ByVal value As String)
            _PR_CF_FECHA_DISP = value
        End Set
    End Property

    Private _PR_CF_FECHA_SIG_PAGO As String
    Property PR_CF_FECHA_SIG_PAGO() As String
        Get
            Return _PR_CF_FECHA_SIG_PAGO
        End Get
        Set(ByVal value As String)
            _PR_CF_FECHA_SIG_PAGO = value
        End Set
    End Property

    Private _PR_KT_DTERETIRO As String
    Property PR_KT_DTERETIRO() As String
        Get
            Return _PR_KT_DTERETIRO
        End Get
        Set(ByVal value As String)
            _PR_KT_DTERETIRO = value
        End Set
    End Property

    Private _PR_KT_DTEGESTION As String
    Property PR_KT_DTEGESTION() As String
        Get
            Return _PR_KT_DTEGESTION
        End Get
        Set(ByVal value As String)
            _PR_KT_DTEGESTION = value
        End Set
    End Property

    Private _PR_CF_FECHA_ULTIMO_PAG As String
    Property PR_CF_FECHA_ULTIMO_PAG() As String
        Get
            Return _PR_CF_FECHA_ULTIMO_PAG
        End Get
        Set(ByVal value As String)
            _PR_CF_FECHA_ULTIMO_PAG = value
        End Set
    End Property

    Private _PR_KT_DTEPP As String
    Property PR_KT_DTEPP() As String
        Get
            Return _PR_KT_DTEPP
        End Get
        Set(ByVal value As String)
            _PR_KT_DTEPP = value
        End Set
    End Property

    Private _PR_KT_DTECARGA As String
    Property PR_KT_DTECARGA() As String
        Get
            Return _PR_KT_DTECARGA
        End Get
        Set(ByVal value As String)
            _PR_KT_DTECARGA = value
        End Set
    End Property

    Private _PR_KT_DTEASIGNA As String
    Property PR_KT_DTEASIGNA() As String
        Get
            Return _PR_KT_DTEASIGNA
        End Get
        Set(ByVal value As String)
            _PR_KT_DTEASIGNA = value
        End Set
    End Property

    Private _VI_ALERTA_GESTION As String
    Property VI_ALERTA_GESTION() As String
        Get
            Return _VI_ALERTA_GESTION
        End Get
        Set(ByVal value As String)
            _VI_ALERTA_GESTION = value
        End Set
    End Property

    Private _VI_DIAS_ALERTA_GESTION As String
    Property VI_DIAS_ALERTA_GESTION() As String
        Get
            Return _VI_DIAS_ALERTA_GESTION
        End Get
        Set(ByVal value As String)
            _VI_DIAS_ALERTA_GESTION = value
        End Set
    End Property

    Private _PR_KT_CUANTASCUENTASFILA As String
    Property PR_KT_CUANTASCUENTASFILA() As String
        Get
            Return _PR_KT_CUANTASCUENTASFILA
        End Get
        Set(ByVal value As String)
            _PR_KT_CUANTASCUENTASFILA = value
        End Set
    End Property

    Private _PR_KT_CUENTATRABAJADAFILA As String
    Property PR_KT_CUENTATRABAJADAFILA() As String
        Get
            Return _PR_KT_CUENTATRABAJADAFILA
        End Get
        Set(ByVal value As String)
            _PR_KT_CUENTATRABAJADAFILA = value
        End Set
    End Property

    Private _Avisos As String
    Property Avisos() As String
        Get
            Return _Avisos
        End Get
        Set(ByVal value As String)
            _Avisos = value
        End Set
    End Property

    Private _PR_KT_UASIGNADOV As String
    Property PR_KT_UASIGNADOV() As String
        Get
            Return _PR_KT_UASIGNADOV
        End Get
        Set(ByVal value As String)
            _PR_KT_UASIGNADOV = value
        End Set
    End Property

End Class