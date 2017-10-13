$(document).ready(function () {
    $("select").material_select();
    $("input").characterCounter();
    $(".tooltipped").tooltip({ delay: 50 });

    $("#valorPago").keydown(function (e) {
        var tamanho = $(this).val().length;
        if (tamanho > 9 && e.which !== 8) {
            e.preventDefault();
            return false;
        }
    });

    $("#valorPago").maskMoney({
        prefix: "R$ ",
        allowNegative: true,
        thousands: ".",
        decimal: ",",
        affixesStay: false
    });

    $("#valorPago").maskMoney("mask");

    $("#valorPago").keyup(function () {
        var valor = $(this).val().length;
        var pagamento = $(this).val().replace("R$","").replace(",",".");

        if (parseInt(valor) > 9 || parseInt(valor) <= 0 || parseFloat(pagamento) > 999 || parseFloat(pagamento) <= 0 || pagamento == null) {
            $("#confirmarPagamento").attr("disabled", "disabled");
            Materialize.toast("Valor pagamento inválido", 3000);
        }
        else
            $("#confirmarPagamento").removeAttr("disabled");
    });

    $("#valorPago").blur(function () {
        $("#valorPago").maskMoney("mask");

        var valor = $(this).val().length;
        var pagamento = $(this).val().replace("R$", "").replace(",", ".");

        if (parseInt(valor) > 9 || parseInt(valor) <= 0 || parseFloat(pagamento) > 999 || parseFloat(pagamento) <= 0 || pagamento == null) {
            $("#confirmarPagamento").attr("disabled", "disabled");
        }
        else
            $("#confirmarPagamento").removeAttr("disabled");
    });

    $("#valorPago").focus(function () {
        var valor = $(this).val().length;
        var pagamento = $(this).val();

        pagamento = pagamento.replace("R$", "").replace(",", ".");
        if (parseInt(valor) > 9 || $(this).val() == "" || (parseFloat(pagamento) > 999 || parseFloat(pagamento) <= 0 || pagamento == null)) {
            $("#confirmarPagamento").attr("disabled", "disabled");
        } else
            $("#confirmarPagamento").removeAttr("disabled");
    });

    $("#valorPago").on("paste", function () {
        var valor = $(this).val().length;
        var pagamento = $(this).val();

        if (parseInt(valor) > 9 || parseInt(valor) <= 0 || parseFloat(pagamento) > 999 || parseFloat(pagamento) <= 0 || pagamento == null) {
            $("#confirmarPagamento").attr("disabled", "disabled");
            Materialize.toast("Valor pagamento inválido", 2000);
        } else
            $("#confirmarPagamento").removeAttr("disabled");
    });

    $("#valorPago").keydown(function (e) {
        if (e.which === 13) {
            AjaxJsPagamento.concluirEdicaoPagamento();
        }
    });
});

//funções que não deixam o usuário digitar "e" ou números negativos
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