$(document).ready(function () {
    $(".tooltipped").tooltip({ delay: 50 });
    $(".button-collapse").sideNav();
    $("select").material_select();
    $(".caret").hide();
    $(".modal").modal({
        dismissible: false
    });

    //fechando modals ao clicar 2 vezes fora
    $("html,body").dblclick(function (e) {
        if ($(e.target).hasClass("modal") || $(e.target).hasClass("modal-content") || $(e.target).hasClass("modal-footer") || $(e.target).hasClass("identQuant")) {
            return false;
        }

        $("#modalCarrinho").modal("close");
        $("#modalQuantidade").modal("close");
        $("#modalLogin").modal("close");
        $("#modalQuantidade").modal("close");
        $("#modalEditarQuantidade").modal("close");
        $("#trocaSenha").modal("close");
    });
});

