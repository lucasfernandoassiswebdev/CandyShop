var AjaxJsShop = (function ($) {
    var url = {};

    var init = function (config) {
        url = config;
    };

    var mostraSaldo = function () {
        chamaPagina(url.mostraSaldo);
    };

    var administracao = function () {
        $.get(url.administracao).done(function(data) {
            $('body').slideUp(function() {
                $('body').hide().html(data).slideDown();
            });
        }).fail(function() {
            Materialize.Toast("Erro ao ir para administração, contate um desenvolvedor");
        });
    };

    var loja = function() {
        $.get(url.loja).done(function (data) {
            $('body').slideUp(function () {
                $('body').hide().html(data).slideDown();
            });
        }).fail(function () {
            Materialize.Toast("Erro ao ir para loja, contate um desenvolvedor");
        });
    }

    var voltarInicio = function () {
        main(url.main);
    };

    //função que vai carregar o corpo inteiro da pagina    

    return {
        init: init,
        mostraSaldo: mostraSaldo,
        voltarInicio: voltarInicio,
        administracao: administracao,
        loja: loja
    }
})(jQuery);