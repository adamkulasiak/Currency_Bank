import { IUser } from "../interfaces/login/IUser";
import { authService } from "../_services/auth.service";
import { history } from "../_helpers/history";
import { alertActions } from "./alert.actions";
import { authConstants } from "../_constants/auth.constants";
import { IUserForRegisterDto } from "../interfaces/register/IUserForRegisterDto";
import { loadingActions } from "./loading.actions";
import { IUserForUpdateDto } from "../interfaces/user/IUserForUpdateDto";
import { userService } from "../_services/user.service";

export const authActions = {
  login,
  register,
  logout,
  update,
};

function login(username: string, password: string) {
  return (dispatch: any) => {
    dispatch(request(username));
    dispatch(loadingActions.enableLoading());
    authService
      .login(username, password)
      .then((user) => {
        dispatch(success(user));
        dispatch(alertActions.success("You have successfully logged in"));
        history.push("/");
      })
      .catch(() => {
        const unathorizeMsg = "Bad username or password";
        dispatch(failure(unathorizeMsg));
        dispatch(alertActions.error(unathorizeMsg));
      })
      .finally(() => dispatch(loadingActions.disableLoading()));
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
    authService
      .register(userForRegister)
      .then(() => {
        dispatch(registerSuccess(userForRegister));
        dispatch(alertActions.success("You have successfully signed up"));
        dispatch(login(userForRegister.UserName, userForRegister.Password));
        history.push("/");
      })
      .catch(() => {
        const message = "Registration failed";
        dispatch(registerFailure(message));
        dispatch(alertActions.error(message));
      })
      .finally(() => dispatch(loadingActions.disableLoading()));
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
    authService.logout();
    dispatch(handleLogout());
    dispatch(alertActions.success("You have successfully logged out"));
  };

  function handleLogout() {
    return { type: authConstants.LOGOUT };
  }
}

function update(userForUpdate: IUserForUpdateDto) {
  return (dispatch: any) => {
    dispatch(loadingActions.enableLoading());
    userService
      .update(userForUpdate)
      .then((user) => {
        dispatch(handleUpdate(user));
        dispatch(alertActions.success("Changes successfull!"));
      })
      .catch((err) => {
        dispatch(alertActions.success("Error ocurred!"));
      });
    dispatch(loadingActions.disableLoading());
  };

  function handleUpdate(user: IUser) {
    return { type: authConstants.UPDATE_SUCCESS, user };
  }
}
