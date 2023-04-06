using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using P229FirstApi.DTOs.AuthDTOs;
using P229FirstApi.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace P229FirstApi.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(IMapper mapper, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            AppUser appUser = _mapper.Map<AppUser>(registerDto);

            await _userManager.CreateAsync(appUser,registerDto.Password);
            await _userManager.AddToRoleAsync(appUser, "Member");

            return Ok("User Yaradildi");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(loginDto.Email);

            //if (!await _userManager.CheckPasswordAsync(appUser,loginDto.Password))
            //{

            //}

            List<Claim> claims = new List<Claim> { 
                new Claim(ClaimTypes.Name,appUser.UserName),
                new Claim(ClaimTypes.Email,appUser.Email),
                new Claim(ClaimTypes.NameIdentifier,appUser.Id),
            };

            var roles = await _userManager.GetRolesAsync(appUser);

            foreach (var item in roles)
            {
                Claim claim = new Claim(ClaimTypes.Role, item);

                claims.Add(claim);
            }

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                claims:claims, signingCredentials:signingCredentials,expires:DateTime.Now.AddHours(4));

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string token = handler.WriteToken(jwtSecurityToken);

            return Ok(token);
        }

        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
            await _roleManager.CreateAsync(new IdentityRole("Member"));

            return Ok();
        }
    }
}
