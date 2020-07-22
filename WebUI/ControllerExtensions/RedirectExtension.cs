using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ControllerExtensions
{
    public static class RedirectExtension
    {
        public static CustomActionResult Success(this RedirectToRouteResult instance, string message)
        {
            return new CustomActionResult(instance, message, "Success");
        }
        public static CustomActionResult Error(this RedirectToRouteResult instance, string message)
        {
            return new CustomActionResult(instance, message, "Error");
        }
        public static CustomActionResult Success(this RedirectResult instance, string message)
        {
            return new CustomActionResult(instance, message, "Success");
        }
        public static CustomActionResult Error(this RedirectResult instance, string message)
        {
            return new CustomActionResult(instance, message, "Error");
        }

        public static CustomViewResult Success(this ViewResult instance, string message)
        {
            return new CustomViewResult(instance, message, "Success");
        }
        public static CustomViewResult Error(this ViewResult instance, string message)
        {
            return new CustomViewResult(instance, message, "Error");
        }
    }
}