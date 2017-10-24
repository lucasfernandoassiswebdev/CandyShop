var obj;
var AjaxJsUsuario = (function ($) {
    var url = {};

    var init = function (config) {
        url = config;
    };

    var cadastroUsuario = function () {
        chamaPaginaUsuarios(url.cadastroUsuario);
    };
    var listaUsuario = function () {
        chamaPaginaUsuarios(url.listaUsuario);
    };
    var editarUsuario = function (cpf, telaAnterior) {
        var usuario = { Cpf: cpf, telaAnterior: telaAnterior };
        chamaPaginaComIdentificador(url.editarUsuario, usuario);
    };
    var detalheUsuario = function (cpf, telaAnterior) {
        var usuario = { Cpf: cpf, telaAnterior: telaAnterior };
        chamaPaginaComIdentificador(url.detalheUsuario, usuario);
    };
    var concluirCadastroUsuario = function (imgBase64) {
        //montantando o objeto que vai chegar no controller
        var usuario = {
            Cpf: $("#cpf").val(),
            NomeUsuario: $("#Nome").val(),
            Imagem: imgBase64,
            Classificacao: $("#Classificacao").val()
        };
        atualizaToken();
        concluirAcao(url.concluirCadastroUsuario, { usuario: usuario, token: obj.access_token }, url.cadastroUsuario);
    };
    var concluirEdicaoUsuario = function (imgBase64, removerImagem, tela) {
        var usuario = {
            Cpf: $("#Cpf").val(),
            NomeUsuario: $("#Nome").val(),
            SaldoUsuario: $("#SaldoUsuario").val().replace("R$ ", "").replace(".", ""),
            SenhaUsuario: $("#Password").val(),
            Ativo: $("#Ativo").val(),
            Imagem: imgBase64,
            Classificacao: $("#Classificacao").val(),
            RemoverImagem: removerImagem
        };
        atualizaToken();
        concluirAcaoEdicaoUsuario(url.concluirEdicaoUsuario, { usuario: usuario, token: obj.access_token }, tela);
    };
    var desativarUsuario = function (cpf, telaAnterior) {
        var usuario = { Cpf: cpf, telaAnterior: telaAnterior };
        atualizaToken();
        chamaPaginaComIdentificador(url.desativarUsuario, usuario);
    };
    var desativarUsuarioConfirmado = function (cpf) {
        var usuario = { Cpf: cpf };
        atualizaToken();
        concluirAcaoEdicaoUsuario(url.desativarUsuarioConfirmado, { usuario: usuario, token: obj.access_token }, url.listarUsuarioInativo);
    };
    var listarUsuarioInativo = function () {
        chamaPaginaUsuarios(url.listarUsuarioInativo);
    };
    var listarUsuarioEmDivida = function () {
        chamaPaginaUsuarios(url.listarUsuarioEmDivida);
    };
    var listarUsuarioPorNome = function () {
        var usuario = { Nome: $("#nomeUsuario").val() };
        chamaPaginaComIdentificador(url.listarUsuarioPorNome, usuario);
    };
    var logOff = function () {
        $.get(url.logOff).done(function (data) {
            $("body").slideUp(1000, function () {
                if (localStorage.getItem("listaProdutos") != null) {
                    localStorage.removeItem("listaProdutos");
                }
                $("body").hide().html(data).slideDown(1000, function () {
                    Materialize.toast("LogOff feito com sucesso", 4000);
                });
            });
        }).fail(function (xhr) {
            console.log(xhr.responseText);
        });
    };
    var trocarSenha = function () {
        var senhas = { NovaSenha: $("#novaSenha").val(), ConfirmaNovaSenha: $("#confirmaNovaSenha").val() };
        console.log(senhas.ConfirmaNovaSenha);
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
    };
})(jQuery);


function chamaPaginaUsuarios(endereco) {
    atualizaToken();
    $.ajax({
        url: endereco,
        type: "GET",
        data: {
            token: obj.access_token
        },
        success: function (dataSucess) {
            $("#DivGrid").slideUp(function () {
                $("#DivGrid").hide().html(dataSucess).slideDown(function() {
                    Materialize.toast(dataSucess.data,3000);
                });
            });
        },
        error: function (xhr) {
            Materialize.toast("Você não está autorizado, contate os administradores", 3000);
            console.log(xhr.responseText);
        }
    });
}

function concluirAcaoEdicaoUsuario(endereco, objeto, tela) {
    $.ajax({
        url: endereco,
        type: "POST",
        data: objeto,
        success: function (message) {
            chamaPaginaUsuarios(tela);
            Materialize.toast(message, 4000);
        }
    });
}

function atualizaToken() {
    obj = localStorage.getItem("tokenCandyShop") ? JSON.parse(localStorage.getItem("tokenCandyShop")) : [];
    if (obj == [])
        Materialize.modal("Há algo de errado com suas validações", 2000);
}