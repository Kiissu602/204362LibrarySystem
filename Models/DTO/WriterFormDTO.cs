﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _204362LibrarySystem.Models.DTO
{
    public class WriterFormDTO
    {
        public string Writer { get; set; }
    }

    public class WriterFormUpdateDTO
    {
        public int WriterID { get; set; }
        public string WriterName { get; set; }
    }
}
