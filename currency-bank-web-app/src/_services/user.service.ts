import { IUser } from "../interfaces/login/IUser";
import { IUserForUpdateDto } from "../interfaces/user/IUserForUpdateDto";
import { put } from "../utils/ApiRequest";

export const userService = {
  update
};

function update(userForUpdate: IUserForUpdateDto) {
  return put<IUser>(`user/update`, userForUpdate).then(user => {
    localStorage.removeItem("user");
    localStorage.setItem("user", JSON.stringify(user));
    return user;
  });
}
