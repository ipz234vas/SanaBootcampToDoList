import React, { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { addTask } from '../redux/actions';

const AddTaskForm = () => {
    const [taskDescription, setTaskDescription] = useState('');
    const [categoryId, setCategoryId] = useState('');
    const [finishDate, setFinishDate] = useState('');
    const dispatch = useDispatch();
    const categories = useSelector(state => state.categories);

    const handleSubmit = (e) => {
        e.preventDefault();
        const newTask = {
            id: Date.now(),
            taskDescription,
            categoryId,
            finishDate,
            isCompleted: false,
        };
        dispatch(addTask(newTask));
        setTaskDescription('');
        setCategoryId('');
        setFinishDate('');
    };

    return (
        <form onSubmit={handleSubmit}>
            <div className="row">
                <div className="col-sm-4 form-group">
                    <label>Опис завдання:</label>
                    <input
                        type="text"
                        className="form-control"
                        value={taskDescription}
                        onChange={(e) => setTaskDescription(e.target.value)}
                    />
                </div>
                <div className="col-sm-4 form-group">
                    <label>Категорія:</label>
                    <select
                        className="form-select"
                        value={categoryId}
                        onChange={(e) => setCategoryId(e.target.value)}
                    >
                        <option value="">Оберіть категорію</option>
                        {categories.map(category => (
                            <option key={category.id} value={category.id}>
                                {category.name}
                            </option>
                        ))}
                    </select>
                </div>
                <div className="col-sm-4 form-group">
                    <label>Виконати до:</label>
                    <input
                        type="date"
                        className="form-control"
                        value={finishDate}
                        onChange={(e) => setFinishDate(e.target.value)}
                    />
                </div>
            </div>
            <button type="submit" className="btn btn-primary mt-3">Додати завдання</button>
        </form>
    );
};

export default AddTaskForm;