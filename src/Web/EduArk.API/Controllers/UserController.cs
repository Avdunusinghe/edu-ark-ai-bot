using EduArk.Application.DTOs.ClassDTOs;
using EduArk.Application.DTOs.CommonDTOs;
using EduArk.Application.DTOs.StudentDTOs;
using EduArk.Application.DTOs.UserDTOs;
using EduArk.Application.Pipelines.Students.Commands.SaveStudentAcademicBehavior;
using EduArk.Application.Pipelines.Students.Queries.GetStudentAcademicBehavior;
using EduArk.Application.Pipelines.Students.Queries.GetStudentDataById;
using EduArk.Application.Pipelines.Users.Commands.DeleteUser;
using EduArk.Application.Pipelines.Users.Commands.SaveUser;
using EduArk.Application.Pipelines.Users.Commands.UploadClassStudents;
using EduArk.Application.Pipelines.Users.Commands.UploadUserProfileImage;
using EduArk.Application.Pipelines.Users.Queries.GetStudentsByFilter;
using EduArk.Application.Pipelines.Users.Queries.GetUserByCurrentUserId;
using EduArk.Application.Pipelines.Users.Queries.GetUserDetailsByFilter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduArk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost("saveUser")]
        public async Task<IActionResult> SaveUser([FromBody] UserDetailsDTO dto)
        {
            var respose = await _mediator.Send(new SaveUserCommand(dto));

            return Ok(respose);
        }

        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _mediator.Send(new DeleteUserCommand(id));

            return Ok(response);
        }

        [HttpPost("getAllUsersByFilter")]
        public async Task<IActionResult> GetUserDetailsByFilter(UserDetailsFilterDTO filter)
        {
            var response = await _mediator.Send(new GetUserDetailsByFilterQuery(filter));

            return Ok(response);
        }

        [HttpGet("getUserByCurrentUserId")]
        public async Task<IActionResult> GetUserByCurrentUserId()
        {
            var response = await _mediator.Send(new GetUserByCurrentUserIdQuery());

            return Ok(response);
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        [Route("uploadUserProfileImage")]
        public async Task<IActionResult> UploadUserProfileImage()
        {
            var request = await Request.ReadFormAsync();

            if (request.Files.Any())
            {

                var container = new BlobContainerDTO();

                foreach (var file in request.Files)
                {
                    container.Files.Add(file);
                }

                container.Id = int.Parse(request["id"]);

                var response = await _mediator
                            .Send(new UploadUserProfileImageCommand(container));

                return Ok(response);
            }
            else
            {
                return BadRequest(ResultDTO.Failure(new List<string>()
                    {
                        "Bad Request"
                    }));
            }
        }
      
        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        [Route("uploadClassStudents")]
        public async Task<IActionResult> UploadClassStudents()
        {
            var container = new FileContainerDTO();

            var request = await Request.ReadFormAsync();

            //container.Id = int.Parse(request["id"]);

            foreach (var file in request.Files)
            {
                container.Files.Add(file);
            }

            var response = await _mediator
                .Send(new UploadClassStudentsCommand()
                {
                    Container = container
                });



            return Ok(response);
        }

        [HttpPost("getStudentsByFilter")]
        public async Task<IActionResult> GetStudentsByFilter([FromBody] ClassStudentFilterDTO dto)
        {
            var response = await _mediator.Send(new GetStudentsByFilterQuery(dto));

            return Ok(response);
        }

        [HttpGet("getStudentAcademicBehavior/{studentId}")]
        public async Task<IActionResult> GetStudentAcademicBehavior(int studentId)
        {
            var response = await _mediator.Send(new GetStudentAcademicBehaviorQuery(studentId));

            return Ok(response);
        }

        [HttpPost("saveStudentAcademicBehavior")]
        public async Task<IActionResult> SaveStudentAcademicBehavior([FromBody] StudentAcademicBehaviorDTO dto)
        {
            var response = await _mediator.Send(new SaveStudentAcademicBehaviorCommand(dto));

            return Ok(response);
        }

        [HttpGet("getStudentDataById/{studentId}")]
        public async Task<IActionResult> GetStudentDataById(int studentId)
        {
            var response = await _mediator.Send(new GetStudentDataByIdQuery(studentId));

            return Ok(response);
        }
    }
}
