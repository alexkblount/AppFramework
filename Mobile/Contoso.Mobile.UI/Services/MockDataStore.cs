using Contoso.Core;
using Contoso.Mobile.Core.Models;
using Contoso.Mobile.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Contoso.Mobile.UI.Services
{
    public abstract class BaseStorageService : IDataStore
    {
        public async Task<string> GetSecureAsync(string secureName)
        {
            if (string.IsNullOrWhiteSpace(secureName))
                throw new ArgumentNullException(nameof(secureName));

            string secureValue = null;

            try
            {
                secureValue = await SecureStorage.GetAsync(secureName);
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
                throw ex;
            }

            return secureValue;
        }

        public async Task SaveSecureAsync(string secureName, string secureValue)
        {
            if (string.IsNullOrWhiteSpace(secureName))
                throw new ArgumentNullException(nameof(secureName));

            try
            {
                if (string.IsNullOrWhiteSpace(secureValue))
                    SecureStorage.Remove(secureName);
                else
                    await SecureStorage.SetAsync(secureName, secureValue);
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
                throw ex;
            }
        }
    }

    public sealed class MockDataStore : BaseStorageService, IDataStoreAuthentication
    {
        private IDataStore<BaseItemModel> _Notes = new MockItemsDataStore();
        public IDataStore<BaseItemModel> Notes { get { return _Notes; } private set { _Notes = value; } }

        public string RefreshToken { get; set; }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            bool isLoggedin = !string.IsNullOrEmpty(email);
            if (isLoggedin)
            {
                await this.SaveSecureAsync(email, password);
                this.RefreshToken = email;
            }
            return isLoggedin;
        }
    }

    public sealed class MockItemsDataStore : IDataStore<BaseItemModel>
    {
        readonly static FolderModel root;

        static MockItemsDataStore()
        {
            var work = new FolderModel { Id = Guid.NewGuid().ToString(), Name = "Work" };
            work.Notes = new System.Collections.ObjectModel.ObservableCollection<BaseItemModel>()
            {
                new NoteModel { Id = Guid.NewGuid().ToString(), Name = "Read documentation", Body="Read the xamarin documentation." },
                new NoteModel { Id = Guid.NewGuid().ToString(), Name = "iOS", Body="At apple.com" },
            };

            root = new FolderModel()
            {
                Notes = new System.Collections.ObjectModel.ObservableCollection<BaseItemModel>()
                {
                    new NoteModel { Id = Guid.NewGuid().ToString(), Name = "Home To-Do", Body="This is an item description." },
                    new FolderModel { Id = Guid.NewGuid().ToString(), Name = "Personal" },
                    work,
                    new FolderModel { Id = Guid.NewGuid().ToString(), Name = "Projects" },
                    new NoteModel { Id = Guid.NewGuid().ToString(), Name = "Call Mom", Body="Call on at 630-430-7404" },
                    new NoteModel { Id = Guid.NewGuid().ToString(), Name = "Take the kids out", Body="This is an item description." },
                    new NoteModel { Id = Guid.NewGuid().ToString(), Name = "Sixth item", Body="This is an item description." }
                }
            };
        }

        internal MockItemsDataStore()
        {
        }

        public async Task<bool> AddAsync(BaseItemModel model)
        {
            root.Notes.Add(model);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateAsync(BaseItemModel model)
        {
            var oldItem = root.Notes.Where(arg => arg.Id == model.Id).FirstOrDefault();
            root.Notes.Remove(oldItem);
            root.Notes.Add(model);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var oldItem = root.Notes.Where(arg => arg.Id == id).FirstOrDefault();
            root.Notes.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public Task<BaseItemModel> GetAsync(string id)
        {
            BaseItemModel GetModel(FolderModel folder)
            {
                BaseItemModel model = folder.Notes?.FirstOrDefault(s => s.Id == id);

                if (model == null)
                    foreach (var m in folder.Notes)
                    {
                        if (m is FolderModel innerFolder)
                            model = GetModel(innerFolder);

                        if (model != null)
                            break;
                    }

                return model;
            }

            if (id == null)
                return GetAsync();
            else
                return Task.FromResult(GetModel(root));
        }

        public async Task<BaseItemModel> GetAsync()
        {
            return await Task.FromResult(root);
        }
    }
}