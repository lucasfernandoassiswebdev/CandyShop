var removerImagemA = false, removerImagemB = false, removerImagemC = false;

$(document).ready(function () {
    $("select").material_select();
    $("input").characterCounter();
    $(".tooltipped").tooltip({ delay: 50 });

    // Removendo as imagens dos inputs
    $("#removerImagem1").click(function () { removerImagem("#imagem1", "#fotoProduto1", removerImagemA) });
    $("#removerImagem2").click(function () { removerImagem("#imagem2", "#fotoProduto2", removerImagemA) });
    $("#removerImagem3").click(function () { removerImagem("#imagem3", "#fotoProduto3", removerImagemA) });

    //Mudando as imagens na tela
    $("#fotoProduto1").change(function () { mudaFotoTela(removerImagemA, this, "#imagem1") });
    $("#fotoProduto2").change(function () { mudaFotoTela(removerImagemB, this, "#imagem2") });
    $("#fotoProduto3").change(function () { mudaFotoTela(removerImagemC, this, "#imagem3") });

    $("#PrecoProduto").maskMoney({
        prefix: "R$ ",
        allowNegative: false,
        thousands: "",
        decimal: ",",
        affixesStay: true
    }).maskMoney("mask");
});

function encodeImageFileAsURL(callback, tela) {
    var base64A, base64B, base64C;
    var imagem1 = document.getElementById("fotoProduto1").files;
    var imagem2 = document.getElementById("fotoProduto2").files;
    var imagem3 = document.getElementById("fotoProduto3").files;

    if (imagem1.length > 0)
        fazVerificacao(imagem1, base64A, verificaSegundaImagem);
    else
        verificaSegundaImagem();

    function verificaSegundaImagem() {
        if (imagem2.length > 0)
            fazVerificacao(imagem2, base64B, verificaTerceiraImagem);
        else
            verificaTerceiraImagem();
    }

    function verificaTerceiraImagem() {
        if (imagem3.length > 0) {
            fazVerificacao(imagem3, base64C, null);
            callback(base64A, base64B, base64C, removerImagemA, removerImagemB, removerImagemC, tela);
        } else
            callback(base64A, base64B, base64C, removerImagemA, removerImagemB, removerImagemC, tela);
    }
}

function fazVerificacao(imagem, base64, funcao) {
    var fileToLoad = imagem[0];
    var fileReader = new FileReader();

    fileReader.onload = function (fileLoadedEvent) {
        base64 = fileLoadedEvent.target.result;
        if (typeof funcao === "function")
            funcao();
    };
    fileReader.readAsDataURL(fileToLoad);
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

// Validações no campo de nome
$("#NomeProduto").keydown(function (e) {
    if (e.which == 13)
        $("#PrecoProduto").focus();
    else
        validaBotao();
}).keyup(validaBotao).blur(validaBotao).on("paste", validaBotao).focus(validaBotao);

// Validações no campo de preço
$("#PrecoProduto").keydown(function (e) {
    if (e.which == 13)
        $("#QtdeProduto").focus();
    else
        validaBotao();
}).keyup(validaBotao).blur(validaBotao).on("paste", validaBotao).focus(validaBotao);

//Validações no campo de quantidade
$("#QtdeProduto").keydown(function (e) {
    replaceLetters($(this).val(), "#QtdeProduto");
    validaBotao();
}).keyup(validaBotao).blur(validaBotao).on("paste", validaBotao).focus(validaBotao);

function mudaFotoTela(remover, input, imagem) {
    readURL(input, imagem);
    remover = false;
}

function removerImagem(imagem, inputFoto, remover) {
    $(imagem).attr("src", "http://189.112.203.1:45000/candyShop/retirado.png");
    $(inputFoto).val("");
    remover = true;
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

function validaBotao() {
    if ($("#NomeProduto").val().length <= 0 || $("#NomeProduto").val().length > 40 ||
        $("#QtdeProduto").val().length > 3 || $("#QtdeProduto").val().length <= 0 ||
        parseInt($("#QtdeProduto").val()) > 999 || parseInt($("#QtdeProduto").val()) <= 0 ||
        parseInt($("#QtdeProduto").val()) >= 999 || parseInt($("#QtdeProduto").val()) <= 0 ||
        parseFloat($("#PrecoProduto").val().replace("R$", "").replace(",", ".")) > 999 ||
        parseFloat($("#PrecoProduto").val().replace("R$", "").replace(",", ".")) <= 0 ||
        $("#PrecoProduto").val() == "R$ 0,00" || $("#PrecoProduto").val() == "")
        $(".botaoEditar").attr("disabled", "disabled");
    else
        $(".botaoEditar").removeAttr("disabled");
}

function replaceLetters(value, input) {
    value = value.replace(/\D/g, "");
    $(input).val(value);
}



