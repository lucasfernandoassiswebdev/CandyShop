$(document).ready(function () {
    $('select').material_select();
    $('input').characterCounter();
    $('.tooltipped').tooltip({ delay: 50 });
    $("#removerImagem1, #removerImagem2, #removerImagem3").hide();
});

$('.botaoVoltar').on('click', function () {
    AjaxJsProduto.listaProduto();
});

//cadastrando o produto
$('.botaoCadastro').on('click', function () {
    encodeImageFileAsURL(AjaxJsProduto.concluirCadastroProduto);
});

function encodeImageFileAsURL(callback) {
    var base64A, base64B, base64C;
    var imagem1 = document.getElementById("fotoProduto1").files;
    var imagem2 = document.getElementById("fotoProduto2").files;
    var imagem3 = document.getElementById("fotoProduto3").files;

    if (imagem1.length > 0) {
        var fileToLoad = imagem1[0];
        var fileReader = new FileReader();

        fileReader.onload = function (fileLoadedEvent) {
            base64A = fileLoadedEvent.target.result;
            verificaSegundaImagem();
        };

        fileReader.readAsDataURL(fileToLoad);
    } else
        verificaSegundaImagem();

    function verificaSegundaImagem() {
        if (imagem2.length > 0) {
            var fileToLoadB = imagem2[0];
            var fileReaderB = new FileReader();

            fileReaderB.onload = function (fileLoadedEvent) {
                base64B = fileLoadedEvent.target.result;
                verificaTerceiraImagem();
            };

            fileReaderB.readAsDataURL(fileToLoadB);
        } else
            verificaTerceiraImagem();
    }

    function verificaTerceiraImagem() {
        if (imagem3.length > 0) {
            var fileToLoadC = imagem3[0];
            var fileReaderC = new FileReader();

            fileReaderC.onload = function (fileLoadedEvent) {
                base64C = fileLoadedEvent.target.result;
                if (typeof callback === "function") {
                    callback(base64A, base64B, base64C);
                }
            };
            fileReaderC.readAsDataURL(fileToLoadC);
        } else
            callback(base64A, base64B, base64C);
    }
}

//editando as imagens na tela
$('#fotoProduto1').change(function () {
    //função que muda a foto do usuário na tela
    readURL(this);
    $("#removerImagem1").show();
});

$('#fotoProduto2').change(function () {
    readURL2(this);
    $("#removerImagem2").show();
});

$('#fotoProduto3').change(function () {
    readURL3(this);
    $("#removerImagem3").show();
});

$("#removerImagem1").click(function () {
    $("#imagem1").attr("src", "../../Imagens/retirado.png");
    $("#fotoProduto1").val("");
});

$("#removerImagem2").click(function () {
    $("#imagem2").attr("src", "../../Imagens/retirado.png");
    $("#fotoProduto2").val("");
});

$("#removerImagem3").click(function () {
    $("#imagem3").attr("src", "../../Imagens/retirado.png");
    $("#fotoProduto3").val("");
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imagem1').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function readURL2(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imagem2').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function readURL3(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imagem3').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

//funções que não deixam o usuário digitar "e" ou números negativos
function FilterInput(event) {
    var keyCode = ('which' in event) ? event.which : event.keyCode;
    isNotWanted = (keyCode === 69 || keyCode === 189 || keyCode === 109);
    return !isNotWanted;
}

function handlePaste(e) {
    var clipboardData, pastedData;

    clipboardData = e.clipboardData || window.clipboardData;
    pastedData = clipboardData.getData('Text').toUpperCase();

    if (pastedData.indexOf('E') > -1) {
        e.stopPropagation();
        e.preventDefault();
    }

    if (pastedData.indexOf('-') > -1) {
        e.stopPropagation();
        e.preventDefault();
    }
}



