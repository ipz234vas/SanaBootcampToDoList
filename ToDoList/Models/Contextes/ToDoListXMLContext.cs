namespace ToDoList.Models.Contextes
{
	public class ToDoListXMLContext
	{
		private readonly IConfiguration configuration;
		private readonly string? XMLConnectionString;

		public ToDoListXMLContext(IConfiguration configuration)
		{
			this.configuration = configuration;
			XMLConnectionString = this.configuration.GetConnectionString("XMLConnection");
		}

		public string _XMLConnectionString => XMLConnectionString;
	}
}
