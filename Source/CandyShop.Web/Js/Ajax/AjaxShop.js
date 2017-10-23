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
            $("body").slideUp(function () {
                $("body").hide().html(data).slideDown();
            });
        }).fail(function () {
            Materialize.Toast("Erro ao ir para administração, contate um desenvolvedor");
        });
    };

    var loja = function () {
        $.get(url.loja).done(function (data) {
            $("body").slideUp(function () {
                $("body").hide().html(data).slideDown();
            });
        }).fail(function () {
            Materialize.Toast("Erro ao ir para loja, contate um desenvolvedor");
        });
    };

    var verificaLogin = function () {
        var usuario = { Cpf: $("#cpf").val(), SenhaUsuario: $("#senha").val() };
        $.post(url.verificaLogin, usuario)
            .done(function (res) {
                $.get(url.padrao)
                    .done(function (data) {
                        var texto = /Logado com sucesso/;
                        if (texto.test(res)) {
                            var cpf = res.replace("Logado com sucesso", "");

                            var informacoesAutorizacao = {
                                grant_type: "password",
                                username: cpf,
                                password: "password"
                            };
                            var queryString = jQuery.param(informacoesAutorizacao);

                            $.ajax({
                                type: "POST",
                                url: "http://localhost:4000/Token",
                                data: queryString,
                                dataType: "text",
                                contentType: "application/x-www-form-urlencoded; charset=utf-8",
                                xhrFields: {
                                    withCredentials: true
                                },
                                success: function (result) {
                                    var token = result;
                                    localStorage.setItem("tokenCandyShop", token);
                                },
                                error: function (req, status, error) {
                                    Materialize.toast(error, 3000);
                                }
                            });
                        } else
                            res = "Você não tem acesso aos recursos do sistema, contate os administradores";

                        $("body").slideUp("slow", function () {
                            $("body").hide().html(data).slideDown(1000, function () {
                                Materialize.toast(res.substr(0,18), 3000);
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
                $("body").slideUp(1000, function () {
                    $("body").hide().html(data).slideDown(1000);
                });
            }).fail(function (xhr) {
                Materialize.toast(xhr.responseText, 4000);
            });
    };

    var listarProdutoPorNome = function (nome) {
        var produto = { Nome: nome };
        chamaPaginaComIdentificador(url.listarProdutoPorNome, produto);
    };

    var listaCategoria = function (categoria) {
        chamaPaginaComIdentificador(url.listaCategoria, { categoria: categoria });
    };

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