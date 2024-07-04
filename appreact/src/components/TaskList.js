import React from 'react';
import { useSelector } from 'react-redux';
import TaskItem from './TaskItem';

const TaskList = () => {
    const tasks = useSelector(state => state.tasks);

    return (
        <table className="table">
            <thead>
            <tr className="text-center">
                <th>Статус</th>
                <th>Завдання</th>
                <th>Виконати до</th>
                <th>Категорія</th>
                <th></th>
            </tr>
            </thead>
            <tbody>
            {tasks.map(task => (
                <TaskItem key={task.id} task={task} />
            ))}
            </tbody>
        </table>
    );
};

export default TaskList;