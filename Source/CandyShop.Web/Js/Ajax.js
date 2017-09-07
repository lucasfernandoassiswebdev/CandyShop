var AjaxJs = (function() {
    var url = {};

    var init = function(config) {
        url = config;
        main();
    };

    function main() {
        //busca o grid
        $.get(url.main).success(function(data) {
            $('#DivGrid').html(data).slideDown(function() { //desce  o divgrid
                //if (mensagem)
                    //$(this).successMessage(mensagem, 5000); //informa mensagem de sucesso                
            });
        }).error(function(xhr) {
            $("#DivGrid").errorMessage(xhr.responseText);
        });
    };

    var voltarInicio = function () {
        $.get(url.main).success(function () {
            $("#divDados").slideUp(function () {
                $("#divDados").empty(); //$(this).empty();                
                $('#DivGrid').slideDown();
            }).error(function (xhr) {
                $("#divGrid").errorMessage(xhr.responseText);
            });
        });
    };

    var pagamento = function() {
        $.get(url.pagamento).success(function(data) {
            $('#DivGrid').slideUp(function() {
                $('#DivDados').hide().html(data).slideDown();
            });            
        }).error(function(xhr) {
            $('#DivGrid').errorMessage(xhr.responseText);
        });
    };

    return {
        init: init,
        pagamento: pagamento,
        voltaInicio: voltarInicio
    };

})();