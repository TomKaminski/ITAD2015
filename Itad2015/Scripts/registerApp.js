var registerModule = (function () {

    var registrationStage = {
        registrationTypeStage: 1,
        inputStage: 2,
        shirtTypeStage: 3,
        shirtSizeStage: 4,
        registerBtnStage: 5
    }

    var isFemale = false;
    var actualStage = 1;
    var formId;

    var setStage = function (stage, canBack) {
        if (actualStage > stage && !canBack)
            return;
        actualStage = stage;
        switch (actualStage) {
            case registrationStage.inputStage:
                {
                    $(formId + " #inputStage").fadeIn();
                    break;
                }
            case registrationStage.shirtTypeStage:
                {
                    $(formId + " #shirtTypeStage").fadeIn();
                    break;
                }
            case registrationStage.shirtSizeStage:
                {
                    if (isFemale) {
                        $(formId + " #shirtSizeStageMale").hide();
                        $(formId + " #shirtSizeStageFemale").fadeIn();

                    } else {
                        $(formId + " #shirtSizeStageFemale").hide();
                        $(formId + " #shirtSizeStageMale").fadeIn();
                    }
                    break;
                }
            case registrationStage.registerBtnStage:
                {
                    $(formId + " #registerBtnStage").fadeIn();
                    break;
                }
        }
    }

    var loopToCurrentStage = function () {
        var tempActualStage = actualStage;
        for (var i = 1; i <= tempActualStage; i++) {
            setStage(i, true);
        }
    }


    var init = function () {
        $("#registerGuestPartial,#registerWorkshopGuestPartial,#inputStage,#shirtTypeStage,#shirtSizeStageFemale,#shirtSizeStageMale,#registerBtnStage").hide();
    }

    var setIsFemale = function (female) {
        isFemale = female;
    }

    var setFormId = function (id) {
        formId = id;
    }

    var getFormId = function () {
        return formId;
    }

    var getActualStage = function () {
        return actualStage;
    }

    // Public API
    return {
        registrationStage: registrationStage,
        setStage: setStage,
        init: init,
        getActualStage: getActualStage,
        setIsFemale: setIsFemale,
        setFormId: setFormId,
        getFormId: getFormId,
        loopToCurrentStage: loopToCurrentStage
    };
})();

var regWorkshopId = "#registerWorkshopGuestPartial";
var regId = "#registerGuestPartial";

$(document).ready(function () {
    registerModule.init();

    $("#registrationTypeStage a").click(function () {
        if ($(this).data("isworkshop") === true) {
            $(regWorkshopId).fadeIn();
            $(regId).hide();
            registerModule.setFormId(regWorkshopId);
        } else {
            $(regWorkshopId).hide();
            $(regId).fadeIn();
            registerModule.setFormId(regId);
        }
        if (registerModule.getActualStage() === 1) {
            registerModule.setStage(2);
        } else {
            registerModule.loopToCurrentStage(registerModule.getActualStage());
        }

    });

    $("input.itad-required-input").focusout(function () {
        var id = registerModule.getFormId();
        setTimeout(function () {
            var isValid = true;
            $(id + " input.itad-required-input").each(function () {
                if ($(this).hasClass("input-validation-error") || $(this).val() === "")
                    isValid = false;
            });
            if (isValid)
                registerModule.setStage(3);
        }, 100);

        if (id === regWorkshopId) {
            $(regId + " #" + $(this).attr('id')).val($(this).val());
        } else {
            $(regWorkshopId + " #" + $(this).attr('id')).val($(this).val());
        }
    });

    $("#TShirtType a").click(function () {
        if ($(this).hasClass("female")) {
            registerModule.setIsFemale(true);
        }
        else {
            registerModule.setIsFemale(false);
        }
        if (registerModule.getActualStage() === 3) {
            registerModule.setStage(4);
        } else {
            registerModule.loopToCurrentStage(registerModule.getActualStage());
        }
    });

    $("#shirtSizeStageFemale a, #shirtSizeStageMale a").click(function () {
        var id = registerModule.getFormId();
        var code = $(this).data("size-code");
        $(regId + " #sizeInput," + regWorkshopId + " #sizeInput").val(code);
        $("#shirtSizeStageFemale a, #shirtSizeStageMale a").removeClass("btn-selected");
        $(this).addClass('btn-selected');

        if (id === regWorkshopId) {
            $(regId +" #shirtSizeStageFemale a, "+ regId+" #shirtSizeStageMale a").removeClass("btn-selected");

            $(regId + " a[data-size-code='" + $(this).data('size-code') + "']").addClass('btn-selected');
        } else {
            $(regWorkshopId + " #shirtSizeStageFemale a, " + regWorkshopId + " #shirtSizeStageMale a").removeClass("btn-selected");

            $(regWorkshopId + " a[data-size-code='" + $(this).data('size-code') +"']").addClass('btn-selected');
        }

        registerModule.setStage(5);
    });

    $("#registerBtnStage input").click(function () {
        var isChecked = $(this)[0].checked;
        var id = registerModule.getFormId();
        if (id === regWorkshopId) {
            $(regId + " #registerBtnStage input").prop('checked', isChecked);
        } else {
            $(regWorkshopId + " #registerBtnStage input").prop('checked', isChecked);
        }
        $("#registerBtnStage a").toggleClass("disabled");
    });
});
