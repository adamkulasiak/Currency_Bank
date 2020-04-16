import { combineReducers } from "redux";

import { authentication, IAuthState } from "./auth.reducer";
import { alert } from "./alert_reducer";

export interface State {
  authentication: IAuthState;
  alert: any;
}

const rootReducer = combineReducers({
  authentication,
  alert,
});

export default rootReducer;
