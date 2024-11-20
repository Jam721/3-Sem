import React, { useState, useEffect } from "react";
import logo from "/CubaLibre.jpg";
import { styled } from "styled-components";
import Cookies from "js-cookie"; // Для работы с куки

const HeaderContainer = styled.header`
  height: 60px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 2rem;
  border-bottom: 2px solid #ccc;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
`;

const Logo = styled.img`
  height: 40px;
  width: auto;
`;

const ButtonContainer = styled.div`
  display: flex;
  align-items: center;
`;

const UserButton = styled.button`
  font-size: 1.1rem;
  font-weight: 500;
  color: #fff;
  background-color: #4CAF50;
  border: none;
  padding: 10px 15px;
  border-radius: 5px;
  cursor: pointer;
  display: flex;
  align-items: center;
  margin-right: 10px;
  &:hover {
    background-color: #45a049;
  }
  &:focus {
    outline: none;
  }
  transition: background-color 0.3s ease, transform 0.3s ease;
  &:hover {
    transform: scale(1.05);
  }
`;

const LogoutButton = styled.button`
  font-size: 1.1rem;
  font-weight: 500;
  color: #fff;
  background-color: #f44336;
  border: none;
  padding: 10px 15px;
  border-radius: 5px;
  cursor: pointer;
  &:hover {
    background-color: #e53935;
  }
  &:focus {
    outline: none;
  }
  transition: background-color 0.3s ease, transform 0.3s ease;
  &:hover {
    transform: scale(1.05);
  }
`;

const AuthButton = styled.button`
  font-size: 1.1rem;
  font-weight: 500;
  color: #fff;
  background-color: #2196F3; /* Синий фон для кнопок Логин и Регистрация */
  border: none;
  padding: 10px 15px;
  border-radius: 5px;
  cursor: pointer;
  margin-right: 10px;
  position: relative;
  overflow: hidden;
  transition: background-color 0.3s ease, transform 0.3s ease, border 0.3s ease;

  &:hover {
    background-color: #1976D2; /* Темнее при наведении */
    transform: scale(1.05);
  }
  &:focus {
    outline: none;
  }

  /* Для регистрации: добавим обводку и анимацию */
  &.register {
    background-color: transparent;
    color: #2196F3;
    border: 2px solid #2196F3;
    padding: 8px 14px;
    font-weight: bold;

    &:hover {
      background-color: #2196F3;
      color: #fff;
    }
  }
  transition: background-color 0.3s ease, transform 0.3s ease, color 0.3s ease;
`;

const Header = ({ setTab }) => {
  const [userName, setUserName] = useState(""); // Для хранения никнейма
  const [isLoggedIn, setIsLoggedIn] = useState(false); // Для проверки логина

  useEffect(() => {
    const token = Cookies.get("tasty"); // Проверяем наличие токена в куки

    if (token) {
      setIsLoggedIn(true); // Если токен есть, пользователь залогинен
      fetchUserData(); // Загружаем данные пользователя
    }
  }, []);

  const fetchUserData = async () => {
    try {
      const response = await fetch("http://localhost:5047/api/User/GetUser", {
        method: "GET",
        credentials: "include", // Авторизация с куки
      });

      if (!response.ok) {
        throw new Error("Failed to fetch user data");
      }

      const userData = await response.json();
      setUserName(userData.userName); // Устанавливаем никнейм
    } catch (error) {
      console.error("Error fetching user data:", error);
    }
  };

  const handleLogout = () => {
    Cookies.remove("tasty"); // Удаляем токен из куки
    setIsLoggedIn(false); // Обновляем состояние логина
    window.location.reload(); // Обновляем страницу
  };

  const handleLoginClick = () => {
    setTab("loginform");
  };

  const handleRegisterClick = () => {
    setTab("registerform");
  };

  return (
    <HeaderContainer>
      <Logo src={logo} alt="Logo" />
      <ButtonContainer>
        {isLoggedIn ? (
          <>
            <UserButton>{`Привет, ${userName}`}</UserButton>
            <LogoutButton onClick={handleLogout}>Logout</LogoutButton>
          </>
        ) : (
          <>
            <AuthButton onClick={handleLoginClick}>Логин</AuthButton>
            <AuthButton className="register" onClick={handleRegisterClick}>
              Регистрация
            </AuthButton>
          </>
        )}
      </ButtonContainer>
    </HeaderContainer>
  );
};

export default Header;
