var obj;
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
        chamaPaginaProdutos(url.cadastrarProduto);
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
        atualizaToken();
        concluirAcao(url.concluirCadastroProduto, { produto: produto, token: obj.access_token }, url.cadastrarProduto);
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
            Ativo: $("input[name='status']:checked").val(),
            ImagemA: baseA,
            ImagemB: baseB,
            ImagemC: baseC,
            RemoverImagemA: removerA,
            RemoverImagemB: removerB,
            RemoverImagemC: removerC
        };
        atualizaToken();
        concluirAcaoEdicaoProduto(url.concluirEdicaoProduto, { produto: produto, token: obj.access_token }, pagina);
    };
    var desativarProduto = function (id, telaAnterior) {
        var produto = { IdProduto: id, telaAnterior: telaAnterior };
        chamaPaginaComIdentificador(url.desativarProduto, produto);
    };
    var desativarProdutoConfirmado = function (id) {
        var produto = { IdProduto: id };
        atualizaToken();
        concluirAcaoEdicao(url.desativarProdutoConfirmado, { produto: produto, token: obj.access_token }, url.listaProduto);
    };
    var listarInativos = function () {
        chamaPaginaProdutos(url.listarInativos);
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

function concluirAcaoEdicaoProduto(endereco, objeto, tela) {
    $.ajax({
        url: endereco,
        type: "POST",
        data: objeto,
        success: function (message) {
            chamaPaginaProdutos(tela);
            Materialize.toast(message, 4000);
        }
    });
}

function chamaPaginaProdutos(endereco) {
    atualizaToken();
    $.ajax({
        url: endereco,
        type: "GET",
        data: {
            token: obj.access_token
        },
        success: function (dataSucess) {
            $("#DivGrid").slideUp(function () {
                $("#DivGrid").hide().html(dataSucess).slideDown(function () {
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

function atualizaToken() {
    obj = localStorage.getItem("tokenCandyShop") ? JSON.parse(localStorage.getItem("tokenCandyShop")) : [];
    if (obj == [])
        Materialize.modal("Há algo de errado com suas validações", 2000);
}

