var removerImagemA = false, removerImagemB = false, removerImagemC = false, cont = 0;
var base64A, base64B, base64C;
$(document).ready(function () {
    base64A = null;
    base64B = null;
    base64C = null;
    $("select").material_select();
    $("input").characterCounter();
    $(".tooltipped").tooltip({ delay: 50 });

    // Removendo as imagens dos inputs
    $("#removerImagem1").click(function () { removerImagem("#imagem1", "#fotoProduto1", 0) });
    $("#removerImagem2").click(function () { removerImagem("#imagem2", "#fotoProduto2", 1) });
    $("#removerImagem3").click(function () { removerImagem("#imagem3", "#fotoProduto3", 2) });

    //Mudando as imagens na tela
    $("#fotoProduto1").change(function () { mudaFotoTela(0, this, "#imagem1") });
    $("#fotoProduto2").change(function () { mudaFotoTela(1, this, "#imagem2") });
    $("#fotoProduto3").change(function () { mudaFotoTela(2, this, "#imagem3") });

    $("#PrecoProduto").maskMoney({
        prefix: "R$ ",
        allowNegative: false,
        thousands: "",
        decimal: ",",
        affixesStay: true
    }).maskMoney("mask");
});

function encodeImageFileAsURL(callback, tela) {
    var imagem1 = document.getElementById("fotoProduto1").files;
    var imagem2 = document.getElementById("fotoProduto2").files;
    var imagem3 = document.getElementById("fotoProduto3").files;

    if (imagem1.length > 0)
        fazVerificacao(imagem1, 0, verificaSegundaImagem);
    else {
        cont++;
        verificaSegundaImagem();
    }

    function verificaSegundaImagem() {
        if (imagem2.length > 0)
            fazVerificacao(imagem2, 1, verificaTerceiraImagem);
        else {
            cont++;
            verificaTerceiraImagem();
        }
    }

    function verificaTerceiraImagem() {
        if (imagem3.length > 0)
            fazVerificacao(imagem3, 2, callback, tela);
        else {
            callback(base64A, base64B, base64C, removerImagemA, removerImagemB, removerImagemC, tela);
            resetaBases();
        }
    }
}

function fazVerificacao(imagem, base, funcao, tela) {
    var fileToLoad = imagem[0];
    var fileReader = new FileReader();
    cont++;

    fileReader.onload = function (fileLoadedEvent) {
        if (base == 0)
            base64A = fileLoadedEvent.target.result;
        else if (base == 1)
            base64B = fileLoadedEvent.target.result;
        else
            base64C = fileLoadedEvent.target.result;

        executaFuncao(verifica);
    };
    fileReader.readAsDataURL(fileToLoad);
    function executaFuncao(callback) {
        if (typeof funcao === "function")
            funcao(base64A, base64B, base64C, removerImagemA, removerImagemB, removerImagemC, tela);

        if (typeof callback === "function")
            callback();
    }

    function verifica() {
        if (cont == 3)
            resetaBases();
    }
}

function resetaBases() {
    cont >= 3;
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
    if (e.which == 188)
        return false;
    replaceLetters($(this).val(), "#QtdeProduto");
    validaBotao();
}).keyup(validaBotao).blur(validaBotao).on("paste", validaBotao).focus(validaBotao);

function mudaFotoTela(remover, input, imagem) {
    readURL(input, imagem);
    if (remover == 0)
        removerImagemA = false;
    else if (remover == 1)
        removerImagemB = false;
    else
        removerImagemC = false;
}

function removerImagem(imagem, inputFoto, remover) {
    $(imagem).attr("src", "http://189.112.203.1:45000/candyShop/retirado.png");
    $(inputFoto).val("");
    if (remover == 0)
        removerImagemA = true;
    else if (remover == 1)
        removerImagemB = true;
    else
        removerImagemC = true;
}

function readURL(input, imagem) {
    if (input.files[0].size > 4194304) {
        Materialize.toast("A imagem deve ser menor que 4 MegaBytes", 5000);
        return;
    }
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
        $("#QtdeProduto").val().length < 0 || 
        parseInt($("#QtdeProduto").val()) < 0 || parseInt($("#QtdeProduto").val()) > 9999 ||
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



