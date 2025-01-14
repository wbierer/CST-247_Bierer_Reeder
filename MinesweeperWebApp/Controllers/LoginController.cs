﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MinesweeperWebApp.Models;
using MinesweeperWebApp.Services.Business;

//Created by: William Bierer & Stuart Reeder
//Contains all Views that pertain to logging in
namespace MinesweeperWebApp.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View("Login");
        }

        //Log a user in and return the action result
        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            UserSecurityService service = new UserSecurityService();
            if (service.Authenticate(user))
            {
                //Add userID to the session
                user.Id = service.GetIdFromName(user.Username);
                Session.Add("UserId", user.Id);
                return Redirect("/Home");
            }
            else
                return View("LoginFailed");
        }

        public ActionResult Logout()
        {
            Session["UserID"] = 0;
            Session.Abandon();
            return Redirect("/Home");
        }
    }
}