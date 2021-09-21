using CourseManagementSystem.Models.Categories;
using CourseManagementSystem.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementSystem.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService service;

        public CategoriesController(ICategoriesService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult List()
        {
            CategoryListViewModel model = new CategoryListViewModel 
            {
                Categories = service.GetCategories()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return View(new CategoryEditViewModel());
            }

            CategoryEditViewModel model = service.GetById(id.Value);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryEditViewModel model)
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

            if (!service.Exists(id.Value))
            {
                return RedirectToAction("List");
            }

            service.Delete(id.Value);

            return RedirectToAction("List");
        }
    }
}
