using Contoso.Core;
using Contoso.Mobile.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Mobile.UI.Services
{
    public sealed class MockDataStore : IDataStore<BaseItemModel>
    {
        readonly static List<BaseItemModel> items;

        static MockDataStore()
        {
            var work = new FolderModel { Id = Guid.NewGuid().ToString(), Name = "Work" };
            work.Notes = new System.Collections.ObjectModel.ObservableCollection<BaseItemModel>()
            {
                new NoteModel { Id = Guid.NewGuid().ToString(), Name = "Read documentation", Body="Read the xamarin documentation." },
                new NoteModel { Id = Guid.NewGuid().ToString(), Name = "iOS", Body="At apple.com" },
            };

            items = new List<BaseItemModel>()
            {
                new NoteModel { Id = Guid.NewGuid().ToString(), Name = "Home To-Do", Body="This is an item description." },
                new FolderModel { Id = Guid.NewGuid().ToString(), Name = "Personal" },
                work,
                new FolderModel { Id = Guid.NewGuid().ToString(), Name = "Projects" },
                new NoteModel { Id = Guid.NewGuid().ToString(), Name = "Call Mom", Body="Call on at 630-430-7404" },
                new NoteModel { Id = Guid.NewGuid().ToString(), Name = "Take the kids out", Body="This is an item description." },
                new NoteModel { Id = Guid.NewGuid().ToString(), Name = "Sixth item", Body="This is an item description." }
            };
        }

        public async Task<bool> AddAsync(BaseItemModel model)
        {
            items.Add(model);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(BaseItemModel model)
        {
            var oldItem = items.Where(arg => arg.Id == model.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(model);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var oldItem = items.Where(arg => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<BaseItemModel> GetAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IList<BaseItemModel>> GetAsync()
        {
            return await Task.FromResult(items);
        }
    }
}
