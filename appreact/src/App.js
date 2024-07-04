import React, { useEffect } from 'react';
import { useDispatch } from 'react-redux';
import AddTaskForm from './components/AddTaskForm';
import TaskList from './components/TaskList';
import { loadCategories } from './redux/actions';

const App = () => {
    const dispatch = useDispatch();

    useEffect(() => {
        const categories = [
            { id: '1', name: 'Робота' },
            { id: '2', name: 'Навчання' },
            { id: '3', name: 'Домашні справи' },
            { id: '4', name: 'Спорт' },
            { id: '5', name: 'Інше' },
        ];
        dispatch(loadCategories(categories));
    }, [dispatch]);

    return (
        <div className="container">
            <h1 className="text-center display-1 mb-5">ToDoList</h1>
            <AddTaskForm />
            <h2 className="text-center">Список завдань</h2>
            <TaskList />
        </div>
    );
};

export default App;
