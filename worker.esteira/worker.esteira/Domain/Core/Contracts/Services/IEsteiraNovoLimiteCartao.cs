using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Contracts.Services
{
    public interface IEsteiraNovoLimiteCartao: IRabbitMQConsumerService
    {
        void AtualizarNovoLimiteCartao(CancellationToken stoppingToken);
    }
}
