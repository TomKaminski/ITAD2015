using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Itad2015.Areas.Admin.ViewModels;
using Itad2015.Contract.DTO.GetDto;
using Itad2015.Contract.DTO.PostDto;
using Itad2015.Contract.Service.Entity;
using Itad2015.Infrastructure.Attributes;

namespace Itad2015.Areas.Admin.Controllers
{
    [SuperAdminAuthorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Admin/User
        public ActionResult Index()
        {
            var userList = _userService.GetAll().Result.Select(Mapper.Map<UserListViewModel>);
            return View(userList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userService.Create(Mapper.Map<UserPostDto>(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var user = Mapper.Map<UserEditViewModel>(_userService.Get(id).Result);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                _userService.Edit(Mapper.Map<UserPostDto>(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var user = Mapper.Map<UserListViewModel>(_userService.Get(id).Result);
            return View(user);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            _userService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}