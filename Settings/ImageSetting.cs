using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Settings
{
    public class ImageSettings : IImageSettings
    {
        public int MaxWidth { get; set; }
    }

    public interface IImageSettings
    {
        public int MaxWidth { get; set; }
    }
}
