$(document).ready(function () {
    $(".button-collapse").sideNav();
    $(".collapsible").collapsible();
    $(".tooltipped").tooltip({ delay: 50 });
    $("select").material_select();
    $(".penis").last().removeClass("select-dropdown");

    $(".closeMenu").click(function () { $(".button-collapse").sideNav("hide"); });
});

$(document)
    .on("ajaxSend", function () {
        $("#DivLoad").fadeIn(300);
    })
    .on("ajaxComplete", function (e, xhr) {
        $("#DivLoad").fadeOut(300);
        if (xhr.status == 401)
            location.reload();
    });
