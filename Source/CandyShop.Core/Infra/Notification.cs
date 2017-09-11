using System.Collections.Generic;
using System.Linq;

namespace CandyShop.Core.Infra
{
    class Notification
    {
        //Criando uma lista para guardar as notification
        private readonly List<string> _notifications;

        //atribuindo a variavel _notifications com a lista 
        public Notification()
        {
          _notifications = new List<string>(); 
        }

        //Método pra adicionar notification no sistema caso precisar
        public void Add(string notification)
        {
            _notifications.Add(notification);
        }

        //Verifica se tem notification na lista (serve para verificações posteriores)
        public bool HasNotification => _notifications != null && _notifications.Any();

        //Pega alguam notification dentro da lista 
        public IEnumerable<string> GetNotification => _notifications;
    }
}
