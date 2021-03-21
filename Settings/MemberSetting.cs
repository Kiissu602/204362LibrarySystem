using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibrarySystem.Setting
{
    public class MemberSettings : IMemberSettings
    {
        public string Secret { get; set; }
    }

    public interface IMemberSettings
    {
        public string Secret { get; set; }
    }
}

