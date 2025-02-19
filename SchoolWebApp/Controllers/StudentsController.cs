﻿using Microsoft.AspNetCore.Mvc;
using SchoolWebApp.DTO;
using SchoolWebApp.Services;

namespace SchoolWebApp.Controllers {
    public class StudentsController : Controller {
        private StudentService _studentService;

        public StudentsController(StudentService studentService) {
            _studentService = studentService;
        }

        public IActionResult Index() {
            IEnumerable<StudentDTO> allStudents = _studentService.GetStudents();
            return View(allStudents);
        }

        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(StudentDTO studentDTO) { 
           await _studentService.AddStudentAsync(studentDTO);
            return RedirectToAction("Index");
        }

        public async Task <IActionResult> UpdateAsync(int id) {
            var studentToEdit = await _studentService.GetStudentByIdAsync(id);
            if (studentToEdit == null) {
                return View("NotFound");
            }
            return View(studentToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Update(StudentDTO studentDTO, int id) {
            await _studentService.UpdateAsync(id, studentDTO);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) {
            var studentToDelete = await _studentService.GetStudentByIdAsync(id);
            if (studentToDelete == null) {
                return View("NotFound");
            }
            await _studentService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }

}
