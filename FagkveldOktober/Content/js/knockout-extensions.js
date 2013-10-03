ko.bindingHandlers.formatMoney = {
    update: function (element, valueAccessor) {
        var data = valueAccessor();
        var oere = ko.utils.unwrapObservable(data.oere);
        var kroner = ko.utils.unwrapObservable(data.kroner);
        var attr = ko.utils.unwrapObservable(data.attr) || 'text';
        var textValue = oere || kroner;
        var actualAmount = oere || kroner;
        if (oere) {
            actualAmount = actualAmount / 100;
        }
        textValue = formatHelper.formatNumber(actualAmount, 2, ',', ' ');
        if (attr == 'text') {
            $(element).text(textValue);
        }
        else {
            $(element).attr(attr, (textValue));
        }

    }
};
