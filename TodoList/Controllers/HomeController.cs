using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var tasks = Repository.TodoList;
            return View(tasks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                Repository.AddTodo(todo);
                TempData["SuccessMessage"] = "Task added successfully!";
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        public IActionResult Update(string title)
        {
            var task = Repository.TodoList.FirstOrDefault(t => t.title == title);

            if (task == null)
            {
                return NotFound(); // Handle not found
            }

            return View(task);

        }

        [HttpPost]
        public IActionResult Update(Todo updatedTask)
        {
            if (ModelState.IsValid)
            {
                var task = Repository.TodoList.FirstOrDefault(t => t.title == updatedTask.title);

                if (task == null)
                {
                    return NotFound(); // Handle not found
                }

                // Update the task with the values from updatedTask
                task.title = updatedTask.title;
                task.description = updatedTask.description;
                task.isCompleted = updatedTask.isCompleted;

                // Redirect to the task list (Index)
                return RedirectToAction("Index");


            }

            //   return View(updatedTask); // Return to the editing form with validation errors
            return RedirectToAction("Index");
        }
        // Note that feature
        [HttpPost]
        public IActionResult Delete(string title) 
        {
            var task = Repository.TodoList.FirstOrDefault(t => t.title.Equals(title));
            if (task == null)
            {
                return NotFound();
            }
            Repository.RemoveTodo(task);
            return RedirectToAction("Index");
        }

        // Filtering feature
        
        [HttpPost]
        public IActionResult UpdateStatus(string title, bool isCompleted)
        {
            var task = Repository.TodoList.FirstOrDefault(t => t.title == title);

            if (task == null)
            {
                return NotFound(); // Handle not found
            }

            task.isCompleted = isCompleted;

            // Save changes to the repository (e.g., you can persist this to a database)
            // Repository.SaveChanges(); // Implement a method to save changes

            return Ok(); // Return success status
        }





    }
}