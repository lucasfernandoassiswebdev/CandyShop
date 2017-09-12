$(document).ready(function () {
    $('#cpf').on('keydown', function () {
        mcpf($('#cpf').val());
    });

    $('#cpf').on('blur', function () {
        if ($('#cpf').val().length > 14) {
            $('#cpf').val($('#cpf').val().substr(0, 13));
            $('#cpf').keydown();
        }
    });

    var margem = $(window).height() - $('form').height();
    margem = margem / 2;
    $('form').css('margin-top', margem);
    $('form').css('margin-bottom', margem);
});

$(window).resize(function () {
    var margem = $(window).height() - $('form').height();
    margem = margem / 2;
    $('form').css('margin-top', margem);
    $('form').css('margin-bottom', margem);
});

function mcpf(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    $('#cpf').val(v);
}