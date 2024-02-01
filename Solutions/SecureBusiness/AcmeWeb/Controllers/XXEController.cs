using AcmeWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Xml;

namespace AcmeWebsite.Controllers
{
    public class XXEController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.result = "";
            return View(new XXEModel());
        }

        [HttpPost]
        public IActionResult UploadXml(XXEModel model)
        {
            var settings = new XmlReaderSettings();
            settings.DtdProcessing = model.ParseDtd ? DtdProcessing.Parse : DtdProcessing.Ignore;
            settings.MaxCharactersFromEntities = model.MaxChars;
            var start = DateTime.Now;
            using (var parser = XmlReader.Create(model.File.OpenReadStream(), settings))
            {
                parser.MoveToContent();
                var content = parser.ReadInnerXml();
            }
            var ms = DateTime.Now.Subtract(start);

            model.ElapsedTime = ms.TotalMilliseconds;
            return View("Index", model);
        }
    }
}