var AjaxJsShop = (function ($) {
    var url = {};

    var init = function (config) {
        url = config;
        main(url.main);
    };

    var mostraSaldo = function () {
        chamaPagina(url.mostraSaldo);
    };

    var administracao = function () {
        $.get(url.administracao).done(function (data) {
            $('body').slideUp(function () {
                $('body').hide().html(data).slideDown();
            });
        }).fail(function () {
            Materialize.Toast("Erro ao ir para administração, contate um desenvolvedor");
        });
    };

    var loja = function() {
        $.get(url.loja).done(function(data) {
            $('body').slideUp(function() {
                $('body').hide().html(data).slideDown();
            });
        }).fail(function() {
            Materialize.Toast("Erro ao ir para loja, contate um desenvolvedor");
        });
    };

    var verificaLogin = function () {
        var usuario = { Cpf: $('#cpf').val(), SenhaUsuario: $('#senha').val() };
        $.post(url.verificaLogin, usuario)
            .done(function (res) {
                $.get(url.padrao)
                    .done(function (data) {
                        $('body').slideUp("slow", function () {
                            $('body').hide().html(data).slideDown(1000, function () {
                                if (res !== "1")
                                    Materialize.toast("Login feito com sucesso!", 4000);
                                else
                                    Materialize.toast("Login Incorreto!", 4000);
                            });
                        });
                    }).fail(function (xhr) {
                        Materialize.toast(xhr.responseText, 4000);
                        $("#modalLogin h2").text("login falhou");

                        setTimeout(function () {
                            $("#modalLogin").modal("open");
                        }, 800);
                    });
            })
            .fail(function (xhr) {
                Materialize.toast(xhr.responseText, 4000);
            });
    };

    var voltarInicio = function () {
        $.get(url.padrao)
            .done(function (data) {
                $('body').slideUp(1000, function () {
                    $('body').hide().html(data).slideDown(1000);
                });
            }).fail(function (xhr) {
                Materialize.toast(xhr.responseText, 4000);
            });
    };

    var listarProdutoPorNome = function (nome) {
        var produto = { Nome: nome };
        chamaPaginaComIdentificador(url.listarProdutoPorNome, produto);
    };

    var listaCategoria = function(categoria) {
        chamaPaginaComIdentificador(url.listaCategoria, { categoria: categoria });
    };

    //função que vai carregar o corpo inteiro da pagina    

    return {
        init: init,
        mostraSaldo: mostraSaldo,
        voltarInicio: voltarInicio,
        administracao: administracao,
        verificaLogin: verificaLogin,
        listarProdutoPorNome: listarProdutoPorNome,
        listaCategoria: listaCategoria,
        loja: loja
    };
})(jQuery);