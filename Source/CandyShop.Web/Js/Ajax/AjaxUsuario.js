var AjaxJsUsuario = (function ($) {
    var url = {};

    var init = function (config) {
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
            Imagem: imgBase64,
            Classificacao: $('#Classificacao').val()
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
            Imagem: imgBase64,
            Classificacao: $('#Classificacao').val()
        };
        concluirAcaoEdicao(url.concluirEdicaoUsuario, usuario, url.listaUsuario);
    };
    var desativarUsuario = function (cpf) {
        var usuario = {
            Cpf: cpf
        };
        chamaPaginaComIdentificador(url.desativarUsuario, usuario);
    };
    var desativarUsuarioConfirmado = function (cpf) {
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
    
    var logOff = function () {
        $.get(url.logOff).done(function (data) {
            $('body').slideUp(function () {
                if (localStorage.getItem('listaProdutos') != null) {
                    localStorage.removeItem('listaProdutos');
                }
                $('body').hide().html(data).slideDown(function () { Materialize.toast("LogOff feito com sucesso", 3000); });
            });
        }).fail(function (xhr) {
            console.log(xhr.responseText);
        });     
    };

    return {
        init: init,
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
        logOff: logOff
    }
})(jQuery);