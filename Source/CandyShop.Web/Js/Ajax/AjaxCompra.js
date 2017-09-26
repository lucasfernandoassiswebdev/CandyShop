var AjaxJsCompra = (function ($) {
    var url = {};

    var init = function (config) {
        url = config;
    };

    var historicoCompra = function () {        
        chamaPagina(url.historicoCompra);
    };
    var listarCompraMes = function (mes) {
        var parametro = { mes: mes };
        chamaPaginaComIdentificador(url.listarCompraMes, parametro);
    };
    var listarCompraSemana = function () {
        chamaPagina(url.listarCompraSemana);
    };

    var listarCompraDia = function () {
        chamaPagina(url.listarCompraDia);
    };

    return {
        init: init,
        historicoCompra: historicoCompra,
        listarCompraSemana: listarCompraSemana,
        listarCompraMes: listarCompraMes,
        listarCompraDia: listarCompraDia
    };
})(jQuery);