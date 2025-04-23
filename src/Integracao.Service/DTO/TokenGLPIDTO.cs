namespace IntegracaoGLPI_DEvOps.Service.DTO
{
    public class TokenGLPIDTO
    {
        public string session_token { get; set; }

        public TokenGLPIDTO() { }

        public TokenGLPIDTO(string sessionToken)
        {
            session_token = sessionToken;
        }
    }
}
