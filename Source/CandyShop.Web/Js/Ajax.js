﻿var AjaxJs = (function($) {
    var url = {};   //objeto que recebe o nome e endereço da pagina

    // Lista de objetos que guarda o nome e o endereco da pagina, sã carregados na pagina padrao
    var init = function(config) {
        url = config;
        main();
    };


    //carrega a pagina de inicio
    function main() {
        $.get(url.main).done(function(data) {                //pega a view main e a carrega no div
            $("#DivGrid").slideUp(function() {
                $('#DivGrid').hide().html(data).slideDown(); //desce  o divgrid                                                                                
            });
        }).fail(function(xhr) {                            //xhr é o código do erro, que é retornado caso o get não tenha sucesso
            $("#DivGrid").errorMessage(xhr.responseText);
        });
    }

    //Função genérica para carregar o div, de acordo com o endereço passado
    function chamaPagina(endereco) {    
        $.get(endereco).done(function (data) {               //data é o conteudo da view
            $('#DivGrid').slideUp(function () {                 //a div é recolhida
                $('#DivGrid').hide().html(data).slideDown();    //escondida, carregada e demonstrada novamente
            });
        }).fail(function (xhr) {
            $('#DivGrid').errorMessage(xhr.responseText);            
        });
    };

    //Variavel que retorna para o inicio
    var voltarInicio = function () {
        main();
    };

    //var que chama a view pagamento
    var pagamento = function () {
        chamaPagina(url.pagamento);
    };

    //var que chama a view historico de compra
    var historicoCompra = function () {
        chamaPagina(url.historicoCompra);
    };


    var listaUsuario = function() {
        chamaPagina(url.listaUsuario);
    }

    var listaProduto = function() {
        chamaPagina(url.listaProduto);
    }

    var detalhePagamento = function () {
        chamaPagina(url.detalhePagamento);
    }
    //retorna links para acessar as paginas.
    return {
        init: init,
        pagamento: pagamento,
        voltarInicio: voltarInicio,
        historicoCompra: historicoCompra,
        listaUsuario: listaUsuario,
        listaProduto: listaProduto,
        detalhePagamento: detalhePagamento
    };

})(jQuery);           //O método ajaxJS é auto executado quando é iniciado o sistema.