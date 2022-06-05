using AutoMapper;
using Demo.Common;
using Demo.Db.Repository;
using Demo.Dtos.Request;
using Demo.Dtos.Response;
using Demo.Models;
using Demo.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Demo.Services.Implementation
{
    public class StudentMarksService : IStudentMarksService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<StudentMarks> _studentMarksRepository;
        
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public StudentMarksService(
            IRepository<Student> StudentRepository,
            IRepository<StudentMarks> StudentMarksRepository,
            IMapper mapper,
            ILogger logger)
        {
            _studentRepository = StudentRepository;
            this._studentMarksRepository = StudentMarksRepository;
            this._mapper = mapper;
            this._logger = logger;
        }
        public async Task<Guid> AddStudentMarks(AddStudentMarksDto registerStudentMarksDto)
        {
            var studentExists = await _studentRepository.GetByIdAsync(registerStudentMarksDto.StudentId);
            if (studentExists == null)
            {
                _logger.LogInformation($"No student registerd with id {registerStudentMarksDto.StudentId}");
                return Guid.Empty;
            }

            var StudentMarks = _mapper.Map<StudentMarks>(registerStudentMarksDto);
            await _studentMarksRepository.InsertAsync(StudentMarks);
            await _studentMarksRepository.SaveAsync();
            return StudentMarks.Id;
        }

        public async Task<Guid> DeleteStudentMarks(DeleteEntityDto deleteEntityDto)
        {
            var existingStudentMarks = await _studentMarksRepository.GetByIdAsync(deleteEntityDto.Id);
            if (existingStudentMarks != null)
            {
                existingStudentMarks.IsDeleted = true;
                await _studentMarksRepository.SaveAsync();
                return existingStudentMarks.Id;
            }
            return Guid.Empty;
        }

        public async Task<Guid> EditStudentMarks(EditStudentMarksDto editStudentMarksDto)
        {
            var existingStudentMarks = await _studentMarksRepository.GetByIdAsync(editStudentMarksDto.Id);
            if (existingStudentMarks != null)
            {
                _mapper.Map(editStudentMarksDto, existingStudentMarks);
                existingStudentMarks.LastModifiedDate = DateTime.Now;
                await _studentMarksRepository.SaveAsync();
                return existingStudentMarks.Id;
            }

            return Guid.Empty;
        }

    
    }
}
