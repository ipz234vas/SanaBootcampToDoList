import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { deleteTask, updateTaskStatus } from '../redux/actions';

const TaskItem = ({ task }) => {
    const dispatch = useDispatch();
    const categories = useSelector(state => state.categories);
    const category = categories.find(cat => cat.id === task.categoryId);

    return (
        <tr className="text-center align-middle" style={{ backgroundColor: task.isCompleted ? 'lightgray' : '', textDecoration: task.isCompleted ? 'line-through' : '' }}>
            <td>
                <input
                    type="checkbox"
                    checked={task.isCompleted}
                    onChange={() => dispatch(updateTaskStatus(task.id))}
                />
            </td>
            <td>{task.taskDescription}</td>
            <td>{task.finishDate}</td>
            <td>{category ? category.name : ' '}</td>
            <td>
                <button className="btn btn-danger" onClick={() => dispatch(deleteTask(task.id))}>X</button>
            </td>
        </tr>
    );
};

export default TaskItem;