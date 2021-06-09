using Microsoft.AspNetCore.Authorization;
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

        [HttpGet] //This should have refresh Token and GET Code 
        [Route("/OAuth/Authorize")]
        public async Task<IActionResult> Authorize(
            string response_type, // authorization flow type 
            string client_id, // client id
            string redirect_uri,
            string scope, // what info I want = email,grandma,tel
            string state) // random string generated to confirm that we are going to back to the same client
        {
            state = "Random Auth Code ??";
            redirect_uri = "not null";
            // ?a=foo&b=bar
            var query = new QueryBuilder();
            query.Add("redirectUri", redirect_uri);
            query.Add("state", state);
            _logger.LogInformation(UriHelper.GetEncodedPathAndQuery(Request));

            return Content("<html> <form id=\"form\" action=\"/OAuth/Authorize" + query.ToString() +"\" method=\"POST\"> <input type=\"submit\" value=\"Submit\" /> <script> document.getElementById('form').submit() </script> </html>", "text/html");
        }

        [HttpPost]
        [Route("/OAuth/Authorize")]
        public async Task Authorize(
            string username,
            string redirectUri,
            string state)
        {
            const string code = "BABAABABABA";
           
            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);
            Response.StatusCode = 302;
            var kv = new KeyValuePair<string, StringValues>("Location", new StringValues("/OAuth/Token" + query.ToString()));
            Response.Headers.Add(kv);

            await Response.CompleteAsync();

            //return Redirect($"{redirectUri}{query.ToString()}");
        }

        [HttpPost]
        [Route("/OAuth/Token")] //Redirect to OG page
        public async Task Token(
            string grant_type, // flow of access_token request
            string code, // confirmation of the authentication process
            string redirect_uri,
            string client_id,
            string refresh_token)
        {
            // some mechanism for validating the code
            redirect_uri = "/OAuth/Authorize";
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
                new Claim("granny", "cookie")
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
                expires: grant_type == "refresh_token"
                    ? DateTime.Now.AddMinutes(5)
                    : DateTime.Now.AddMilliseconds(1),
                signingCredentials);

            var access_token = new JwtSecurityTokenHandler().WriteToken(token);

            var responseObject = new
            {
                access_token,
                token_type = "Bearer",
                raw_claim = "oauthTutorial",
                refresh_token = "RefreshTokenSampleValueSomething77"
            };

           

            var responseJson = JsonConvert.SerializeObject(responseObject);
            var responseBytes = Encoding.UTF8.GetBytes(responseJson);
            Response.StatusCode = 302;
            var kv = new KeyValuePair<string, StringValues>("Location", new StringValues("/Weatherforecast"));
            Response.Headers.Add(kv);
            await Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length); //Should 

            //return Redirect("WeatherForecast");
        }

        [HttpGet, Route("OAuth/{**catchAll}")]
        public IActionResult Get()
        {
            return Ok("CATCH ALL");
        }
        
        bool Validate(string accesToken)
        {
            if (HttpContext.Request.Query.TryGetValue("access_token", out var accessToken))
            {

                return true;
            }
            return false;
        }
    }
}
