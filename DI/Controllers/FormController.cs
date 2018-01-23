using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DI.Controllers
{
    public class FormController : Controller
    {
        // GET: Form
        public ActionResult EntryForm()
        {
            return View();
        }
    }
}