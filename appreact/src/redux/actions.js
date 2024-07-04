export const addTask = (task) => ({
    type: 'ADD_TASK',
    payload: task,
});

export const deleteTask = (taskId) => ({
    type: 'DELETE_TASK',
    payload: taskId,
});

export const updateTaskStatus = (taskId) => ({
    type: 'UPDATE_TASK_STATUS',
    payload: taskId,
});

export const loadCategories = (categories) => ({
    type: 'LOAD_CATEGORIES',
    payload: categories,
});