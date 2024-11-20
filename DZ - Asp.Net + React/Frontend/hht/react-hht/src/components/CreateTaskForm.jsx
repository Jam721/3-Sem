import React, { useState } from "react";
import "./CreateTaskForm.css"; // Подключаем CSS файл для стилизации
import { FaCalendarAlt, FaClock, FaTextHeight } from "react-icons/fa";

export default function CreateTaskForm() {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [periodOfTime, setPeriodOfTime] = useState(0);
  const [createdAt, setCreatedAt] = useState("");
  const [endDate, setEndDate] = useState("");
  const [complexity, setComplexity] = useState(1);
  const [priority, setPriority] = useState(0);
  const [errorMessage, setErrorMessage] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    const taskData = {
      title,
      description,
      periodOfTime: parseInt(periodOfTime),
      createdAt: new Date(createdAt).toISOString(),
      endDate: new Date(endDate).toISOString(),
      complexity: parseInt(complexity),
      priority: parseInt(priority),
    };

    try {
      const response = await fetch("http://localhost:5047/api/Mission/Create", {
        method: "POST",
        credentials: "include",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(taskData),
      });

      console.log(taskData);

      if (!response.ok) {
        throw new Error("Failed to create task");
      }

      alert("Задача успешно создана!");
      window.location.href = "/home"; // Перенаправление на главную страницу
    } catch (error) {
      setErrorMessage("Не удалось создать задачу. Проверьте введенные данные.");
      console.error(error);
    }
  };

  return (
    <div className="container">
      <div className="wrapper">
        <form onSubmit={handleSubmit}>
          <h1>Создать задачу</h1>
          {errorMessage && <p className="error-message">{errorMessage}</p>}

          <div className="input-box">
            <input
              type="text"
              placeholder="Название"
              value={title}
              onChange={(e) => setTitle(e.target.value)}
              required
              className="input-field"
            />
            <FaTextHeight className="icon" />
          </div>

          <div className="input-box">
            <textarea
              placeholder="Описание"
              value={description}
              onChange={(e) => setDescription(e.target.value)}
              required
              className="input-field"
            />
            <FaTextHeight className="icon" />
          </div>

          <div className="input-box">
            <input
              type="number"
              placeholder="Время выполнения (мин)"
              value={periodOfTime}
              onChange={(e) => setPeriodOfTime(e.target.value)}
              required
              className="input-field"
            />
            <FaClock className="icon" />
          </div>

          <div className="input-box">
            <input
              type="datetime-local"
              placeholder="Дата начала"
              value={createdAt}
              onChange={(e) => setCreatedAt(e.target.value)}
              required
              className="input-field"
            />
            <FaCalendarAlt className="icon" />
          </div>

          <div className="input-box">
            <input
              type="datetime-local"
              placeholder="Дата окончания"
              value={endDate}
              onChange={(e) => setEndDate(e.target.value)}
              required
              className="input-field"
            />
            <FaCalendarAlt className="icon" />
          </div>

          <div className="input-box">
            <select
              value={complexity}
              onChange={(e) => setComplexity(e.target.value)}
              className="input-field"
            >
              <option value="1">Легко</option>
              <option value="2">Средне</option>
              <option value="3">Сложно</option>
            </select>
          </div>

          <div className="input-box">
            <select
              value={priority}
              onChange={(e) => setPriority(e.target.value)}
              className="input-field"
            >
              <option value="0">Можно игнорировать</option>
              <option value="1">Не важно</option>
              <option value="2">Важно</option>
              <option value="3">Очень важно</option>
            </select>
          </div>

          <button type="submit" className="submit-btn">Создать задачу</button>
        </form>
      </div>
    </div>
  );
}
