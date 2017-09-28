$(document).ready(function () {
    $('.tooltipped').tooltip({ delay: 50 });
    $(".button-collapse").sideNav();
    $('select').material_select();
    $('.caret').hide();
    $('.modal').modal({
        dismissible: false
    });

    $(window).on('dblclick', function() {
        $('#modalCarrinho').modal('close');
        $('#modalQuantidade').modal('close');
        $('#modalLogin').modal('close');
        $('#modalEditarCompra').modal('close');
        $('#modalQuantidade').modal('close');
        $('#modalEditarQuantidade').modal('close');
    });

    //não deixando que o usuário insira valores inválidos
    var verifyInt = /^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ]+$/;

    $('#quantidade').on('blur', function () {
        mNumbers($('#quantidade').val());
        if ($('#quantidade').val().match(verifyInt)) {
            mNumbers($('#quantidade').val());
            $('#confirmacao').removeAttr('href').removeClass('modal-trigger modal-close');
        } else if ($('#quantidade').val() == 0) {
            mNumbers($('#quantidade').val());
            $('#quantidade').val('');
            $('#confirmacao').removeAttr('href').removeClass('modal-trigger modal-close');
        } else {
            $('#confirmacao').attr('href', '#modalCarrinho').addClass('modal-trigger modal-close');
        }
    });

    $('#quantidadeEdit').on('blur', function () {
        mNumbers($('#quantidadeEdit').val());
        if ($('#quantidadeEdit').val().match(verifyInt) || $('#quantidadeEdit').val() === 0) {
            $('#quantidadeEdit').val('');
            $('#confirmaCompra').removeClass('modal-close');
        } else {
            $('#confirmaCompra').addClass('modal-close');
        }
    });

    $('#quantidadeEdit').on('blur', function () {
        mNumbers($('#quantidadeEdit').val());
        if ($('#quantidadeEdit').val().match(verifyInt)) {
            $('#editarQuantidade').removeClass('modal-close');
        } else if ($('#quantidadeEdit').val() === 0) {
            $('#quantidadeEdit').val('');
            $('#editarQuantidade').removeClass('modal-close');
        } else
            $('#editarQuantidade').addClass('modal-close');
    });

    $('#confirmacao').on('click', function () {
        if ($('#quantidade').val().match(verifyInt)) {
            $('#confirmacao').removeClass('modal-close modal-trigger');
        } else if ($('#quantidade').val() === 0) {
            $('#confirmacao').removeClass('modal-close modal-trigger');
        } else {
            $('#confirmacao').addClass('modal-close modal-trigger');
        }
    });

    $('#quantidade, #quantidadeEdit').on('keydown', function () {
        mNumbers($(this).val());
    });

    //limpando o carrinho
    $('#limpar').on('click', function () {
        $('.collection li').remove();
        if (localStorage.getItem('listaProdutos') != null) {
            localStorage.removeItem('listaProdutos');
        }
    });

    //editando a quantidade dos itens no carrinho
    $("#editarQuantidade").on("click", function () {
        $("#modalCarrinho .collection li:eq(" + $("#modalEditarQuantidade").data("index") + ") p")
            .text("Quantidade: " + $("#quantidadeEdit").val())
            .attr('data-Quantidade', $("#quantidadeEdit").val());
    });
});

//função que remove caracteres que não sejam numéricos
function mNumbers(v) {
    v = v.replace(/\D/g, "");
    $('#quantidade, #quantidadeEdit').val(v);
}