class Helper {
    constructor(sheet, index) {
        sheet.set_showGridLines(false);

        this.sheet = sheet;
        this.index = index;
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
        print = function (range, text) {
            let multiCellRange = this.sheet.get_range(range);
            multiCellRange.merge();
            multiCellRange.set_value(text);
            multiCellRange.set_textAlign("center");
            multiCellRange.set_verticalAlign("center");

            multiCellRange.set_borderBottomColor("black");
            multiCellRange.set_borderLeftColor("black");
            multiCellRange.set_borderRightColor("black");
            multiCellRange.set_borderTopColor("black");
            return multiCellRange
        }

        printFormula = function (range, formula) {
            let multiCellRange = this.sheet.get_range(range);
            multiCellRange.merge();
            multiCellRange.set_formula(formula);
            multiCellRange.set_textAlign("center");
            multiCellRange.set_verticalAlign("center");

            multiCellRange.set_borderBottomColor("black");
            multiCellRange.set_borderLeftColor("black");
            multiCellRange.set_borderRightColor("black");
            multiCellRange.set_borderTopColor("black");
            return multiCellRange
        }

        printValues = function (range, values) {
            let multiCellRange = this.sheet.get_range(range);
            multiCellRange.set_values(values);
            multiCellRange.set_textAlign("center");
            multiCellRange.set_verticalAlign("center");

            multiCellRange.set_borderBottomColor("black");
            multiCellRange.set_borderLeftColor("black");
            multiCellRange.set_borderRightColor("black");
            multiCellRange.set_borderTopColor("black");
            return multiCellRange
        }

        color = function (range, color) {
            var multiCellRange = this.sheet.get_range(range);
            multiCellRange.set_background(color);
            return multiCellRange
        }

      
    }



class SummaryJS {
    constructor(sheet) {
        
        this.sheet = sheet;
    }

    Llenar() {
        var impresora = new Helper(this.sheet)
        this.print("B2:B5", "PERIODO");

        this.print("C2:R2", "PLAZAS");

        this.print("C3:J3", "ASIGNACION");
        this.print("C4:D4", "2017");
        this.print("E4:F4", "2018");
        this.print("G4:H4", "2019");
        this.print("I4:J4", "2020");

        this.print("K3:R3", "RECUPERACIÓN");
        this.print("K4:L4", "2017");
        this.print("M4:N4", "2018");
        this.print("O4:P4", "2019");
        this.print("Q4:R4", "2020");

        this.print("C5", "CUENTAS");
        this.print("E5", "CUENTAS");
        this.print("G5", "CUENTAS");
        this.print("I5", "CUENTAS");
        this.print("K5", "CUENTAS");
        this.print("M5", "CUENTAS");
        this.print("O5", "CUENTAS");
        this.print("Q5", "CUENTAS");

        this.print("D5", "MONTO");
        this.print("F5", "MONTO");
        this.print("H5", "MONTO");
        this.print("J5", "MONTO");
        this.print("L5", "MONTO");
        this.print("N5", "MONTO");
        this.print("P5", "MONTO");
        this.print("R5", "MONTO");

        this.print("B6", "ENERO").set_bold(true);
        this.print("B7", "FEBRERO").set_bold(true);
        this.print("B8", "MARZO").set_bold(true);
        this.print("B9", "ABRIL").set_bold(true);
        this.print("B10", "MAYO").set_bold(true);
        this.print("B11", "JUNIO").set_bold(true);
        this.print("B12", "JULIO").set_bold(true);
        this.print("B13", "AGOSTO").set_bold(true);
        this.print("B14", "SEPTIEMBRE").set_bold(true);
        this.print("B15", "OCTUBRE").set_bold(true);
        this.print("B16", "NOVIEMBRE").set_bold(true);
        this.print("B17", "DICIEMBRE").set_bold(true);
        this.print("B18", "TOTAL").set_bold(true);


        this.color("B2:R5", "rgb(237,237,237)").set_bold(true);

        this.printFormula("K18", "SUM(K6:K17)")
        this.printFormula("L18", "SUM(L6:L17)")
        this.printFormula("M18", "SUM(M6:M17)")
        this.printFormula("N18", "SUM(N6:N17)")
        this.printFormula("O18", "SUM(O6:O17)")
        this.printFormula("P18", "SUM(P6:P17)")
        this.printFormula("Q18", "SUM(Q6:Q17)")
        this.printFormula("R18", "SUM(R6:R17)")

        this.color("k3:R18", "rgb(255,242,204)");

        let cuentas_y_montos = [1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 8, 7, 6, 5, 4, 3]
        this.printValues("C6:R17", [cuentas_y_montos, cuentas_y_montos, cuentas_y_montos, cuentas_y_montos, cuentas_y_montos, cuentas_y_montos, cuentas_y_montos, cuentas_y_montos, cuentas_y_montos, cuentas_y_montos, cuentas_y_montos, cuentas_y_montos])
    }
}