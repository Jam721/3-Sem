import React from "react";
import "./TabsSection.css"; // Подключаем CSS файл для стилизации

const TabsSection = ({ active, onChange }) => {
  return (
    <div className="tabs">
      <button
        onClick={() => onChange("post")}
        className={`tab-button ${active === "post" ? "active" : ""}`}
      >
        Список задач
      </button>
      <button
        onClick={() => onChange("CreateTaskForm")}
        className={`tab-button ${active === "CreateTaskForm" ? "active" : ""}`}
      >
        Создать задачу
      </button>
    </div>
  );
};

export default TabsSection;
