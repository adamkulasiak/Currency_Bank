import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { ConfirmProvider } from "material-ui-confirm";

import "./index.css";
import App from "./App";
import { store } from "./_helpers/store";

ReactDOM.render(
  <Provider store={store}>
    <ConfirmProvider>
      <App />
    </ConfirmProvider>
  </Provider>,
  document.getElementById("root")
);
