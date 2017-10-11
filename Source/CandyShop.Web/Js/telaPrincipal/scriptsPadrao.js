//no dia 26/09/2017, Lenon Bordini errou
//não apague este comentário, é imprescindível ao funcionamento do código
//e do nosso ego e auto-estima
//(no fundo ele ainda tem coração, mas gosta de pinto)

var imagem, preco, nome, imagem, quantidade = 0, quantidadeDisponivel, Id;

$(document).ready(function () {
    //pesquisa por nome é feita quando se aperta a tecla "enter" na barra de pesquisa
    $("#search").keydown(function (e) {
        if (e.which === 13) {
            AjaxJsShop.listarProdutoPorNome($("#search").val());
        }
    });

    //limpando os inputs
    $(".modal-close").not($("#editarQuantidade")).click(function () {
        $("#quantidade, #quantidadeEdit, #novaSenha, #confirmaNovaSenha").val("");
        $("#novaSenha").removeAttr("disabled");
    });

    $(".tooltipped").tooltip({ delay: 50 });
    $(".button-collapse").sideNav();

    $("#DivGrid").on("click", ".btn-floating", function () {
        preco = $(this).attr("data-Preco");
        nome = $(this).attr("data-Nome");
        imagem = $(this).attr("data-Imagem");
        Id = $(this).attr("data-Id");
        quantidadeDisponivel = $(this).attr("data-quantidadeDisponivel");
    });

    //verificando senhas e chamando ajax pra efetivar alteracoes    
    var ilegais = /[\W_]/;

    $("#novaSenha").on("blur", function () {
        if ($(this).val().length > 12) {
            Materialize.toast("Senha deve conter de 8 a 12 caracteres!", 3000);
            $(this).focus();
        }
        if (ilegais.test($(this).val())) {
            Materialize.toast("Digite apenas letras e numeros!", 3000);
            $(this).focus();
        }
    });

    $("#confirmaNovaSenha").blur(function () {
        if ($(this).val() == $("#novaSenha").val()) {
            $("#TrocarSenha").removeAttr("disabled");
        } else {
            $("#TrocarSenha").attr("disabled", "disabled");
            Materialize.toast("As senhas não conferem!", 5000);
        }
        if ($(this).val() == "") {
            $("#novaSenha").removeAttr("disabled");
        }
    });

    $("#TrocarSenha").click(function () {
        if ($("#novaSenha").val().length < 8) {
            Materialize.toast("Senha deve conter de 8 a 12 caracteres", 3000);
            return;
        }
        AjaxJsUsuario.trocarSenha();
        $("#trocaSenha").modal("close");
        $("#novaSenha").removeAttr("disabled");
        $(this).Attr("disabled", "disabled");
    });

    //adicionando os itens do localstorage no carrinho
    if (localStorage.getItem("listaProdutos") !== null) {
        $(".collection h1").remove();
        var i = 1;
        JSON.parse(localStorage.getItem("listaProdutos")).forEach(function (produto) {
            $(".collection").append($("<li>", {
                html: [
                    $("<img>",
                        {
                            src: produto.Imagem,
                            "class": "circle",
                            style: "max-width:100px;margin-top:-1.1%"
                        }),
                    $("<span>",
                        {
                            html: produto.Nome,
                            "class": "title",
                            "data-Id": produto.Id
                        }),
                    $("<p>",
                        {
                            html: "Quantidade: " + produto.Quantidade,
                            "data-Quantidade": produto.Quantidade
                        }),
                    $("<a>", {
                        href: "#!",
                        "class": "modal-trigger secondary-content",
                        style: "margin-top:10px",
                        html: [
                            $("<i>", {
                                html: "delete",
                                "class": "small material-icons",
                                "id": i
                            }).click(function () {
                                //$("#modalQuantidade").data("index", $(this).closest("li").index());                                
                                var li = $(this).closest("li");
                                var listaProdutos = localStorage.getItem("listaProdutos") ? JSON.parse(localStorage.listaProdutos) : [];
                                listaProdutos = listaProdutos.filter(function (item) {
                                    return item.Id !== li.find("[data-Id]").attr("data-Id");
                                });
                                localStorage.removeItem("listaProdutos");
                                localStorage.setItem("listaProdutos", JSON.stringify(listaProdutos));
                                li.remove();
                            })
                        ]
                    }),
                    $("<a>", {
                        href: "#modalEditarQuantidade",
                        "class": "modal-trigger modal-close secondary-content",
                        "data-quantidadeDisponivel": produto.QuantidadeDisponivel,
                        style: "margin-right:34px;margin-top:10px",
                        html: [
                            $("<i>", {
                                html: "mode_edit",
                                "class": "material-icons",
                                style: "font-size:30px !important"
                            }).on("click", function () {
                                $("#modalEditarQuantidade").data("index", $(this).closest("li").index());
                            })
                        ]
                    })
                ],
                "class": "collection-item avatar"
            }));
            i++;
        });
    }

    //adicionando os itens no carrinho
    $("#adicionaCarrinho").click(function () {
        $("#confirmarCompra").removeAttr("disabled");
        var i = 1;

        var listaProdutos = localStorage.getItem("listaProdutos") ? JSON.parse(localStorage.listaProdutos) : [];

        var produto = {
            Id: Id,
            Nome: nome,
            Quantidade: quantidade,
            Imagem: imagem
        }

        if (listaProdutos.filter(function (v) { return v.Id == produto.Id }).length) {
            Materialize.toast("Produto já esta no carrinho");
            return;
        }
        else
            listaProdutos.push(produto);

        //removendo a mensagem de carrinho vazio
        $(".collection h1").remove();

        //adicionando o novo item no carrinho
        $(".collection").append($("<li>",
            {
                html: [
                    $("<img>", { src: imagem, "class": "circle", style: "max-width:100px;margin-top:-1.1%" }),
                    $("<span>",
                        {
                            html: nome,
                            "class": "title",
                            "data-Id": Id
                        }),
                    $("<p>",
                        {
                            html: "Quantidade: " + quantidade,
                            "data-Quantidade": quantidade
                        }),
                    $("<a>",
                        {
                            href: "#!",
                            "class": "modal-trigger secondary-content",
                            style: "margin-top:10px",
                            html: [
                                $("<i>",
                                    {
                                        html: "delete",
                                        "class": "small material-icons"
                                    }).on("click", function () {
                                        var li = $(this).closest("li");
                                        var listaProdutos = localStorage.getItem("listaProdutos") ? JSON.parse(localStorage.listaProdutos) : [];
                                        listaProdutos = listaProdutos.filter(function (item) {
                                            return item.Id !== li.find("[data-Id]").attr("data-Id");
                                        });
                                        localStorage.removeItem("listaProdutos");
                                        localStorage.setItem("listaProdutos", JSON.stringify(listaProdutos));
                                        li.remove();
                                    })
                            ]
                        }),
                    $("<a>", {
                        href: "#modalEditarQuantidade",
                        "class": "modal-trigger modal-close secondary-content",
                        "data-quantidadeDisponivel": quantidadeDisponivel,
                        style: "margin-right:34px;margin-top:10px",
                        html: [
                            $("<i>", {
                                html: "mode_edit",
                                "class": "material-icons",
                                style: "font-size:30px !important"
                            }).on("click", function () {
                                $("#modalEditarQuantidade").data("index", $(this).closest("li").index());
                            })
                        ]
                    })
                ],
                "class": "collection-item avatar"
            }));
        i++;

        quantidade = 0;
        $("#adicionaCarrinho").attr("disabled", "");
        localStorage.removeItem("listaProdutos");
        localStorage.setItem("listaProdutos", JSON.stringify(listaProdutos));
    });

    //desabilita o botão de confirmar compra ao abrir o carrinho
    $("a[href='#modalCarrinho'], #modalCarrinho").click(function () {
        if ($(".collection li").length > 0) {
            $("#confirmarCompra").removeAttr("disabled");
            $("#limpar").removeAttr("disabled");
        }
        else {
            $("#confirmarCompra").attr("disabled", "disabled");
            $("#limpar").attr("disabled", "disabled");
            if ($(".collection h1").length == 0)
                $(".collection").append($("<h1>", {
                    html: "Carrinho vazio",
                    style: "margin-top:20px"
                }));
        }
    });

    //colocando foco no modal quantidade
    $("#modalQuantidade").modal({
        ready: function () {
            $("#quantidade").focus();
        }
    });

    //colocando foco no modal login
    $("#modalLogin").modal({
        ready: function () {
            $("#cpf").focus();
        }
    });

    //colocando foco no modal trocaSenha
    $("#trocaSenha").modal({
        ready: function () {
            $("#novaSenha").focus();
        }
    });

    //desabilitando botão quando houverem quantidades inválidas
    var verifyInt = /^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ]+$/;
    //tecla pressionada
    $("#quantidade").keyup(function () {
        quantidade = $("#quantidade").val();
        if (parseInt(quantidade) <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || parseInt(quantidade) > quantidadeDisponivel) {
            $("#adicionaCarrinho").attr("disabled", "disabled");
        } else {
            $("#adicionaCarrinho").removeAttr("disabled");
        }
    });

    //onclick dentro da modal
    $("#modalQuantidade").on("click", function () {
        quantidade = $("#quantidade").val();
        if (parseInt(quantidade) <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || parseInt(quantidade) > quantidadeDisponivel) {
            $("#adicionaCarrinho").attr("disabled", "disabled");
        } else {
            $("#adicionaCarrinho").removeAttr("disabled");
        }
    });

    //foco saindo do input
    $("#quantidade").blur(function () {
        quantidade = $("#quantidade").val();
        if (parseInt(quantidade) <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || parseInt(quantidade) > quantidadeDisponivel) {
            $("#adicionaCarrinho").attr("disabled", "disabled");
        } else {
            $("#adicionaCarrinho").removeAttr("disabled");
        }
    });
    //texto colado no input
    $("#quantidade").on("paste", function () {
        quantidade = $("#quantidade").val();
        if (quantidade <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || quantidade > quantidadeDisponivel) {
            $("#adicionaCarrinho").attr("disabled", "disabled");
        } else {
            $("#adicionaCarrinho").removeAttr("disabled");
        }
    });

    //editando a quantidade dos itens no carrinho
    $("#editarQuantidade").click(function () {
        $("#modalCarrinho .collection li:eq(" + $("#modalEditarQuantidade").data("index") + ") p")
            .text("Quantidade: " + $("#quantidadeEdit").val())
            .attr("data-Quantidade", $("#quantidadeEdit").val());
        $("#modalCarrinho").modal("open");

        var produtos = JSON.parse(localStorage.getItem("listaProdutos"));
        var produto = produtos[$("#modalEditarQuantidade").data("index")];
        console.log(produto);
        produto.Quantidade = $("#quantidadeEdit").val();
        produtos[$("#modalEditarQuantidade").data("index")] = produto;
        localStorage.removeItem("listaProdutos");
        localStorage.setItem("listaProdutos", JSON.stringify(produtos));
    });
    //desabilitando no modal de editar a quantidade
    $("#quantidadeEdit").on("blur", function () {
        quantidade = $("#quantidadeEdit").val();
        if (quantidade <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || quantidade > quantidadeDisponivel) {
            $("#editarQuantidade").attr("disabled", "disabled");
        } else {
            $("#editarQuantidade").removeAttr("disabled");
        }
    });
    $("#quantidadeEdit").keydown(function () {
        quantidade = $("#quantidadeEdit").val();
        if (quantidade <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || quantidade > quantidadeDisponivel) {
            $("#editarQuantidade").attr("disabled", "disabled");
        } else {
            $("#editarQuantidade").removeAttr("disabled");
        }
    });
    $("#quantidadeEdit").on("paste", function () {
        quantidade = $("#quantidadeEdit").val();
        if (quantidade <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || quantidade > quantidadeDisponivel) {
            $(".QtdeInvalida").errorMessage("Quantidade deve ser maior que zero!", 5000);
            $("#editarQuantidade").attr("disabled", "disabled");
        } else {
            $("#editarQuantidade").removeAttr("disabled");
        }
    });

    //tirando caracteres inválidos dos campos de quantidade
    $("#quantidade, #quantidadeEdit").keydown(function () {
        mNumbers($(this).val());
    });

    $("#quantidade, #quantidadeEdit").on("paste", function () {
        mNumbers($(this).val());
    });

    //limpando o carrinho
    $("#limpar").click(function () {
        $(".collection li").remove();

        //adicionando a mensagem de carrinho vazio novamente
        if ($(".collection h1").length == 0)
            $(".collection").append($("<h1>", {
                html: "Carrinho vazio",
                style: "margin-top:20px"
            }));

        if (localStorage.getItem("listaProdutos") != null) {
            localStorage.removeItem("listaProdutos");
            listaProdutos = [];
        }
    });
});

//função que remove caracteres que não sejam numéricos
function mNumbers(v) {
    v = v.replace(/\D/g, "");
    $("#quantidade, #quantidadeEdit").val(v);
}