using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using Demo.Services.Interface;
using Demo.Common;
using Demo.ViewModels.Request;
using Demo.Dtos.Request;
using Demo.ViewModels.Response;

namespace DEMOWebApi.Controllers
{
    

    [Route("api/[controller]/[action]")]
    [ApiController]

    public class StudentController : ControllerBase
    {
        private readonly IStudentService _StudentService;
        private readonly IStudentMarksService _studentMarksService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public StudentController(
            IStudentService studentService,
            IStudentMarksService studentMarksService,
            IMapper mapper,
            ILogger logger
            )
        {
            _StudentService = studentService;
            this._studentMarksService = studentMarksService;
            _mapper = mapper;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<Result<string>> AddStudent(AddStudentViewModel studentViewModel)
        {
            try
            {
                
                _logger.LogInformation($"AddStudent Request Recived ==> {JsonConvert.SerializeObject(studentViewModel)} ");
                var studentDto = _mapper.Map<AddStudentDto>(studentViewModel);
                var result = await _StudentService.AddStudent(studentDto);
                _logger.LogInformation($"AddStudent Response Generated ==> {result} ");
                return new Result<string>
                {
                    Output = Helper.RecordSaved,
                    EntityId = result,
                    Status = ResponseStatus.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Raised {ex.Message}");
                return new Result<string>
                {
                    Errors = new List<Error>()
                    {
                        new Error()
                        {
                            ErrorMessage = ex.Message
                        }
                    },
                    Status = ResponseStatus.Exception
                };
            }

        }


        [HttpPost]
        public async Task<Result<string>> AddStudentAndMarks(AddStudentAndMarksViewModel studentAndMarksViewModel)
        {
            try
            {

                _logger.LogInformation($"AddStudentAndMarks Request Recived ==> {JsonConvert.SerializeObject(studentAndMarksViewModel)} ");
                var StudentAndMarksDto = _mapper.Map<AddStudentAndMarksDto>(studentAndMarksViewModel);
                StudentAndMarksDto.Student = _mapper.Map<AddStudentDto>(studentAndMarksViewModel);
                var result = await _StudentService.AddStudentAndMarks(StudentAndMarksDto);
                _logger.LogInformation($"AddStudentAndMarks Response Generated ==> {result} ");
                return new Result<string>
                {
                    Output = Helper.RecordSaved,
                    EntityId = result,
                    Status = ResponseStatus.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Raised {ex.Message}");
                return new Result<string>
                {
                    Errors = new List<Error>()
                    {
                        new Error()
                        {
                            ErrorMessage = ex.Message
                        }
                    },
                    Status = ResponseStatus.Exception
                };
            }

        }


        [HttpPost]
        public async Task<Result<string>> UpdateStudent(EditStudentViewModel editStudentViewModel)
        {
            try
            {
                _logger.LogInformation($"UpdateStudent Request Recived ==> {JsonConvert.SerializeObject(editStudentViewModel)} ");
                var Student = _mapper.Map<EditStudentDto>(editStudentViewModel);
                var result = await _StudentService.EditStudent(Student);
                _logger.LogInformation($"UpdateStudent Response Generated ==> {result} ");
                return new Result<string>
                {
                    Output = Helper.RecordUpdated,
                    EntityId = result,
                    Status = ResponseStatus.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Raised {ex.Message}");
                return new Result<string>
                {
                    Errors = new List<Error>()
                    {
                        new Error()
                        {
                            ErrorMessage = ex.Message
                        }
                    },
                    Status = ResponseStatus.Exception
                };
            }

        }

        
        [HttpPost]
        public async Task<Result<string>> DeleteStudent(DeleteEntityDto deleteEntityDto)
        {
            try
            {
                _logger.LogInformation($"DeleteStudent Request Recived ==> {JsonConvert.SerializeObject(deleteEntityDto)} ");
                var deleteStudent = _mapper.Map<DeleteEntityDto>(deleteEntityDto);
                var result = await _StudentService.DeleteStudent(deleteStudent);

                _logger.LogInformation($"DeleteStudent Response Generated ==> {result} ");
                return new Result<string>
                {
                    Output = Helper.RecordDeleted,
                    EntityId = result,
                    Status = ResponseStatus.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Raised {ex.Message}");
                return new Result<string>
                {
                    Errors = new List<Error>()
                    {
                        new Error()
                        {
                            ErrorMessage = ex.Message
                        }
                    },
                    Status = ResponseStatus.Exception
                };
            }

        }

        
        [HttpPost]
        public async Task<ResultMultipleRecords<IEnumerable<ReturnStudentMarksViewModel>>> GetMultipleStudentsRecords(GetFilteredRecordsViewModel getStudentRecordsViewModel)
        {
            try
            {
                _logger.LogInformation($"GetMultipleStudentsRecords Request Recived ==> {JsonConvert.SerializeObject(getStudentRecordsViewModel)} ");
                var getRecords = _mapper.Map<GetFilteredRecordsDto>(getStudentRecordsViewModel);
                var result = await _StudentService.GetMultipleStudentRecords(getRecords);
                _logger.LogInformation($"GetMultipleStudentsRecords Response Generated ==> {JsonConvert.SerializeObject(result)} ");

                if (result.Count == 0 || result == null)
                {
                    return new ResultMultipleRecords<IEnumerable<ReturnStudentMarksViewModel>>
                    {
                        Errors = new List<Error>()
                            {
                                new Error()
                                {
                                    ErrorMessage = Helper.NoRecordsFound
                                }
                            },

                        Status = ResponseStatus.Invalid
                    };
                }

                return new ResultMultipleRecords<IEnumerable<ReturnStudentMarksViewModel>>
                {
                    Output = _mapper.Map<IEnumerable<ReturnStudentMarksViewModel>>(result.Records),
                    Status = ResponseStatus.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Raised {ex.Message}");
                return new ResultMultipleRecords<IEnumerable<ReturnStudentMarksViewModel>>
                {
                    Errors = new List<Error>()
                    {
                        new Error()
                        {
                            ErrorMessage = ex.Message
                        }
                    },
                    Status = ResponseStatus.Exception
                };
            }

        }


        [HttpGet]
        public async Task<ResultMultipleRecords<IEnumerable<ReturnStudentAndMarksViewModel>>> GetAllStudentsAndTheirMarks()
        {
            try
            {
                _logger.LogInformation($"GetAllStudentsAndTheirMarks Request Recived");
                var result = await _StudentService.GetAllStudentAndTheirMarks();
                _logger.LogInformation($"GetAllStudentsAndTheirMarks Response Generated ==> {JsonConvert.SerializeObject(result)} ");

                if (result.Count == 0 || result == null)
                {
                    return new ResultMultipleRecords<IEnumerable<ReturnStudentAndMarksViewModel>>
                    {
                        Errors = new List<Error>()
                            {
                                new Error()
                                {
                                    ErrorMessage = Helper.NoRecordsFound
                                }
                            },

                        Status = ResponseStatus.Invalid
                    };
                }

                return new ResultMultipleRecords<IEnumerable<ReturnStudentAndMarksViewModel>>
                {
                    Output = _mapper.Map<IEnumerable<ReturnStudentAndMarksViewModel>>(result.Records),
                    Status = ResponseStatus.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Raised {ex.Message}");
                return new ResultMultipleRecords<IEnumerable<ReturnStudentAndMarksViewModel>>
                {
                    Errors = new List<Error>()
                    {
                        new Error()
                        {
                            ErrorMessage = ex.Message
                        }
                    },
                    Status = ResponseStatus.Exception
                };
            }

        }



        [HttpGet]
        public async Task<IEnumerable<ReturnStudentAndMarksViewModel>> GetAllStudentsAndTheirMarksTemp()
        {
            try
            {
                _logger.LogInformation($"GetAllStudentsAndTheirMarks Request Recived");
                var result = await _StudentService.GetAllStudentAndTheirMarks();
                _logger.LogInformation($"GetAllStudentsAndTheirMarks Response Generated ==> {JsonConvert.SerializeObject(result)} ");

                if (result.Count == 0 || result == null)
                {
                    return new List<ReturnStudentAndMarksViewModel>();
                }

                return _mapper.Map<IEnumerable<ReturnStudentAndMarksViewModel>>(result.Records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Raised {ex.Message}");
                return new List<ReturnStudentAndMarksViewModel>();
            }

        }



        [HttpPost]
        public async Task<Result<string>> AddStudentMarks(AddStudentMarksViewModel StudentMarksViewModel)
        {
            try
            {

                _logger.LogInformation($"AddStudentMarks Request Recived ==> {JsonConvert.SerializeObject(StudentMarksViewModel)} ");
                var StudentMarksDto = _mapper.Map<AddStudentMarksDto>(StudentMarksViewModel);
                var result = await _studentMarksService.AddStudentMarks(StudentMarksDto);
                _logger.LogInformation($"AddStudentMarks Response Generated ==> {result} ");
                return new Result<string>
                {
                    Output = Helper.RecordSaved,
                    EntityId = result,
                    Status = ResponseStatus.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Raised {ex.Message}");
                return new Result<string>
                {
                    Errors = new List<Error>()
                    {
                        new Error()
                        {
                            ErrorMessage = ex.Message
                        }
                    },
                    Status = ResponseStatus.Exception
                };
            }

        }


        [HttpPost]
        public async Task<Result<string>> UpdateStudentMarks(EditStudentMarksViewModel editStudentMarksViewModel)
        {
            try
            {
                _logger.LogInformation($"UpdateStudentMarks Request Recived ==> {JsonConvert.SerializeObject(editStudentMarksViewModel)} ");
                var StudentMarks = _mapper.Map<EditStudentMarksDto>(editStudentMarksViewModel);
                var result = await _studentMarksService.EditStudentMarks(StudentMarks);
                _logger.LogInformation($"UpdateStudentMarks Response Generated ==> {result} ");
                return new Result<string>
                {
                    Output = Helper.RecordUpdated,
                    EntityId = result,
                    Status = ResponseStatus.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Raised {ex.Message}");
                return new Result<string>
                {
                    Errors = new List<Error>()
                    {
                        new Error()
                        {
                            ErrorMessage = ex.Message
                        }
                    },
                    Status = ResponseStatus.Exception
                };
            }

        }


        [HttpPost]
        public async Task<Result<string>> DeleteStudentMarks(DeleteEntityDto deleteEntityDto)
        {
            try
            {
                _logger.LogInformation($"DeleteStudentMarks Request Recived ==> {JsonConvert.SerializeObject(deleteEntityDto)} ");
                var deleteStudentMarks = _mapper.Map<DeleteEntityDto>(deleteEntityDto);
                var result = await _studentMarksService.DeleteStudentMarks(deleteStudentMarks);

                _logger.LogInformation($"DeleteStudentMarks Response Generated ==> {result} ");
                return new Result<string>
                {
                    Output = Helper.RecordDeleted,
                    EntityId = result,
                    Status = ResponseStatus.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception Raised {ex.Message}");
                return new Result<string>
                {
                    Errors = new List<Error>()
                    {
                        new Error()
                        {
                            ErrorMessage = ex.Message
                        }
                    },
                    Status = ResponseStatus.Exception
                };
            }

        }


    }
}
