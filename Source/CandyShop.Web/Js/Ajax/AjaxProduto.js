var AjaxJsProduto = (function ($) {
    var url = {}; //objeto que recebe o nome e endereço da pagina

    // Lista de objetos que guarda o nome e o endereco da pagina, sã carregados na pagina padrao
    var init = function (config) {
        url = config;
        //main(url.main);
    };

    var listaProduto = function () {
        chamaPagina(url.listaProduto);
    };
    var cadastrarProduto = function () {
        chamaPagina(url.cadastrarProduto);
    };
    var concluirCadastroProduto = function (baseA, baseB, baseC) {
        var produto = {
            NomeProduto: $('#NomeProduto').val(),
            PrecoProduto: $('#PrecoProduto').val(),
            QtdeProduto: $('#QtdeProduto').val(),
            Categoria: $('#Categoria').val(),
            ImagemA: baseA,
            ImagemB: baseB,
            ImagemC: baseC
        };
        concluirAcao(url.concluirCadastroProduto, produto, url.cadastrarProduto);
    };
    var detalheProduto = function (id) {
        var produto = { IdProduto: id };
        chamaPaginaComIdentificador(url.detalheProduto, produto);
    };
    var editarProduto = function (id) {
        var produto = { IdProduto: id };
        chamaPaginaComIdentificador(url.editarProduto, produto);
    };
    var concluirEdicaoProduto = function () {
        var produto = {
            IdProduto: $('#IdProduto').val(),
            NomeProduto: $('#NomeProduto').val(),
            PrecoProduto: $('#PrecoProduto').val(),
            QtdeProduto: $('#QtdeProduto').val(),
            Categoria: $('#Categoria').val(),
            Ativo: $('#Ativo').val()
        };
        concluirAcaoEdicao(url.concluirEdicaoProduto, produto, url.listaProduto);
    };
    var desativarProduto = function (id) {
        var produto = { IdProduto: id };
        chamaPaginaComIdentificador(url.desativarProduto, produto);
    };
    var desativarProdutoConfirmado = function (id) {
        var produto = { IdProduto: id };
        concluirAcao(url.desativarProdutoConfirmado, produto, url.listaProduto);
    };
    var listarInativos = function () {
        chamaPagina(url.listarInativos);
    };
    var listarProdutoPorNome = function () {
        var produto = { Nome: $('#nomeProduto').val() };
        chamaPaginaComIdentificador(url.listarProdutoPorNome, produto);
    };

    //retorna links para acessar as paginas.
    return {
        init: init,
        listaProduto: listaProduto,
        cadastrarProduto: cadastrarProduto,
        concluirCadastroProduto: concluirCadastroProduto,
        detalheProduto: detalheProduto,
        editarProduto: editarProduto,
        concluirEdicaoProduto: concluirEdicaoProduto,
        desativarProduto: desativarProduto,
        desativarProdutoConfirmado: desativarProdutoConfirmado,
        listarInativos: listarInativos,
        listarProdutoPorNome: listarProdutoPorNome
    };

})(jQuery); //O método ajaxJS é auto executado quando é iniciado o sistema.