namespace Template.Domain.ViewModels
{
    public class ResponseApi<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
