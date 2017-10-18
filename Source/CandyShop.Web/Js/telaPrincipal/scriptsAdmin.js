$(document).ready(function () {
    $(".button-collapse").sideNav();
    $(".collapsible").collapsible();
    $(".modal").modal();
    $(".tooltipped").tooltip({ delay: 50 });
    $("select").material_select();
    $(".penis").last().removeClass("select-dropdown");

    $(".closeMenu").click(function () { $(".button-collapse").sideNav("hide"); });

    $("#nomeUsuario").keydown(function (e) {
        if (e.which === 13)
            closeModalsClearInputs("#modalPesquisaUsuario", "#nomeUsuario", AjaxJsUsuario.listarUsuarioPorNome, null);
    });

    $("#pesquisarUsuario").click(function () {
        closeModalsClearInputs("#modalPesquisaUsuario", "#nomeUsuario", AjaxJsUsuario.listarUsuarioPorNome, null);
    });
});

$("#nomeProduto").keydown(function (e) {
    if (e.which === 13)
        closeModalsClearInputs("#modalPesquisaProduto", "#nomeProduto", AjaxJsProduto.listarProdutoPorNome, $("#nomeProduto").val());
});

$("#pesquisarProduto").click(function () {
    closeModalsClearInputs("#modalPesquisaProduto", "#nomeProduto", AjaxJsProduto.listarProdutoPorNome, $("#nomeProduto").val());
});

function closeModalsClearInputs(modal, input, funcao, parametro) {
    funcao(parametro);
    $(modal).modal("close");
    $(input).val("");
}

// Código que faz o loading aparecer na tela toda vez que começa uma requisição Ajax
$(document).ajaxStart(function () { $("#DivLoad").fadeIn(300); });

// Código que tira o loading da tela
$(document).ajaxStop(function () { $("#DivLoad").fadeOut(300); });