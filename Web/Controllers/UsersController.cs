using System.Threading.Tasks;
using Application.Requests.User.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<ActionResult<object>> Get()
		{
			return await _mediator.Send(new GetUser("1"));
		}
	}
}