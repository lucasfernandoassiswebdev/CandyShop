$(document).ready(function () {
    // Inicializando plugins
    $(".tooltipped").tooltip({ delay: 50 });
    $(".button-collapse").sideNav();
    $("select").material_select();

    // Efeito imagens passando
    $('.image').mouseover(function () {
        var imagem = $(this);
        imagem.data["lista"] = imagem.attr('data-imagem').split(',');
        var i = 1;
        imagem.data["intervalo"] = setInterval(function () {
            imagem.attr('src', imagem.data["lista"][i]);
            i === 2 ? i = 0 : i++;
        }, 500);
    }).mouseout(function () {
        clearInterval($(this).data["intervalo"]);
        $(this).attr('src', $(this).data["lista"][0]);
        });

    //fechando modals ao clicar 2 vezes fora
    $("html,body").dblclick(function (e) {
        if ($(e.target).hasClass("modal") || $(e.target).hasClass("modal-content") || $(e.target).hasClass("modal-footer") || $(e.target).hasClass("identQuant"))
            return false;
        $("#quantidade, #quantidadeEdit, #novaSenha, #confirmaNovaSenha, #cpf, #senha").val("");
        $("#novaSenha").removeAttr("disabled");
        $("#logar, #TrocarSenha").attr("disabled", "disabled");
        $("#modalCarrinho, #modalLogin, #modalEditarQuantidade, #trocaSenha").modal("close");
    });
});

