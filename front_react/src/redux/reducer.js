import { SET_CATEGORIES, SET_TASKS} from './actions';

const initialState = {
    tasks: [],
    categories: [],
};

const reducer = (state = initialState, action) => {
    switch (action.type) {
        case SET_CATEGORIES:
            return { ...state, categories: action.payload };
        case SET_TASKS:
            return { ...state, tasks: action.payload };
        default:
            return state;
    }
};

export default reducer;