import { IUser } from "../interfaces/login/IUser";
import { IUserForUpdateDto } from "../interfaces/user/IUserForUpdateDto";
import { _put } from "../utils/ApiRequest";

export const userService = {
  update,
};

function update(userForUpdate: IUserForUpdateDto) {
  return _put<IUser>(`user/update`, userForUpdate).then((user) => {
    localStorage.removeItem("user");
    localStorage.setItem("user", JSON.stringify(user));
    return user;
  });
}
