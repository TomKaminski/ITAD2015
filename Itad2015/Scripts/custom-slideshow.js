/*=============================================================
    Authour URI: www.binarytheme.com
    License: Commons Attribution 3.0

    http://creativecommons.org/licenses/by/3.0/

    100% To use For Personal And Commercial Use.
    IN EXCHANGE JUST GIVE US CREDITS AND TELL YOUR FRIENDS ABOUT US
   
    ========================================================  */

(function ($) {
    "use strict";
    var mainApp = {


        plusMain: function () {

            //FUNCTION TO SCROLL BETWEEN LEFT MENU LINKS
            $(function () {
                $('nav a').bind('click', function (event) {
                    var $anchor = $(this);

                    $('html, body').stop().animate({
                        scrollTop: $($anchor.attr('href')).offset().top
                    }, 1000, 'easeInOutExpo');
                    $('.left-panel').animate({ left: '-230px' });
                    event.preventDefault();
                });
            });

            $(function () {
                $('.slide-inner').bind('click', function (event) {
                    var $anchor = $(this);

                    $('html, body').stop().animate({
                        scrollTop: $($anchor.attr('href')).offset().top - $(window).height()/2
                    }, 1000, 'easeInOutExpo');
                    event.preventDefault();
                });
            });


            // MAIN FUNTION FOR TRIGGER LEFT MENU


            $('.Icon-trigger span').click(function () {
                if (
            $('.left-panel').css('left') == '-230px') {
                    $('.left-panel').animate({ left: '0px' });
                }
                else
                    $('.left-panel').animate({ left: '-230px' });
            });


            $("#example, body").vegas({
                delay: 9000,
                fade: 1000,
                slides: [
                    { src: '/Content/images/slides/1.jpg' },
                    { src: '/Content/images/slides/2.jpg' },
                    { src: '/Content/images/slides/3.jpg' },
                    { src: '/Content/images/slides/4.jpg' },
                    { src: '/Content/images/slides/5.jpg' }
                ],
                overlay: true
            });


            ///** VEGAS SLIDESHOW IMAGES  **/
            //$(function () {
            //    $('body').vegas('slideshow', {
            //        backgrounds: [
            //          { src: 'Content/images/slides/2.jpg', fade: 1000, delay: 9000 }, //CHANGE THESE IMAGES WITH YOUR ORIGINAL IMAGES
            //           { src: 'Content/images/slides/1.jpg', fade: 1000, delay: 9000 }, //THESE IMAGES ARE FOR DEMO PURPOSE ONLY YOU, CAN NOT USE THEM WITHOUT AUTHORS PERMISSION
            //            { src: 'Content/images/slides/3.jpg', fade: 1000, delay: 9000 }, //SEE DOCUMENTATION FOR ORIGINAL URLs/LINKs OF IMAGES

            //        ]
            //    })('overlay', {
            //        /** SLIDESHOW OVERLAY IMAGE **/
            //        src: 'Content/vegas/overlays/01.png' // THERE ARE TOTAL 01 TO 15 .png IMAGES AT THE PATH GIVEN, WHICH YOU CAN USE HERE
            //    });


            //});

            /*
            YOU CAN WRITE 
            YOUR SCRIPTS HERE
            
            */



        },

        initialization: function () {
            mainApp.plusMain();

        }


    }
    // INITIALIZING ///

    $(document).ready(function () {
        mainApp.plusMain();
    });


}(jQuery));


