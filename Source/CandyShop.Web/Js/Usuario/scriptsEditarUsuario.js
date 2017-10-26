var removerImagem = false;
//inicia os métodos que o materialize pede
//e desativa os botões de envio caso o formulário esteja inválido
$(document).ready(function () {
    validaBotao();

    $("input").characterCounter();

    $(".tooltipped").tooltip({ delay: 50 });

    $("select").material_select();

    $(".cpf").each(function () {
        $(this).val(mcpf($(this).val()));
    });

    $("#Nome, #SaldoUsuario, #Password, #SaldoUsuario").keydown(validaBotao).keyup(validaBotao).blur(validaBotao).focus(validaBotao).on("paste", validaBotao);
    $("#SaldoUsuario").keydown(function (e) {
        var saldo = $(this).val().replace("R$", "").replace(",", ".").trim();
        if (saldo == 0 && e.which == 109 || e.which == 189)
            return false;
    }).keyup(function () {
        var saldo = $(this).val().replace("R$", "").replace(",", ".").trim();
        if (saldo == "- 0.00") {
            saldo = saldo.replace("-", "");
            $("#SaldoUsuario").val(saldo);
        };
    });

    $("#fotoUsuario").change(function () {
        //função que muda a foto do usuário na tela
        readURL(this);
        removerImagem = false;
    });

    $("#removerImagem").click(function () {
        $("#imagem").attr("src", "http://189.112.203.1:45000/candyShop//retirado.png");
        $("#fotoUsuario").val("");
        removerImagem = true;
    });

    $("#SaldoUsuario").maskMoney({
        prefix: "R$ ",
        allowNegative: true,
        thousands: ".",
        decimal: ",",
        affixesStay: true,
        //clearMaskOnLostFocus: false
    }).maskMoney("mask");
});

function validaBotao() {
    //validando o campo de nome
    var qtdeNome = $("#Nome").val().length;
    //validando o campo de senha
    var qtdeSenha = $("#Password").val().length;
    var tamanhoCampo = $("#SaldoUsuario").val().length;
    var valorInserido = $("#SaldoUsuario").val();
    valorInserido = valorInserido.replace("R$ ", "").replace(",", ".");
    //desabilitando o botão caso um dos dois esteja inválido
    if (qtdeNome > 50 || qtdeNome <= 0 || qtdeSenha > 12 || qtdeSenha < 6 || tamanhoCampo > 9 ||
        tamanhoCampo < 0 || parseFloat(valorInserido) > 999.99)
        $(".botaoEditar").attr("disabled", "disabled");
    else
        $(".botaoEditar").removeAttr("disabled");
}

function readURL(input) {
    //verificar se a imagem é maior que 4 megas
    if (input.files[0].size > 4194304) {
        Materialize.toast("A imagem deve ser menor que 4 MegaBytes", 5000);
        return;
    }
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $("#imagem").attr("src", e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
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



