import { userService } from "./../_services/auth.service";
import { history } from "./../_helpers/history";
import { alertActions } from "./alert.actions";
import { userConstants } from "./../_constants/user.constants";

export const userActions = {
  login,
  logout,
};

function login(username: string, password: string) {
  return (dispatch: any) => {
    dispatch(request({ username }));

    userService.login(username, password).then(
      (user) => {
        dispatch(success(user));
        dispatch(alertActions.success("You are successfully logged in"));
        history.push("/");
      },
      (error) => {
        dispatch(failure(error));
        dispatch(alertActions.error(error));
      }
    );
  };
  function request(user: any) {
    return { type: userConstants.LOGIN_REQUEST, user };
  }
  function success(token: any) {
    return { type: userConstants.LOGIN_SUCCESS, token };
  }
  function failure(error: string) {
    return { type: userConstants.LOGIN_FAILURE, error };
  }
}

function logout() {
  return (dispatch: any) => {
    userService.logout();
    dispatch(alertActions.success("You are successfully logged out"));
    return { type: userConstants.LOGOUT };
  };
}
