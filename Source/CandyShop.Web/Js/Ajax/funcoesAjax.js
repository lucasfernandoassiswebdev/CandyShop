//carrega a pagina de inicio
function main(endereco) {
    //pega a view main e a carrega no div
    $.get(endereco).done(function (data) {
        $("#DivGrid").slideUp(function () {
            //desce  o divgrid  
            $('#DivGrid').hide().html(data).slideDown();
        });
        //xhr é o código do erro, que é retornado caso o get não tenha sucesso
    }).fail(function (xhr) {
        console.log(xhr.responseText);
    });
}

//função que vai carregar o corpo inteiro da pagina
function carregaPadrao() {
    $.get(url.padrao)
        .done(function (data) {
            $('body').slideUp(function () {
                $('body').hide().html(data).slideDown();
                console.log("carrega padrao");
            });
        }).fail(function (xhr) {
            console.log(xhr.responseText);
        });
}

function deslogar() {
    $.get(url.logOff).done(function (data) {
        $('body').slideUp(function () {
            $('body').hide().html(data).slideDown();
        });
    }).fail(function (xhr) {
        console.log(xhr.responseText);
    });
}

//Função genérica para carregar o div, de acordo com o endereço passado
function chamaPagina(endereco) {
    //data é o conteudo da view
    $.get(endereco).done(function (data) {
        //a div é recolhida
        $('#DivGrid').slideUp(function () {
            //escondida, carregada e demonstrada novamente                
            $('#DivGrid').hide().html(data).slideDown();
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
            Materialize.toast(message, 1500);
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
            Materialize.toast(message, 1500);
        }
    });
}
