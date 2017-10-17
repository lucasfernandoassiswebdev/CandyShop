var AjaxJsProduto = (function ($) {
    var url = {}; //objeto que recebe o nome e endereço da pagina

    // Lista de objetos que guarda o nome e o endereco da pagina, sã carregados na pagina padrao
    var init = function (config) {
        url = config;
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
            PrecoProduto: $('#PrecoProduto').val().replace("R$ ", "").replace(".", ""),
            QtdeProduto: $('#QtdeProduto').val(),
            Categoria: $('#Categoria').val(),
            ImagemA: baseA,
            ImagemB: baseB,
            ImagemC: baseC
        };
        concluirAcao(url.concluirCadastroProduto, produto, url.cadastrarProduto);
    };
    var detalheProduto = function (id, telaAnterior) {
        var produto = { IdProduto: id, telaAnterior: telaAnterior };
        chamaPaginaComIdentificador(url.detalheProduto, produto);
    };
    var editarProduto = function (id, telaAnterior) {
        var produto = { IdProduto: id, telaAnterior: telaAnterior };
        chamaPaginaComIdentificador(url.editarProduto, produto);
    };
    var concluirEdicaoProduto = function (baseA, baseB, baseC, removerA, removerB, removerC, pagina) {
        var produto = {
            IdProduto: $('#IdProduto').val(),
            NomeProduto: $('#NomeProduto').val(),
            PrecoProduto: $('#PrecoProduto').val().replace("R$", "").replace(".", ""),
            QtdeProduto: $('#QtdeProduto').val(),
            Categoria: $('#Categoria').val(),
            Ativo: $('#Ativo').val(),
            ImagemA: baseA,
            ImagemB: baseB,
            ImagemC: baseC,
            RemoverImagemA: removerA,
            RemoverImagemB: removerB,
            RemoverImagemC: removerC
        };
        concluirAcaoEdicao(url.concluirEdicaoProduto, produto, pagina);
    };
    var desativarProduto = function (id, telaAnterior) {
        var produto = { IdProduto: id, telaAnterior: telaAnterior };
        chamaPaginaComIdentificador(url.desativarProduto, produto);
    };
    var desativarProdutoConfirmado = function (id) {
        var produto = { IdProduto: id };
        concluirAcaoEdicao(url.desativarProdutoConfirmado, produto, url.listaProduto);
    };
    var listarInativos = function () {
        chamaPagina(url.listarInativos);
    };
    var listarProdutoPorNome = function (nome) {
        var produto = { Nome: nome };
        chamaPaginaComIdentificador(url.listarProdutoPorNome, produto);
    };
    var listaCategoria = function (categoria) {
        chamaPaginaComIdentificador(url.listaCategoria, { categoria: categoria });
    }

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
        listarProdutoPorNome: listarProdutoPorNome,
        listaCategoria: listaCategoria
    };

})(jQuery); //O método ajaxJS é auto executado quando é iniciado o sistema.