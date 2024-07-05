using CashFlow.Domain.Entities;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Domain.Services.LoggedUser;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CashFlow.Infrastructure.Services.LoggedUser
{
    public class LoggedUser : ILoggedUser
    {
        private readonly CashFlowDbContext _dbcontext;
        private readonly ITokenProvider _tokenProvider;

        public LoggedUser(CashFlowDbContext dbContext, ITokenProvider tokenProvider)
        {
            _dbcontext = dbContext;
            _tokenProvider = tokenProvider;
        }
        public async Task<User> Get()
        {
            string token = _tokenProvider.TokenOnRequest();

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecurityTOken = tokenHandler.ReadJwtToken(token);

            var identifier = jwtSecurityTOken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

            return await _dbcontext.User.AsNoTracking().FirstAsync(user => user.Guid == Guid.Parse(identifier));
        }
    }
}
