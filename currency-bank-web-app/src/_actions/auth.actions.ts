import { IUser } from "../interfaces/login/IUser";
import { userService } from "../_services/auth.service";
import { history } from "../_helpers/history";
import { alertActions } from "./alert.actions";
import { authConstants } from "../_constants/auth.constants";
import { IUserForRegisterDto } from "../interfaces/register/IUserForRegisterDto";
import { loadingActions } from "./loading.actions";

export const authActions = {
  login,
  register,
  logout
};

function login(username: string, password: string) {
  return (dispatch: any) => {
    dispatch(request(username));
    dispatch(loadingActions.enableLoading());
    userService.login(username, password).then(
      user => {
        dispatch(success(user));
        dispatch(alertActions.success("You have successfully logged in"));
        history.push("/");
        dispatch(loadingActions.disableLoading());
      },
      () => {
        const unathorizeMsg = "Bad username or password";
        dispatch(failure(unathorizeMsg));
        dispatch(alertActions.error(unathorizeMsg));
        dispatch(loadingActions.disableLoading());
      }
    );
  };
  function request(username: string) {
    return { type: authConstants.LOGIN_REQUEST, username };
  }
  function success(user: IUser) {
    return { type: authConstants.LOGIN_SUCCESS, user };
  }
  function failure(error: string) {
    return { type: authConstants.LOGIN_FAILURE, error };
  }
}

function register(userForRegister: IUserForRegisterDto) {
  return (dispatch: any) => {
    dispatch(registerRequest(userForRegister));
    dispatch(loadingActions.enableLoading());
    userService.register(userForRegister).then(
      () => {
        dispatch(registerSuccess(userForRegister));
        dispatch(alertActions.success("You have successfully signed up"));
        dispatch(login(userForRegister.UserName, userForRegister.Password));
        history.push("/");
        dispatch(loadingActions.disableLoading());
      },
      () => {
        const message = "Registration failed";
        dispatch(registerFailure(message));
        dispatch(alertActions.error(message));
        dispatch(loadingActions.disableLoading());
      }
    );
  };

  function registerRequest(userForRegister: IUserForRegisterDto) {
    return { type: authConstants.REGISTER_REQUEST, userForRegister };
  }

  function registerSuccess(userForRegister: IUserForRegisterDto) {
    return { type: authConstants.REGISTER_SUCCESS, userForRegister };
  }

  function registerFailure(errorMessage: string) {
    return { type: authConstants.REGISTER_FAILURE, errorMessage };
  }
}

function logout() {
  return (dispatch: any) => {
    userService.logout();
    dispatch(handleLogout());
    dispatch(alertActions.success("You have successfully logged out"));
  };

  function handleLogout() {
    return { type: authConstants.LOGOUT };
  }
}