using Contoso.Core;
using Contoso.Mobile.Core.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Contoso.Mobile.Core.Services
{
    public interface IDataStore : IBaseService
    {
        Task<string> GetSecureAsync(string secureName);

        Task SaveSecureAsync(string secureName, string secureValue);
    }

    public interface IDataStoreAuthentication : IDataStore
    {
        IDataStore<BaseItemModel> Notes { get; }

        Task<bool> AuthenticateAsync(string email, string password);

        string RefreshToken { get; }
    }
}