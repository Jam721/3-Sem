// DaySelector.jsx
import React from "react";

const daysOfWeek = [
    "Понедельник",
    "Вторник",
    "Среда",
    "Четверг",
    "Пятница",
    "Суббота",
    "Воскресенье",
];

const DaySelector = ({ onDaySelect }) => {
    return (
        <div className="day-selector">
            {daysOfWeek.map(day => (
                <button key={day} onClick={() => onDaySelect(day)}>
                    {day}
                </button>
            ))}
        </div>
    );
};

export default DaySelector;
