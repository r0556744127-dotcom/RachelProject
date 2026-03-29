using AutoMapper;
using Repositories.models;
using Repositories.Repositories;
using service.Dto;
using service.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Implementations
{
    public class SubmissionService : IsubmissionService
    {
        private readonly SubmissionRepository _submissionRepository;
        private readonly AssignmentRepository _assignmentRepository;
        private readonly IMapper _mapper;
        public SubmissionService(SubmissionRepository submissionRepository, AssignmentRepository assignmentRepository, IMapper mapper)
        {
            _submissionRepository = submissionRepository;
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteSubmissionAsync(int submissionId)
        {
            var submission = await _submissionRepository.GetById(submissionId);
            if (submission == null)
            {
                return false;
            }
            await _submissionRepository.DeleteItem(submissionId);
            return true;
        }
        //// עדכון ציון להגשה ספציפית
        //Task<bool> UpdateSubmissionGradeAsync(int submissionId, UpdateGradeDto gradeData);
        public async Task<StudentSubmissionDto> GetSubmission(int submissionId)
        {
            var submission1 = await _submissionRepository.GetById(submissionId);
            if (submission1 == null)
                return null;
            var submission2 = _mapper.Map<StudentSubmissionDto>(submission1);
            return submission2;
        }

        public Task<bool> SubmitAssignmentAsync(int studentId, CreateSubmissionDto submissionData)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SubmitSubmissionAsync(int studentId, CreateSubmissionDto submissionData)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{submissionData.File.FileName}";
            var folderPath = Path.Combine("Submissions"); // תיקיית שמירת הקבצים
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await submissionData.File.CopyToAsync(stream);
            }

            var submission = _mapper.Map<Submission>(submissionData);
            submission.FilePath = filePath;
            submission.SubmittedAt = DateTime.Now; // זמן הגשה
            submission.Grade = 0;
            await _submissionRepository.AddItem(submission);

            return true;
        }
        public async Task<bool> UpdateSubmissionGradeAsync(int submissionId, UpdateGradeDto gradeData)
        {
            if(submissionId ==0)
                return false;
            await _submissionRepository.UpdateItem(submissionId, _mapper.Map<Submission>(gradeData));
            return true;
        }
        public async Task UpdateSubmissionTeacher(int id, TeacherSubmissionDto item)
        {
            await _submissionRepository.UpdateItem(id, _mapper.Map<Submission>(item));
        }
       
        public async Task UpdateSubmissionStudent(int id,StudentSubmissionDto  item)
        {
            await _submissionRepository.UpdateItem(id, _mapper.Map<Submission>(item));

        }
    }
}
