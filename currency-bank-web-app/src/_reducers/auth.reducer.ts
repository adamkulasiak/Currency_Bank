import { authConstants } from "../_constants/auth.constants";
import { IUser } from "../interfaces/login/IUser";

const user = localStorage.getItem("user");

export interface IAuthState {
  loggingIn: boolean;
  loggedIn: boolean;
  user: IUser | null;
}

const initialState: IAuthState = user
  ? { loggedIn: true, loggingIn: false, user: JSON.parse(user) }
  : { loggedIn: false, loggingIn: false, user: null };

export function authentication(state = initialState, action: any) {
  switch (action.type) {
    case authConstants.LOGIN_REQUEST:
      return {
        loggingIn: true,
        loggedIn: false,
        user: null,
      };
    case authConstants.LOGIN_SUCCESS:
      return {
        loggingIn: false,
        loggedIn: true,
        user: action.user,
      };
    case authConstants.LOGIN_FAILURE:
      return {
        loggingIn: false,
        loggedIn: false,
        user: null,
      };
    case authConstants.LOGOUT:
      return {
        loggingIn: false,
        loggedIn: false,
        user: null,
      };
    case authConstants.UPDATE_SUCCESS:
      return {
        loggingIn: false,
        loggedIn: true,
        user: action.user,
      };
    default:
      return state;
  }
}
