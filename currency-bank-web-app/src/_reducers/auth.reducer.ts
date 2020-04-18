import { userConstants } from "./../_constants/user.constants";
import { IUser } from "../interfaces/login/IUser";

const token = localStorage.getItem("token");
const username = localStorage.getItem("username");

export interface IAuthState {
  loggingIn: boolean;
  loggedIn: boolean;
  user: IUser | null;
  username: string | null;
}

const initialState: IAuthState = token
  ? { loggedIn: true, loggingIn: false, username, user: null }
  : { loggedIn: false, loggingIn: false, username, user: null };

export function authentication(state = initialState, action: any) {
  switch (action.type) {
    case userConstants.LOGIN_REQUEST:
      return {
        loggingIn: true,
        loggedIn: false,
        user: null,
        username,
      };
    case userConstants.LOGIN_SUCCESS:
      return {
        loggingIn: false,
        loggedIn: true,
        user: action.user,
        username,
      };
    case userConstants.LOGIN_FAILURE:
      return {
        loggingIn: false,
        loggedIn: false,
        user: null,
        username,
      };
    case userConstants.LOGOUT:
      return {
        loggingIn: false,
        loggedIn: false,
        token: null,
        username,
      };
    default:
      return state;
  }
}
