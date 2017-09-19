using CandyShop.Application;
using CandyShop.Application.Interfaces;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CandyShop.Web
{
    public class SimpleInjectorContainer
    {
        public static Container Build()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Register <IUsuarioApplication, UsuarioApplication> ();
            container.Register <IProdutoApplication, ProdutoApplication> ();

            container.Verify();
            return container;
        }
    }
}