import React from "react";
import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import IconButton from "@material-ui/core/IconButton";
import MenuIcon from "@material-ui/icons/Menu";
import { Link } from "react-router-dom";
import { userActions } from "../_actions/user.actions";
import { connect } from "react-redux";

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      flexGrow: 1,
    },
    title: {
      flexGrow: 1,
    },
  })
);

interface IProps {
  loggedIn: boolean;
  dispatch: any;
}

function MainBar(props: IProps) {
  const classes = useStyles();

  const logout = () => {
    const { dispatch } = props;
    dispatch(userActions.logout());
  };

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" className={classes.title}>
            <Link to="/">Currency Bank</Link>
          </Typography>
          {props.loggedIn && (
            <Button color="inherit" onClick={logout}>
              Logout
            </Button>
          )}
          {!props.loggedIn && (
            <>
              <Button color="inherit">
                <Link to="/login">Login</Link>
              </Button>
              <Button color="inherit">
                <Link to="/register">Register</Link>
              </Button>
            </>
          )}
        </Toolbar>
      </AppBar>
    </div>
  );
}

export default connect()(MainBar);
