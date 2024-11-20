import React, { useState } from "react";
import "./LoginForm.css";
import { FaUser, FaLock } from "react-icons/fa";

export default function LoginForm({ onSwitch }) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch("http://localhost:5047/api/User/Login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ UserName: username, Password: password }),
      });

      if (!response.ok) {
        throw new Error("Login failed");
      }

      const data = await response.json();

      document.cookie = `tasty=${data.token}; path=/;`;

      window.location.href = "/home";
    } catch (error) {
      setErrorMessage(
        "Не удалось залогиниться. Проверьте свои учетные данные."
      );
      console.error(error);
    }
  };

  return (
    <div className="container">
      <div className="wrapper">
        <form onSubmit={handleSubmit}>
          <h1>Логин</h1>
          {errorMessage && <p className="error-message">{errorMessage}</p>}
          <div className="input-box">
            <input
              type="text"
              placeholder="Имя Пользователя"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
            />
            <FaUser className="icon" />
          </div>
          <div className="input-box">
            <input
              type="password"
              placeholder="Пароль"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
            <FaLock className="icon" />
          </div>
          <div className="remember-forgot">
            <label>
              <input type="checkbox" />
              Запомнить меня
            </label>
          </div>
          <button type="submit">Логин</button>
          <div className="register-link">
            <p>
              Нет аккаунта?{" "}
              <a
                href="#"
                onClick={(e) => {
                  e.preventDefault();
                  onSwitch();
                }}
              >
                Регистрация
              </a>
            </p>
          </div>
        </form>
      </div>
    </div>
  );
  
}
