// No dia 26/09/2017, Lenon Bordini errou
// não apague este comentário, é imprescindível ao funcionamento do código
// e do nosso ego e auto-estima
// (no fundo ele ainda tem coração, mas gosta de pinto).

var imagem, preco, nome, imagem, quantidade = 0,
    quantidadeDisponivel, Id, totalCompra = 0;

$(document).ready(function () {
    //pesquisa por nome de produto é feita quando se aperta a tecla "enter" na barra de pesquisa
    $(".input-field #search").keydown(function (e) {
        if (e.which === 13) {
            AjaxJsShop.listarProdutoPorNome($(this).val());
            $(this).val("");
        }
    });

    // Fechando sideNav quando o usuário selecionar alguma opção
    $(".closeMenu").on("click", function () {
        $(".button-collapse").sideNav("hide");
    });

    // Limpando os inputs
    $(".modal-close").not($("#editarQuantidade")).click(function () {
        $("#quantidade, #quantidadeEdit, #novaSenha, #confirmaNovaSenha, #cpf, #senha").val("");
        $("#novaSenha").removeAttr("disabled");
        $("#logar").attr("disabled", "disabled");
        $("#TrocarSenha").attr("disabled", "disabled");
    });

    /* Quando o botão de adicionar um item no carrinho é pressionado, as variáveis que montarão
       o objeto do produto são preenchidas */
    $("#DivGrid").on("click", ".btn-floating", function () {
        preco = $(this).attr("data-Preco");
        nome = $(this).attr("data-Nome");
        imagem = $(this).attr("data-Imagem");
        Id = $(this).attr("data-Id");
        quantidadeDisponivel = $(this).attr("data-quantidadeDisponivel");
    });

    // Verificando senhas e chamando ajax pra efetivar alteracoes    
    var ilegais = /[\W_]/;

    $("#novaSenha").on("paste", function () {
        if ($(this).val().length > 12 || $(this).val().length <= 0 || ilegais.test($(this).val())) {
            Materialize.toast("Senha deve conter de 8 a 12 caracteres!", 3000);
            $(this).focus();
        }
    });

    $("#novaSenha").blur(function () {
        if ($(this).val().length > 12 || $(this).val().length <= 0 || ilegais.test($(this).val())) {
            $(this).focus();
        }
    });

    // Verificando se as senhas batem
    $("#confirmaNovaSenha").blur(function () {
        if ($(this).val() == $("#novaSenha").val() && $(this).val() !== "" && $("#confirmaNovaSenha").val() !== "")
            $("#TrocarSenha").removeAttr("disabled");
        else 
            $("#TrocarSenha").attr("disabled","disabled");
    });

    $("#confirmaNovaSenha").keyup(function () {
        if ($(this).val() == $("#novaSenha").val() && $(this).val() !== "" && $("#confirmaNovaSenha").val() !== "")
            $("#TrocarSenha").removeAttr("disabled");
        else {
            $("#TrocarSenha").attr("disabled", "disabled");
        }
    });

    $("#novaSenha").keyup(function () {
        if ($(this).val() == $("#confirmaNovaSenha").val() && $(this).val() !== "" && $("#confirmaNovaSenha").val() !== "")
            $("#TrocarSenha").removeAttr("disabled");
        else 
            $("#TrocarSenha").attr("disabled", "disabled");
    });

    $("#TrocarSenha").click(function () {
        if ($("#novaSenha").val().length < 8) {
            Materialize.toast("Senha deve conter de 8 a 12 caracteres", 3000);
            return;
        }
        AjaxJsUsuario.trocarSenha();
        $("#confirmaNovaSenha").val("");
        $("#novaSenha").val("");
        $("#trocaSenha").modal("close");
    });

    $("#confirmaNovaSenha").keydown(function (e) {
        if (e.which == 13) {
            if ($("#novaSenha").val().length < 8) {
                Materialize.toast("Senha deve conter de 8 a 12 caracteres", 3000);
                $("#trocaSenha").modal("open");
                $("#confirmaNovaSenha").focus();
                return;
            }
            AjaxJsUsuario.trocarSenha();
            $("#confirmaNovaSenha").val("");
            $("#novaSenha").val("");
            $("#trocaSenha").modal("close");
            $("#novaSenha").removeAttr("disabled");
        }
    });

    // Adicionando os itens do localstorage no carrinho
    if (localStorage.getItem("listaProdutos") !== null) {
        $(".collection h1").remove();
        var i = 1;
        JSON.parse(localStorage.getItem("listaProdutos")).forEach(function (produto) {
            $(".collection").append($("<li>", {
                html: [
                    $("<img>", {
                        src: produto.Imagem,
                        "class": "circle",
                        style: "max-width:100px;margin-top:-1.1%"
                    }),
                    $("<span>", {
                        html: produto.Nome,
                        "class": "title",
                        "data-Id": produto.Id
                    }),
                    $("<p>", {
                        html: "Quantidade: " + produto.Quantidade,
                        "data-Quantidade": produto.Quantidade
                    }),
                    $("<a>", {
                        href: "#!",
                        "class": "secondary-content",
                        title: "Remover item",
                        style: "margin-top:10px",
                        html: [
                            $("<i>", {
                                html: "delete",
                                "class": "small material-icons",
                                "id": i
                            }).click(function () {
                                var li = $(this).closest("li");
                                var listaProdutos = localStorage.getItem("listaProdutos") ? JSON.parse(localStorage.listaProdutos) : [];
                                listaProdutos = listaProdutos.filter(function (item) {
                                    return item.Id !== li.find("[data-Id]").attr("data-Id");
                                });
                                localStorage.removeItem("listaProdutos");
                                localStorage.setItem("listaProdutos", JSON.stringify(listaProdutos));
                                li.remove();
                                // Recalculando o total
                                totalCompra = 0;
                                listaProdutos.forEach(function (produto) {
                                    var precoCorreto = produto.Preco.replace(",", ".");
                                    totalCompra += parseFloat(precoCorreto) * produto.Quantidade;
                                });
                                $("#totalCompra").text("R$ " + totalCompra);
                            })
                        ]
                    }),
                    $("<a>", {
                        href: "#modalEditarQuantidade",
                        "class": "modal-trigger modal-close secondary-content iconeEditar",
                        title: "Editar quantidade",
                        "data-quantidadeDisponivel": produto.QuantidadeDisponivel,
                        style: "margin-right:34px;margin-top:10px",
                        html: [
                            $("<i>", {
                                html: "mode_edit",
                                "class": "material-icons",
                                style: "font-size:30px !important"
                            }).click(function () {
                                $("#modalEditarQuantidade").data("index", $(this).closest("li").index());
                            })
                        ]
                    })
                ],
                "class": "collection-item avatar"
            }));
            i++;
            var precoCorreto = produto.Preco.replace(",", ".");
            totalCompra += parseInt(produto.Quantidade) * parseFloat(precoCorreto);
        });
        $("#totalCompra").text("R$ " + parseFloat(totalCompra)).attr('title', 'Total da compra');
    }

    // Adicionando os itens no carrinho
    $("#adicionaCarrinho").click(function () {
        $("#confirmarCompra").removeAttr("disabled");
        var i = 1;

        var listaProdutos = localStorage.getItem("listaProdutos") ? JSON.parse(localStorage.getItem("listaProdutos")) : [];

        var produto = {
            Id: Id,
            Nome: nome,
            Quantidade: quantidade,
            Imagem: imagem,
            Preco: preco,
            QuantidadeDisponivel: quantidadeDisponivel
        };

        if (listaProdutos.filter(function (v) { return v.Id == produto.Id; }).length) {
            Materialize.toast("Produto já esta no carrinho", 2000);
            return;
        } else
            listaProdutos.push(produto);

        // Removendo a mensagem de carrinho vazio
        $(".collection h1").remove();

        $(".collection").append($("<li>", {
            html: [
                $("<img>", { src: imagem, "class": "circle", style: "max-width:100px;margin-top:-1.1%" }),
                $("<span>", {
                    html: nome,
                    "class": "title",
                    "data-Id": Id
                }),
                $("<p>", {
                    html: "Quantidade: " + quantidade,
                    "data-Quantidade": quantidade
                }),
                $("<a>", {
                    href: "#!",
                    "class": "secondary-content",
                    style: "margin-top:10px",
                    html: [
                        $("<i>", {
                            html: "delete",
                            "class": "small material-icons"
                        }).click(function () {
                            var li = $(this).closest("li");
                            var listaProdutos = localStorage.getItem("listaProdutos") ? JSON.parse(localStorage.getItem("listaProdutos")) : [];
                            listaProdutos = listaProdutos.filter(function (item) {
                                return item.Id !== li.find("[data-Id]").attr("data-Id");
                            });
                            localStorage.removeItem("listaProdutos");
                            localStorage.setItem("listaProdutos", JSON.stringify(listaProdutos));
                            li.remove();

                            // Recalculando o total
                            totalCompra = 0;
                            listaProdutos.forEach(function (produto) {
                                var precoCorreto = produto.Preco.replace(",", ".");
                                totalCompra += parseFloat(precoCorreto) * produto.Quantidade;
                            });
                            $("#totalCompra").text("R$ " + totalCompra);
                        })
                    ]
                }),
                $("<a>", {
                    href: "#modalEditarQuantidade",
                    "class": "modal-trigger modal-close secondary-content iconeEditar",
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

        /* Edita total compra
           Como o valor vem na lista do localStorage com "," ao invés de ".", a a troca deve ser feita
           para que a conversão para double funcione */
        var precoConcertado = preco.replace(",", ".");
        totalCompra += parseInt(quantidade) * parseFloat(precoConcertado);
        $("#totalCompra").text("R$ " + totalCompra).attr('title', 'Total da compra');

        // Adiciona item, remove localStorage e seta de novo com a lista atualizada
        quantidade = 0;
        $("#adicionaCarrinho").attr("disabled", "");
        localStorage.removeItem("listaProdutos");
        localStorage.setItem("listaProdutos", JSON.stringify(listaProdutos));
    });

    // Desabilita o botão de confirmar compra ao abrir o carrinho
    $("a[href='#modalCarrinho'], #modalCarrinho").click(function () {
        if ($(".collection li").length > 0) {
            $("#confirmarCompra").removeAttr("disabled");
            $("#limpar").removeAttr("disabled");
        } else {
            $("#confirmarCompra").attr("disabled", "disabled");
            $("#limpar").attr("disabled", "disabled");
            if ($(".collection h1").length == 0)
                $(".collection").append($("<h1>", {
                    html: "Carrinho vazio",
                    style: "margin-top:20px"
                }));
        }
    });

    $("#modalLogin").modal({
        dismissible: false,
        ready: function () {
            $("#cpf").focus();
        }
    });

    // Colocando foco no modal quantidade
    $("#modalQuantidade").modal({
        dismissible: false,
        ready: function () {$("#quantidade").focus();}
    });

    $("#modalCarrinho").modal({dismissible: false});

    // Colocando foco no modal trocaSenha
    $("#trocaSenha").modal({
        dismissible: false,
        ready: function () {
            $("#novaSenha").focus();
        }
    });

    $("#modalEditarQuantidade").modal({
        dismissible: false,
        ready: function () {
            $("#quantidadeEdit").focus();
        }
    });

    // Limpando input senha
    $("a[href='#modalLogin']:eq(1)").click(function () {
        $("#senha").val("");
    });

    //desabilitar botao de login se campo de cpf estiver vazio
    $("#senha").blur(function () {
        if ($(this).val().length > 0 && $("#cpf").val().length > 13) 
            $("#logar").removeAttr("disabled");
        else
            $("#logar").attr("disabled","disabled");
    });

    $("#senha").keyup(function () {
        if ($(this).val().length > 0 && $("#cpf").val().length > 13) 
            $("#logar").removeAttr("disabled");
        else
            $("#logar").attr("disabled", "disabled");
    });

    $("#cpf").blur(function () {
        if ($(this).val().length > 13 && $("#senha").val().length > 0) 
            $("#logar").removeAttr("disabled");
        else
            $("#logar").attr("disabled", "disabled");
    });

    $("#cpf").keyup(function () {
        if ($(this).val().length > 13 && $("#senha").val().length > 0) 
            $("#logar").removeAttr("disabled");
        else
            $("#logar").attr("disabled", "disabled");
    });

    var verifyInt = /^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ]+$/;
    $("#quantidade").keyup(function () {
        FilterInput(event);
        quantidade = $("#quantidade").val();
        if (parseInt(quantidade) <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || parseInt(quantidade) > quantidadeDisponivel)
            $("#adicionaCarrinho").attr("disabled", "disabled");
        else
            $("#adicionaCarrinho").removeAttr("disabled");
    });

    $("#quantidade, #quantidadeEdit").keydown(function (e) {
        var tamanho = $(this).val().length;
        if (tamanho > 2 && e.which !== 8) {
            e.preventDefault();
            return false;
        }
    });

    // Foco saindo do input
    $("#quantidade").blur(function () {
        quantidade = $("#quantidade").val();
        if (parseInt(quantidade) <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || parseInt(quantidade) > quantidadeDisponivel)
            $("#adicionaCarrinho").attr("disabled", "disabled");
        else
            $("#adicionaCarrinho").removeAttr("disabled");
    });

    // Texto colado no input
    $("#quantidade").on("paste", function () {
        quantidade = $("#quantidade").val();
        handlePaste(event);
        if (quantidade <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || quantidade > quantidadeDisponivel)
            $("#adicionaCarrinho").attr("disabled", "disabled");
        else
            $("#adicionaCarrinho").removeAttr("disabled");
    });

    // Onclick dentro da modal
    $("#modalQuantidade").click(function () {
        quantidade = $("#quantidade").val();
        if (parseInt(quantidade) <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || parseInt(quantidade) > quantidadeDisponivel)
            $("#adicionaCarrinho").attr("disabled", "disabled");
        else
            $("#adicionaCarrinho").removeAttr("disabled");
    });

    // Editando a quantidade dos itens no carrinho
    $("#editarQuantidade").click(function () {
        $("#modalCarrinho .collection li:eq(" + $("#modalEditarQuantidade").data("index") + ") p")
            .text("Quantidade: " + $("#quantidadeEdit").val())
            .attr("data-Quantidade", $("#quantidadeEdit").val());
        $("#modalCarrinho").modal("open");

        // Atualizando a quantidade no localStorage
        var produtos = JSON.parse(localStorage.getItem("listaProdutos"));
        var produto = produtos[$("#modalEditarQuantidade").data("index")];
        produto.Quantidade = $("#quantidadeEdit").val();
        produtos[$("#modalEditarQuantidade").data("index")] = produto;
        localStorage.removeItem("listaProdutos");
        localStorage.setItem("listaProdutos", JSON.stringify(produtos));

        // Calculando o novo total da compra e exibindo na tela
        totalCompra = 0;
        produtos.forEach(function (produto) {
            var precoCorreto = produto.Preco.replace(",", ".");
            totalCompra += parseFloat(precoCorreto) * produto.Quantidade;
        });
        $("#totalCompra").text("R$ " + totalCompra);
    });

    // Desabilitando no modal de editar a quantidade
    $("#quantidadeEdit").blur(function () {
        quantidade = $("#quantidadeEdit").val();
        if (quantidade <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || quantidade > quantidadeDisponivel)
            $("#editarQuantidade").attr("disabled", "disabled");
        else
            $("#editarQuantidade").removeAttr("disabled");
    });

    $("#quantidadeEdit").keyup(function () {
        FilterInput(event);
        quantidade = $("#quantidadeEdit").val();
        if (parseInt(quantidade) <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || parseInt(quantidade) > quantidadeDisponivel)
            $("#editarQuantidade").attr("disabled", "disabled");
        else
            $("#editarQuantidade").removeAttr("disabled");
    });

    $("#quantidadeEdit").on("paste", function () {
        handlePaste(event);
        quantidade = $("#quantidadeEdit").val();
        if (quantidade <= 0 || quantidade == null || quantidade == "" || quantidade == "undefined" || quantidade.match(verifyInt) || quantidade > quantidadeDisponivel) {
            $(".QtdeInvalida").errorMessage("Quantidade deve ser maior que zero!", 5000);
            $("#editarQuantidade").attr("disabled", "disabled");
        } else
            $("#editarQuantidade").removeAttr("disabled");
    });

    // Removendo caracteres inválidos dos campos de quantidade
    $("#quantidade, #quantidadeEdit").keydown(function () {
        mNumbers($(this).val());
    });

    $("#quantidade, #quantidadeEdit").on("paste", function () {
        mNumbers($(this).val());
    });

    // Limpando o carrinho
    $("#limpar").click(function () {
        $(".collection li").remove();

        // Adicionando a mensagem de carrinho vazio novamente
        if ($(".collection h1").length == 0)
            $(".collection").append($("<h1>", {
                html: "Carrinho vazio",
                style: "margin-top:20px"
            }));

        // Se o localStorage existir ele será apagado aqui
        if (localStorage.getItem("listaProdutos") !== null) {
            localStorage.removeItem("listaProdutos");
        }

        // Atualizando informações na tela
        totalCompra = 0;
        $("#totalCompra").text("R$ " + totalCompra);
    });

    // Exibindo a quantidade atualmente disponível quando o usuário vai editar o item
    $("body").on("click", ".iconeEditar", function () {
        quantidadeDisponivel = $(this).attr("data-quantidadedisponivel");
        $("#quantidadeEdit").val("");
        // Exibindo a quantidade disponível em estoque no modal de edição do carrinho
        $("#estoqueEdit").html("Quantidade disponível : " + $(this).attr("data-quantidadedisponivel") + " itens no estoque");
    });

    /* Função que é executada quando a tecla enter é pressionada no modal de quantidade 
       validando se o valor digitado pode ser inserido no carrinho */
    $("#quantidade").keydown(function (e) {
        if (e.which === 13) {
            mNumbers($("#quantidade").val());
            var qtde = $("#quantidade").val();
            if (parseInt(qtde) <= quantidadeDisponivel && qtde > 0) {
                $("#adicionaCarrinho").trigger("click");
                $("#modalQuantidade").modal("close");
                $("#modalCarrinho").modal("open");
            } else {
                Materialize.toast("Quantidade indisponível para compra!", 2000);
                return;
            }
        }
    });

    $("#quantidadeEdit").keydown(function (e) {
        if (e.which === 13) {
            mNumbers($("#quantidadeEdit").val());
            var qtde = $("#quantidadeEdit").val();
            if (qtde <= quantidadeDisponivel && qtde > 0) {
                $("#modalEditarQuantidade").modal("close");
                $("#editarQuantidade").trigger("click");
            } else {
                Materialize.toast("Quantidade indisponível para compra!", 2000);
                return;
            }
        }
    });

    //validando campo de CPF
    $("#cpf").keydown(function (e) {
        mcpf($("#cpf").val());
        if (e.which == 13)
            $("#senha").focus();
    });

    $("#cpf").on("blur", function () {
        if ($("#cpf").val().length > 14) {
            $("#cpf").val($("#cpf").val().substr(0, 13));
        }
    });

    $("#senha").keydown(function (e) {
        if (e.which == 13)
            AjaxJsShop.verificaLogin();
    });

    $("#novaSenha").keydown(function (e) {
        if (e.which == 13)
            $("#confirmaNovaSenha").focus();
    });
});

// Função que remove caracteres que não sejam numéricos
function mNumbers(v) {
    v = v.replace(/\D/g, "");
    $("#quantidade, #quantidadeEdit").val(v);
}

function FilterInput(event) {
    var keyCode = ("which" in event) ? event.which : event.keyCode;
    var isNotWanted = (keyCode === 69 || keyCode === 189 || keyCode === 109 || keyCode == 190);
    return !isNotWanted;
}

function handlePaste(e) {
    var clipboardData = e.clipboardData || window.clipboardData;
    var pastedData = clipboardData.getData("Text").toUpperCase();

    if (pastedData.indexOf("E") > -1) {
        e.stopPropagation();
        e.preventDefault();
    }

    if (pastedData.indexOf("-") > -1) {
        e.stopPropagation();
        e.preventDefault();
    }

    if (pastedData.indexOf(".") > -1) {
        e.stopPropagation();
        e.preventDefault();
    }
}

function mcpf(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    $("#cpf").val(v);
}

