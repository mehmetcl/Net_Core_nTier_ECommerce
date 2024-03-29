﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.EntityLayer.DTOS
{
    public class TokenDto
    {
        public string AccessToken { get; set; }

        public DateTime AccessTokenExpiration  { get; set; }

        public string RefleshToken { get; set; }

        public DateTime RefleshTokenExpiration { get; set; }
    }
}
