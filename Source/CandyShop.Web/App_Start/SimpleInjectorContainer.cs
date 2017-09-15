using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CandyShop.Web.App_Start
{
    public class SimpleInjectorContainer
    {
        public static Container Build()
        { 
        var container = new Container();
        container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            container.Verify();
            return container;
        }
    }
}