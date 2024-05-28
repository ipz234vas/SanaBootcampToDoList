using ToDoList.Repositories;

namespace ToDoList.Factories
{
    public class RepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public RepositoryType RepositoryType { get; set; }

        public RepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IRepository CreateRepository()
        {
            return RepositoryType switch
            {
                RepositoryType.DataBase => _serviceProvider.GetRequiredService<DBRepository>(),
                RepositoryType.XML => _serviceProvider.GetRequiredService<XMLRepository>(),
                _ => throw new ArgumentException("Invalid repository type", nameof(RepositoryType))
            };
        }
    }
}
