﻿using Mango.Mango.Web.Models;
using Mango.Web.Models;

namespace Mango.Web.Services.IService
{
    public interface IBaseService
    {
        Task<ResponseDTO?> SendAsync(RequestDTO requestDto);
    }
}
