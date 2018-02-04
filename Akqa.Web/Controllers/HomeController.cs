using Akqa.Logic;
using Akqa.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Akqa.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly INumberConverter _numberConverter;

        public HomeController(INumberConverter numberConverter)
        {
            if (numberConverter == null)
            {
                 throw new ArgumentNullException(nameof(numberConverter));
            }

            _numberConverter = numberConverter;
        }

        /// <summary>
        /// default view - displays a form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            var view =  View();
            return view;
        }

        /// <summary>
        /// received posted form, does convertion and redirects to "Converted" view
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Index(ConvertingModel model)
        {
            if (!ModelState.IsValid)
            {                
                return View(model);
            }

            //does number convertion
            var converted = await _numberConverter.Convert(model.Number);

            // model is passed using TempData instead of url params to keep the url clean            
            TempData["model"] = new ConvertedModel
            {
                Name = model.Name,
                Text = converted
            };            


            return RedirectToAction("Converted");
        }

        /// <summary>
        /// Displays converted data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Converted()
        {
            var model = TempData["model"] as ConvertedModel;

            //if model is not pased, f.e when refreshng, redirects to default view
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            
            return View(model);

        }
    }
}