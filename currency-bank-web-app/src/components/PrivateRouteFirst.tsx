import React from "react";
import { Route, Redirect } from "react-router-dom";
import MainPage from "../views/MainPage";

export const PrivateRouteFirst = ({ ...rest }) => (
  <Route
    {...rest}
    render={(props) =>
      localStorage.getItem("token") ? (
        <MainPage />
      ) : (
        <Redirect
          to={{ pathname: "/login", state: { from: props.location } }}
        />
      )
    }
  />
);
