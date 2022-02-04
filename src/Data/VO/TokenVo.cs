namespace RestWith.NET.Data.VO
{
    public class TokenVo
    {
        public TokenVo(bool authenticated, string created, string expiration, string acessToken, string refresToken)
        {
            Authenticated = authenticated;
            Created = created;
            Expiration = expiration;
            AcessToken = acessToken;
            RefresToken = refresToken;
        }

        public bool Authenticated { get; set;}
        public string Created { get; set; }
        public  string Expiration { get; set; } 
        public  string AcessToken { get; set; }
        public  string RefresToken { get; set; }
    }
}