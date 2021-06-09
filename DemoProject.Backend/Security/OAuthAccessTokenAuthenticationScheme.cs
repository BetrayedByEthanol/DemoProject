using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Backend.Security
{
    public class OAuthAccessTokenAuthenticationScheme : IAuthenticationHandler
    {
        private HttpContext _context;


        public Task<AuthenticateResult> AuthenticateAsync()
        {
           

            if(_context.Session.TryGetValue("accessToken", out byte[] tokenbytes) && _context.Request.Cookies.TryGetValue("access_token", out string accessToken))
            {
                var token = Encoding.UTF8.GetString(tokenbytes);

                var tokenhandler = new JwtSecurityTokenHandler();
                var parameters = new TokenValidationParameters();
                parameters.ValidateLifetime = true;
                var claim = tokenhandler.ValidateToken(accessToken, parameters, out SecurityToken validToken);
                if (token == accessToken && validToken is not null)
                {
                    //renew token
                    return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claim, "OAuth")));
                }
                else
                {
                    //request refresh
                    return Task.FromResult(AuthenticateResult.NoResult());
                    
                }
            }
            else
            {
                if(_context.Request.Cookies.TryGetValue("refresh_token", out string refresh_token))
                {
                    //if client id and ip match refresh
                    //else go to login
                }
                else
                {
                    //if password user and client id match success
                    //else send fail
                }
                //return fail
                return Task.FromResult(AuthenticateResult.Fail("no"));
            }
        }

        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            _context.Response.StatusCode = 302;
            _context.Response.Headers.Add("Location", "/Weatherforecast");
            return Task.CompletedTask;
        }

        public Task ForbidAsync(AuthenticationProperties properties)
        {
            throw new NotImplementedException();
        }

        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _context = context;
            return Task.CompletedTask;
        }
    }
}
