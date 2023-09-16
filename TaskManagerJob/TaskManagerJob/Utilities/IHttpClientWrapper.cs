namespace TaskManagerJob.Utilities
{
    public interface IHttpClientWrapper
    {
      //  T SendPostEmailAsync<T>(string baseUrl, object body);
        Task<T> SendPostEmailRequest<T, U>(PostRequest<U> request);
    }
}
