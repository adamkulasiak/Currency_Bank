import React from "react";
import { Route, Redirect } from "react-router-dom";
import HistoryPage from "../views/HistoryPage";

export const PrivateRouteSecond = ({ ...rest }) => (
  <Route
    {...rest}
    render={(props) =>
      localStorage.getItem("token") ? (
        <HistoryPage />
      ) : (
        <Redirect
          to={{ pathname: "/login", state: { from: props.location } }}
        />
      )
    }
  />
);
