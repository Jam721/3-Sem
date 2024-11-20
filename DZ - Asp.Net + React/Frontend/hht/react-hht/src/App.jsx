import React, { useState } from "react";
import Header from "./components/Header/Header.jsx";
import TabsSection from "./components/TabsSection.jsx";
import PostList from "./components/PostList.jsx";
import CreateTaskForm from "./components/CreateTaskForm.jsx"; // Импортируем компонент для создания задачи
import LoginForm from "./components/LoginForm/LoginForm.jsx";
import RegisterForm from "./components/RegisterForm/RegisterForm.jsx";
import IntroSection from "./components/introSection.jsx";

function App() {
  const [tab, setTab] = useState("post");
  const [selectedDay, setSelectedDay] = useState(null); // Добавляем состояние для выбранного дня

  const handleDayClick = (day) => {
    setSelectedDay(day);
  };

  if (tab === "loginform") {
    return <LoginForm onSwitch={() => setTab("registerform")} />;
  } else if (tab === "registerform") {
    return <RegisterForm onSwitch={() => setTab("loginform")} />;
  }

  return (
    <>
      <Header setTab={setTab} /> {/* Передаем setTab для управления вкладками */}
      <main>
        <IntroSection />
        <TabsSection active={tab} onChange={setTab} />
        {tab === "post" && <PostList selectedDay={selectedDay} />} {/* Передаем выбранный день в PostList */}
        {tab === "CreateTaskForm" && <CreateTaskForm />} {/* Добавляем отображение CreateTaskForm */}
      </main>
    </>
  );
}

export default App;
