using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Contracts.Services
{
    public interface IRabbitMQConsumerService
    {

        // void AssinarEvento(EventHandler<BasicDeliverEventArgs> func, string Queue = null);

        void AssinarEvento(string fila);


    }

}
