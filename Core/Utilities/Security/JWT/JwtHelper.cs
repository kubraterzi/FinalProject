using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; set; } // appsettings.json içerisindeki TokenOptions bilgilerine ulaşmak için oluşturduk.
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();// appsetting.json içerisindeki Audience, Issuer, AccesTokenExpiration ve
                                                                                         // SecurityKey bilgilerini çektik ve Get komutu ile TokenOptions class ımızdaki
                                                                                         // property ler ile mapledik.
        }
        
        
        
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);// oturumun açık kalma süresini ayarlamak için, token üretildiği andan itibaren appsettings.json içerisinde belirtilen süreyi tanımladık.
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey); // güvenlik anahtarına ihtiyacım var -> createsecuritykey ile ürettik
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey); // hangi algoritmayı kullanayım, anahtar nedir? -> createsigningcredentials ile belirttik
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims); 

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            return new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now, 
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c=> c.Name).ToArray());

            return claims;
        }
    }
}