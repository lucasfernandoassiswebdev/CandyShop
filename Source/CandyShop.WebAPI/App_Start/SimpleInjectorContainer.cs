using CandyShop.Core.Infra;
using CandyShop.Core.Services;
using CandyShop.Core.Services.Compra;
using CandyShop.Core.Services.CompraProduto;
using CandyShop.Core.Services.Pagamento;
using CandyShop.Core.Services.Produto;
using CandyShop.Core.Services.Usuario;
using CandyShop.Repository.DataBase;
using CandyShop.Repository.Repositorys;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CandyShop.WebAPI
{
    // Container do simple injector onde registramos as dependências
    public class SimpleInjectorContainer
    {
        public static Container Build()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            /* As dependências são registradas uma a uma, declarando a referência
               da instância, e a classe que será instanciada quando alguém chamar
               por essa referência, no primeiro caso, toda vez que alguém chamar
               pela interface INotification, uma classe Notification será instanciada,
               o Lifestyle.Scoped diz que apenas uma instância será criada, e todos
               que tiverem uma referência a essa instância usarão ela, nos outros casos
               uma nova instância da classe é criada */
            container.Register<INotification, Notification>(Lifestyle.Scoped);
            container.Register<IUsuarioRepository, UsuarioRepository>();
            container.Register<IUsuarioService, UsuarioService>();
            container.Register<IProdutoService, ProdutoService>();
            container.Register<IPagamentoRepository, PagamentoRepository>();
            container.Register<IPagamentoService, PagamentoService>();
            container.Register<IProdutoRepository, ProdutoRepository>();
            container.Register<ICompraRepository, CompraRepository>();
            container.Register<ICompraProdutoRepository, CompraProdutoRepository>();
            container.Register<Conexao>(Lifestyle.Scoped);
            container.Register<Imagens>();

            container.Verify();
            return container;
        }
    }
}