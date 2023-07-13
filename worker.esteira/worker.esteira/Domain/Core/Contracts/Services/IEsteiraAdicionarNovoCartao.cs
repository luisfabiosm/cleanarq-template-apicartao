using Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Contracts.Services
{
    public interface IEsteiraAdicionarNovoCartao
    {

        Task AssinarAdicionarCartao();
    }
}
