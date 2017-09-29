using CandyShop.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace CandyShop.Core.Infra
{
    public class Notification : INotification
    {
        // Criando uma lista para guardar as notifications
        private readonly List<string> _notifications;
        
        public Notification()
        {
            _notifications = new List<string>();
        }

        // Método pra adicionar uma notification
        public void Add(string notification)
        {
            _notifications.Add(notification);
        }

        /* 
          Verifica se há alguma notification na lista 
          (É utilizado para verificações posteriores) */
        public bool HasNotification()
        {
            return _notifications != null && _notifications.Any();
        }

        // Pega a notification dentro da lista 
        public IEnumerable<string> GetNotification()
        {
            return _notifications;
        } 
    }
}
