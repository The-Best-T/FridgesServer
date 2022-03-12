using Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace InnowiseProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        public HomeController(IRepositoryManager repository)
        {
            _repositoryManager = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[]{"ok","ok" };
        }
    }
}
