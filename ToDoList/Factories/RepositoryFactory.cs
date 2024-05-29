using ToDoList.Repositories;

namespace ToDoList.Factories
{
	public class RepositoryFactory
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public RepositoryFactory(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
		{
			_serviceProvider = serviceProvider;
			_httpContextAccessor = httpContextAccessor;
		}
		public RepositoryType GetRepositoryType()
		{
			if (_httpContextAccessor.HttpContext.Session.GetString("RepositoryName") != null)
			{
				var repositoryTypeString = _httpContextAccessor.HttpContext.Session.GetString("RepositoryName");
				return (RepositoryType)Enum.Parse(typeof(RepositoryType), repositoryTypeString);
			}
			return RepositoryType.DataBase;
		}
		public IRepository CreateRepository()
		{
			var repositoryType = GetRepositoryType();
			return repositoryType switch
			{
				RepositoryType.DataBase => _serviceProvider.GetRequiredService<DBRepository>(),
				RepositoryType.XML => _serviceProvider.GetRequiredService<XMLRepository>(),
				_ => throw new ArgumentException("Invalid repository type", nameof(repositoryType))
			};
		}
	}
}
