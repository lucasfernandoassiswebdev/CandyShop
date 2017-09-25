var AjaxJsCompra = (function ($) {
    var url = {};

    var init = function (config) {
        url = config;
    };

    var historicoCompra = function () {
        chamaPagina(url.historicoCompra);
    };

    return {
        init: init,
        historicoCompra: historicoCompra
    };
})(jQuery);