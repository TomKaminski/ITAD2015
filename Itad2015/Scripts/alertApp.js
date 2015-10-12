var alertApp = (function () {
    function prepareAlert(alertStyle, text) {
        var obj = $("<div class='alert alert-dismissible col-xs-12 itad-alert text-center' role='alert' style='position: fixed; top: 60px;border-radius:0; opacity: 0.8'></div>");
        obj.append("<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>");
        obj.append(text);
        obj.addClass(alertStyle);
        return obj;
    }

    var appendAlert = function (alertStyle, text) {
        $(".alert.aler-dismissible").remove();
        var alert = prepareAlert(alertStyle, text);
        $('body').append(alert);
    }

    return {
        appendAlert: appendAlert
    }
})();