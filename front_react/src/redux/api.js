import { from } from 'rxjs';

export const BASE_URL = "https://localhost:7163/graphql";

export const graphqlRequest = (query, variables) => {
    return from(
        fetch(BASE_URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Repository": localStorage.getItem("Repository") ?? "DataBase"
            },
            body: JSON.stringify({ query, variables }),
        }).then(response => response.json())
    );
};

export const fetchCategoriesQuery = () => `
    {
        categories {
            name
        }
    }
`;

export const fetchTasksQuery = () => `
    {
        tasks {
            id
            taskDescription
            isCompleted
            finishDate
            category {
                id
                name
            }
        }
    }
`;
//????
export const addTaskMutation = () => `
    mutation AddTask($todo: ToDoInputType!) {
        addTask(todo: $todo) {
            id
            taskDescription
            isCompleted
            categoryId
            finishDate
            category {
              id
              name
            }
        }
    }
`;
//others
