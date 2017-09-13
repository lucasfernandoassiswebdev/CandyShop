$(document).ready(function() {
    $(".button-collapse").sideNav();
    $('.collapsible').collapsible();
    $('.modal').modal();

    $(".dropdown-button").on('click',function () {
        $('.button-collapse').sideNav('hide');
    });
});