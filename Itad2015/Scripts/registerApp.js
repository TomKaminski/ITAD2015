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

$.validator.setDefaults({
    ignore: []
});

function fillBackendErrorMessages(result) {
    var id = registerModule.getFormId();
    $(".serverErrorsContainer, .serverSuccessContainer").empty();
    if (result.status === false) {
        for (var j = 0; j < result.errors.length; j++) {
            $(id + " .serverErrorsContainer").append("<p>" + result.errors[j] + "<br/></p>");
        }
    }
}

$(document).ready(function () {


    registerModule.init();

    $(".dropdown-menu li a").click(function (e) {
        e.preventDefault();
        var text = $(this).text();
        var workshopId = $(this).data("workshop-id");


        $("#workshopId").val(workshopId).trigger("change");
        $("#workshopId").valid();
        $(".workshop-container").hide();
        $(".workshop-container[data-workshop-id='" + workshopId + "']").fadeIn(1000);
        $("#activeWorkshop").html(text);
    });

    $("#registrationTypeStage .chooseConferenceType").click(function () {

        $("#registrationTypeStage .chooseConferenceType").removeClass("iconColor");
        $(this).addClass("iconColor");

        $("button.dropdown-toggle, .dropdown-menu li a").click(function () {
            if (!$(".dropdown").hasClass("open")) {
                $('.dropdown-toggle').children("span").addClass("dropup");
            } else {
                $('.dropdown-toggle').children("span").removeClass("dropup");

            }
        });
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
        var id = registerModule.getFormId();
        var code = parseInt($(id + " .itad-shirt.active").data("size-code"));

        $(".itad-gender").removeClass("active");
        if ($(this).hasClass("itad-girl")) {
            registerModule.setIsFemale(true);
            $(".itad-gender.itad-girl").addClass("active");

            if (code > 5) {
                $(".itad-shirt").removeClass("active");
                $(regId + " #sizeInput," + regWorkshopId + " #sizeInput").val(code - 5);
                $(" .itad-shirt[data-size-code='" + (code-5) + "']").addClass('active');
            }
        }
        else {
            if (code <= 5) {
                $(".itad-shirt").removeClass("active");
                $(regId + " #sizeInput," + regWorkshopId + " #sizeInput").val(code + 5);
                $(" .itad-shirt[data-size-code='" + (code + 5) + "']").addClass('active');
            }

            $(".itad-gender.itad-guy").addClass("active");
            registerModule.setIsFemale(false);
        }


        if (registerModule.getActualStage() === 3) {
            registerModule.setStage(4);
        } else {
            registerModule.loopToCurrentStage(registerModule.getActualStage());
        }
    });

    $(".itad-shirt").click(function () {
        var id = registerModule.getFormId();
        var code = $(this).data("size-code");
        $(regId + " #sizeInput," + regWorkshopId + " #sizeInput").val(code);
        $(".itad-shirt").removeClass("active");
        $(this).addClass('active');

        if (id === regWorkshopId) {
            $(regId + " .itad-shirt[data-size-code='" + $(this).data('size-code') + "']").addClass('active');
        } else {
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
        success: function (data) {
            console.log(data);
            fillBackendErrorMessages(data);
        }
    };

    $('#RegisterForm, #RegisterWorkshopForm').ajaxForm(options);

    $('#registerBtnStage a').click(function () {
        $(this).parent().parent().submit();
    });
});
