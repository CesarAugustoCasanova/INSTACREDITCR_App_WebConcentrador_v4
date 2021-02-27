try {

    Telerik.Web.UI.RadDatePicker.prototype.FormattedDate = function () {
        return this.get_selectedDate().ddmmyyy();
    }
} catch (ex) { }
try {
    Telerik.Web.UI.RadComboBox.prototype.Get_CheckedValues = function () {
        var itemsValues = [];
        this.get_checkedItems().forEach(selectedItem => itemsValues.push(selectedItem.get_value()));
        return itemsValues.join(",");
    }
} catch (ex) { }
try {
    Telerik.Web.UI.RadComboBox.prototype.Get_CheckedTexts = function () {
        var itemsValues = [];
        this.get_checkedItems().forEach(selectedItem => itemsValues.push(selectedItem.get_text()));
        return itemsValues.join(",");
    }
} catch (ex) { }
try {
    Telerik.Web.UI.RadComboBox.prototype.Get_SelectedValue = function () {
        return this.get_selectedItem().get_value()
    }
} catch (ex) { }
try {
    Telerik.Web.UI.RadComboBox.prototype.Get_SelectedText = function () {
        return this.get_selectedItem().get_text()

    }
} catch (ex) { }
try {
    Telerik.Web.UI.RadCheckBoxList.prototype.Get_CheckedValues = function () {
        var itemsValues = [];
        this.get_selectedItems().forEach(selectedItem => itemsValues.push(selectedItem.get_value()));
        return itemsValues.join(",");
    }
} catch (ex) { }
try {
    Telerik.Web.UI.RadCheckBoxList.prototype.Get_CheckedTexts = function () {
        var itemsValues = [];
        this.get_selectedItems().forEach(selectedItem => itemsValues.push(selectedItem.get_text()));
        return itemsValues.join(",");
    }
} catch (ex) { }
try {
    Date.prototype.ddmmyyy = function () {
        var mm = this.getMonth() + 1; // getMonth() is zero-based
        var dd = this.getDate();

        return [
            (dd > 9 ? '' : '0') + dd,
            (mm > 9 ? '' : '0') + mm,
            this.getFullYear()
        ].join('/');
    };
} catch (ex) { }