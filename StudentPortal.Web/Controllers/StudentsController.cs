﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;



namespace StudentPortal.Web.Controllers
{
    public class StudentsController :Controller
    {
        private readonly ApplicationDbContext dbContext;

		public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed,


            };

            await dbContext.Stundents.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Stundents.ToListAsync();

            return View(students);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Stundents.FindAsync(id);

            return View(student);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await dbContext.Stundents.FindAsync(viewModel.Id);

            if(student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;
                await dbContext.SaveChangesAsync();
            }


            return RedirectToAction("List", "Students");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {  
            var student = await dbContext.Stundents.FindAsync(viewModel.Id);

            if(student is not null)
            {
                dbContext.Stundents.Remove(student);
                await dbContext.SaveChangesAsync();
            }
            
            return RedirectToAction("List", "Students");
        }
    }
}
