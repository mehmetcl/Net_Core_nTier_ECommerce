﻿using ECommerce.EntityLayer.Concrete;
using ECommerce.EntityLayer.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BusinessLayer.Abstract
{
    public interface ITokenService
    {
        TokenDto CreateToken(User user);

        ClientTokenDto CreateTokenByClient(Client client);
    }
}
