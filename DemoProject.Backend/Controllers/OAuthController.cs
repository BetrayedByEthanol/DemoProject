using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OAuthController : ControllerBase
    {
        private readonly ILogger<OAuthController> _logger;
        public OAuthController(ILogger<OAuthController> logger) : base()
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/OAuth/Authorize")]
        public async Task<IActionResult> Authorize(
            string response_type, // authorization flow type 
            string client_id, // client id
            string redirect_uri,
            string scope, // what info I want = email,grandma,tel
            string state) // random string generated to confirm that we are going to back to the same client
        {
            state = RandomString(69);
            redirect_uri ??= "not null";
            // ?a=foo&b=bar
            var query = new QueryBuilder();
            query.Add("redirectUri", redirect_uri);
            query.Add("state", state);
            _logger.LogInformation(UriHelper.GetEncodedPathAndQuery(Request));

            return Content("<html> <form id=\"form\" action=\"/OAuth/Authorize" + query.ToString() + "\" method=\"POST\"> <input type=\"submit\" value=\"Submit\" /> <script> document.getElementById('form').submit() </script> </html>", "text/html");
        }

        [HttpPost]//This should have refresh Token
        [Route("/OAuth/Authorize")]
        public async Task Authorize(
            string username,
            string redirectUri,
            string state)
        {
            string code = RandomString(69);
            if ((state is null) || (redirectUri is null))
            {
                state = "";
                redirectUri = "/login";
            }

            //Refresh Check

            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);
            query.Add("redirect_uri", redirectUri);
            Response.StatusCode = 307;
            Response.ContentType = "application/json";
            var kv = new KeyValuePair<string, StringValues>("Location", new StringValues("/OAuth/Token" + query.ToString()));
            Response.Headers.Add(kv);

            await Response.CompleteAsync();
        }


        //Spit Token part and code verifing part
        [HttpPost]
        [Route("/OAuth/Token")] //Redirect to OG page
        public async Task Token(
            string code, // confirmation of the authentication process
            string redirect_uri,
            string client_id,
            string refresh_token)
        {
            // some mechanism for validating the code
            var qstring = Request.Query.ToString();
            var access_token = generateToken();
            var responseObject = new
            {
                access_token,
                token_type = "Bearer",
                raw_claim = "oauthTutorial",
                refresh_token = "RefreshTokenSampleValueSomething77",
                redirect_uri = redirect_uri

            };



            var responseJson = JsonConvert.SerializeObject(responseObject);
            var responseBytes = Encoding.UTF8.GetBytes(responseJson);
            HttpContext.Session.Set("accessToken", Encoding.UTF8.GetBytes(access_token));

            await Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length); //Should 
        }

        public static bool Validate(string accesToken, out ClaimsPrincipal claims)
        {
            claims = null;
            if (!String.IsNullOrEmpty(accesToken))
            {
                JwtSecurityTokenHandler tokenHandler = new();
                try
                {
                    TokenValidationParameters parameters = new();
                    claims = tokenHandler.ValidateToken(accesToken, parameters, out SecurityToken securityToken);
                    return true;
                }
                catch (SecurityTokenException ex)
                {
                    return false;
                }

            }
            return false;
        }

        public static string generateToken()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
                new Claim("granny", "cookie"),
                new ClaimsIdentity
            };

            var secretBytes = Encoding.UTF8.GetBytes("Secret Needs to be at least 16 chars");
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;


            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                "Issuer",
                "Audience",
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        [Route("/OAuth/Register")]
        public async Task registerAccount()
        {

        }

        [HttpPost]
        [Route("/OAuth/verify")]
        public async Task verifyAccount()
        {

        }

        [HttpPost]
        [Route("/OAuth/login")]
        public async Task login()
        {

        }

        public static string RandomString(int length)
        {
            var rng = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[rng.Next(s.Length)]).ToArray());
        }

    }
}
