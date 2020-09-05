using System.Threading;
using System.Threading.Tasks;
using Application.Items;
using MediatR;
using MongoDB.Driver;

namespace Application.Requests.User.Queries.Get
{
	public class GetUser : IRequest<Unit>
	{
		public GetUser(string userId)
		{
			UserId = userId;
		}

		private string UserId { get; }

		public class Handler : IRequestHandler<GetUser, Unit>
		{
			private readonly IDbContext _context;

			public Handler(IDbContext context)
			{
				_context = context;
			}

			public async Task<Unit> Handle(GetUser request, CancellationToken cancellationToken)
			{
				var self = SteelAxe.Self;

				var hashCode = self.GetHashCode();

				using var session = await _context.StartSessionAsync(cancellationToken: cancellationToken);

				var user = await _context.Users.FindAsync(r => r.UserId == request.UserId, cancellationToken: cancellationToken);

				await session.CommitTransactionAsync(cancellationToken);
				return Unit.Value;
			}
		}
	}
}