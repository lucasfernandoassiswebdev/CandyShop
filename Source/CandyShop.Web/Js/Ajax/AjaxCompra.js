var AjaxJsCompra = (function ($) {
    var url = {};

    var init = function (config) {
        url = config;
    };
    var inserirCompra = function () {
        var listaProdutos = [];
        var produto;
        var produtos = $('.collection li');
        var i = 1;
        //var j = 0;
        $.each(produtos,
            function () {
                produto = {
                    Produto: { IdProduto: $('li:nth-child('+ i +') span').attr('data-id') },
                    QtdeCompra: $('li:nth-child('+ i +') p').attr('data-Quantidade')
                };
                listaProdutos.push(produto);
                i++;
                //j++;
            });
        console.log(listaProdutos);
        var compra = { Itens: listaProdutos };

        $.post(url.inserirCompra, compra)
            .done(function (message) {
                $.get(url.navbar)
                    .done(function (data) {
                        $('body').slideUp(function () {
                            $('body').hide().html(data).slideDown(function () {
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
    }
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
    var detalheCompra = function (idCompra) {
        chamaPaginaComIdentificador(url.detalheCompra, { idCompra: idCompra });
    };
    var editarCompra = function (idCompra) {
        chamaPaginaComIdentificador(url.editarCompra, { IdCompra: idCompra });
    };
    var concluirEdicaoCompra = function (idCompra, cpfUsuario) {
        var listaProdutos = [], i = 3;
        $('select').each(function () {
            var itemCompra = {
                Id: $(this).val(),
                QtdeProduto: $('input:eq(' + i + ')').val()
            };
            listaProdutos.push(itemCompra);
            i++;
        });

        var compra = {
            IdCompra: idCompra,
            DataCompra: $('input:eq(0)').val() + $('input:eq(1)').val(),
            Usuario : {
              Cpf : cpfUsuario   
            },
            Itens: listaProdutos
        };
        concluirAcaoEdicao(url.editarCompra, compra, url.listarCompraMes);
    };
    return {
        init: init,
        historicoCompra: historicoCompra,
        listarCompraSemana: listarCompraSemana,
        listarCompraMes: listarCompraMes,
        listarCompraDia: listarCompraDia,
        inserirCompra: inserirCompra,
        detalheCompra: detalheCompra,
        editarCompra: editarCompra,
        concluirEdicaoCompra: concluirEdicaoCompra
    };
})(jQuery);