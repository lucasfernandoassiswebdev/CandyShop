

$(document).ready(function () {
    $(".tooltipped").tooltip({ delay: 50 });
    $(".button-collapse").sideNav();
    $("select").material_select();
    //fechando modals ao clicar 2 vezes fora
    $("html,body").dblclick(function (e) {
        if ($(e.target).hasClass("modal") || $(e.target).hasClass("modal-content") || $(e.target).hasClass("modal-footer") || $(e.target).hasClass("identQuant"))
            return false;
        $("#quantidade, #quantidadeEdit, #novaSenha, #confirmaNovaSenha,#Senhacpf, #cpf, #senha, #email").val("");
        $("#novaSenha").removeAttr("disabled");
        $("#logar, #TrocarSenha, #Enviar, #CadastrarEmail").attr("disabled", "disabled");
        $("#modalCarrinho, #modalQuantidade, #modalLogin, #modalQuantidade, #modalEditarQuantidade, #trocaSenha, #EsqueceuSenha, #cadastraEmail").modal("close");
    });
});

