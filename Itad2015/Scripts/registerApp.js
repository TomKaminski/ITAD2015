var registerModule = (function () {

    var isFemale = false;
    var actualStage = 1;
    var formId;

    var setStage = function (stage, canBack) {
        if (actualStage > stage && !canBack)
            return;
        actualStage = stage;
        switch (actualStage) {
            case 4:
                {
                    if (isFemale) {
                        $(formId + " section[data-stage-id='"+actualStage+"'].male").hide();
                        $(formId + " section[data-stage-id='" + actualStage + "'].female").fadeIn(1000);

                    } else {
                        $(formId + " section[data-stage-id='" + actualStage + "'].female").hide();
                        $(formId + " section[data-stage-id='" + actualStage + "'].male").fadeIn(1000);
                    }
                    break;
                }
            default :
            {
                $(formId + " section[data-stage-id='" + actualStage + "'").fadeIn(1000);
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
        $(regId + " section, " + regWorkshopId + " section").hide();
        $(".chooseConferenceType").removeClass("iconColor");
        $("input.itad-required-input, textarea").val('');
        $(".itad-gender, .itad-shirt").removeClass("active");
        $(".rulesCheckbox").prop('checked', false);
        $("a.registerSubmit").addClass("disabled");
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
    $(".serverErrorsContainer").empty();
    if (result.status === false) {
        for (var j = 0; j < result.errors.length; j++) {
            $(registerModule.getFormId() + " .serverErrorsContainer").append("<p>" + result.errors[j] + "<br/></p>");
        }
    }
}

$(document).ready(function () {

    $(window).scroll(function () {
        if (!$("#fun-numbers").hasClass('fun-number-done')) {
            var hT = $("#fun-numbers").offset().top;
            var hH = $("#fun-numbers").outerHeight();
            var wH = $(window).height();
            var wS = $(this).scrollTop();

            if (wS > (hT + hH - wH + 100)) {
                $("#fun-numbers").addClass('fun-number-done');
                $('.fun-number').each(function () {
                    $(this).animateNumber(
                    {
                        number: $(this).data("number"),
                        easing: 'easeInQuad',
                        'font-size': '45px'
                    }, $(this).data("speed"));
                });
            }
        }
    });

    registerModule.init();

    $(".dropdown-menu li a").click(function (e) {
        e.preventDefault();
        var text = $(this).text();
        var workshopId = $(this).data("workshop-id");

        $("#workshopId").val(workshopId).trigger("change").valid();
        $(".workshop-container").hide();
        $(".workshop-container[data-workshop-id='" + workshopId + "']").fadeIn(1000);
        $("#activeWorkshop").html(text);
    });

    $(".chooseConferenceType").click(function () {
        $(".chooseConferenceType").removeClass("iconColor");
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

    $(".itad-gender").click(function () {
        var id = registerModule.getFormId();
        var code = parseInt($(id + " .itad-shirt.active").data("size-code"));

        $(".itad-gender").removeClass("active");
        if ($(this).hasClass("itad-girl")) {
            registerModule.setIsFemale(true);
            $(".itad-gender.itad-girl").addClass("active");

            if (code > 5) {
                $(".itad-shirt").removeClass("active");
                $(regId + " #sizeInput," + regWorkshopId + " #sizeInput").val(code - 5);
                $(" .itad-shirt[data-size-code='" + (code - 5) + "']").addClass('active');
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
        $(regId + " #sizeInput," + regWorkshopId + " #sizeInput").val($(this).data("size-code"));
        $(".itad-shirt").removeClass("active");
        $(this).addClass('active');

        if (registerModule.getFormId() === regWorkshopId) {
            $(regId + " .itad-shirt[data-size-code='" + $(this).data('size-code') + "']").addClass('active');
        } else {
            $(regWorkshopId + " .itad-shirt[data-size-code='" + $(this).data('size-code') + "']").addClass('active');
        }

        registerModule.setStage(5);
    });

    $(".rulesCheckbox").click(function () {
        var isChecked = $(this)[0].checked;
        $(".rulesCheckbox").prop('checked', isChecked);
        $("a.registerSubmit").toggleClass("disabled");
    });


    var options = {
        success: function (data) {
            $(".overlay").hide();
            if (data.status === true) {
                $('body').scrollTop(0);
                registerModule.setStage(1, true);
                registerModule.init();
                alertApp.appendAlert("alert-success", "Rejestracja przebiegła pomyślnie, na podany adres email została wysłana wiadomość potwierdzająca");
            } else {
                fillBackendErrorMessages(data);
            }

        },
        beforeSubmit: function() {
            $(".overlay").fadeIn();
        }
    };

    $('#RegisterForm, #RegisterWorkshopForm').ajaxForm(options);

    $('a.registerSubmit').click(function () {
        $(".serverErrorsContainer").empty();
        $(this).parent().parent().submit();
    });
});
