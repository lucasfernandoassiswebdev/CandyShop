var AjaxJsCompra = (function($) {
    var url = {};

    var init = function(config) {
        url = config;
    };

    var inserirCompra = function() {
        var listaProdutos = [];
        var produto;
        var produtos = $('.collection li');
        var i = 2;
        var j = 2;
        $.each(produtos,
            function() {
                produto = {
                    Produto: { IdProduto: $('span:eq(' + i + ')').attr('data-Id') },
                    QtdeCompra: $('p:eq(' + j + ')').attr('data-quantidade')
                };                
                listaProdutos.push(produto);
                i++;
                j++;
            });

        var compra = { Itens: listaProdutos };

        $.post(url.inserirCompra, compra)
            .done(function(message) {
                $.get(url.navbar)
                    .done(function(data) {
                        $('body').slideUp(function() {
                            $('body').hide().html(data).slideDown(function() {
                                Materialize.toast(message, 3000);
                            });
                        });
                    }).fail(function(xhr) {
                        console.log(xhr.responseText);
                    });
            })
            .fail(function(xhr) {
                console.log(xhr.responseText);
            });
    }

    var historicoCompra = function() {
        chamaPagina(url.historicoCompra);
    };
    var listarCompraMes = function(mes) {
        var parametro = { mes: mes };
        chamaPaginaComIdentificador(url.listarCompraMes, parametro);
    };
    var listarCompraSemana = function() {
        chamaPagina(url.listarCompraSemana);
    };

    var listarCompraDia = function() {
        chamaPagina(url.listarCompraDia);
    };

    var detalheCompra = function(idCompra) {
        chamaPaginaComIdentificador(url.detalheCompra, { idCompra: idCompra });
    };

    return {
        init: init,
        historicoCompra: historicoCompra,
        listarCompraSemana: listarCompraSemana,
        listarCompraMes: listarCompraMes,
        listarCompraDia: listarCompraDia,
        inserirCompra: inserirCompra,
        detalheCompra: detalheCompra
    };
})(jQuery);