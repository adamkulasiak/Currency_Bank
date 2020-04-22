import { IUser } from "./../interfaces/login/IUser";
import { apiHeader } from "../_helpers/api-header";
import { IUserForRegisterDto } from "../interfaces/register/IUserForRegisterDto";
import { post } from "../utils/ApiRequest";

export const authService = {
  login,
  register,
  logout,
};

function login(username: string, password: string) {
  return post<IUser>("auth/login", {
    UserName: username,
    Password: password,
  }).then((user) => {
    localStorage.setItem("token", JSON.stringify(user.token));
    delete user.accounts;
    localStorage.setItem("user", JSON.stringify(user));
    return user;
  });
}

function register(userForRegisterDto: IUserForRegisterDto) {
  return post<any>(`/auth/register`, userForRegisterDto).then((response) => {
    return response.result;
  });
}

function logout() {
  localStorage.removeItem("token");
  localStorage.removeItem("user");
}
