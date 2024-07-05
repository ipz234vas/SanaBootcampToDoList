import { ofType } from 'redux-observable';
import { mergeMap } from 'rxjs/operators';
import { from, of } from 'rxjs';
import { fetchTasksSuccess, fetchTasksFailure } from '../actions/tasksActions';
import { FETCH_TASKS_REQUEST } from '../actions/actionTypes';

const fetchTasksEpic = action$ => action$.pipe(
    ofType(FETCH_TASKS_REQUEST),
    mergeMap(action =>
        from(fetch('/graphql', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                query: `
          {
            tasks {
              id
              title
              completed
            }
          }
        `,
            }),
        })
            .then(response => response.json())
            .then(result => fetchTasksSuccess(result.data.tasks))
            .catch(error => of(fetchTasksFailure(error.message))))
    )
);

export { fetchTasksEpic };
