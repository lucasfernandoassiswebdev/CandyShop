using CandyShop.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace CandyShop.Core.Infra
{
    public class Notification : INotification
    {
        //Criando uma lista para guardar as notification
        private readonly List<string> _notifications;
        
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
        public bool HasNotification()
        {
            return _notifications != null && _notifications.Any();
        }

        //Pega alguma notification dentro da lista 
        public IEnumerable<string> GetNotification()
        {
            return _notifications;
        } 
    }
}
