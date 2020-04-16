import { userConstants } from "./../_constants/user.constants";

const token = localStorage.getItem("token");

export interface IAuthState {
  loggingIn: boolean;
  loggedIn: boolean;
  token: string | null;
}

const initialState: IAuthState = token
  ? { loggedIn: true, loggingIn: false, token }
  : { loggedIn: false, loggingIn: false, token: null };

export function authentication(state = initialState, action: any) {
  switch (action.type) {
    case userConstants.LOGIN_REQUEST:
      return {
        loggingIn: true,
        loggedIn: false,
        token: null,
      };
    case userConstants.LOGIN_SUCCESS:
      return {
        loggingIn: false,
        loggedIn: true,
        token: action.token,
      };
    case userConstants.LOGIN_FAILURE:
      return {
        loggingIn: false,
        loggedIn: false,
        token: null,
      };
    case userConstants.LOGOUT:
      return {
        loggingIn: false,
        loggedIn: false,
        token: null,
      };
    default:
      return state;
  }
}
