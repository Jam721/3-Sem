import { useState, useEffect } from "react";
import "./PostList.css";
import Button from "./Button/Button";
import { FaCheck, FaTimes, FaRegClock } from 'react-icons/fa';

const complexityMap = {
    1: "Легко",
    2: "Средне",
    3: "Сложно",
};

const priorityMap = {
    0: "Можно игнорировать",
    1: "Не важно",
    2: "Важно",
    3: "Очень важно",
};

const declension = (number, words) => {
    const cases = [2, 0, 1, 1, 1, 2];
    return words[ 
        (number % 100 > 4 && number % 100 < 20) 
            ? 2 
            : cases[Math.min(number % 10, 5)]
    ];
};

const calculateTimeLeft = (endDate) => {
    const end = new Date(endDate); // Время окончания задачи в UTC
    const now = new Date(); // Текущее время в UTC

    // Логируем значения для отладки
    console.log("End Time (UTC):", end.toISOString());  // Время окончания задачи в формате UTC
    console.log("Now Time (UTC):", now.toISOString());  // Текущее время в формате UTC
    console.log("Time Left in ms:", end - now);  // Разница в миллисекундах

    // Разница во времени в миллисекундах
    const timeLeft = end - now;

    if (timeLeft <= 0) {
        console.log("Task is overdue");
        return "Время истекло";  // Если время истекло
    }

    // Преобразуем разницу во времени в дни, часы, минуты
    const days = Math.floor(timeLeft / (1000 * 60 * 60 * 24));
    const hours = Math.floor((timeLeft % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minutes = Math.floor((timeLeft % (1000 * 60 * 60)) / (1000 * 60));
    const seconds = Math.floor((timeLeft % (1000 * 60)) / 1000);

    let result = "";

    if (days > 0) {
        result += `${days} ${declension(days, ['день', 'дня', 'дней'])} `;
    }
    result += `${hours} ${declension(hours, ['час', 'часа', 'часов'])} `;
    result += `${minutes} ${declension(minutes, ['минута', 'минуты', 'минут'])} `;
    result += `${seconds} ${declension(seconds, ['секунда', 'секунды', 'секунд'])}`;

    return result;
};






const getStatusIndicator = (status, endDate) => {
    const now = new Date();
    const end = new Date(endDate);

    // Преобразуем в UTC
    if (status === "Выполнено") {
        return <span className="status-indicator green"><FaCheck /> Выполнено</span>;
    }

    if (end < now) {
        return <span className="status-indicator red"><FaTimes /> Просрочено</span>;
    }

    return <span className="status-indicator blue"><FaRegClock /> В процессе</span>;
};

export default function PostList() {
    const [weeks, setWeeks] = useState([]);
    const [error, setError] = useState(null);
    const [currentWeekIndex, setCurrentWeekIndex] = useState(0);
    const [selectedDay, setSelectedDay] = useState(null);

    useEffect(() => {
        const fetchWeeks = async () => {
            try {
                const response = await fetch("http://localhost:5047/api/Mission/GetWeek", {
                    method: "GET",
                    credentials: "include",
                });

                if (!response.ok) {
                    throw new Error("Failed to fetch weeks. Status: " + response.status);
                }

                const weeksData = await response.json();
                setWeeks(weeksData);
            } catch (error) {
                console.error("Error fetching weeks:", error);
                setError("Failed to load weeks: " + error.message);
            }
        };

        fetchWeeks();
    }, []);

    const handleDeleteTask = async (taskId) => {
        try {
            const response = await fetch(`http://localhost:5047/api/Mission/Delete/${taskId}`, {
                method: "DELETE",
                credentials: "include",
            });
    
            if (!response.ok) {
                throw new Error("Failed to delete task");
            }
    
            // Обновление состояния с использованием новой копии данных
            setWeeks(prevWeeks => 
                prevWeeks.map(week => ({
                    ...week, // Копируем объект недели
                    days: week.days.map(day => ({
                        ...day, // Копируем объект дня
                        missions: day.missions.filter(mission => mission.id !== taskId) // Отфильтровываем задачу
                    }))
                }))
            );
            window.location.reload();
        } catch (error) {
            console.error("Error deleting task:", error);
            setError("Не удалось удалить задачу. Попробуйте снова.");
        }
    };
    

    const handleCompleteTask = async (taskId) => {
        try {
            const response = await fetch(`http://localhost:5047/api/Mission/Complete/${taskId}`, {
                method: "PUT",
                credentials: "include",
            });
    
            if (!response.ok) {
                throw new Error("Failed to complete task");
            }
    
            // Получаем обновленные данные задачи с сервера
            const updatedTask = await response.json();
    
            // Обновляем только ту задачу, которая была выполнена
            setWeeks(prevWeeks =>
                prevWeeks.map(week => ({
                    ...week,
                    days: week.days.map(day => ({
                        ...day,
                        missions: day.missions.map(mission =>
                            mission.id === taskId
                                ? updatedTask // Обновляем задачу с полученными данными с сервера
                                : mission
                        )
                    }))
                }))
            );
            window.location.reload();
        } catch (error) {
            console.error("Error completing task:", error);
            setError("Не удалось выполнить задачу. Попробуйте снова.");
        }
    };

    const handleNextWeek = () => {
        if (currentWeekIndex < weeks.length - 1) {
            setCurrentWeekIndex(currentWeekIndex + 1);
            setSelectedDay(null);
        }
    };

    const handlePreviousWeek = () => {
        if (currentWeekIndex > 0) {
            setCurrentWeekIndex(currentWeekIndex - 1);
            setSelectedDay(null);
        }
    };

    const handleDayClick = (day) => {
        setSelectedDay(day);
    };

    return (
        <section>
            {error ? (
                <p>{error}</p>
            ) : (
                <>
                    <h2>{weeks[currentWeekIndex]?.week}</h2>
                    <div className="day-panels">
                        {weeks[currentWeekIndex]?.days.map(day => (
                            <div
                                key={day.day}
                                className={`day-panel ${selectedDay === day ? 'active' : ''}`}
                                onClick={() => handleDayClick(day)}
                            >
                                <h3>{day.day}</h3>
                                <p>Задачи: {day.missions.length}</p>
                            </div>
                        ))}
                    </div>

                    <div className="tasks-container">
                        {selectedDay ? (
                            selectedDay.missions.length > 0 ? (
                                selectedDay.missions.map(post => (
                                    <div className="post-card" key={post.id}>
                                        <div className="post-content">
                                        <h4 style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                                            {post.title}
                                            {/* Проверка выполнения */}
                                            {post.completed ? (
                                                <span className="status-indicator green">
                                                    <FaCheck /> Выполнено
                                                </span>
                                            ) : (
                                                getStatusIndicator(post.status, post.endDate)
                                            )}
                                        </h4>

                                            <p><strong>Описание: </strong> {post.description}</p>
                                            <p><strong>Дата создания: </strong> {new Date(post.createdAt).toLocaleString('en-GB', { timeZone: 'UTC' })}</p>
                                            <p><strong>Оставшееся время: </strong> {calculateTimeLeft(post.endDate)}</p>
                                            <p><strong>Статус:</strong> {post.completed ? "Выполнено" : "Не выполнено"}</p>
                                            <p><strong>Сложность: </strong> {complexityMap[post.complexity]}</p>
                                            <p><strong>Приоритет: </strong> {priorityMap[post.priority]}</p>
                                        </div>

                                        <div className="action-buttons">
                                            {post.status !== 'Выполнено' && (
                                                <button className="complete-button" onClick={() => handleCompleteTask(post.id)}>
                                                    Выполнить
                                                </button>
                                            )}
                                            <button className="delete-button" onClick={() => handleDeleteTask(post.id)}>
                                                Удалить
                                            </button>
                                        </div>
                                    </div>

                                ))
                            ) : (
                                <p>Нет задач на этот день</p>
                            )
                        ) : (
                            <p>Выберите день для просмотра задач</p>
                        )}
                    </div>

                    <div className="pagination">
                        <Button onClick={handlePreviousWeek} disabled={currentWeekIndex === 0}>Назад</Button>
                        <Button onClick={handleNextWeek} disabled={currentWeekIndex === weeks.length - 1}>Вперед</Button>
                    </div>
                </>
            )}
        </section>
    );
}
