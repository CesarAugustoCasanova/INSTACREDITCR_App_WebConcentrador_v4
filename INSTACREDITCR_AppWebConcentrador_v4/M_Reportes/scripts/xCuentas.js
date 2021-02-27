
class RollBack {
    constructor(sheet, subproducto,tipo) {
        this.sheet = sheet;
        this.subproducto = subproducto;
        this.tipo = tipo;
    }

    LlenarEncabezado() {
        let startDate = new Date().startDate();
        let paymentsReferred = new Date().datePayments();
        this.sheet.get_range("A5").set_value(startDate);
        this.sheet.get_range("D5").set_value(moment().format('DD/MM/YYYY'));
        this.sheet.get_range("G5").set_value(paymentsReferred);
    }

    setTotales() {
        console.log("ok")
        let data = { subproducto: this.subproducto, tipo: this.tipo};
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
                this.sheet.get_range("E9").set_value(info.BUCKET_1);
                this.sheet.get_range("G9").set_value(info.BUCKET_2);
                this.sheet.get_range("I9").set_value(info.BUCKET_3);
                this.sheet.get_range("K9").set_value(info.BUCKET_4);
                this.sheet.get_range("M9").set_value(info.BUCKET_5);
                this.sheet.get_range("O9").set_value(info.BUCKET_6);
                this.sheet.get_range("Q9").set_value(info.BUCKET_7);
                this.sheet.get_range("S9").set_value(info.BUCKET_8);
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

    setRollBackData() {

    }

    LlenarHojaxCuentas() {
        this.setTotales();
        this.setRollBackData();
    };




}

