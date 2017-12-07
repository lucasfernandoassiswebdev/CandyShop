//inicia os métodos que o materialize pede 
//e desativa os botões de envio caso o formulário esteja inválido
$(document).ready(function () {
    $("input").characterCounter();
    $(".tooltipped").tooltip({ delay: 50 });
    $("select").material_select();




    //colocar mascara no cpf do login 

    $("#cpf").keyup(function () {
        mcpf($("#cpf").val());
        validaBotao();
    }).keydown(function (e) {
        if (e.which == 109 || e.which == 107 || e.which == 69 || e.which == 189 || e.which == 188 || e.which == 190)
            return false;
        mcpf($("#cpf").val());
        validaBotao();
    }).on("blur", function () {
        mcpf($("#cpf").val());
        validaBotao();
    });

    $("#Nome").keydown(validaBotao).keyup(validaBotao).on("blur", validaBotao);

    //esconde o botão que retira a imagem
    $("#removerImagem").hide();

    $("#fotoUsuario").change(function () {
        //função que muda a foto do usuário na tela
        readURL(this);

        //mostrando o botão que retira a imagem
        $("#removerImagem").show();
    });

    //voltando a lista de usuários
    $(".botaoVoltar").click(AjaxJsUsuario.listaUsuario);

    $(".botaoCadastro").click(function () {
        //convertendo a imagem para base64
        encodeImageFileAsURL(AjaxJsUsuario.concluirCadastroUsuario);
    });

    //mudando a imagem quando o usário retirar a atual
    $("#removerImagem").click(function () {
        $("#imagem").attr("src", "http://189.112.203.1:45000/candyShop/retirado.png");
        $("#fotoUsuario").val("");
    });
});

function readURL(input) {
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




//função que remove caracteres inválidos do campo de CPF e aplica a máscara
function mcpf(v) {
    //retirando caracteres a mais do campo
    if ($("#cpf").val().length > 14) {
        $("#cpf").val($("#cpf").val().substr(0, 13));
        $("#cpf").keydown();
    }

    v = v.replace(/\D/g, "");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    $("#cpf").val(v);
}

function TestaCPF(strCpf) {
    var soma;
    var resto;
    soma = 0;
    if (strCpf == "00000000000" || strCpf == "11111111111" || strCpf == "22222222222" ||
        strCpf == "33333333333" || strCpf == "44444444444" || strCpf == "55555555555" ||
        strCpf == "66666666666" || strCpf == "77777777777" || strCpf == "88888888888" ||
        strCpf == "99999999999")
        return false;
    for (i = 1; i <= 9; i++)
        soma = soma + parseInt(strCpf.substring(i - 1, i)) * (11 - i);
    resto = (soma * 10) % 11;
    if ((resto == 10) || (resto == 11))
        resto = 0;
    if (resto !== parseInt(strCpf.substring(9, 10)))
        return false;
    soma = 0;
    for (i = 1; i <= 10; i++)
        soma = soma + parseInt(strCpf.substring(i - 1, i)) * (12 - i);
    resto = (soma * 10) % 11;
    if ((resto == 10) || (resto == 11))
        resto = 0;
    if (resto !== parseInt(strCpf.substring(10, 11)))
        return false;

    $("#ErroCpf").errorMessage(false, "CPF Inválido!", 2000);
    return true;
}

function validaBotao() {
    //validando o cpf
    var cpfNew = $("#cpf").val();
    cpfNew = cpfNew.replace(/\.|\-/g, "");
    $("#ErroCpf").successMessage(TestaCPF(cpfNew), "CPF OK!");

    //desabilitando o botão caso um dos dois esteja inválido
    if (!TestaCPF(cpfNew)) {
        $("#ErroCpf").errorMessage(true,"CPF Inválido!", 2000);
    }

    //valida email no cadastro de usuario
    $("#EmailCadastro").keyup(function () {
        var validaEmail = /^[_a-z0-9-]+(.[a-z0-9]+)@smn.com.br$/;
        var inputEmail = $("#EmailCadastro").val();
        if (validaEmail.test(inputEmail)) {
            $(".botaoCadastro").removeAttr("disabled");
        } else {
            $(".botaoCadastro").attr("disabled", "disabled");
        }
    });
}

function encodeImageFileAsURL(callback) {
    var filesSelected = document.getElementById("fotoUsuario").files;
    if (filesSelected.length > 0) {
        var fileToLoad = filesSelected[0];
        var fileReader = new FileReader();

        fileReader.onload = function (fileLoadedEvent) {
            var srcData = fileLoadedEvent.target.result;
            if (typeof callback === "function")
                callback(srcData);
        };

        fileReader.readAsDataURL(fileToLoad);
    } else {
        callback();
    }
}


