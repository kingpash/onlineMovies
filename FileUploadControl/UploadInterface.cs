using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FileUploadControl
{
   public  interface UploadInterface
    {
        void uploadfilemultiple(IList<IFormFile> files);

    }
}
