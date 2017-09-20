var AjaxJs = (function ($) {
    var url = {}; //objeto que recebe o nome e endereço da pagina

    // Lista de objetos que guarda o nome e o endereco da pagina, sã carregados na pagina padrao
    var init = function (config) {
        url = config;
        main();
    };

    //carrega a pagina de inicio
    function main() {
        $.get(url.main).done(function (data) { //pega a view main e a carrega no div
            $("#DivGrid").slideUp(function () {
                $('#DivGrid').hide().html(data).slideDown(); //desce  o divgrid                                                                                
            });
        }).fail(function (xhr) { //xhr é o código do erro, que é retornado caso o get não tenha sucesso
            console.log(xhr.responseText);
        });
    }

    //função que vai carregar o corpo inteiro da pagina
    function carregaPadrao() {
        $.get(url.padrao).done(function (data) {
            $('body').slideUp(function () {
                $('body').hide().html(data).slideDown(); 
            });
        }).fail(function (xhr) { 
            console.log(xhr.responseText);
        });
    }

    //Função genérica para carregar o div, de acordo com o endereço passado
    function chamaPagina(endereco) {
        $.get(endereco).done(function (data) { //data é o conteudo da view
            $('#DivGrid').slideUp(function () { //a div é recolhida
                $('#DivGrid').hide().html(data).slideDown(); //escondida, carregada e demonstrada novamente                
            });
        }).fail(function (xhr) {
            console.log(xhr.responseText);
        });
    }

    function chamaPaginaComIdentificador(endereco, identificador) {
        $.get(endereco, identificador).done(function (data) { //data é o conteudo da view
            $('#DivGrid').slideUp(function () { //a div é recolhida
                $('#DivGrid').hide().html(data).slideDown(); //escondida, carregada e demonstrada novamente
            });
        }).fail(function (xhr) {
            console.log(xhr.responseText);
        });
    }

    function concluirAcao(endereco, objeto, tela) {
        $.post(endereco, objeto)
            .done(function (message) { //passar o parametro data aqui quando for definida a mensagem 
                chamaPagina(tela);
                Materialize.toast(message, 3000);
            })
            .fail(function (xhr) {
                console.log(xhr.responseText);
            });
    }

    //Variavel que retorna para o inicio
    var voltarInicio = function () {
        main();
    };

    //gerenciamento da lojinha
    var historicoCompra = function () {
        chamaPagina(url.historicoCompra);
    };
    var mostraSaldo = function () {
        chamaPagina(url.mostraSaldo);
    };
    //pagamentos
    var detalhePagamento = function () {
        chamaPagina(url.detalhePagamento);
    };
    var pagamento = function () {
        chamaPagina(url.pagamento);
    };

    //usuarios
    var cadastroUsuario = function () {
        chamaPagina(url.cadastroUsuario);
    };
    var listaUsuario = function () {
        chamaPagina(url.listaUsuario);
    };    
    var editarUsuario = function (cpf) {
        var usuario = { Cpf: cpf };
        chamaPaginaComIdentificador(url.editarUsuario, usuario);
    };
    var detalheUsuario = function (cpf) {
        var usuario = { Cpf: cpf };
        chamaPaginaComIdentificador(url.detalheUsuario, usuario);
    };
    var concluirCadastroUsuario = function () {
        var usuario = {
            Cpf: $('#cpf').val(),
            NomeUsuario: $('#Nome').val()
        };
        concluirAcao(url.concluirCadastroUsuario, usuario, url.cadastroUsuario);
    };
    var concluirEdicaoUsuario = function () {
        var usuario = {
            Cpf: $('#Cpf').val(),
            NomeUsuario: $('#Nome').val(),
            SaldoUsuario: $('#SaldoUsuario').val(),
            SenhaUsuario: $('#Password').val(),
            Ativo: $('#Ativo').val()
        };
        concluirAcao(url.concluirEdicaoUsuario, usuario, url.listaUsuario);
    };
    var desativarUsuario = function (cpf) {
        var usuario = { Cpf: cpf };
        chamaPaginaComIdentificador(url.desativarUsuario, usuario);
    };
    var desativarUsuarioConfirmado = function (cpf) {
        var usuario = { Cpf: cpf };
        concluirAcao(url.desativarUsuarioConfirmado, usuario, url.listaUsuario);
    };
    var listarUsuarioInativo = function () {
        chamaPagina(url.listarUsuarioInativo);
    };
    var listarUsuarioEmDivida = function () {
        chamaPagina(url.listarUsuarioEmDivida);
    };
    var listarUsuarioPorNome = function () {
        var usuario = { Nome: $('#nomeUsuario').val() };
        chamaPaginaComIdentificador(url.listarUsuarioPorNome, usuario);
    };
    var verificaLogin = function() {
        var usuario = { Cpf: $('#cpf').val(), SenhaUsuario: $('#senha').val() };

        $.post(url.verificaLogin, usuario)
            .done(function(message) {
                carregaPadrao();
                Materialize.toast(message, 3000);
            })
            .fail(function(xhr) {
                Materialize.toast(xhr.responseText, 3000);
            });
    };
    var logOff = function () {
        Materialize.toast("Deslogado", 3000);
        carregaPadrao();
    }
    //produtos
    var listaProduto = function () {
        chamaPagina(url.listaProduto);
    };
    var cadastrarProduto = function () {
        chamaPagina(url.cadastrarProduto);
    };
    var concluirCadastroProduto = function () {
        var produto = {
            NomeProduto: $('#NomeProduto').val(),
            PrecoProduto: $('#PrecoProduto').val(),
            QtdeProduto: $('#QtdeProduto').val(),
            Categoria: $('#Categoria').val()
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
        concluirAcao(url.concluirEdicaoProduto, produto, url.listaProduto);
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
        //para admin  e usuario
        init: init,
        voltarInicio: voltarInicio,

        //gerenciamento da lojinha
        mostraSaldo: mostraSaldo,
        //pagamento
        pagamento: pagamento,
        detalhePagamento: detalhePagamento,
        //usuario                
        historicoCompra: historicoCompra,
        concluirCadastroUsuario: concluirCadastroUsuario,
        listaUsuario: listaUsuario,
        cadastroUsuario: cadastroUsuario,
        concluirEdicaoUsuario: concluirEdicaoUsuario,
        editarUsuario: editarUsuario,
        detalheUsuario: detalheUsuario,
        desativarUsuario: desativarUsuario,
        desativarUsuarioConfirmado: desativarUsuarioConfirmado,
        listarUsuarioInativo: listarUsuarioInativo,
        listarUsuarioEmDivida: listarUsuarioEmDivida,
        listarUsuarioPorNome: listarUsuarioPorNome,
        verificaLogin: verificaLogin,
        logOff : logOff,
        //produtos
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