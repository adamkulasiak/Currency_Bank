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
import Backdrop from "@material-ui/core/Backdrop";
import { makeStyles, CircularProgress } from "@material-ui/core";

const useStyles = makeStyles((theme) => ({
  backdrop: {
    zIndex: theme.zIndex.drawer + 1,
    color: "#fff",
  },
}));

interface IProps {
  loggedIn: boolean;
  loggingIn: boolean;
  user: IUser | null;
  type: string;
  message: string;
  isLoading: boolean;
  dispatch: any;
}

function App(props: IProps) {
  const classes = useStyles();
  return (
    <div className="App">
      <Router history={history}>
        <MainBar loggedIn={props.loggedIn} user={props.user} />
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
      <Backdrop className={classes.backdrop} open={props.isLoading}>
        <CircularProgress color="inherit" />
      </Backdrop>
    </div>
  );
}

function mapStateToProps(state: State) {
  const { loggedIn, loggingIn, user } = state.authentication;
  const { type, message } = state.alert;
  const { isLoading } = state.loading;
  return {
    loggedIn,
    loggingIn,
    user,
    type,
    message,
    isLoading,
  };
}

export default connect(mapStateToProps)(App);
