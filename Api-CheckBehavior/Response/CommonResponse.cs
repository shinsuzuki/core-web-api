namespace Api_CheckBehavior.Response
{
    public class CommonResponse<T>
    {
        public bool IsSuccess { get; set; }

        public T? Data { get; set; }
    }
}
