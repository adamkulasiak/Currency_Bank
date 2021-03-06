import { alertConstants } from "../_constants/alert.contants";

const initialState = {
  type: "success",
  message: ""
};

export function alert(state = initialState, action: any) {
  switch (action.type) {
    case alertConstants.SUCCESS:
      return {
        type: "success",
        message: action.message
      };
    case alertConstants.ERROR:
      return {
        type: "error",
        message: action.message
      };
    case alertConstants.CLEAR:
      return {
        type: "clear",
        message: ""
      };
    default:
      return state;
  }
}
