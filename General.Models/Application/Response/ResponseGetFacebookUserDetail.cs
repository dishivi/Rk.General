using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Models.Application.Response
{
    public record ResponseGetFacebookUserDetail
    {
        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }

        public ResponseGetFacebookPicture Picture { get; set; }

        public record ResponseGetFacebookPicture
        {
            public ResponseGetFacebookPictureData Data { get; set; }

            public record ResponseGetFacebookPictureData
            {
                public int Height { get; set; }

                public string Url { get; set; }

                public int Width { get; set; }

                public bool Is_Silhouette { get; set; }
            }
        }
    }
}
