using Blazored.SessionStorage;
using System.Text.Json;

namespace Blazor_Server_App_Login.Extensions
{
    public static class SessionStorageExt
    {
        //Guarda La sesion dentro del navegador del ususario en localstorage, solo se puede una sesion
        public static async Task SaveStorage<T>(
            this ISessionStorageService sessionStorageService,
            string Key,
            T item) where T : class
        {
            var itemJson = JsonSerializer.Serialize(item);
            await sessionStorageService.SetItemAsStringAsync(Key, itemJson);
        }
        public static async Task<T?> ObtenerStorage<T>(
            this ISessionStorageService sessionStorageService,
            string Key) where T : class
        {
            var itemJson = await sessionStorageService.GetItemAsStringAsync(Key);
            if (itemJson != null) { 
                var item = JsonSerializer.Deserialize<T>(itemJson);
                return item;
            }
            else
                 return null;
        }
    }
}
