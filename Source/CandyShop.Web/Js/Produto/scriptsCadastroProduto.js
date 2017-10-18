$(document).ready(function () {
    // Inicializando métodos jQuery framework js Materialize
    $("select").material_select();
    $("input").characterCounter();
    $(".tooltipped").tooltip({ delay: 50 });
    // Escondendo os botões de remover até que o usuário troque a imagem
    $("#removerImagem1, #removerImagem2, #removerImagem3").hide();

    // Colocando a máscara no campo de valor de produto
    $("#PrecoProduto").maskMoney({
        prefix: "R$ ",
        allowNegative: false,
        thousands: ".",
        decimal: ",",
        affixesStay: true
    })
        .maskMoney("mask").keyup(validaBotao).blur(validaBotao).on("paste", validaBotao).focus(validaBotao)
        .keydown(function (e) {
            var tamanhoCampo = $(this).val().length;
            if (tamanhoCampo > 2 &&
                e.which !== 8 &&
                e.which !== 46 &&
                e.which !== 38 &&
                e.which !== 37 &&
                e.which !== 40 &&
                e.which !== 39) {
                $(".botaoCadastro").attr("disabled", "disabled");
                e.preventDefault();
                return false;
            }
            validaBotao();
        });
    // Fazendo as validações no campo de nome
    $("#NomeProduto").keydown(function (e) {
        if (e.which == 13)
            $("#PrecoProduto").focus();
        else
            validaBotao();
    }).keyup(validaBotao).blur(validaBotao).on("paste", validaBotao).focus(validaBotao);

    // Fazendo as validações no campo de quantidade do produto
    $("#QtdeProduto").keydown(function (e) {
        var tamanhoCampo = $(this).val().length;

        if (tamanhoCampo > 2 && e.which !== 8) {
            $(".botaoCadastro").attr("disabled", "disabled");
            e.preventDefault();
            return false;
        }

        if (e.which == 13)
            $(".penis").focus();
        else
            validaBotao();
    }).keyup(validaBotao).blur(validaBotao).focus(validaBotao).on("paste", validaBotao);

    // Finalizando o cadastro
    $(".botaoCadastro").click(function () { encodeImageFileAsURL(AjaxJsProduto.concluirCadastroProduto); });

    // Chamando o método da função Ajax que foi definido na página inicial do Admin
    $(".botaoVoltar").click(AjaxJsProduto.listaProduto);

    // Editando as imagens na tela
    $("#fotoProduto1").change(function () { mudaImagem("#removerImagem1", "#imagem1"); });
    $("#fotoProduto2").change(function () { mudaImagem("#removerImagem2", "#imagem2"); });
    $("#fotoProduto3").change(function () { mudaImagem("#removerImagem3", "#imagem3"); });

    // Removendo as imagens da tela
    $("#removerImagem1").click(function () { removeImagem("#imagem1", "#fotoProduto1"); });
    $("#removerImagem2").click(function () { removeImagem("#imagem2", "#fotoProduto2"); });
    $("#removerImagem3").click(function () { removeImagem("#imagem3", "#fotoProduto3"); });
});

function mudaImagem(imagemMostrar, imagem) {
    $(imagemMostrar).show();
    readURL(this, imagem);
}

function removeImagem(imagem, input) {
    $(imagem).attr("src", "http://189.112.203.1:45000/candyShop/retirado.png");
    $(input).val("");
}

/* Função que transforma as imagens em base64 para serem posteriormente 
   renomeadas e copiadas na aplicação */
function encodeImageFileAsURL(callback) {
    var base64A, base64B, base64C;
    var imagem1 = document.getElementById("fotoProduto1").files;
    var imagem2 = document.getElementById("fotoProduto2").files;
    var imagem3 = document.getElementById("fotoProduto3").files;

    if (imagem1.length > 0)
        verificaImagem(imagem1, verificaSegundaImagem, base64A);
    else
        verificaSegundaImagem();

    function verificaSegundaImagem() {
        if (imagem2.length > 0)
            verificaImagem(imagem2, verificaTerceiraImagem, base64B);
        else
            verificaTerceiraImagem();
    }

    function verificaTerceiraImagem() {
        if (imagem3.length > 0)
            verificaImagem(imagem3, callback, base64C);
        else
            callback(base64A, base64B, base64C);
        /* Na linha acima, nós garantimos com o uso de callback que a função
           de cadastro será executada apenas depois que as 3 imagens(ou quantas o
           usuário tiver mandado) tiverem sido convertidas para base64*/
    }
}

function verificaImagem(imagem, funcao, base64) {
    var fileToLoad = imagem[0];
    var fileReader = new FileReader();

    fileReader.onload = function (fileLoadedEvent) {
        base64 = fileLoadedEvent.target.result;
        if (typeof funcao === "function")
            funcao();
    };
    fileReader.readAsDataURL(fileToLoad);
}

function readURL(input, imagem) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(imagem).attr("src", e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

// Funções que não deixam o usuário digitar "e" ou números negativos
function FilterInput(event) {
    var keyCode = ("which" in event) ? event.which : event.keyCode;
    var isNotWanted = (keyCode == 69 || keyCode == 189 || keyCode == 109 || keyCode == 190);
    return !isNotWanted;
}

function handlePaste(e) {
    var clipboardData = e.clipboardData || window.clipboardData;
    var pastedData = clipboardData.getData("Text").toUpperCase();

    if (pastedData.indexOf("e") > -1) {
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

function validaBotao() {
    if ($("#NomeProduto").val().length <= 0 || $("#NomeProduto").val().length > 40 ||
        $("#QtdeProduto").val().length > 3 || $("#QtdeProduto").val().length <= 0 ||
        parseInt($("#QtdeProduto").val()) > 999 || parseInt($("#QtdeProduto").val()) <= 0 ||
        parseInt($("#QtdeProduto").val()) >= 999 || parseInt($("#QtdeProduto").val()) <= 0 ||
        parseFloat($("#PrecoProduto").val().replace("R$", "").replace(",", ".")) > 999 ||
        parseFloat($("#PrecoProduto").val().replace("R$", "").replace(",", ".")) <= 0 ||
        $("#PrecoProduto").val() == "R$ 0,00" || $("#PrecoProduto").val() == "")
        $(".botaoCadastro").attr("disabled", "disabled");
    else
        $(".botaoCadastro").removeAttr("disabled");
}



