﻿using GraphQL.Types;

namespace ToDoListAPI.Types
{
	public class InputTaskType : InputObjectGraphType
	{
		public InputTaskType()
		{
			Field<IntGraphType>("id");
			Field<StringGraphType>("taskDescription");
			Field<BooleanGraphType>("isCompleted");
			Field<DateGraphType>("finishDate");
			Field<IntGraphType>("categoryId");
		}
	}
}
