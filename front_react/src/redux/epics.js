import { ofType, combineEpics } from 'redux-observable';
import { switchMap, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';

import { graphqlRequest, fetchCategoriesQuery, fetchTasksQuery } from './api.js';

import { fetchCategories, fetchTasks, setCategories, setTasks } from './actions.js';

const handleError = error => of({ type: 'API_ERROR', payload: error });

const fetchCategoriesEpic = (action$) => action$.pipe(
    ofType(fetchCategories.type),
    switchMap(() =>
        graphqlRequest(fetchCategoriesQuery(), {}).pipe(
            map(response => setCategories(response.data.categories)),
            catchError(handleError)
        )
    )
);

const fetchTasksEpic = (action$) => action$.pipe(
    ofType(fetchTasks.type),
    switchMap(() =>
        graphqlRequest(fetchTasksQuery(), {}).pipe(
            map(response => setTasks(response.data.todos)),
            catchError(handleError)
        )
    )
);

export const rootEpic = combineEpics(
    fetchCategoriesEpic,
    fetchTasksEpic,
);