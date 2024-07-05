import { FETCH_TASKS_SUCCESS, FETCH_TASKS_FAILURE } from '../actions/actionTypes';

const initialState = {
    tasks: [],
    error: null,
};

const tasksReducer = (state = initialState, action) => {
    switch (action.type) {
        case FETCH_TASKS_SUCCESS:
            return {
                ...state,
                tasks: action.payload,
            };
        case FETCH_TASKS_FAILURE:
            return {
                ...state,
                error: action.payload,
            };
        default:
            return state;
    }
};

export default tasksReducer;