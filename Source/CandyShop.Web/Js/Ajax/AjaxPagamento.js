var AjaxJsPagamento = (function ($) {
    var url = {};

    var init = function (config) {
        url = config;
    };

    var detalhePagamento = function () {
        chamaPagina(url.detalhePagamento);
    };
    var listarPagamento = function () {
        chamaPagina(url.listarPagamento);
    };
    var listarPagamentoMes = function (mes) {
        var parametro = { mes: mes };
        chamaPaginaComIdentificador(url.listarPagamentoMes, parametro);
    };
    var listarPagamentoSemana = function () {
        chamaPagina(url.listarPagamentoSemana);
    };

    var listarPagamentoDia = function () {
        chamaPagina(url.listarPagamentoDia);
    };

    var inserirPagamento = function () {
        chamaPagina(url.inserirPagamento);
    };
    var concluirPagamento = function () {
        var pagamento = { ValorPagamento: $('#valorPago').val() };
        //concluirAcao(url.concluirPagamento, pagamento, url.listarPagamento);

        $.post(url.concluirPagamento, pagamento)            
            .done(function (message) {
                carregaPadrao();
                chamaPagina(url.listarPagamento);
                Materialize.toast(message, 3000);
            })
            .fail(function (xhr) {
                console.log(xhr.responseText);
            });
    };

    return {
        init: init,
        //pagamento
        listarPagamento: listarPagamento,
        listarPagamentoSemana: listarPagamentoSemana,
        detalhePagamento: detalhePagamento,
        inserirPagamento: inserirPagamento,
        concluirPagamento: concluirPagamento,
        listarPagamentoMes: listarPagamentoMes,
        listarPagamentoDia: listarPagamentoDia
    }
})(jQuery);