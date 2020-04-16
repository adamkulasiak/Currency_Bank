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
        dispatch(alertActions.success("You have successfully logged in"));
        history.push("/");
      },
      (error) => {
        dispatch(failure(error));
        dispatch(alertActions.error(error));
      }
    );
  };
  function request(username: any) {
    return { type: userConstants.LOGIN_REQUEST, username };
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
    dispatch(alertActions.success("You have successfully logged out"));
    dispatch(handleLogout());
  };

  function handleLogout() {
    return { type: userConstants.LOGOUT };
  }
}
