$(document).ready(function () {
    $(".button-collapse").sideNav();
    $(".collapsible").collapsible();
    $(".tooltipped").tooltip({ delay: 50 });
    $("select").material_select();
    $(".penis").last().removeClass("select-dropdown");

    $(".closeMenu").click(function () { $(".button-collapse").sideNav("hide"); });
});
// Código que faz o loading aparecer na tela toda vez que começa uma requisição Ajax
$(document).ajaxStart(function () { $("#DivLoad").fadeIn(300); });

// Código que tira o loading da tela
$(document).ajaxStop(function () { $("#DivLoad").fadeOut(300); });