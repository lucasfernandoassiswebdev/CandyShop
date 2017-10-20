var AjaxJsUsuario = (function($) {
    var url = {};

    var init = function(config) {
        url = config;
    };

    var cadastroUsuario = function() {
        chamaPagina(url.cadastroUsuario);
    };
    var listaUsuario = function() {
        chamaPagina(url.listaUsuario);
    };
    var editarUsuario = function(cpf, telaAnterior) {
        var usuario = { Cpf: cpf, telaAnterior: telaAnterior };
        chamaPaginaComIdentificador(url.editarUsuario, usuario);
    };
    var detalheUsuario = function (cpf, telaAnterior) {
        var usuario = { Cpf: cpf, telaAnterior: telaAnterior };
        chamaPaginaComIdentificador(url.detalheUsuario, usuario);
    };
    var concluirCadastroUsuario = function(imgBase64) {
        //montantando o objeto que vai chegar no controller
        var usuario = {
            Cpf: $('#cpf').val(),
            NomeUsuario: $('#Nome').val(),
            Imagem: imgBase64,
            Classificacao: $('#Classificacao').val()
        };
        concluirAcao(url.concluirCadastroUsuario, usuario, url.cadastroUsuario);
    };
    var concluirEdicaoUsuario = function(imgBase64,removerImagem, tela) {                
        var usuario = {
            Cpf: $('#Cpf').val(),
            NomeUsuario: $('#Nome').val(),
            SaldoUsuario: $('#SaldoUsuario').val().replace("R$ ", "").replace(".", ""),
            SenhaUsuario: $('#Password').val(),
            Ativo: $('#Ativo').val(),
            Imagem: imgBase64,
            Classificacao: $('#Classificacao').val(),
            RemoverImagem: removerImagem
        };
        concluirAcaoEdicao(url.concluirEdicaoUsuario, usuario, tela);
    };
    var desativarUsuario = function (cpf, telaAnterior) {
        var usuario = { Cpf: cpf, telaAnterior: telaAnterior };
        chamaPaginaComIdentificador(url.desativarUsuario, usuario);
    };
    var desativarUsuarioConfirmado = function(cpf) {
        var usuario = { Cpf: cpf };
        concluirAcaoEdicao(url.desativarUsuarioConfirmado, usuario, url.listarUsuarioInativo);
    };
    var listarUsuarioInativo = function() {
        chamaPagina(url.listarUsuarioInativo);
    };
    var listarUsuarioEmDivida = function() {
        chamaPagina(url.listarUsuarioEmDivida);
    };
    var listarUsuarioPorNome = function() {
        var usuario = { Nome: $('#nomeUsuario').val() };
        chamaPaginaComIdentificador(url.listarUsuarioPorNome, usuario);
    };
    var logOff = function() {
        $.get(url.logOff).done(function(data) {
            $('body').slideUp(1000, function() {
                if (localStorage.getItem('listaProdutos') != null) {
                    localStorage.removeItem('listaProdutos');
                }
                $('body').hide().html(data).slideDown(1000, function() {
                    Materialize.toast("LogOff feito com sucesso", 4000);
                });
            });
        }).fail(function(xhr) {
            console.log(xhr.responseText);
        });
    };
    var trocarSenha = function () {
        var senhas = { NovaSenha: $('#novaSenha').val(), ConfirmaNovaSenha: $('#confirmaNovaSenha').val() };        
        $.post(url.trocarSenha, senhas)
            .done(function (message) {                
                Materialize.toast(message, 4000);                
            })
            .fail(function (xhr) {
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
        logOff: logOff,
        trocarSenha: trocarSenha
    }
})(jQuery);