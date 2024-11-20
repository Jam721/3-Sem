import React, { useState } from "react";
import "../LoginForm/LoginForm.css";
import { FaUser, FaLock } from "react-icons/fa";
import { MdAlternateEmail } from "react-icons/md";

export default function RegisterForm({ onSwitch }) {
  const [email, setEmail] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await fetch(
        "http://localhost:5047/api/User/Register",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            UserName: username,
            Email: email,
            Password: password,
          }),
        }
      );

      if (!response.ok) {
        throw new Error("Registration failed");
      }

      alert("Registration successful! Please log in.");
      onSwitch();
    } catch (error) {
      setErrorMessage("Не удалось зарегистрироваться. Проверьте свои данные.");
      console.error(error);
    }
  };

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        height: "100vh", // Занимает всю высоту экрана
      }}
    >
      <div className="wrapper">
        <form onSubmit={handleSubmit}>
          <h1>Регистрация</h1>
          {errorMessage && <p className="error-message">{errorMessage}</p>}
          <div className="input-box">
            <input
              type="text"
              placeholder="Почта"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
            <MdAlternateEmail className="icon" />
          </div>
          <div className="input-box">
            <input
              type="text"
              placeholder="Имя пользователя"
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
          <button type="submit">Регистрация</button>
          <div className="register-link">
            <p>
              Есть аккаунт?{" "}
              <a
                href="#"
                onClick={(e) => {
                  e.preventDefault();
                  onSwitch();
                }}
              >
                Логин
              </a>
            </p>
          </div>
        </form>
      </div>
    </div>
  );
}
