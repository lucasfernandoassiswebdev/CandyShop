var imagem, preco, nome, imagem, quantidade;

$(document).ready(function () {
    $('#DivGrid').on('click', '.btn-floating', function () {
        preco = $(this).attr('data-Preco');
        nome = $(this).attr('data-Nome');
        imagem = '../../Imagens/' + $(this).attr('data-Imagem');
    });
    
    $('#confirmacao').click(function () {
        quantidade = $('#quantidade').val();
    });

    $('#adicionaCarrinho').off('click').on('click',function () {
        $("div[class='collection']").append($("<li>",
        {
            html: [
                $('<img>', { src: imagem, class: 'circle' }),
                $('<span>', { html: nome, class: 'title' }),
                $('<p>', { html: 'Quantidade: ' + quantidade }),
                $('<a>',
                {
                    href: '#modal4',
                    class: 'modal-trigger secondary-content',
                    html: [
                        $('<i>', { html: 'mode_edit', class: 'material-icons' })
                    ]
                })
            ],
            class: 'collection-item avatar'
        }));
    });

    $('#cpf').on('keydown', function () {
        mcpf($('#cpf').val());
    });

    $('#cpf').on('blur', function () {
        if ($('#cpf').val().length > 14) {
            $('#cpf').val($('#cpf').val().substr(0, 13));
            $('#cpf').keydown();
        }
    });
});

function mcpf(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    $('#cpf').val(v);
}