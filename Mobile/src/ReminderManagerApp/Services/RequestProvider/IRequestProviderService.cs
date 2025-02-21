namespace ReminderManagerApp.Services
{
    public interface IRequestProviderService
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "");
        Task<TResult> PostAsync<TResult,TSend>(string uri, TSend data, string token = "");
        Task<TSend> PostAsync<TSend>(string uri, TSend data, string token = "");
        Task<TResult> DeleteAsync<TResult>(string uri, string token = "");
        Task<TResult> PatchAsync<TResult, TSend>(string uri, TSend data, string token = "");
    }
}
