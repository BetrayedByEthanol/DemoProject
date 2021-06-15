using DemoProject.Backend.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Backend.Security
{
    public class OAuthAuthenticationScheme : IAuthenticationHandler
    {
        private HttpContext _context;


        public Task<AuthenticateResult> AuthenticateAsync()
        {

            string token = _context.Session.GetString("accessToken");
            string accessToken = "";
            if (_context.Request.Cookies.TryGetValue("access_token", out accessToken) && !String.IsNullOrEmpty(token))
            {

                var tokenhandler = new JwtSecurityTokenHandler();
                var parameters = new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Secret Needs to be at least 16 chars")),
                    ValidAlgorithms = new string[] { SecurityAlgorithms.HmacSha256 },
                    ValidAudience = "Audience",
                    ValidIssuer = "Issuer"
                };

                try
                {
                    var claim = tokenhandler.ValidateToken(accessToken, parameters, out SecurityToken validToken);
                    if (token == accessToken && validToken is not null)
                    {
                        //Successful access
                        var newToken = OAuthController.generateToken();
                        _context.Response.Cookies.Append("accessToken", newToken);
                        _context.Session.SetString("accessToken", newToken);
                        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claim, "OAuth")));
                    }
                    else
                    {
                        //Access Token provided by client does not match with what api expects
                        return Task.FromResult(
                            AuthenticateResult.Fail("token error",
                            new AuthenticationProperties()
                            {
                                RedirectUri = "/refresh"
                            }));

                    }
                }
                catch (SecurityTokenException ex)
                {
                    //Token has expired
                    return Task.FromResult(AuthenticateResult.Fail(ex, new AuthenticationProperties()
                    {
                        RedirectUri = "/refresh"
                    }));
                }
            }
            else
            {
                //ask Authentication service if token is valid
                if (OAuthController.Validate(accessToken, out ClaimsPrincipal claims))
                {
                    var newToken = OAuthController.generateToken();
                    _context.Response.Cookies.Append("accessToken", newToken);
                    _context.Session.SetString("accessToken", newToken);
                    return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claims, "OAuth")));
                }

                return Task.FromResult(
                            AuthenticateResult.Fail("token error",
                            new AuthenticationProperties()
                            {
                                RedirectUri = "/refresh"
                            }));
            }
        }

        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            _context.Response.StatusCode = 302;
            _context.Response.Headers.Add("Location", "/OAuth/Authorize");
            _context.Response.Headers.Add("Callback_Uri", _context.Request.Path.Value);
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
