namespace General.Models.Application.Response
{
    public record ResponseFacebookLogin
    {
        public FacebookLoginResponseDetail Data { get; set; }

        public record FacebookLoginResponseDetail
        {
            public string App_Id { get; set; }

            public string Type { get; set; }

            public string Application { get; set; }

            public long Data_Access_Expires_At { get; set; }

            public long Expires_At { get; set; }

            public bool Is_Valid { get; set; }

            public string User_Id { get; set; }

            public List<string> Scopes { get; set; }

        }
    }
}
