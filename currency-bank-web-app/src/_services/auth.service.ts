import { apiHeader } from "../_helpers/api-header";

export const userService = {
  login,
  logout,
};

function login(username: string, password: string) {
  const requestOptions = {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ UserName: username, Password: password }),
  };

  return fetch(`http://localhost:5000/api/auth/login`, requestOptions)
    .then(handleResponse)
    .then((user) => {
      localStorage.setItem("token", JSON.stringify(user.token));
      return user.token;
    });
}

function logout() {
  localStorage.removeItem("token");
}

function handleResponse(response: any) {
  return response.text().then((text: string) => {
    const data = text && JSON.parse(text);
    if (!response.ok) {
      if (response.status === 401) {
        logout();
        window.location.reload(true);
      }

      const error = (data && data.message) || response.statusText;
      return Promise.reject(error);
    }

    return data;
  });
}
