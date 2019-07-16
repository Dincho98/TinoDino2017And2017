﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCPOS.Eracun.Entities
{
    public class Partner : EntityBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Oib { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
