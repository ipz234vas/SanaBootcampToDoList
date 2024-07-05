export const FETCH_CATEGORIES = 'FETCH_CATEGORIES';
export const SET_CATEGORIES = 'SET_CATEGORIES';
export const FETCH_TASKS = 'FETCH_TASKS';
export const SET_TASKS = 'SET_TASKS';

export const fetchCategories = () => ({ type: FETCH_CATEGORIES });
export const setCategories = categories => ({ type: SET_CATEGORIES, payload: categories });
export const fetchTasks = () => ({ type: FETCH_TASKS });
export const setTasks = tasks => ({ type: SET_TASKS, payload: tasks });