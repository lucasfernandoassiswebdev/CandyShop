$(document).ready(function () {
    $(".button-collapse").sideNav();
    $(".collapsible").collapsible();
    $(".modal").modal();
    $(".tooltipped").tooltip({ delay: 50 });
    $("select").material_select();
    $(".penis").last().removeClass("select-dropdown");

    $(".closeMenu").on("click",function () {
        $(".button-collapse").sideNav("hide");
    });

    $("#nomeUsuario").keydown(function (e) {
        if (e.which === 13) {
            AjaxJsUsuario.listarUsuarioPorNome();
            $("#modalPesquisaUsuario").modal("close");
            $("#nomeUsuario").val("");
        }
    });

    $("#pesquisarUsuario").on("click",
        function () {
            AjaxJsUsuario.listarUsuarioPorNome();
            $("#modalPesquisaUsuario").modal("close");
            $("#nomeUsuario").val("");
        });

    $("#nomeProduto").keydown(function (e) {
        if (e.which === 13) {
            AjaxJsProduto.listarProdutoPorNome($("#nomeProduto").val());
            $("#modalPesquisaProduto").modal("close");
            $("#nomeProduto").val("");
        }
    });

    $("#pesquisarProduto").on("click", function () {        
            AjaxJsProduto.listarProdutoPorNome($("#nomeProduto").val());
            $("#modalPesquisaProduto").modal("close");
            $("#nomeProduto").val("");        
    });

});

// Código que faz o loading aparecer na tela toda vez que começa uma requisição Ajax
$(document).ajaxStart(function () {
    $("#DivLoad").fadeIn(300);
});

// Código que tira o loading da tela
$(document).ajaxStop(function () {
    $("#DivLoad").fadeOut(300);
});