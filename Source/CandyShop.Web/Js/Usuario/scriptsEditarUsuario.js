$('#fotoUsuario').change(function () {
    //função que muda a foto do usuário na tela
    readURL(this);
});

$('.botaoVoltar').on('click', function () {
    //voltando a lista de usuários
    AjaxJs.listaUsuario();
});

$('.botaoEditar').on('click', function () {
    encodeImageFileAsURL(AjaxJs.concluirEdicaoUsuario);
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imagem').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

//inicia os métodos que o materialize pede 
//e desativa os botões de envio caso o formulário esteja inválido
$(document).ready(function () {
    $('input').characterCounter();
    $('.tooltipped').tooltip({ delay: 50 });

    $('#Nome, #SaldoUsuario, #Password').on('keydown', function () {
        validaBotao();
    });

    $('#Nome, #SaldoUsuario, #Password').on('blur', function () {
        validaBotao();
    });
});




function validaBotao() {
    //validando o cpf
    var cpfNew = $('#cpf').val();
    cpfNew = cpfNew.replace(/\.|\-/g, '');

    //validando o campo de nome
    var qtde = $('#Nome').val().length;

    //desabilitando o botão caso um dos dois esteja inválido
    if (qtde > 50 || qtde === 0) {
        $('.botaoCadastro').attr('disabled', 'disabled');
    } else {
        $('.botaoCadastro').removeAttr('disabled');
    }
}

function encodeImageFileAsURL(callback) {
    var filesSelected = document.getElementById("fotoUsuario").files;
    if (filesSelected.length > 0) {
        var fileToLoad = filesSelected[0];
        var fileReader = new FileReader();

        fileReader.onload = function (fileLoadedEvent) {
            var srcData = fileLoadedEvent.target.result; // <--- data: base64
            if (typeof callback === "function") {
                callback(srcData);
            }
        }

        fileReader.readAsDataURL(fileToLoad);
    }
}

