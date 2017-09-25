$(document).ready(function() {
    $('select').material_select();
    $('input').characterCounter();
    $('.tooltipped').tooltip({ delay: 50 });

    $('#fotoProduto1').change(function () {
        //função que muda a foto do usuário na tela
        readURL(this);
    });

    $('#fotoProduto2').change(function () {
        readURL2(this);
    });

    $('#fotoProduto3').change(function () {
        readURL3(this);
    });
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imagem1').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function readURL2(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imagem2').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function readURL3(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imagem3').attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function encodeImageFileAsURL(callback) {
    var filesSelected = document.getElementById("fotoUsuario").files;
    if (filesSelected.length > 0) {
        var fileToLoad = filesSelected[0];
        var fileReader = new FileReader();

        fileReader.onload = function(fileLoadedEvent) {
            var srcData = fileLoadedEvent.target.result; // <--- data: base64
            if (typeof callback === "function") {
                callback(srcData);
            }
        };

        fileReader.readAsDataURL(fileToLoad);
    } else {
        callback();
    }
}