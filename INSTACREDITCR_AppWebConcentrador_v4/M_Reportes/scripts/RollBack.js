
class RollBackJS {
    constructor(sheet, subproducto, tipo) {
        this.sheet = sheet;
        this.subproducto = subproducto;
        this.tipo = tipo;


        /*
         * El hecho de declarar funciones en el constructor las hace 'privadas'
         * Esto se hace para dejar una unica funcion 'publica' que haga todo.
         */
        this.customParse = function (data) {
            return tipo == 'cuentas' ? parseInt(data) : parseFloat(data)
        }

        this.LlenarEncabezado = function () {
            let startDate = new Date().startDate();
            let paymentsReferred = new Date().datePayments();
            this.sheet.get_range("A5").set_value(startDate);
            this.sheet.get_range("D5").set_value(moment().format('DD/MM/YYYY'));
            this.sheet.get_range("G5").set_value(paymentsReferred);
        }

        this.setTotales = function () {
            let data = { subproducto: this.subproducto, tipo: this.tipo };
            var info;
            fetch('RollBack.aspx/getTotales',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json;charset=utf-8' },
                    body: JSON.stringify(data)
                }
            )
                .then(res => res.json())
                .then(res => {
                    info = JSON.parse(res.d)[1];
                    this.sheet.get_range("E9").set_value(this.customParse(info.BUCKET_1));
                    this.sheet.get_range("G9").set_value(this.customParse(info.BUCKET_2));
                    this.sheet.get_range("I9").set_value(this.customParse(info.BUCKET_3));
                    this.sheet.get_range("K9").set_value(this.customParse(info.BUCKET_4));
                    this.sheet.get_range("M9").set_value(this.customParse(info.BUCKET_5));
                    this.sheet.get_range("O9").set_value(this.customParse(info.BUCKET_6));
                    this.sheet.get_range("Q9").set_value(this.customParse(info.BUCKET_7));
                    this.sheet.get_range("S9").set_value(this.customParse(info.BUCKET_8));
                })
                .catch(res => {
                    console.log({ error: res });
                    this.sheet.get_range("E9").set_value(-1);
                    this.sheet.get_range("G9").set_value(-1);
                    this.sheet.get_range("I9").set_value(-1);
                    this.sheet.get_range("K9").set_value(-1);
                    this.sheet.get_range("M9").set_value(-1);
                    this.sheet.get_range("O9").set_value(-1);
                    this.sheet.get_range("Q9").set_value(-1);
                    this.sheet.get_range("S9").set_value(-1);
                })
        }

        this.setRollBackData = async function () {
            let data = { subproducto: this.subproducto, tipo: this.tipo };
            var info;
            var HTMLcard = this.tipo == 'cuentas' ? $("#hoja1") : $("#hoja2");
            var HTMLprogress = this.tipo == 'cuentas' ? $("#hoja1_Progreso") : $("#hoja2_Progreso");
            info = await fetch('RollBack.aspx/getRollBackData',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json;charset=utf-8' },
                    body: JSON.stringify(data)
                }
            )
                .then(res => res.json())
                .then(res => {
                    HTMLcard.removeClass("border-warning")
                    HTMLcard.addClass("border-primary")
                    info = JSON.parse(res.d);
                    let total = info.length
                    info.forEach((row, index) => {
                        setTimeout(() => {
                            HTMLprogress.text(((index + 1) * 100 / total).toFixedDown(2) + "%")
                            switch (row.ident) {
                                case "1":
                                    this.PrintBK1(row);
                                    break;
                                case "2":
                                    this.PrintBK2(row);
                                    break;
                                case "3":
                                    this.PrintBK3(row);
                                    break;
                                case "4":
                                    this.PrintBK4(row);
                                    break;
                                case "5":
                                    this.PrintBK5(row);
                                    break;
                                case "6":
                                    this.PrintBK6(row);
                                    break;
                                case "7":
                                    this.PrintBK7(row);
                                    break;
                                case "8":
                                    this.PrintBK8(row);
                                    break;
                            }
                        }, 50)
                    })
                })
                .catch(res => (console.error(res), alert("Ups! Hubo un problema al traer la información")))



        }

        this.PrintBK1 = function (data) {
            let hoja = this.sheet;
            let fila = 11
            switch (data.DIFERENCIA) {
                case "RollBk":
                    hoja.get_range("D" + fila).set_value(this.customParse(data.BK0));
                    hoja.get_range("E" + fila).set_value(this.customParse(data.BK1));
                    break;
                case "CMP":
                    hoja.get_range("D" + (fila + 1)).set_value(this.customParse(data.BK0));
                    break;
                case "CDB":
                    hoja.get_range("D" + (fila + 2)).set_value(this.customParse(data.BK0));
                    break;
            }

        }
        this.PrintBK2 = function (data) {
            let hoja = this.sheet;
            let fila = 15
            switch (data.DIFERENCIA) {
                case "RollBk":
                    hoja.get_range("D" + fila).set_value(this.customParse(data.BK0));
                    hoja.get_range("E" + fila).set_value(this.customParse(data.BK1));
                    hoja.get_range("G" + fila).set_value(this.customParse(data.BK2));
                    break;
                case "CMP":
                    hoja.get_range("D" + (fila + 1)).set_value(this.customParse(data.BK0));
                    break;
                case "CDB":
                    hoja.get_range("D" + (fila + 2)).set_value(this.customParse(data.BK0));
                    break;
            }

        }
        this.PrintBK3 = function (data) {
            let hoja = this.sheet;
            let fila = 19
            switch (data.DIFERENCIA) {
                case "RollBk":
                    hoja.get_range("D" + fila).set_value(this.customParse(data.BK0));
                    hoja.get_range("E" + fila).set_value(this.customParse(data.BK1));
                    hoja.get_range("G" + fila).set_value(this.customParse(data.BK2));
                    hoja.get_range("I" + fila).set_value(this.customParse(data.BK3));
                    break;
                case "CMP":
                    hoja.get_range("D" + (fila + 1)).set_value(this.customParse(data.BK0));
                    break;
                case "CDB":
                    hoja.get_range("D" + (fila + 2)).set_value(this.customParse(data.BK0));
                    break;
            }

        }
        this.PrintBK4 = function (data) {
            let hoja = this.sheet;
            let fila = 23
            switch (data.DIFERENCIA) {
                case "RollBk":
                    hoja.get_range("D" + fila).set_value(this.customParse(data.BK0));
                    hoja.get_range("E" + fila).set_value(this.customParse(data.BK1));
                    hoja.get_range("G" + fila).set_value(this.customParse(data.BK2));
                    hoja.get_range("I" + fila).set_value(this.customParse(data.BK3));
                    hoja.get_range("K" + fila).set_value(this.customParse(data.BK4));
                    break;
                case "CMP":
                    hoja.get_range("D" + (fila + 1)).set_value(this.customParse(data.BK0));
                    break;
                case "CDB":
                    hoja.get_range("D" + (fila + 2)).set_value(this.customParse(data.BK0));
                    break;
            }

        }
        this.PrintBK5 = function (data) {
            let hoja = this.sheet;
            let fila = 27
            switch (data.DIFERENCIA) {
                case "RollBk":
                    hoja.get_range("D" + fila).set_value(this.customParse(data.BK0));
                    hoja.get_range("E" + fila).set_value(this.customParse(data.BK1));
                    hoja.get_range("G" + fila).set_value(this.customParse(data.BK2));
                    hoja.get_range("I" + fila).set_value(this.customParse(data.BK3));
                    hoja.get_range("K" + fila).set_value(this.customParse(data.BK4));
                    hoja.get_range("M" + fila).set_value(this.customParse(data.BK5));
                    break;
                case "CMP":
                    hoja.get_range("D" + (fila + 1)).set_value(this.customParse(data.BK0));
                    break;
                case "CDB":
                    hoja.get_range("D" + (fila + 2)).set_value(this.customParse(data.BK0));
                    break;
            }

        }
        this.PrintBK6 = function (data) {
            let hoja = this.sheet;
            let fila = 31
            switch (data.DIFERENCIA) {
                case "RollBk":
                    hoja.get_range("D" + fila).set_value(this.customParse(data.BK0));
                    hoja.get_range("E" + fila).set_value(this.customParse(data.BK1));
                    hoja.get_range("G" + fila).set_value(this.customParse(data.BK2));
                    hoja.get_range("I" + fila).set_value(this.customParse(data.BK3));
                    hoja.get_range("K" + fila).set_value(this.customParse(data.BK4));
                    hoja.get_range("M" + fila).set_value(this.customParse(data.BK5));
                    hoja.get_range("O" + fila).set_value(this.customParse(data.BK6));
                    break;
                case "CMP":
                    hoja.get_range("D" + (fila + 1)).set_value(this.customParse(data.BK0));
                    break;
                case "CDB":
                    hoja.get_range("D" + (fila + 2)).set_value(this.customParse(data.BK0));
                    break;
            }

        }
        this.PrintBK7 = function (data) {
            let hoja = this.sheet;
            let fila = 35
            switch (data.DIFERENCIA) {
                case "RollBk":
                    hoja.get_range("D" + fila).set_value(this.customParse(data.BK0));
                    hoja.get_range("E" + fila).set_value(this.customParse(data.BK1));
                    hoja.get_range("G" + fila).set_value(this.customParse(data.BK2));
                    hoja.get_range("I" + fila).set_value(this.customParse(data.BK3));
                    hoja.get_range("K" + fila).set_value(this.customParse(data.BK4));
                    hoja.get_range("M" + fila).set_value(this.customParse(data.BK5));
                    hoja.get_range("O" + fila).set_value(this.customParse(data.BK6));
                    hoja.get_range("Q" + fila).set_value(this.customParse(data.BK7));
                    break;
                case "CMP":
                    hoja.get_range("D" + (fila + 1)).set_value(this.customParse(data.BK0));
                    break;
                case "CDB":
                    hoja.get_range("D" + (fila + 2)).set_value(this.customParse(data.BK0));
                    break;
            }

        }
        this.PrintBK8 = function (data) {
            let hoja = this.sheet;
            let fila = 39
            switch (data.DIFERENCIA) {
                case "RollBk":
                    hoja.get_range("D" + fila).set_value(this.customParse(data.BK0));
                    hoja.get_range("E" + fila).set_value(this.customParse(data.BK1));
                    hoja.get_range("G" + fila).set_value(this.customParse(data.BK2));
                    hoja.get_range("I" + fila).set_value(this.customParse(data.BK3));
                    hoja.get_range("K" + fila).set_value(this.customParse(data.BK4));
                    hoja.get_range("M" + fila).set_value(this.customParse(data.BK5));
                    hoja.get_range("O" + fila).set_value(this.customParse(data.BK6));
                    hoja.get_range("Q" + fila).set_value(this.customParse(data.BK7));
                    hoja.get_range("S" + fila).set_value(this.customParse(data.BK8));
                    break;
                case "CMP":
                    hoja.get_range("D" + (fila + 1)).set_value(this.customParse(data.BK0));
                    break;
                case "CDB":
                    hoja.get_range("D" + (fila + 2)).set_value(this.customParse(data.BK0));
                    break;
            }

        }

    }

    LlenarHoja() {
        this.LlenarEncabezado();
        this.setTotales();
        this.setRollBackData();
    };


    LlenarCuentasBucket() {
        var HTMLcard = $("#hoja3");
        var HTMLprogress = $("#hoja3_Progreso");

        let data = { subproducto: this.subproducto };
        fetch('RollBack.aspx/getCuentasBucket',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json;charset=utf-8' },
                body: JSON.stringify(data)
            }
        )
            .then(res => res.json())
            .then(res => {

                let info = JSON.parse(res.d);
                HTMLcard.removeClass("border-warning")
                HTMLcard.addClass("border-primary")
                let total = info.length
                info.forEach((data, index) => {
                    setTimeout(() => {
                        HTMLprogress.text(((index + 1) * 100 / total).toFixedDown(2) + "%")
                        let fila = index + 2;
                        this.sheet.get_range(`A${fila}`).set_value(data.GRUPO);
                        this.sheet.get_range(`B${fila}`).set_value(data.CUENTA);
                        this.sheet.get_range(`C${fila}`).set_value(data.MONTO_VENCIDO);
                        this.sheet.get_range(`D${fila}`).set_value(data.SALDO_DEUDOR);
                        this.sheet.get_range(`E${fila}`).set_value(data.MONTO_MINIMO);
                        this.sheet.get_range(`F${fila}`).set_value(data.NUM_PAGOS);
                        this.sheet.get_range(`G${fila}`).set_value(data.MONTO);
                        this.sheet.get_range(`H${fila}`).set_value(data.DIFERENCIA);
                        this.sheet.get_range(`I${fila}`).set_value(data.DIAS_MORA);
                        this.sheet.get_range(`J${fila}`).set_value(data.BUCKET_INICIAL);
                        this.sheet.get_range(`K${fila}`).set_value(data.BUCKET_ACTUAL);
                        this.sheet.get_range(`L${fila}`).set_value(data.SUBPRODUCTO);
                        this.sheet.get_range(`M${fila}`).set_value(data.GESTOR);
                        this.sheet.get_range(`N${fila}`).set_value(data.AGENCIA_ACTUAL);
                        this.sheet.get_range(`O${fila}`).set_value(data.FECHA_REF);
                        this.sheet.get_range(`P${fila}`).set_value(data.ESTATUS);
                    }, 10)
                });
            })
            .catch(res => (console.error(res), alert("Ups! Hubo un problema al traer la información")))

    }

    LlenarPagos() {
        var HTMLcard = $("#hoja4");
        var HTMLprogress = $("#hoja4_Progreso");

        let data = { subproducto: this.subproducto };
        fetch('RollBack.aspx/getPagos',
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json;charset=utf-8' },
                body: JSON.stringify(data)
            }
        )
            .then(res => res.json())
            .then(res => {
                let info = JSON.parse(res.d);


                let total = info.length
                if (total == 0) {
                    HTMLcard.removeClass("border-warning")
                    HTMLcard.addClass("border-dark")
                } else {
                    HTMLcard.removeClass("border-warning")
                    HTMLcard.addClass("border-primary")
                    info.forEach((data, index) => {
                        setTimeout(() => {
                            HTMLprogress.text(((index + 1) * 100 / total).toFixedDown(2) + "%")
                            let fila = index + 2;
                            this.sheet.get_range(`A${fila}`).set_value(data.CUENTA);
                            this.sheet.get_range(`B${fila}`).set_value(data.MONTO);
                            this.sheet.get_range(`C${fila}`).set_value(data.DTE_TRANSACCION);
                            this.sheet.get_range(`D${fila}`).set_value(data.DTE_REGISTRO);
                            this.sheet.get_range(`E${fila}`).set_value(data.DESCRIPCION);
                        }, 10)
                    });
                }
            })
            .catch(res => (console.error(res), alert("Ups! Hubo un problema al traer la información")))

    }
}

Number.prototype.toFixedDown = function (digits) {
    var re = new RegExp("(\\d+\\.\\d{" + digits + "})(\\d)"),
        m = this.toString().match(re);
    return m ? parseFloat(m[1]) : this.valueOf();
};

try {
    Telerik.Web.UI.RadComboBox.prototype.Get_SelectedValue = function () {
        return this.get_selectedItem().get_value()
    }
} catch (ex) { }

Date.prototype.startDate = function () {
    var fecha = "";
    if (this.getDate() < 13)
        fecha = moment().subtract(1, 'months');
    else
        fecha = moment();

    return fecha.format('[13]/MM/YYYY');
};

Date.prototype.datePayments = function () {
    var fecha = "";
    if (this.getDate() < 13)
        fecha = moment().subtract(1, 'months');
    else
        fecha = moment();
    var fechaAyer = moment().subtract(1, 'days');
    return [fecha.format('[13] MMM'), 'to', fechaAyer.format('DD MMM YYYY')].join(' ');
}