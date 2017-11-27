/* Arquivos Ajax são usados para fazer as requisições nos controllers web,
   as functions definidas aqui montam os objetos necessários, chamam os controllers
   e carregam as páginas necessárias */
var obj;
var AjaxJsCompra = (function ($) {
    var url = {};

    var init = function (config) {
        url = config;
    };
    var inserirCompra = function() {
        var listaProdutos = [];
        var produto;
        var produtos = $(".collection li");
        var i = 1;
        $.each(produtos,
            function() {
                produto = {
                    Produto: { IdProduto: $("li:nth-child(" + i + ") span").attr("data-id") },
                    QtdeCompra: $("li:nth-child(" + i + ") p").attr("data-Quantidade")
                };
                listaProdutos.push(produto);
                i++;
            });
        console.log(listaProdutos);
        var compra = { Itens: listaProdutos };
        atualizaToken();
        $.post(url.inserirCompra, { compra: compra, token: obj.access_token })
            .done(function(message) {
                $.get(url.navbar)
                    .done(function(data) {
                        $("body").slideUp(function() {
                            $("body").hide().html(data).slideDown(function() {
                                Materialize.toast(message, 4000);
                            });
                        });
                    }).fail(function(xhr) {
                        console.log(xhr.responseText);
                    });
                if (message === "Sua compra foi registrada com sucesso") 
                    localStorage.removeItem("listaProdutos");
            })
            .fail(function(xhr) {
                console.log(xhr.responseText);
                Materialize.toast("Algo deu errado, recarregue a página ou contate um administrador",2000);
            });
    };

    var historicoCompra = function () {
        chamaPaginaCompra(url.historicoCompra, "#DivGrid");
    };
    var listarCompra = function () {
        chamaPaginaCompra(url.listarCompra, "#DivGrid");
    };
    var listarCompraMes = function (mes) {
        atualizaToken();
        chamaPaginaComIdentificador(url.listarCompraMes, { mes: mes, token: obj.access_token});
    };
    var listarCompraSemana = function () {
        chamaPaginaCompra(url.listarCompraSemana, "#DivGrid");
    };
    var listarCompraDia = function () {
        chamaPaginaCompra(url.listarCompraDia, "#DivGrid");
    };
    var detalheCompra = function (idCompra, paginaAnterior) {
        atualizaToken();
        chamaPaginaComIdentificador(url.detalheCompra, { idCompra: idCompra, paginaAnterior: paginaAnterior, token: obj.access_token});
    };
    
    return {
        init: init,
        historicoCompra: historicoCompra,
        listarCompraSemana: listarCompraSemana,
        listarCompra: listarCompra,
        listarCompraMes: listarCompraMes,
        listarCompraDia: listarCompraDia,
        inserirCompra: inserirCompra,
        detalheCompra: detalheCompra
    };
})(jQuery);

function atualizaToken() {
    obj = localStorage.getItem("tokenCandyShop") ? JSON.parse(localStorage.getItem("tokenCandyShop")) : [];
    if (obj == [])
        Materialize.modal("Há algo de errado com suas validações", 2000);
}

function chamaPaginaCompra(endereco, div) {
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