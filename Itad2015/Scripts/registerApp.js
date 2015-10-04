﻿var registerModule = (function () {

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
                    $(formId + " #inputStage").fadeIn(1000);
                    break;
                }
            case registrationStage.shirtTypeStage:
                {
                    $(formId + " #shirtTypeStage").fadeIn(1000);
                    break;
                }
            case registrationStage.shirtSizeStage:
                {
                    if (isFemale) {
                        $(formId + " #shirtSizeStageMale").hide();
                        $(formId + " #shirtSizeStageFemale").fadeIn(1000);

                    } else {
                        $(formId + " #shirtSizeStageFemale").hide();
                        $(formId + " #shirtSizeStageMale").fadeIn(1000);
                    }
                    break;
                }
            case registrationStage.registerBtnStage:
                {
                    $(formId + " #registerBtnStage").fadeIn(1000);
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

    $("#registrationTypeStage .chooseConferenceType").click(function () {

        $("#registrationTypeStage .chooseConferenceType").removeClass("iconColor");
        $(this).addClass("iconColor");


        if ($(this).data("isworkshop") === true) {
            $(regWorkshopId).fadeIn(1000);
            $(regId).hide();
            registerModule.setFormId(regWorkshopId);
        } else {
            $(regWorkshopId).hide();
            $(regId).fadeIn(1000);
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

    $("#TShirtType .itad-gender").click(function () {
        $(".itad-gender").removeClass("active");
        if ($(this).hasClass("itad-girl")) {
            registerModule.setIsFemale(true);
            $(".itad-gender.itad-girl").addClass("active");
        }
        else {
            $(".itad-gender.itad-guy").addClass("active");
            registerModule.setIsFemale(false);
        }


        if (registerModule.getActualStage() === 3) {
            registerModule.setStage(4);
        } else {
            registerModule.loopToCurrentStage(registerModule.getActualStage());
        }
    });

    $("#shirtSizeStageFemale .itad-shirt, #shirtSizeStageMale .itad-shirt").click(function () {
        var id = registerModule.getFormId();
        var code = $(this).data("size-code");
        $(regId + " #sizeInput," + regWorkshopId + " #sizeInput").val(code);
        $("#shirtSizeStageFemale .itad-shirt, #shirtSizeStageMale .itad-shirt").removeClass("active");
        $(this).addClass('active');

        if (id === regWorkshopId) {
            $(regId + " #shirtSizeStageFemale .itad-shirt, " + regId + " #shirtSizeStageMale .itad-shirt").removeClass("active");

            $(regId + " .itad-shirt[data-size-code='" + $(this).data('size-code') + "']").addClass('active');
        } else {
            $(regWorkshopId + " #shirtSizeStageFemale .itad-shirt, " + regWorkshopId + " #shirtSizeStageMale .itad-shirt").removeClass("active");

            $(regWorkshopId + " .itad-shirt[data-size-code='" + $(this).data('size-code') + "']").addClass('active');
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


    var options = { 
        success:    function(data) {
            console.log(data);
        } 
    };

    $('#RegisterForm, #RegisterWorkshopForm').ajaxForm(options);

    $('#registerBtnStage a').click(function() {
        $(this).parent().parent().submit();
    });
});
