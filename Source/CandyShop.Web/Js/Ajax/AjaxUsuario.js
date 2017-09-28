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
    var verificaLogin = function (callback) {
        var usuario = { Cpf: $('#cpf').val(), SenhaUsuario: $('#senha').val() };
        $.post(url.verificaLogin, usuario)
            .done(function (res) {
                if (res !== "1")
                    $.get(url.padrao)
                        .done(function (data) {
                            $('body').slideUp(function () {
                                $('body').hide().html(data).slideDown(function () {
                                    Materialize.toast("Login feito com sucesso!", 4000);
                                    if (callback === "function")
                                        callback();
                                });
                            });
                        }).fail(function (xhr) {
                            Materialize.toast(xhr.responseText, 4000);
                        });
                else
                    Materialize.toast("Login Incorreto", 4000);

            })
            .fail(function (xhr) {
                Materialize.toast(xhr.responseText, 3000);
            });
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
        verificaLogin: verificaLogin,
        logOff: logOff
    }
})(jQuery);