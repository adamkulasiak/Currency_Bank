import { alertConstants } from "./../_constants/alert.contants";

export const alertActions = {
  success,
  error,
};

function success(message: string) {
  return { type: alertConstants.SUCCESS, message };
}

function error(message: string) {
  return { type: alertConstants.ERROR, message };
}
