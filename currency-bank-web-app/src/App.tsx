import React, { useState } from "react";
import { Router, Route } from "react-router-dom";
import { history } from "./_helpers/history";
import { connect } from "react-redux";
import Login from "./views/Login";
import MainBar from "./components/MainBar";
import { PrivateRoute } from "./components/PrivateRoute";
import { State } from "./_reducers";
import Snack from "./components/Snack";

interface IProps {
  loggedIn: boolean;
  loggingIn: boolean;
  token: string | null;
  type: string;
  message: string;
  dispatch: any;
}

function App(props: IProps) {
  return (
    <div className="App">
      <Router history={history}>
        <MainBar loggedIn={props.loggedIn} />
        <PrivateRoute exact path="/" />
        <Route path="/login" component={Login} />
      </Router>
      {props.message && <Snack message={props.message} />}
    </div>
  );
}

function mapStateToProps(state: State) {
  const { loggedIn, loggingIn, token } = state.authentication;
  const { type, message } = state.alert;
  return {
    loggedIn,
    loggingIn,
    token,
    type,
    message,
  };
}

export default connect(mapStateToProps)(App);
