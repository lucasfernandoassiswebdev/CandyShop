//no dia 26/09/2017, Lenon Bordini errou
//não apague este comentário, é imprescindível ao funcionamento do código
//e do nosso ego e auto-estima
//(no fundo ele ainda tem coração, mas gosta de pinto)

var imagem, preco, nome, imagem, quantidade = 0, quantidadeDisponivel, Id;
var listaProdutos = [];

$(document).ready(function () {
    //pesquisa por nome é feita quando se aperta a tecla "enter" na barra de pesquisa
    $('#search').on('keypress', function (e) {
        var textoPesquisa = $('#search').val();
        if (e.which == 13)
            AjaxJsProduto.listarProdutoPorNome(textoPesquisa);
    });

    //limpando os inputs
    $('.modal-close').on('click', function () {
        $('#quantidade, #quantidadeEdit').val('');
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

    //adicionando os itens do localstorage no carrinho
    if (localStorage.getItem('listaProdutos') !== null) {
        //$('.collection').empty();
        
        JSON.parse(localStorage.getItem('listaProdutos')).forEach(function (produto) {
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
                        href: "#modalQuantidade",
                        'class': "modal-trigger modal-close secondary-content",
                        "data-quantidadeDisponivel": produto.QuantidadeDisponivel,
                        html: [
                            $("<i>", {
                                html: "mode_edit",
                                "class": "material-icons"
                            }).on("click", function () {
                                $("#modalQuantidade").data("index", $(this).closest("li").index());
                            })
                        ]
                    })
                ],
                "class": "collection-item avatar"
            }));
        });
    }

    //adicionando os itens no carrinho
    $("#adicionaCarrinho").on("click", function () {
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
                            href: "#modalEditarQuantidade",
                            'class': "modal-close modal-trigger secondary-content",
                            "data-quantidadeDisponivel": quantidadeDisponivel,
                            html: [
                                $("<i>",
                                    {
                                        html: "mode_edit",
                                        "class": "material-icons"
                                    }).on("click",
                                    function () {
                                        $("#modalEditarQuantidade").data("index", $(this).closest("li").index());
                                    })
                            ]
                        })
                ],
                "class": "collection-item avatar"
            }));
        var produto = {
            Id: Id,
            Nome: nome,
            Quantidade: quantidade,
            Imagem: imagem
        }
        listaProdutos.push(produto);
        localStorage.removeItem('listaProdutos');
        localStorage.setItem('listaProdutos', JSON.stringify(listaProdutos));
    });

    //desabilitando botão quando houverem quantidades inválidas
    var verifyInt = /^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ]+$/;
    $('#quantidade').on('keydown', function () {
        quantidade = $("#quantidade").val();
        if (quantidade <= 0 || quantidade == null || quantidade == '' || quantidade == 'undefined' || quantidade.match(verifyInt)) {
            //$(".QtdeInvalida").errorMessage("Quantidade deve ser maior que zero!", 5000);
            $('#adicionaCarrinho').attr('disabled', 'disabled');
        } else {
            $('#adicionaCarrinho').removeAttr('disabled');
        }
    });
    $('#quantidade').on('blur', function () {
        quantidade = $("#quantidade").val();
        if (quantidade <= 0 || quantidade == null || quantidade == '' || quantidade == 'undefined' || quantidade.match(verifyInt)) {
            //$(".QtdeInvalida").errorMessage("Quantidade deve ser maior que zero!", 5000);
            $('#adicionaCarrinho').attr('disabled', 'disabled');
        } else {
            $('#adicionaCarrinho').removeAttr('disabled');
        }
    });
    //desabilitando no modal de editar a quantidade
    $('#quantidadeEdit').on('blur', function () {
        quantidade = $("#quantidadeEdit").val();
        if (quantidade <= 0 || quantidade == null || quantidade == '' || quantidade == 'undefined' || quantidade.match(verifyInt)) {
            //$(".QtdeInvalida").errorMessage("Quantidade deve ser maior que zero!", 5000);
            $('#editarQuantidade').attr('disabled', 'disabled');
        } else {
            $('#editarQuantidade').removeAttr('disabled');
        }
    });
    $('#quantidadeEdit').on('keydown', function () {
        quantidade = $("#quantidadeEdit").val();
        if (quantidade <= 0 || quantidade == null || quantidade == '' || quantidade == 'undefined' || quantidade.match(verifyInt)) {
            //$(".QtdeInvalida").errorMessage("Quantidade deve ser maior que zero!", 5000);
            $('#editarQuantidade').attr('disabled', 'disabled');
        } else {
            $('#editarQuantidade').removeAttr('disabled');
        }
    });

    $('#quantidade, #quantidadeEdit').on('keydown', function () {
        mNumbers($(this).val());
    });

    //limpando o carrinho
    $("#limpar").on("click", function () {
        $(".collection li").remove();
        if (localStorage.getItem("listaProdutos") != null) {
            localStorage.removeItem("listaProdutos");
            listaProdutos = [];
        }
    });
});



//validando campo de CPF
$("#cpf").on("keydown", function () {
    mcpf($("#cpf").val());
});

$("#cpf").on("blur", function () {
    if ($("#cpf").val().length > 14) {
        $("#cpf").val($("#cpf").val().substr(0, 13));
        $("#cpf").keydown();
    }
});

function mcpf(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    $("#cpf").val(v);
}

//função que remove caracteres que não sejam numéricos
function mNumbers(v) {
    v = v.replace(/\D/g, "");
    $("#quantidade, #quantidadeEdit").val(v);
}