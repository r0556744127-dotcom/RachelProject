using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories.InterFaces;
using Repositories.models;
using Repositories.Repositories;
using service.Dto;
using service.interfaces;
using service.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Implementations
{

    public class AssignmentService : IAssignmentService
    {
        private readonly AssignmentRepository _assignmentRepository;
        private readonly IMapper _mapper;//מתרגם מ Dto ל model
        public AssignmentService(AssignmentRepository assignmentRepository, IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
        }

        public async Task<Assignment> GetAssignmentById(int assignmentId)
        {
            return await _assignmentRepository.GetById(assignmentId);
        }


        public async Task<bool> CreateAssignmentAsync(CreateAssignmentDto assignmentDto)
        {
            // 1. בדיקה אם קיים - תקין
            var exitingAssignment = await _assignmentRepository.GetById(assignmentDto.Id);
            if (exitingAssignment != null)
                return false;

            string savedPath = null;
            if (assignmentDto.File != null)
            {
                var fileName = $"{Guid.NewGuid()}_{assignmentDto.File.FileName}";
                // כדאי להשתמש ב-wwwroot כדי שהקבצים יהיו נגישים להורדה בדפדפן
                var path = Path.Combine("wwwroot", "Uploads", fileName);

                // ודאי שהתיקייה קיימת
                var directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await assignmentDto.File.CopyToAsync(stream);
                }

                savedPath = fileName; // שומרים רק את שם הקובץ או הנתיב היחסי
            }

            // 2. המרה מה-DTO למודל
            var assignment = _mapper.Map<Assignment>(assignmentDto);

            // 3. התיקון הקריטי: עדכון הנתיב בתוך האובייקט שהולך ל-DB
            assignment.FilePath = savedPath;

            // 4. שמירה בבסיס הנתונים
            await _assignmentRepository.AddItem(assignment);
            return true;
        }
        public async Task<List<TeacherAssignmentDto>> GetAssignmentsByTeacherAsync(int teacherId)
        {
      
            var allAssignments = await _assignmentRepository.GetAllAsync();

  
            var teacherAssignments = allAssignments.Where(a => a.Id == teacherId).ToList();

  
            return _mapper.Map<List<TeacherAssignmentDto>>(teacherAssignments);
        }
        public async Task<bool> DeleteAssignmentAsync(int assignmentId)
        {
            var delAssignment = await _assignmentRepository.GetById(assignmentId);
            if (delAssignment == null)
                return false;
            await _assignmentRepository.DeleteItem(assignmentId);
            return true;
        }
        public async Task<bool> UpdateAssignmentAsync(int id, TeacherAssignmentDto assignmentDto)
        {
            var assignment = _mapper.Map<Assignment>(assignmentDto);
            await _assignmentRepository.UpdateItem(id, _mapper.Map<Assignment>(assignmentDto));
            return true;
        }
    }

}
