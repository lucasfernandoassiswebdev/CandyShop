$('#fotoUsuario').change(function () {
    readURL(this);
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function(e) {
            $('#imagem').attr('src', e.target.result);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

$(document).ready(function () {
    $('input').characterCounter();

    $('#cpf').on('keydown', function () {
        mcpf($('#cpf').val());
    });

    $('#cpf').on('blur', function () {
        if ($('#cpf').val().length > 14) {
            $('#cpf').val($('#cpf').val().substr(0, 13));
            $('#cpf').keydown();
        }
    });

    $('#Nome').on('keydown', function () {
        var qtde = $('#Nome').val().length;

        if (qtde > 50 || qtde === 0) {
            $('.waves ').attr('disabled',true);
        } else {
            $('.waves').removeAttr('disabled');
        }
    });

    $('#Nome').on('blur', function () {
        var qtde = $('#Nome').val().length;

        if (qtde > 50 || qtde === 0) {
            $('.waves ').attr('disabled',true);
        } else {
            $('.waves').removeAttr('disabled');
        }
    });
});

function mcpf(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    $('#cpf').val(v);
}

