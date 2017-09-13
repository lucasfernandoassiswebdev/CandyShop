$(document).ready(function() {
    $(".button-collapse").sideNav();
    $('.collapsible').collapsible();
    $('.modal').modal();

    $(".closeMenu").on('click',function () {
        $('.button-collapse').sideNav('hide');
    });
});