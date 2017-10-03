$(document).ready(function () {
    $('select').material_select();
    $('input').characterCounter();
    $('.tooltipped').tooltip({ delay: 50 });
});

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