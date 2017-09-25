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

    //função que vai carregar o corpo inteiro da pagina    

    return {
        init: init,
        mostraSaldo: mostraSaldo,
        voltarInicio: voltarInicio,        
}
})(jQuery);