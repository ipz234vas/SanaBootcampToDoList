import { createEpicMiddleware } from 'redux-observable'
import { createStore, applyMiddleware} from 'redux';
import reducer from './reducer.js';
import { rootEpic } from './epics';

const epicMiddleware = createEpicMiddleware();
const store = createStore(reducer, applyMiddleware(epicMiddleware));

epicMiddleware.run(rootEpic);

export default store;
