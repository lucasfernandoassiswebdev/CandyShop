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
        var pagamento = { ValorPagamento: $("#valorPago").val().replace("R$ ", "") };
        $.post(url.concluirPagamento, pagamento)            
            .done(function (message) {
                $.get(url.padrao)
                    .done(function (data) {
                        $("body").slideUp(function () {
                            $("body").hide().html(data).slideDown(function() {
                                Materialize.toast(message, 3000);
                            });
                        });
                    }).fail(function (xhr) {
                        console.log(xhr.responseText);
                    });                
            })
            .fail(function (xhr) {
                console.log(xhr.responseText);
            });        
    };

    var editarPagamento = function(idPagamento, paginaAnterior) {
        chamaPaginaComIdentificador(url.editarPagamento, { idPagamento: idPagamento, paginaAnterior: paginaAnterior});
    }

    var concluirEdicaoPagamento = function(paginaAnterior, parameter) {
        var pagamento = {
            IdPagamento: $("#IdPagamento").val(),
            ValorPagamento: $("#valorPago").val().replace("R$ ", ""),
            Usuario: { Cpf: $("#Cpf").val() } 
        };
        $.post(url.concluirEdicaoPagamento, pagamento)
            .done(function(data) {
                if (typeof paginaAnterior === "function") {
                    if (parameter != null) {
                        paginaAnterior(parameter);
                    }
                    paginaAnterior();
                }
                Materialize.toast(data,4000);
            }).fail(function(xhr) {
                Materialize.toast(xhr.responseText, 4000);
            });
    };

    return {
        init: init,
        //pagamento
        listarPagamento: listarPagamento,
        detalhePagamento: detalhePagamento,
        inserirPagamento: inserirPagamento,
        concluirPagamento: concluirPagamento,
        listarPagamentoDia: listarPagamentoDia,
        listarPagamentoSemana: listarPagamentoSemana,
        listarPagamentoMes: listarPagamentoMes,
        editarPagamento: editarPagamento,
        concluirEdicaoPagamento: concluirEdicaoPagamento
    }
})(jQuery);