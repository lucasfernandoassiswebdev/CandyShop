var AjaxJsShop = (function ($) {
    var url = {};

    var init = function (config) {
        url = config;        
    };

    var mostraSaldo = function () {
        chamaPagina(url.mostraSaldo);
    };

    var voltarInicio = function() {
        main(url.main);
    };

    return {
        init: init,
        mostraSaldo: mostraSaldo,
        voltarInicio: voltarInicio
}
})(jQuery);