var AjaxJsUsuario = (function($) {
    var url = {};

    var init = function(config) {
        url = config;
    };

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
    var concluirCadastroUsuario = function (imgBase64) {
        //montantando o objeto que vai chegar no controller
        var usuario = {
            Cpf: $('#cpf').val(),
            NomeUsuario: $('#Nome').val(),
            Imagem: imgBase64
        };
        concluirAcao(url.concluirCadastroUsuario, usuario, url.cadastroUsuario);
    };
    var concluirEdicaoUsuario = function (imgBase64) {
        var usuario = {
            Cpf: $('#Cpf').val(),
            NomeUsuario: $('#Nome').val(),
            SaldoUsuario: $('#SaldoUsuario').val(),
            SenhaUsuario: $('#Password').val(),
            Ativo: $('#Ativo').val(),
            Imagem: imgBase64
        };
        concluirAcaoEdicao(url.concluirEdicaoUsuario, usuario, url.listaUsuario);
    };
    var desativarUsuario = function (cpf) {
        alert(cpf);
        var usuario = {
            Cpf: cpf
        };
        chamaPaginaComIdentificador(url.desativarUsuario, usuario);
    };
    var desativarUsuarioConfirmado = function (cpf) {
        alert(cpf);
        var usuario = { Cpf: cpf };
        concluirAcaoEdicao(url.desativarUsuarioConfirmado, usuario, url.listarUsuarioInativo);
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
    var verificaLogin = function () {
        var usuario = { Cpf: $('#cpf').val(), SenhaUsuario: $('#senha').val() };

        $.post(url.verificaLogin, usuario)
            .done(function () {
                carregaPadrao();
            })
            .fail(function (xhr) {
                Materialize.toast(xhr.responseText, 3000);
            });
    };
    var logOff = function () {
        deslogar();
        Materialize.toast("Deslogado", 3000);
    };

    return {
        init: init,
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
        logOff: logOff
    }
})(jQuery);