using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PracticeInventory.WebUI.Infrastructure;
using PracticeInventory.WebUI.Models.DTO.Account;
using PracticeInventory.WebUI.Models.DTO.Role;
using PracticeInventory.WebUI.Models.DTO.User;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using static PracticeInventory.WebUI.Infrastructure.AgentConfig;

namespace PracticeInventory.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ValidateToken()
        {
            var token = RequestManager.GetToken(_httpContextAccessor);
            if (!String.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var claims = new UserClaim()
                {
                    UserName = jwtSecurityToken.Claims.Where(x => x.Type.Contains("name")).FirstOrDefault().Value,
                    RoleName = jwtSecurityToken.Claims.Where(x => x.Type.Contains("role")).FirstOrDefault().Value
                };
                return Json(claims);
            }
            return Unauthorized("You are not authenticated.");
        }
        public async Task<IActionResult> Register(RegisterDTO payload)
        {
            var requestUrl = Account.Register;
            var response = await RequestManager.PostRequestAnonymous(_config, requestUrl, payload);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Json("Success Registration");
            }
            return BadRequest(await response.Content.ReadAsStringAsync());
        }
        public async Task<IActionResult> Login(LoginDTO payload)
        {
            var requestUrl = Account.Login;
            var response = await RequestManager.PostRequestAnonymous(_config, requestUrl, payload);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var token = await response.Content.ReadAsStringAsync();
                RequestManager.SetTokenCookie(this._httpContextAccessor, token);
                return Json("Success Login.");
            }
            return Unauthorized("Invalid credentials.");
        }
        public IActionResult Logout()
        {
            RequestManager.ClearAllCookies(_httpContextAccessor);
            return Json("Cookies has been cleared.");
        }
        public async Task<IActionResult> GetUsers()
        {
            var requestUrl = Users.GetAll;
            var response = await RequestManager.GetRequest(_config, _httpContextAccessor, requestUrl);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<UserDTO>>(json);
                return Json(users);
            }
            else if(response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized("You are not authenticated.");
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return Forbid("You don't have permission.");
            }
            var retursn = await response.Content.ReadAsStringAsync();
            return BadRequest(retursn);
        }
        public async Task<IActionResult> GetUser(string UserId)
        {
            var requestUrl = Users.GetDetail + $"/{UserId}";
            var response = await RequestManager.GetRequest(_config, _httpContextAccessor, requestUrl);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserDTO>(json);
                return Json(user);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized("You are not authenticated.");
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return Forbid("You don't have permission.");
            }
            return BadRequest(await response.Content.ReadAsStringAsync());
        }
        public async Task<IActionResult> GetRoles()
        {
            var requestUrl = Roles.GetAll;
            var response = await RequestManager.GetRequest(_config, _httpContextAccessor, requestUrl);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<List<RoleDTO>>(json);
                return Json(roles);
            }
            return BadRequest(await response.Content.ReadAsStringAsync());
        }
        public async Task<IActionResult> AddUser(AddUserDTO payload)
        {
            var requestUrl = Users.Add;
            var response = await RequestManager.PostRequest(_config, _httpContextAccessor, requestUrl, payload);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Json("User has been added.");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized("You are not authenticated.");
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return Forbid("You don't have permission.");
            }
            return BadRequest(await response.Content.ReadAsStringAsync());
        }
        public async Task<IActionResult> UpdateUser(UpdateUserDTO payload)
        {
            var requestUrl = Users.Update;
            var response = await RequestManager.PutRequest(_config, _httpContextAccessor, requestUrl, payload);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Json("User has been updated.");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized("You are not authenticated.");
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return Forbid("You don't have permission.");
            }
            return BadRequest(await response.Content.ReadAsStringAsync());
        }
        public async Task<IActionResult> DeleteUser(string UserId)
        {
            var requestUrl = Users.Delete + $"/{UserId}";
            var response = await RequestManager.DeleteRequest(_config, _httpContextAccessor, requestUrl);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Json("User has been deleted.");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Unauthorized("You are not authenticated.");
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return Forbid("You don't have permission.");
            }
            return BadRequest(await response.Content.ReadAsStringAsync());
        }
    }
}