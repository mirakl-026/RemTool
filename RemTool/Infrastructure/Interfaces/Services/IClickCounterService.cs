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
        // синхронные методы
        #region CRUD
        public void CreateClickCounter(ClickCounter counter);

        public ClickCounter ReadClickCounter(string id);

        public void UpdateClickCounter(ClickCounter counter);

        public void DeleteClickCounter(string id);
        #endregion

        public void IncreaseCounter(string counterId, string toolTypeId);

        public void ResetCounter(string counterId);

        public void ResetAllCounters();

        public void DeleteAllCounters();


        // асинхронные методы
        #region CRUD_Async
        public Task CreateClickCounterAsync(ClickCounter counter);

        public Task<ClickCounter> ReadClickCounterAsync(string id);

        public Task UpdateClickCounterAsync(ClickCounter counter);

        public Task DeleteClickCounterAsync(string id);
        #endregion

        public Task IncreaseCounterAsync(string counterId, string toolTypeId);

        public Task ResetCounterAsync(string counterId);

        public Task ResetAllCountersAsync();

        public Task DeleteAllCountersAsync();
    }
}
