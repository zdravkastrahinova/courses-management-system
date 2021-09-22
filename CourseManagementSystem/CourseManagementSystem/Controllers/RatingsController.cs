using CourseManagementSystem.Models.Ratings;
using CourseManagementSystem.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementSystem.Controllers
{
    public class RatingsController : Controller
    {
        private readonly IRatingsService service;

        public RatingsController(IRatingsService service)
        {
            this.service = service;
        }

        public IActionResult Rate(int courseId)
        {
            RatingEditViewModel model = new RatingEditViewModel
            {
                CourseId = courseId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Rate(RatingEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            service.Insert(model);

            return RedirectToAction("Details", "Courses", new { id = model.CourseId });
        }
    }
}
