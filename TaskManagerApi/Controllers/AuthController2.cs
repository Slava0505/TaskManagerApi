﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManagerApi.Models;
using TaskManagerApi.Dto;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagerApi.Controllers
{

    [Route("auth/")]
    [ApiController]
    public class AuthController2 : ControllerBase
    {

        // тестовые данные вместо использования базы данных
        private readonly List<User> _users = new List<User>
        {
            new User { Username="admin", Password="admin", Role =  new Role("admin") }
        };

        [HttpPost("login")]
        public IActionResult Token([FromBody] LoginDto model)
        {
            var identity = GetIdentity(model.Username, model.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: JwtConfigurations.Issuer,
                audience: JwtConfigurations.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(JwtConfigurations.Lifetime)),
                signingCredentials: new SigningCredentials(JwtConfigurations.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return new JsonResult(response);
        }
        
        private ClaimsIdentity GetIdentity(string username, string password)
        {
            var user = _users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user == null)
            {
                return null;
            }

            // Claims описывают набор базовых данных для авторизованного пользователя
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            //Claims identity и будет являться полезной нагрузкой в JWT токене, которая будет проверяться стандартным атрибутом Authorize
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        [HttpGet]
        [Authorize]//Данный Endpoint доступен только для авторизованных пользователей
        [Route("test_login")]
        public IActionResult TestAuth()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

    }

}