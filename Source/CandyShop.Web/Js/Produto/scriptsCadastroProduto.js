$(document).ready(function () {
    // Inicializando métodos jQuery framework js Materialize
    $("select").material_select();
    $("input").characterCounter();
    $(".tooltipped").tooltip({ delay: 50 });
    // Escondendo os botões de remover até que o usuário troque a imagem
    $("#removerImagem1, #removerImagem2, #removerImagem3").hide();

    $("#PrecoProduto").maskMoney({
        prefix: "R$ ",
        allowNegative: false,
        thousands: ".",
        decimal: ",",
        affixesStay: false
    });

    $("#PrecoProduto").maskMoney("mask");

    $("#PrecoProduto").keyup(function () {
        var tamanhoCampo = $(this).val().length;
        var valorInserido = $(this).val();
        valorInserido = valorInserido.replace("R$", "").replace(",", ".");
        if (parseInt(tamanhoCampo) > 9 || parseInt(tamanhoCampo) <= 0 || parseFloat(valorInserido) > 999 || parseFloat(valorInserido) <= 0 || valorInserido == null) {
            $(".botaoCadastro").attr("disabled", "disabled");
            Materialize.toast("Valor inserido é inválido", 3000);
        }
        else
            $(".botaoCadastro").removeAttr("disabled");
    });

    $("#PrecoProduto").blur(function () {
        var tamanhoCampo = $(this).val().length;
        var valorInserido = $(this).val();

        valorInserido = valorInserido.replace("R$", "").replace(",", ".");
        if (parseInt(tamanhoCampo) > 6 || (parseFloat(valorInserido) > 999 || parseFloat(valorInserido) <= 0 || valorInserido == null)) {
            $(".botaoCadastro").attr("disabled", "disabled");
        } else
            $(".botaoCadastro").removeAttr("disabled");
    });

    $("#PrecoProduto").on("paste", function () {
        var tamanhoCampo = $(this).val().length;
        var valorInserido = $(this).val();

        valorInserido = valorInserido.replace("R$", "").replace(",", ".");
        if (parseInt(tamanhoCampo) > 6 || (parseFloat(valorInserido) > 999 || parseFloat(valorInserido) <= 0 || valorInserido == null)) {
            $(".botaoCadastro").attr("disabled", "disabled");
            Materialize.toast("Valor inserido é inválido", 2000);
        } else
            $(".botaoCadastro").removeAttr("disabled");
    });

    $(".botaoVoltar").on("click", function () {
        // Chamando o método da função Ajax que foi definido na página inicial do Admin
        AjaxJsProduto.listaProduto();
    });

    $(".botaoCadastro").on("click", function () {
        encodeImageFileAsURL(AjaxJsProduto.concluirCadastroProduto);
    });

    // Editando as imagens na tela
    $("#fotoProduto1").change(function () {
        // Função que muda a foto do usuário na tela
        readURL(this);
        $("#removerImagem1").show();
    });

    $("#fotoProduto2").change(function () {
        readURL2(this);
        // Quando a imagem muda, o botão que possibilita removê-la é exibido na tela
        $("#removerImagem2").show();
    });

    $("#fotoProduto3").change(function () {
        readURL3(this);
        $("#removerImagem3").show();
    });

    $("#removerImagem1").click(function () {
        $("#imagem1").attr("src", "Imagens/retirado.png");
        $("#fotoProduto1").val("");
    });

    $("#removerImagem2").click(function () {
        $("#imagem2").attr("src", "Imagens/retirado.png");
        $("#fotoProduto2").val("");
    });

    $("#removerImagem3").click(function () {
        $("#imagem3").attr("src", "Imagens/retirado.png");
        $("#fotoProduto3").val("");
    });
});

/* Função que transforma as imagens em base64 para serem posteriormente 
   renomeadas e copiadas na aplicação */
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
        /* Na linha acima, nós garantimos com o uso de callback que a função
           de cadastro será executada apenas depois que as 3 imagens(ou quantas o
           usuário tiver mandado) tiverem sido convertidas para base64*/
    }
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#imagem1").attr("src", e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function readURL2(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#imagem2").attr("src", e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function readURL3(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#imagem3").attr("src", e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

// Funções que não deixam o usuário digitar "e" ou números negativos
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



