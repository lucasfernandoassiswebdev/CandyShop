var removerImagem = false;
//inicia os métodos que o materialize pede
//e desativa os botões de envio caso o formulário esteja inválido
$(document).ready(function() {
    validaBotao();

    $("input").characterCounter();

    $(".tooltipped").tooltip({ delay: 50 });

    $("select").material_select();

    $(".cpf").each(function() {
        $(this).val(mcpf($(this).val()));
    });

    $("#Nome, #SaldoUsuario, #Password").keydown(validaBotao).blur(validaBotao).focus(validaBotao);

    $("#fotoUsuario").change(function() {
        //função que muda a foto do usuário na tela
        readURL(this);
        removerImagem = false;
    });

    $("#removerImagem").click(function() {
        $("#imagem").attr("src", "http://189.112.203.1:45000/candyShop//retirado.png");
        $("#fotoUsuario").val("");
        removerImagem = true;
    });

    $("#SaldoUsuario").maskMoney({
        prefix: "R$ ",
        allowNegative: true,
        thousands: ".",
        decimal: ",",
        affixesStay: true
    }).maskMoney("mask");

    $("#SaldoUsuario").keyup(validaCampos).blur(validaCampos).on("paste", validaCampos);
});

function validaCampos() {
        var tamanhoCampo = $("#SaldoUsuario").val().length;
        var valorInserido = $("#SaldoUsuario").val();
        valorInserido = valorInserido.replace("R$", "").replace(",", ".");
        if (tamanhoCampo > 9 || tamanhoCampo <= 0 || parseFloat(valorInserido) > 999 || parseFloat(valorInserido) == 0 || valorInserido == "") {
            $(".botaoEditar").attr("disabled", "disabled");
        }
        else
            $(".botaoEditar").removeAttr("disabled");
    }

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
        if (qtdeNome > 50 || qtdeNome <= 0 || qtdeSenha > 12 || qtdeSenha <= 0 || qtdeSenha < 7)
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
                var srcData = fileLoadedEvent.target.result;
                if (typeof callback === "function")
                    callback(srcData, removerImagem, tela);
            };
            fileReader.readAsDataURL(fileToLoad);
        } else
            callback(null, removerImagem, tela);
    }

function FilterInput(event) {
        var keyCode = ("which" in event) ? event.which : event.keyCode;
        var isNotWanted = (keyCode == 69 || keyCode == 190);
        return !isNotWanted;
    }

function handlePaste(e) {
        var clipboardData = e.clipboardData || window.clipboardData;
        var pastedData = clipboardData.getData("Text").toUpperCase();

        if (pastedData.indexOf("e") > -1) {
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
        return v;
    }



