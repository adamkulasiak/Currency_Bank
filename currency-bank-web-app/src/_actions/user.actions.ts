import { userService } from "./../_services/auth.service";
import { history } from "./../_helpers/history";
import { alertActions } from "./alert.actions";
import { userConstants } from "./../_constants/user.constants";
import { IUserForRegisterDto } from "../interfaces/register/IUserForRegisterDto";

export const userActions = {
  login,
  register,
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
      () => {
        const unathorizeMsg = "Bad username or password";
        dispatch(failure(unathorizeMsg));
        dispatch(alertActions.error(unathorizeMsg));
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

function register(userForRegister: IUserForRegisterDto) {
  return (dispatch: any) => {
    dispatch(registerRequest(userForRegister));

    userService.register(userForRegister).then(
      () => {
        dispatch(registerSuccess(userForRegister));
        dispatch(alertActions.success("You have successfully signed up"));
        dispatch(login(userForRegister.UserName, userForRegister.Password));
        history.push("/");
      },
      () => {
        const message = "Registration failed";
        dispatch(registerFailure(message));
        dispatch(alertActions.error(message));
      }
    );
  };

  function registerRequest(userForRegister: IUserForRegisterDto) {
    return { type: userConstants.REGISTER_REQUEST, userForRegister };
  }

  function registerSuccess(userForRegister: IUserForRegisterDto) {
    return { type: userConstants.REGISTER_SUCCESS, userForRegister };
  }

  function registerFailure(errorMessage: string) {
    return { type: userConstants.REGISTER_FAILURE, errorMessage };
  }
}

function logout() {
  return (dispatch: any) => {
    userService.logout();
    dispatch(handleLogout());
    dispatch(alertActions.success("You have successfully logged out"));
  };

  function handleLogout() {
    return { type: userConstants.LOGOUT };
  }
}
