namespace IntegracaoGLPI_DevOps.ViewModel
{
    public class ResultViewModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public long Length { get; set; } = 0;
        public dynamic Data { get; set; }
    }
}
