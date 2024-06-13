namespace ToDoListAPI.Middlewares
{
	public class RepositoryMiddleware
	{
		private readonly RequestDelegate _next;

		public RepositoryMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			var repositoryTypeString = context.Request.Headers["Repository"].FirstOrDefault();

			if (!string.IsNullOrEmpty(repositoryTypeString))
				context.Items["RepositoryType"] = repositoryTypeString;

			await _next(context);
		}
	}
}
