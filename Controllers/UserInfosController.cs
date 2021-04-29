using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace backendApi.Controllers
{

    [ApiController]

    public class UserInfosController : ControllerBase
    {
        private UserInfoData.IUserInfoData _userInfoData;
        private IWebHostEnvironment _webhost;
        public UserInfosController(UserInfoData.IUserInfoData userInfoData, IWebHostEnvironment webhost)
        {
           _userInfoData = userInfoData;
            _webhost = webhost;
        }
        [HttpGet]
        [Route("/api/[controller]")]

        public IActionResult GetUserInfo()
        {
            return Ok(_userInfoData.GetUserInfo());

        }

        [HttpGet]
        [Route("/api/[controller]/{id}")]

        public IActionResult GetUserInfo(Guid id)
        {
            var Products = _userInfoData.GetUserInfo(id);
            if (Products != null)
            {
                return Ok(Products);
            }

            return NotFound($"Product with id :{id} not found");

        }


        [HttpPost]
        [Route("/api/[controller]")]

        public async Task<IActionResult> GetUserInfoAsync(UserInfo info)
        {
            // Products.ImageName = await Post();
            _userInfoData.AddUserInfo(info);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + info.StuffId, info);
        }

        [HttpDelete]
        [Route("/api/[controller]/{id}")]

        public IActionResult DeleteProducts(Guid id)
        {
            var user = _userInfoData.GetUserInfo(id);
            if (user != null)
            {
                _userInfoData.DeleteUserInfo(user);
                return Ok("Deleted sucessfully");
            }

            return NotFound($"Product with id :{id} not found");



        }

        [HttpPut]
        [Route("/api/[controller]/{id}")]

        public IActionResult EditProducts(Guid id, Models.UserInfo info)
        {
            var existing_user = _userInfoData.GetUserInfo(id);

            if (existing_user != null)
            {
                info.StuffId = existing_user.StuffId;
                _userInfoData.EditUserInfo(existing_user);
            }

            return Ok(info);




        }
    }
}