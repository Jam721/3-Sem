// import React, { useState } from "react";
// import Cookies from "js-cookie";

// const CreatePost = ({ onPostCreated }) => {
//   const [title, setTitle] = useState("");
//   const [description, setDescription] = useState("");

//   const handleSubmit = async (e) => {
//     e.preventDefault();

//     try {
//       const token = Cookies.get("tasty");
//       console.log("Token from cookies:", token);

//       if (!token) {
//         throw new Error("Token is missing. Please log in.");
//       }

//       const response = await fetch("http://26.249.179.3:5087/api/Post/Create", {
//         method: "POST",
//         headers: {
//           "Content-Type": "application/json",
//           Authorization: `Bearer ${token}`, // Используем обратные кавычки для шаблонной строки
//         },
//         body: JSON.stringify({ title, description }),
//         credentials: "include",
//       });

//       if (!response.ok) {
//         const errorMessage = await response.text(); // Получаем текст ошибки
//         console.error("Error response:", errorMessage); // Логирование ответа сервера
//         throw new Error("Failed to create post: " + errorMessage);
//       }

//       const newPost = await response.json();
//       console.log("New post created:", newPost); // Логирование нового поста

//       // Проверка наличия функции onPostCreated перед вызовом
//       if (typeof onPostCreated === "function") {
//         onPostCreated(newPost);
//       }

//       // Сброс значений формы
//       setTitle("");
//       setDescription("");
//     } catch (error) {
//       console.error("Error saving post:", error);
//     }
//   };

//   return (
//     <form className="create-post-form" onSubmit={handleSubmit}>
//       <h3>Create Post</h3>
//       <input
//         type="text"
//         placeholder="Title"
//         value={title}
//         onChange={(e) => setTitle(e.target.value)}
//         required
//       />
//       <textarea
//         placeholder="Description"
//         value={description}
//         onChange={(e) => setDescription(e.target.value)}
//         required
//       />
//       <button type="submit">Create Post</button>
//     </form>
//   );
// };

// export default CreatePost;
