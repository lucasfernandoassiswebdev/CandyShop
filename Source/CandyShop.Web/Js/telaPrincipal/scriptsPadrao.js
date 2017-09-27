//no dia 26/09/2017, Lenon Bordini errou
//não apague este comentário, é imprescindível ao funcionamento do código
//e do nosso ego e auto-estima
//(no fundo ele ainda tem coração, mas gosta de pinto)

var imagem, preco, nome, imagem, quantidade, Id;
var listaProdutos = [];

$(document).ready(function () {
    $(".tooltipped").tooltip({ delay: 50 });
    $(".button-collapse").sideNav();


    $("#DivGrid").on("click", ".btn-floating", function () {
        preco = $(this).attr("data-Preco");
        nome = $(this).attr("data-Nome");
        imagem = $(this).attr("data-Imagem");
        Id = $(this).attr("data-Id");
    });

    $("#confirmacao").click(function () {
        quantidade = $("#quantidade").val();
    });

    if (localStorage.getItem('listaProdutos') !== null) {
        JSON.parse(localStorage.getItem('listaProdutos')).forEach( function (produto) {
            $("div[class='collection']").append($("<li>", {
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
                        href: "#modal4",
                        'class': "modal-trigger secondary-content",
                        html: [
                            $("<i>", {
                                html: "mode_edit",
                                "class": "material-icons"
                            }).on("click", function () {
                                $("#modal4").data("index", $(this).closest("li").index());
                            })
                        ]
                    })
                ],
                class: "collection-item avatar"
            }));
        });
    }

    $("#adicionaCarrinho").off("click").on("click", function () {
        $("div[class='collection']").append($("<li>", {
            html: [
                $("<img>", { src: imagem, class: "circle", style: "max-width:100px;margin-top:-1.1%" }),
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
                $("<a>", {
                    href: "#modal4",
                    'class': "modal-trigger secondary-content",
                    html: [
                        $("<i>", {
                            html: "mode_edit",
                            "class": "material-icons"
                        }).on("click", function () {
                            $("#modal4").data("index", $(this).closest("li").index());
                        })
                    ]
                })
            ],
            class: "collection-item avatar"
        }));

        var produto = {
            Id: Id,
            Nome: nome,
            Quantidade: quantidade,
            Imagem: imagem
        }

        listaProdutos.push(produto);
        console.log(listaProdutos);
        localStorage.setItem('listaProdutos', JSON.stringify(listaProdutos));
    });


    $("#cpf").on("keydown", function () {
        mcpf($("#cpf").val());
    });

    $("#cpf").on("blur", function () {
        if ($("#cpf").val().length > 14) {
            $("#cpf").val($("#cpf").val().substr(0, 13));
            $("#cpf").keydown();
        }
    });
});

function mcpf(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    $("#cpf").val(v);
}