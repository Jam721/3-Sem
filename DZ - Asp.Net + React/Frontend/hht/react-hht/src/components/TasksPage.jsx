import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import "./TasksPage.css";

export default function TasksPage() {
  const { week, day } = useParams(); // Получаем параметры из URL
  const [missions, setMissions] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchMissions = async () => {
      try {
        const response = await fetch(`http://26.249.179.3:5047/api/Mission/GetDayMissions/${week}/${day}`, {
          method: "GET",
          credentials: "include",
        });

        if (!response.ok) {
          throw new Error("Ошибка при получении задач. Статус: " + response.status);
        }

        const missionsData = await response.json();
        setMissions(missionsData);
      } catch (error) {
        console.error("Ошибка при получении задач:", error);
        setError("Не удалось загрузить задачи: " + error.message);
      }
    };

    fetchMissions();
  }, [week, day]);

  return (
    <section>
      <h1>Задачи на {day} {week}</h1>
      {error ? (
        <p>{error}</p>
      ) : missions.length > 0 ? (
        <ul className="task-list">
          {missions.map((mission) => (
            <li key={mission.id} className="task-card">
              <h2>{mission.title}</h2>
              <p>{mission.description}</p>
              <p>Сложность: {mission.complexity}</p>
              <p>Приоритет: {mission.priority}</p>
              <p>Дата создания: {new Date(mission.createdAt).toLocaleString()}</p>
            </li>
          ))}
        </ul>
      ) : (
        <p>Нет доступных задач на этот день</p>
      )}
    </section>
  );
}
