var removerImagem = false;
//inicia os métodos que o materialize pede
//e desativa os botões de envio caso o formulário esteja inválido
$(document).ready(function () {
    $("input").characterCounter();
    $(".tooltipped").tooltip({ delay: 50 });
    $("select").material_select();

    $("#Nome, #SaldoUsuario, #Password").on("keydown", function () {
        validaBotao();
    });

    $("#Nome, #SaldoUsuario, #Password").on("blur", function () {
        validaBotao();
    });
});

$("#fotoUsuario").change(function () {
    //função que muda a foto do usuário na tela
    readURL(this);
    removerImagem = false;
});

$("#removerImagem").click(function () {
    $("#imagem").attr("src", "Imagens/retirado.png");
    $("#fotoUsuario").val("");
    removerImagem = true;
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#imagem").attr("src", e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function validaBotao() {
    //validando o campo de nome
    var qtdeNome = $("#Nome").val().length;
    //validando o campo de senha
    var qtdeSenha = $("#Password").val().length;

    //desabilitando o botão caso um dos dois esteja inválido
    if (qtdeNome > 50 || qtdeSenha > 12 || qtdeNome === 0 || qtdeSenha === 0)
        $(".botaoEditar").attr("disabled", "disabled");
    else
        $(".botaoEditar").removeAttr("disabled");
}

function encodeImageFileAsURL(callback, tela) {
    var filesSelected = document.getElementById("fotoUsuario").files;
    if (filesSelected.length > 0) {
        var fileToLoad = filesSelected[0];
        var fileReader = new FileReader();

        fileReader.onload = function (fileLoadedEvent) {
            var srcData = fileLoadedEvent.target.result; // <--- data: base64
            if (typeof callback === "function") {
                callback(srcData, removerImagem, tela);
            }
        };
        fileReader.readAsDataURL(fileToLoad);
    } else
        callback(null, removerImagem, tela);
}

function FilterInput(event) {
    var keyCode = ("which" in event) ? event.which : event.keyCode;
    var isNotWanted = (keyCode === 69 || keyCode === 189 || keyCode === 109);
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
}



