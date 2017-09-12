$(document).ready(function() {
    $(".button-collapse").sideNav();
    $('.collapsible').collapsible();

    $(".dropdown-button").on('click',function () {
        $('.button-collapse').sideNav('hide');
    });
});