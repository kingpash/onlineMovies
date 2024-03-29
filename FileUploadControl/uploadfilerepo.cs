﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FileUploadControl

{
    public class uploadfilerepo : UploadInterface
    {

        private IHostingEnvironment hostingEnvironment;
        public uploadfilerepo(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;



        }
        public async void uploadfilemultiple(IList<IFormFile> files)
        {
            long totalByes = files.Sum(f => f.Length);
            foreach (IFormFile item in files)
            {

                string filename = item.FileName.Trim('"');
                byte[] buffer = new byte[16 * 1024];
                using (FileStream output = System.IO.File.Create(this.GetpathAndFileName(filename)))
                {


                    using (Stream input = item.OpenReadStream())
                    {

                        // long totalReadBytes = 0;
                        int readBytes;
                        while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                        {

                            await output.WriteAsync(buffer, 0, readBytes);
                            totalByes += readBytes;



                        }



                    }

                }

            }
        }

        private string GetpathAndFileName(string filename)
        {
            string path = this.hostingEnvironment.WebRootPath + "\\uploads\\";

            if (!Directory.Exists(path))

                Directory.CreateDirectory(path);
            return path + filename;


        }
    }
}
