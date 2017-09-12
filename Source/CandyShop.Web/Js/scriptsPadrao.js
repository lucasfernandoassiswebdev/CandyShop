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

    $('#adicionaCarrinho').click(function () {
        $("div[class='collection']").append($('<li>',
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
});