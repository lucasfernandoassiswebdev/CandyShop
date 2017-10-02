$(document).ready(function () {
    $('.button-collapse').sideNav();
    $('.collapsible').collapsible();
    $('.modal').modal();
    $('.tooltipped').tooltip({ delay: 50 });
    $('select').material_select();
    $('.penis').last().removeClass('select-dropdown');

    $(".closeMenu").on('click',function () {
        $('.button-collapse').sideNav('hide');
    });
});

$(document).ajaxStart(function () {
    $("#DivLoad").fadeIn(300);
});

$(document).ajaxStop(function () {
    $("#DivLoad").fadeOut(300);
});