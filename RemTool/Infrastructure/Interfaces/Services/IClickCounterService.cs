using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RemTool.Models;

// интерфейс для ведения статистики (кликов по инструментам)
namespace RemTool.Infrastructure.Interfaces.Services
{
    public interface IClickCounterService
    {
        public void IncreaseCounter(string counterId, string toolTypeId);

        public void ResetCounter(string counterId);

        public void ResetAllCounters();
    }
}
