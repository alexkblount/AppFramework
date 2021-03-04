using Contoso.Core;
using Contoso.Mobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Mobile.Core.Services
{
    public sealed class MockItemsDataStore : IDataStore<ItemModel>
    {
        readonly List<ItemModel> items;

        public MockItemsDataStore()
        {
            items = new List<ItemModel>()
            {
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new ItemModel { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
        }

        public async Task<bool> AddAsync(ItemModel model)
        {
            items.Add(model);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(ItemModel model)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == model.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(model);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var oldItem = items.Where((ItemModel arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ItemModel> GetAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IList<ItemModel>> GetAsync()
        {
            return await Task.FromResult(items);
        }
    }
}