//carrega a pagina de inicio
function main(endereco) {
    //pega a view main e a carrega no div
    $.get(endereco).done(function (data) {
        $("#DivGrid").slideUp(1000, function () {
            //desce  o divgrid  
            $('#DivGrid').hide().html(data).fadeIn(1000);
        });
        //xhr é o código do erro, que é retornado caso o get não tenha sucesso
    }).fail(function (xhr) {
        console.log(xhr.responseText);
    });
}

//Função genérica para carregar o div, de acordo com o endereço passado
function chamaPagina(endereco) {    
    $.get(endereco).done(function (data) {        
        $('#DivGrid').slideUp(function () {            
            $('#DivGrid').hide().html(data).slideDown(function() {                
            });            
        });
    }).fail(function (xhr) {
        console.log(xhr.responseText);
    });
}

function chamaPaginaComIdentificador(endereco, identificador) {
    $.get(endereco, identificador).done(function (data) {
        $('#DivGrid').slideUp(function () {
            $('#DivGrid').hide().html(data).slideDown();
        });
    }).fail(function (xhr) {
        console.log(xhr.responseText);
    });
}

function concluirAcao(endereco, objeto, tela) {
    $.post(endereco, objeto)
        .done(function (message) {
            chamaPagina(tela);
            Materialize.toast(message, 4000);
        })
        .fail(function (xhr) {
            console.log(xhr.responseText);
        });
}

function concluirAcaoEdicao(endereco, objeto, tela) {
    $.ajax({
        url: endereco,
        type: 'PUT',
        data: objeto,
        success: function (message) {
            chamaPagina(tela);
            Materialize.toast(message, 4000);
        }
    });
}
