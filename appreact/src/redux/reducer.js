const initialState = {
    tasks: [],
    categories: [],
};

const reducer = (state = initialState, action) => {
    switch (action.type) {
        case 'ADD_TASK':
            return { ...state, tasks: [action.payload, ...state.tasks] };
        case 'DELETE_TASK':
            return {
                ...state,
                tasks: state.tasks.filter(task => task.id !== action.payload),
            };
        case 'UPDATE_TASK_STATUS':
            const taskToUpdate = state.tasks.find(task => task.id === action.payload);
            const updatedTask = { ...taskToUpdate, isCompleted: !taskToUpdate.isCompleted };

            // Remove the task from the list
            const filteredTasks = state.tasks.filter(task => task.id !== action.payload);

            // If the task is now completed, add it to the end of the list
            if (updatedTask.isCompleted) {
                return {
                    ...state,
                    tasks: [...filteredTasks, updatedTask],
                };
            } else {
                // If the task is not completed, add it back to its original position
                return {
                    ...state,
                    tasks: [updatedTask, ...filteredTasks],
                };
            }
        case 'LOAD_CATEGORIES':
            return {
                ...state,
                categories: action.payload,
            };
        default:
            return state;
    }
};

export default reducer;