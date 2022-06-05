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
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<StudentMarks> _studentMarksRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public StudentService(IRepository<Student> studentRepository,
            IRepository<StudentMarks> studentMarksRepository,
            IMapper mapper,
            ILogger logger)
        {
            this._studentRepository = studentRepository;
            this._studentMarksRepository = studentMarksRepository;
            this._mapper = mapper;
            this._logger = logger;
        }
        public async Task<Guid> AddStudent(AddStudentDto registerStudentDto)
        {
            var student=_mapper.Map<Student>(registerStudentDto);
            await _studentRepository.InsertAsync(student);
            await _studentRepository.SaveAsync();
            return student.Id;
        }

        public async Task<Guid> AddStudentAndMarks(AddStudentAndMarksDto addStudentAndMarksDto)
        {
            var student = _mapper.Map<Student>(addStudentAndMarksDto.Student);
            await _studentRepository.InsertAsync(student);
            await _studentRepository.SaveAsync();

            var studentMarks = _mapper.Map<StudentMarks>(addStudentAndMarksDto);
            studentMarks.StudentId = student.Id;
            await _studentMarksRepository.InsertAsync(studentMarks);
            await _studentMarksRepository.SaveAsync();
            return student.Id;
        }

        public async Task<Guid> DeleteStudent(DeleteEntityDto deleteEntityDto)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(deleteEntityDto.Id);
            if (existingStudent != null)
            {
                existingStudent.IsDeleted = true;
                await _studentRepository.SaveAsync();
                return existingStudent.Id;
            }
            return Guid.Empty;
        }

        public async Task<Guid> EditStudent(EditStudentDto editStudentDto)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(editStudentDto.Id);
            if (existingStudent != null)
            {
                _mapper.Map(editStudentDto, existingStudent);
                existingStudent.LastModifiedDate = DateTime.Now;
                await _studentRepository.SaveAsync();
                return existingStudent.Id;
            }

            return Guid.Empty;
        }

        public async Task<MultipleRecordsAndCount<IEnumerable<ReturnStudentAndMarksDto>>> GetAllStudentAndTheirMarks()
        {
            var returnStudentAndMarksDtoList = new List<ReturnStudentAndMarksDto>();

            var students = await _studentRepository.NonDeletedEntity.ToListAsync();

            _mapper.Map(students, returnStudentAndMarksDtoList);

            var studentsMarks = await _studentMarksRepository.NonDeletedEntity
           .ToListAsync();

            foreach (var returnStudentAndMarksDto in returnStudentAndMarksDtoList)
            {
                var studentMarksModel = studentsMarks.Where(x => x.StudentId.Equals(returnStudentAndMarksDto.StudentId)).FirstOrDefault();
                if (studentMarksModel != null)
                {
                    _mapper.Map(studentMarksModel, returnStudentAndMarksDto);
                }
            }

            return new MultipleRecordsAndCount<IEnumerable<ReturnStudentAndMarksDto>>
            {
                Count = returnStudentAndMarksDtoList.Count(),
                Records = returnStudentAndMarksDtoList
            };
        }

        public async Task<MultipleRecordsAndCount<IEnumerable<ReturnStudentMarksDto>>> GetMultipleStudentRecords(GetFilteredRecordsDto getStudenttRecordsDto)
        {
            var returnStudentMarksDtoList = new List<ReturnStudentMarksDto>();

            var students = _studentRepository.NonDeletedEntity;

            foreach (var student in students)
            {
                var returnStudentMarksDto=new ReturnStudentMarksDto();
                returnStudentMarksDto.Student = _mapper.Map<EditStudentDto>(student);
                returnStudentMarksDtoList.Add(returnStudentMarksDto);
            }

            var studentsMarks = await _studentMarksRepository.NonDeletedEntity
              .ToListAsync();

            foreach (var returnStudentMarksDto in returnStudentMarksDtoList)
            {
                var studentMarksRecordExsists=studentsMarks.FirstOrDefault(x => x.StudentId.Equals(returnStudentMarksDto.Student.Id));
                if (studentMarksRecordExsists != null)
                {
                    _mapper.Map(studentMarksRecordExsists, returnStudentMarksDto);
                    
                }
            }



            if (getStudenttRecordsDto.SearchFilter?.MarksAbove > 0)
            {
                returnStudentMarksDtoList = returnStudentMarksDtoList.Where(x => x.Marks >= getStudenttRecordsDto.SearchFilter.MarksAbove).ToList();
            }
            if (getStudenttRecordsDto.SearchFilter?.Standard > 0)
            {
                returnStudentMarksDtoList = returnStudentMarksDtoList.Where(x => x.Student.Standard.Equals(getStudenttRecordsDto.SearchFilter.Standard)).ToList();
            }
            if (getStudenttRecordsDto.SearchFilter?.Subject > 0)
            {
                returnStudentMarksDtoList = returnStudentMarksDtoList.Where(x => x.Student.Standard.Equals(getStudenttRecordsDto.SearchFilter.Subject)).ToList();
            }
            if (!string.IsNullOrEmpty(getStudenttRecordsDto.SearchFilter?.Name))
            {
                returnStudentMarksDtoList = returnStudentMarksDtoList.Where(x => x.Student.FirstName.Contains(getStudenttRecordsDto.SearchFilter.Name) ||
                                                         x.Student.LastName.Contains(getStudenttRecordsDto.SearchFilter.Name)
                                                      ).ToList();
            }

            int totalCount = returnStudentMarksDtoList.Count();

          
            return new MultipleRecordsAndCount<IEnumerable<ReturnStudentMarksDto>>
            {
                Count = totalCount,
                Records = returnStudentMarksDtoList
            };

        }
    }
}
