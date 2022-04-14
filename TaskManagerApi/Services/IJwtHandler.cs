using System.Collections.Generic;
using System.Security.Claims;
using TaskManagerApi.AuthModels;

namespace TaskManagerApi.Services
{
    public interface IJwtHandler
    {
        JsonWebToken Create(string username);
    }
}