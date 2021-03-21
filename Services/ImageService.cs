using LibrarySystem.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IImageSettings _settings;

        public ImageService(IWebHostEnvironment environment, IImageSettings settings)
        {
            _environment = environment;
            _settings = settings;
        }

        public string SaveImg(IFormFile imageDat)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                imageDat.CopyTo(memoryStream);
                //convertion
                using (Image<Rgba32> image = Image.Load(memoryStream.ToArray()))
                {
                    image.Metadata.ExifProfile = null;

                    double ratio = Convert.ToDouble(image.Width) / image.Height;

                    if (image.Width >= image.Height && image.Width > _settings.MaxWidth)
                    {
                        image.Mutate(x => x
                         .Resize(_settings.MaxWidth, (int)Math.Round(_settings.MaxWidth / ratio))
                        );
                    }
                    else if (image.Width < image.Height && image.Height > _settings.MaxWidth)
                    {
                        image.Mutate(x => x
                         .Resize((int)Math.Round(_settings.MaxWidth * ratio), _settings.MaxWidth)
                        );
                    }

                    string guid = Guid.NewGuid().ToString();
                    string newFName = guid + ".jpg";
                    try
                    {
                        if (!Directory.Exists(Path.Join(_environment.WebRootPath, "uploads")))
                        {
                            Directory.CreateDirectory(Path.Join(_environment.WebRootPath, "uploads"));
                        }

                        JpegEncoder encoder = new JpegEncoder()
                        {
                            Quality = 100                              
                        };
                        image.Save(Path.Join(_environment.WebRootPath, "uploads", newFName), encoder);


                        return newFName;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }
    }
}
