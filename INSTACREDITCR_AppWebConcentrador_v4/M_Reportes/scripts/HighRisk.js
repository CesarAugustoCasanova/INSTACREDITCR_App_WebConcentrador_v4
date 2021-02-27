
class HighRiskJS {
    constructor(sheet, tipo) {
        this.sheet = sheet;
        this.tipo = tipo;

        this.LlenarEncabezado = function () {
            let startDate = new Date().startDate();
            let paymentsReferred = new Date().datePayments();
            this.sheet.get_range("A5").set_value(startDate);
            this.sheet.get_range("F5").set_value(moment().format('DD/MM/YYYY'));
            this.sheet.get_range("I5").set_value(paymentsReferred);
        }

        this.SetData = function () {
            let data = { tipo: this.tipo };
            var info;
            fetch('HighRisk.aspx/getData',
                {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json;charset=utf-8' },
                    body: JSON.stringify(data)
                }
            )
                .then(res => res.json())
                .then(res => {
                    info = JSON.parse(res.d);
                    info.forEach(row => {
                        switch (row.FILA) {
                            case "encabezado":
                                this.sheet.get_range("H10").set_value(parseInt(row.BK0_CUENTAS));
                                this.sheet.get_range("I10").set_value(parseFloat(row.BK0_MONTO));
                                break;
                            case "CDB":
                                this.sheet.get_range("F13").set_value(parseInt(row.BKC_CUENTAS));
                                this.sheet.get_range("G13").set_value(parseFloat(row.BKC_MONTO));
                                break;
                            case "CMP":
                                this.sheet.get_range("F14").set_value(parseInt(row.BKC_CUENTAS));
                                this.sheet.get_range("G14").set_value(parseFloat(row.BKC_MONTO));
                                break;
                            case "RollBk":
                                this.sheet.get_range("F12").set_value(parseInt(row.BKC_CUENTAS));
                                this.sheet.get_range("G12").set_value(parseFloat(row.BKC_MONTO));
                                this.sheet.get_range("H12").set_value(parseInt(row.BK0_CUENTAS));
                                this.sheet.get_range("I12").set_value(parseFloat(row.BK0_MONTO));
                                break;
                        }
                    })
                })
                .catch(res => (alert("No se pudo cargar la informacion. Intente más tarde."), console.log(res)))
        }
    }

    LlenarHoja() {
        this.LlenarEncabezado();
        this.SetData();
    }
}

Date.prototype.startDate = function () {
    var fecha = "";
    if (this.getDate() < 6)
        fecha = moment().subtract(1, 'months');
    else
        fecha = moment();

    return fecha.format('[6]/MM/YYYY');
};

Date.prototype.datePayments = function () {
    var fecha = "";
    if (this.getDate() < 13)
        fecha = moment().subtract(1, 'months');
    else
        fecha = moment();
    var fechaAyer = moment().subtract(1, 'days');
    return [fecha.format('[6] MMM'), 'to', fechaAyer.format('[12] MMM YYYY')].join(' ');
}