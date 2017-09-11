var AjaxJs = (function() {
    var url = {};

    var init = function(config) {
        url = config;
        main();
    };

    function main() {        //carrega a pagina principal
        $.get(url.main).success(function(data) {
            $('#DivGrid').html(data).slideDown(function() { //desce  o divgrid
                //if (mensagem)
                    //$(this).successMessage(mensagem, 5000); //informa mensagem de sucesso                
            });
        }).error(function(xhr) {
            $("#DivGrid").errorMessage(xhr.responseText);
        });
    };

    function chamaPagina(endereco) {
        $.get(endereco).success(function (data) {
            $('#DivGrid').slideUp(function () {
                $('#DivGrid').hide().html(data).slideDown();
            });
        }).error(function (xhr) {
            $('#DivGrid').errorMessage(xhr.responseText);            
        });
    };

    var voltarInicio = function () {
        $.get(url.main).success(function () {
            $("#DivGrid").slideUp(function () {
                $("#DivGrid").empty(); //$(this).empty();                
                main();
            }).error(function (xhr) {
                $("#DivGrid").errorMessage(xhr.responseText);
            });
        });
    };

    var pagamento = function () {
        chamaPagina(url.pagamento);
    };

    var historicoCompra = function () {
        chamaPagina(url.historicoCompra);
    };

    return {
        init: init,
        pagamento: pagamento,
        voltarInicio: voltarInicio,
        historicoCompra: historicoCompra
    };

})();