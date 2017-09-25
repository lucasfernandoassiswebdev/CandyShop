var AjaxJsShop = (function ($) {
    var url = {};

    var init = function (config) {
        url = config;        
    };

    var mostraSaldo = function () {
        chamaPagina(url.mostraSaldo);
    };   

    return {
        init: init,
        mostraSaldo: mostraSaldo         
}
})(jQuery);