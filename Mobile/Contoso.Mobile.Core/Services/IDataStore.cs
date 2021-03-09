using Contoso.Core;
using Contoso.Mobile.Core.Models;
using System.Threading.Tasks;

namespace Contoso.Mobile.Core.Services
{
    public interface IDataStore
    {
        Task<bool> AuthenticateAsync(string email, string password);

        IDataStore<BaseItemModel> Notes { get; }
    }
}