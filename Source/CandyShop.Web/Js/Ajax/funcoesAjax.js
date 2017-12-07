// Esse arquivo tem as funções genéricas encapsuladas que são usados nos outros arquivos
// Carrega a pagina de inicio
function main(endereco) {
    // Pega os dados da view recebida e a carrega na DivGrid
    $.get(endereco).done(function (data) {
        // É aplicado um efeito de "cortina" nesse carregamento
        $("#DivGrid").slideUp(1000, function () {
            // Desce  a "cortina"  
            $("#DivGrid").hide().html(data).fadeIn(1000);
        });
        // Xhr é o erro que é retornado caso o get não tenha sucesso
    }).fail(function (xhr) {
        /* Caso não tenha sido possível carregar a página, o erro
           é exibido no console */
        console.log(xhr.responseText);
    });
}

// Função genérica para carregar a página de acordo com o endereço passado
function chamaPagina(endereco) {
    $.get(endereco).done(function (data) {        
        $("#DivGrid").slideUp(function () {            
            $("#DivGrid").hide().html(data).slideDown();            
        });
    }).fail(function (xhr) {
        console.log(xhr.responseText);
    });
}

function chamaPaginaComIdentificador(endereco, identificador) {
    $.get(endereco, identificador).done(function (data) {
        $("#DivGrid").slideUp(function () {
            $("#DivGrid").hide().html(data).slideDown();
        });
    }).fail(function (xhr) {
        Materialize.toast("Algo deu errado",2000);
        console.log(xhr.responseText);
    });
}

// Função utilizada em métodos de insert por exemplo
function concluirAcao(endereco, objeto, tela) {
    $.post(endereco, objeto)
        .done(function (message) {
            chamaPagina(tela);
            Materialize.toast(message, 4000);
        })
        .fail(function (xhr) {
            Materialize.toast(xhr.responseText,4000);
            console.log(xhr.responseText);
        });
}

// Função utilizado quando o controller usa o verbo HTTP PUT(Editar)
function concluirAcaoEdicao(endereco, objeto, tela) {
    $.ajax({
        url: endereco,
        type: "POST",
        data: objeto,
        success: function (message) {
            chamaPagina(tela);
            Materialize.toast(message, 4000);
        }
    });
}
