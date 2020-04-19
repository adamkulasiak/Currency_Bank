import { combineReducers } from "redux";

import { authentication, IAuthState } from "./auth.reducer";
import { alert } from "./alert_reducer";
import { loading, ILoadingState } from "./loading.reducer";

export interface State {
  authentication: IAuthState;
  alert: any;
  loading: ILoadingState;
}

const rootReducer = combineReducers({
  authentication,
  alert,
  loading
});

export default rootReducer;
