using CourseManagementSystem.Models.Courses;
using CourseManagementSystem.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesService service;
        private readonly ICategoriesService categoriesService;

        public CoursesController(ICoursesService service, ICategoriesService categoriesService)
        {
            this.service = service;
            this.categoriesService = categoriesService;
        }

        public IActionResult List()
        {
            CourseListViewModel model = new CourseListViewModel
            {
                Courses = service.GetCourses()
            };

            return View(model);
        }

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List");
            }

            CourseDetailsViewModel model = service.GetCourse(id.Value);
            if (model == null)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }

        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return View(new CourseEditViewModel
                {
                    SelectedCategories = categoriesService.GetSelectableCategories()
                });
            }

            CourseEditViewModel model = service.GetById(id.Value);
            if (model == null)
            {
                return RedirectToAction("List");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CourseEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Id == 0)
            {
                service.Insert(model);

                return RedirectToAction("List");
            }

            if (!service.Exists(model.Id))
            {
                return RedirectToAction("List");
            }

            service.Update(model);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("List");
            }

            service.Delete(id.Value);

            return RedirectToAction("List");
        }
    }
}
