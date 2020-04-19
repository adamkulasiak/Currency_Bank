import React from "react";
import { Router, Route } from "react-router-dom";
import { history } from "./_helpers/history";
import { connect } from "react-redux";
import Login from "./views/Login";
import MainBar from "./components/MainBar";
import { PrivateRoute } from "./components/PrivateRoute";
import { State } from "./_reducers";
import Snack from "./components/Snack";
import Register from "./views/Register";
import { IUser } from "./interfaces/login/IUser";

interface IProps {
  loggedIn: boolean;
  loggingIn: boolean;
  user: IUser | null;
  username: string | null;
  type: string;
  message: string;
  dispatch: any;
}

function App(props: IProps) {
  return (
    <div className="App">
      <Router history={history}>
        <MainBar
          loggedIn={props.loggedIn}
          user={props.user}
          username={props.username}
        />
        <PrivateRoute exact path="/" />
        <Route path="/login" component={Login} />
        <Route path="/register" component={Register} />
      </Router>
      {props.message && (
        <Snack
          dispatch={props.dispatch}
          message={props.message}
          type={props.type}
        />
      )}
    </div>
  );
}

function mapStateToProps(state: State) {
  const { loggedIn, loggingIn, user, username } = state.authentication;
  const { type, message } = state.alert;
  return {
    loggedIn,
    loggingIn,
    user,
    username,
    type,
    message
  };
}

export default connect(mapStateToProps)(App);
