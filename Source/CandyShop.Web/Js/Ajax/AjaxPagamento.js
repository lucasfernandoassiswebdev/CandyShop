var obj;
var AjaxJsPagamento = (function ($) {
    var url = {};

    var init = function (config) {
        url = config;
    };

    var detalhePagamento = function () {
        chamaPaginaPagamento(url.detalhePagamento,"#DivGrid");
    };
    var listarPagamento = function () {
        chamaPaginaPagamento(url.listarPagamento,"#DivGrid");
    };
    var listarPagamentoMes = function (mes) {
        atualizaToken();
        chamaPaginaComIdentificador(url.listarPagamentoMes, { mes: mes, token: obj.access_token});
    };
    var listarPagamentoSemana = function () {
        chamaPaginaPagamento(url.listarPagamentoSemana,"#DivGrid");
    };
    var listarPagamentoDia = function () {
        chamaPaginaPagamento(url.listarPagamentoDia,"#DivGrid");
    };
    var inserirPagamento = function () {
        chamaPagina(url.inserirPagamento);
    };
    var concluirPagamento = function () {
        var pagamento = { ValorPagamento: $("#valorPago").val().replace("R$ ", "") };
        atualizaToken();
        concluirAcaoPagamento(url.concluirPagamento, {pagamento: pagamento, token: obj.access_token}, url.padrao, "body");
    };

    var editarPagamento = function (idPagamento, paginaAnterior) {
        atualizaToken();
        chamaPaginaComIdentificador(url.editarPagamento, { idPagamento: idPagamento, paginaAnterior: paginaAnterior, token: obj.access_token });
    };
    var concluirEdicaoPagamento = function(paginaAnterior, parameter) {
        var pagamento = {
            IdPagamento: $("#IdPagamento").val(),
            ValorPagamento: $("#valorPago").val().replace("R$ ", ""),
            Usuario: { Cpf: $("#Cpf").val() } 
        };
        atualizaToken();
        $.post(url.concluirEdicaoPagamento, { pagamento: pagamento, token: obj.access_token })
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
    };
})(jQuery);

function chamaPaginaPagamento(endereco, div) {
    atualizaToken();
    $.ajax({
        url: endereco,
        type: "GET",
        data: {
            token: obj.access_token
        },
        success: function (dataSucess) {
            $(div).slideUp(function () {
                $(div).hide().html(dataSucess).slideDown(function () {
                    Materialize.toast(dataSucess.data, 3000);
                });
            });
        },
        error: function (xhr) {
            Materialize.toast("Você não está autorizado, contate os administradores", 3000);
            console.log(xhr.responseText);
        }
    });
}

function concluirAcaoPagamento(endereco, objeto, tela, div) {
    $.post(endereco, objeto)
        .done(function (message) {
            chamaPaginaPagamento(tela,div);
            Materialize.toast(message, 4000);
        })
        .fail(function (xhr) {
            console.log(xhr.responseText);
        });
}

function atualizaToken() {
    obj = localStorage.getItem("tokenCandyShop") ? JSON.parse(localStorage.getItem("tokenCandyShop")) : [];
    if (obj == [])
        Materialize.modal("Há algo de errado com suas validações", 2000);
}


