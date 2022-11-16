using AebTestProject.Models.Request;
using AebTestProject.Models.Response;
using AebTestProject.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AebTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskRepository _repository;
        private readonly IMapper _mapper;
        public TaskController(ITaskRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Добавить задачу
        /// </summary>
        /// <param name="taskCreate"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public IActionResult Add([FromBody] CreateTask taskCreate)
        {
            _repository.CreateTask(_mapper.Map<Models.Task>(taskCreate));
            return Ok("Запись добавлена");
        }

        /// <summary>
        /// Вернуть все имеющиеся задачи
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTasks")]
        public IActionResult GetAll()
        {
            var tasks = _repository.GetTasksList();
            return Ok(tasks.Select(_mapper.Map<Models.Task, TaskResponseModel>).ToList());
        }

        /// <summary>
        /// Вернуть задачу по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetTask")]
        public async Task<IActionResult> GetTaskAsync(Guid id)
        {
            var task = await _repository.GetTaskAsync(id);
            if (task != null)
            {
                return Ok(_mapper.Map<TaskResponseModel>(task));
            }
            else
            {
                return NotFound("Not Found");
            }
        }

        /// <summary>
        /// Обновить данные задачи
        /// </summary>
        /// <param name="id"></param>
        /// <param name="taskUpdate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPut("Update")]
        public IActionResult UpdateTask(Guid id, [FromBody] UpdateTask taskUpdate)
        {
            if (id != taskUpdate.Id)
            {
                throw new Exception("BAD_REQUEST");
            }

            else
            {
                _repository.UpdateTask(_mapper.Map<Models.Task>(taskUpdate));
                return Ok(_mapper.Map<TaskResponseModel>(_repository.GetTaskAsync(id)));
            }
        }

        /// <summary>
        /// Удалить задачу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        public IActionResult Delete(Guid id)
        {
            _repository.DeleteTask(_repository.GetTaskAsync(id).Result);
            return Ok();
        }

        /// <summary>
        /// Пагинация
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetPage")]
        public IActionResult GetTasksPage(int pageIndex, int pageSize = 30)
        {
            var tasks = _repository.GetTasksPage(pageIndex, pageSize);
            if(tasks.Count !=0)
            return Ok(tasks.Select(_mapper.Map<Models.Task, TaskResponseModel>).ToList());
            return NotFound("Ничего не найдено");
        }

        /// <summary>
        /// Отбор по полям
        /// </summary>
        /// <param name="getTaskByFilter"></param>
        /// <returns></returns>
        [HttpGet("GetFilter")]
        public IActionResult GetFilterTask([FromQuery] GetTaskByFilter getTaskByFilter)
        {
            var tasks = _repository.GetFilter(getTaskByFilter.CompletionDate, getTaskByFilter.CompleteBeforeDate, getTaskByFilter.Title);
            if (tasks.Count != 0)
                return Ok(tasks.Select(_mapper.Map<Models.Task, TaskResponseModel>).ToList());
            return NotFound("Ничего не найдено");
        }


    }
}
