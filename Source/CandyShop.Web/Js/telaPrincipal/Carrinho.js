var i = 1, preco, nome, imagem, Id, quantidadeDisponivel;

$(document).ready(function () {
    $("#DivGrid").on("click", ".adicionaItemCarrinho", function () {
        preco = $(this).attr("data-Preco");
        nome = $(this).attr("data-Nome");
        imagem = $(this).attr("data-Imagem");
        Id = $(this).attr("data-Id");
        quantidadeDisponivel = $(this).attr("data-quantidadedisponivel");
    });

    //--------------------Carrinho--------------------
    // Adicionando os itens no carrinho
    $(".adicionaItemCarrinho").click(function () {
        var listaProdutos = localStorage.getItem("listaProdutos") ? JSON.parse(localStorage.getItem("listaProdutos")) : [];

        var produto = {
            Id: Id,
            Nome: nome,
            Quantidade: 1,
            Imagem: imagem,
            Preco: preco,
            QuantidadeDisponivel: quantidadeDisponivel
        };
        console.log(produto);
        // Se o produto que está sendo adicionado ainda não está no carrinho, uma mensagem é exibida
        if (listaProdutos.filter(function (v) { return v.Id == produto.Id; }).length) {
            Materialize.toast("Produto já esta no carrinho", 2000);
            return;
        } else
            listaProdutos.push(produto);

        // Removendo a mensagem de carrinho vazio
        $(".collection h1").remove();

        // Escrevendo o produto no modal do carrinho
        $(".collection").append($("<li>", {
            html: [
                $("<img>", { src: imagem, "class": "circle", style: "max-width:100px;margin-top:-1.1%" }),
                $("<span>", { html: nome, "class": "title", "data-Id": Id }),
                $("<p>", { html: "Quantidade: " + quantidade, "data-Quantidade": quantidade }),
                $("<a>", {
                    href: "#!", "class": "secondary-content", style: "margin-top:10px",
                    html: [
                        $("<i>", { html: "delete", "class": "small material-icons" })
                            .click(function () {
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
                                // Após o item ter sido adicionado o total é atualizado e exibido
                                $("#totalCompra").text("Total da compra: R$ " + totalCompra);
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
                            html: "mode_edit", "class": "material-icons",
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

        // Produto inserido = preço atualizado
        if (String(totalCompra).length > 7)
            totalCompra = String(totalCompra).substr(7, totalCompra.length);
        $("#totalCompra").text("Total da compra: R$ " + String(totalCompra).replace(".", ",")).attr("title", "Total da compra");

        // Adiciona item, remove localStorage e seta de novo com a lista atualizada
        quantidade = 0;
        localStorage.removeItem("listaProdutos");
        localStorage.setItem("listaProdutos", JSON.stringify(listaProdutos));
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
                    $("<span>", { html: produto.Nome, "class": "title", "data-Id": produto.Id }),
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
                            $("<i>", { html: "delete", "class": "small material-icons", "id": i })
                                .click(function () {
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
                                        var precoCorreto = String(produto.Preco).replace(",", ".");
                                        totalCompra += parseFloat(precoCorreto) * parseInt(produto.Quantidade);
                                        console.log(totalCompra);
                                    });
                                    totalCompra = String(totalCompra).replace(".", ",");
                                    if (totalCompra.length > 6)
                                        totalCompra = String(totalCompra).substr(6, totalCompra.length);
                                    $("#totalCompra").text("Total da compra: R$ " + totalCompra).attr("title", "Total da compra");
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
                                html: "mode_edit", "class": "material-icons",
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
        if (totalCompra.length > 7)
            totalCompra = totalCompra.substr(7, totalCompra.length);
        $("#totalCompra").text("Total da compra: R$ " + String(totalCompra).replace(".", ",")).attr("title", "Total da compra");
    }
});

