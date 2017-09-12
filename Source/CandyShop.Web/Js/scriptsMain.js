$(document).ready(function () {
    $('.tooltipped').tooltip({ delay: 50 });
    $(".button-collapse").sideNav();
    $('select').material_select();
    $('.caret').hide();
    $('.modal').modal();

    var qtde = $('.card-title').length;
    for (var i = 0; i < qtde; i++) {
        var botao = $('.btn-floating:eq(' + i + ')').css('top');
        var pixels = botao.replace('px', '');
        $('.card-title:eq(' + i + ')').css('top', parseFloat(pixels) + 20).css('margin-bottom', '20px');
    }
});

$('#quantidade, #quantidadeEdit').on('keydown',
    function () {
        mNumbers($(this).val());
    }
);

var verifyInt = /^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ]+$/;

$('#abreModal').on('click',
    function () {
        $('#quantidade').val('');
    }
);

$('#quantidade').on('blur',
    function () {
        mNumbers($('#quantidade').val());
        if ($('#quantidade').val().match(verifyInt)) {
            $('#confirmacao').removeClass('modal-close modal-trigger');
        } else if ($('#quantidade').val() === 0) {
            $('#quantidade').val('');
            $('#confirmacao').removeClass('modal-close modal-trigger');
        } else {
            $('#confirmacao').addClass('modal-close modal-trigger');
        }
    }
);

$('#quantidadeEdit').on('blur',
    function () {
        mNumbers($('#quantidadeEdit').val());
        if ($('#quantidadeEdit').val().match(verifyInt) || $('#quantidadeEdit').val() === 0) {
            $('#quantidadeEdit').val('');
            $('#confirmaCompra').removeClass('modal-close');
        } else {
            $('#confirmaCompra').addClass('modal-close');
        }
    }
);

$('#confirmacao').on('click',
    function () {
        if ($('#quantidade').val().match(verifyInt)) {
            $('#confirmacao').removeClass('modal-close modal-trigger');
        } else if ($('#quantidade').val() === 0) {
            $('#confirmacao').removeClass('modal-close modal-trigger');
        } else {
            $('#confirmacao').addClass('modal-close modal-trigger');
        }
    }
);

$('#limpar').on('click',
    function () {
        $('.collection li').remove();
    }
);

function mNumbers(v) {
    v = v.replace(/\D/g, "");
    $('#quantidade, #quantidadeEdit').val(v);
}