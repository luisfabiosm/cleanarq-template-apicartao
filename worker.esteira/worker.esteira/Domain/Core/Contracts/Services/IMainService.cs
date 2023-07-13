using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Contracts.Services
{
    internal interface IMainService
    {
        Task IniciarService(CancellationToken stoppingToken);

    }
}
