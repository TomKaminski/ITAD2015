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
                        $(formId + " section[data-stage-id='" + actualStage + "'].male").hide();
                        $(formId + " section[data-stage-id='" + actualStage + "'].female").fadeIn(1000);

                    } else {
                        $(formId + " section[data-stage-id='" + actualStage + "'].female").hide();
                        $(formId + " section[data-stage-id='" + actualStage + "'].male").fadeIn(1000);
                    }
                    break;
                }
            default:
                {
                    $(formId + " section[data-stage-id='" + actualStage + "']").fadeIn(1000);
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

$.validator.unobtrusive.adapters.addBool("mustbetrue", "required");

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + expires;
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') c = c.substring(1);
        if (c.indexOf(name) === 0) return c.substring(name.length, c.length);
    }
    return "";
}

function fillBackendErrorMessages(result) {
    $(".serverErrorsContainer").empty();
    if (result.status === false) {
        for (var j = 0; j < result.errors.length; j++) {
            $(registerModule.getFormId() + " .serverErrorsContainer").append("<p>" + result.errors[j] + "<br/></p>");
        }
    }
}

$(document).ready(function () {

    if (getCookie("cookiesAccepted") === "") {
        $(".cookie-container").show();
    }

    $(".cookies-accept").click(function (e) {
        setCookie("cookiesAccepted", true, 14);
        $(".cookie-container").hide();
        e.preventDefault();
    });


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
                var id = registerModule.getFormId();
                if (id === regId) {
                    $("#registeredGuestsCount").text(parseInt($("#registeredGuestsCount").text()) - 1);
                } else {
                    var selectedWorkshop = $("input#workshopId").val();
                    var workshopCounter = $(".workshop-container[data-workshop-id='" + selectedWorkshop + "'] .workshopGuestCounter");
                    workshopCounter.text(parseInt(workshopCounter.text()) - 1);
                }
                $('body').scrollTop(0);
                registerModule.setStage(1, true);
                registerModule.init();
                alertApp.appendAlert("alert-success", "<img src='/Content/images/Mail/tick_green.png' /><span style='padding-left:20px;'>Rejestracja przebiegła pomyślnie. Na twój email została wysłana wiadomość wymagająca potwierdzenia. Jest to wymagane do sfinalizowania procesu rejestracji.</span>");
            } else {

                fillBackendErrorMessages(data);
            }

        },
        beforeSubmit: function () {
            $(".overlay").hide();
            $(".overlay").show();
        }
    };

    $('#RegisterForm, #RegisterWorkshopForm').ajaxForm(options);

    $('a.registerSubmit').click(function () {
        $(".serverErrorsContainer").empty();
        $(this).parent().parent().submit();
    });


    $(".agenda-box:not(a)").click(function () {
        var obj = $(this);
        if ($(this).hasClass("collapsed")) {
            obj.removeClass("collapsed").addClass("expanded");
            obj.children('.collapsed-content').hide();
            obj.children('.expanded-content').fadeIn();
                obj.find('.circle').circleProgress();
        } else {
            obj.children('.expanded-content').hide();
            obj.addClass("collapsed").removeClass("expanded");
            obj.children('.collapsed-content').show();
        }
    });

    $('.circle').circleProgress({
        size: 120,
        fill: {
            gradient: ["white", "white"]
        }
    }).on('circle-animation-progress', function (event, progress, stepValue) {
        var value = stepValue * 100;
        var minutes = parseInt(Math.round((value * 60) / 100));
        $(this).find('strong').text(String(minutes));
    });
});
