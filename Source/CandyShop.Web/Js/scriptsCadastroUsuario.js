﻿//muda a foto do usuário na tela
$('#fotoUsuario').change(function () {
    //readURL(this,encodeImageFileAsURL);
    readURL(this);
});

//$('.botaoCadastro').on('click',function() {
//    encodeImageFileAsURL();
//    AjaxJs.concluirCadastroUsuario();
//});

$('.botaoVoltar').on('click', function() {
    AjaxJs.listaUsuario();
});

function readURL(input/*,callback*/) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imagem').attr('src', e.target.result);
            //if (typeof callback === "function") {
            //    callback();
            //}
        };
        reader.readAsDataURL(input.files[0]);
    }
}

//inicia os métodos que o materialize pede 
//e desativa os botões de envio caso o formulário esteja inválido
$(document).ready(function () {
    $('input').characterCounter();
    $('.tooltipped').tooltip({ delay: 50 });

    $('#cpf').on('keydown', function () {
        mcpf($('#cpf').val());
        validaBotao();
    });

    $('#cpf').on('blur', function () {
        mcpf($('#cpf').val());
        //retirando caracteres a mais do campo
        if ($('#cpf').val().length > 14) {
            $('#cpf').val($('#cpf').val().substr(0, 13));
            $('#cpf').keydown();
        }

        validaBotao();
    });

    $('#Nome').on('keydown', function () {
        validaBotao();
    });

    $('#Nome').on('blur', function () {
        validaBotao();
    });
});

//função que remove caracteres inválidos do campo de CPF
function mcpf(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    $('#cpf').val(v);
}

function TestaCPF(strCPF) {
    var Soma;
    var Resto;
    Soma = 0;
    //strCPF  = RetiraCaracteresInvalidos(strCPF,11);
    if (strCPF === "00000000000")
        return false;
    for (i = 1; i <= 9; i++)
        Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;
    if ((Resto === 10) || (Resto === 11))
        Resto = 0;
    if (Resto !== parseInt(strCPF.substring(9, 10)))
        return false;
    Soma = 0;
    for (i = 1; i <= 10; i++)
        Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;
    if ((Resto === 10) || (Resto === 11))
        Resto = 0;
    if (Resto !== parseInt(strCPF.substring(10, 11)))
        return false;
    return true;
}

function validaBotao() {
    //validando o cpf
    var cpfNew = $('#cpf').val();
    cpfNew = cpfNew.replace(/\.|\-/g, '');

    //validando o campo de nome
    var qtde = $('#Nome').val().length;

    if (!TestaCPF(cpfNew) || qtde > 50 || qtde === 0) {
        $('.botaoCadastro ').attr('disabled', 'disabled');
    } else {
        $('.botaoCadastro ').removeAttr('disabled');
    }
}

function encodeImageFileAsURL() {
    var filesSelected = document.getElementById("fotoUsuario").files;
    if (filesSelected.length > 0) {
        var fileToLoad = filesSelected[0];
        var fileReader = new FileReader();

        fileReader.onload = function (fileLoadedEvent) {
            var srcData = fileLoadedEvent.target.result; // <--- data: base64
            $('#uploadImagem').val(srcData);
        }

        fileReader.readAsDataURL(fileToLoad);
    }
}

$('#formCadastro').submit(function (e) {
    e.preventDefault();
    $(this).ajaxSubmit({
        error: function (xhr) {
            Materialize.toast(xhr.responseText, 3000);
        }
    });
    return false;
});