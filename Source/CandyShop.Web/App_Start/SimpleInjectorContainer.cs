using CandyShop.Application;
using CandyShop.Application.Applications;
using CandyShop.Application.Interfaces;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CandyShop.Web
{
    /* Container do Simple Injector onde são mapeadas as dependências, comentários
       mais detalhados sobre isso estão disponíveis no container da API */
    public class SimpleInjectorContainer
    {
        public static Container Build()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register <IUsuarioApplication, UsuarioApplication> ();
            container.Register <IProdutoApplication, ProdutoApplication> ();
            container.Register <IPagamentoApplication, PagamentoApplication>();
            container.Register <ICompraApplication, CompraApplication>();

            container.Verify();
            return container;
        }
    }
}