import { combineEpics } from 'redux-observable';
import { fetchTasksEpic } from './tasksEpic';

const rootEpic = combineEpics(
    fetchTasksEpic
);

export default rootEpic;