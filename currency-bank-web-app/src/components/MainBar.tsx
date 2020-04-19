import React, { useState } from "react";
import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";
import Button from "@material-ui/core/Button";
import ExitToAppIcon from "@material-ui/icons/ExitToApp";
import PersonIcon from "@material-ui/icons/Person";
import Tooltip from "@material-ui/core/Tooltip";
import { Link } from "react-router-dom";
import { connect } from "react-redux";
import { IUser } from "../interfaces/login/IUser";
import { authActions } from "../_actions/auth.actions";
import UserPage from "../views/UserPage";
import logo from "../assets/64.png";

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      flexGrow: 1,
    },
    title: {
      flexGrow: 1,
    },
    iconWithLink: {
      padding: 10,
      cursor: "pointer",
    },
  })
);

interface IProps {
  loggedIn: boolean;
  user: IUser | null;
  dispatch: any;
}

function MainBar(props: IProps) {
  const classes = useStyles();

  const [isUserPageOpen, setIsUSerPageOpen] = useState<boolean>(false);

  const logout = () => {
    const { dispatch } = props;
    dispatch(authActions.logout());
  };

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" className={classes.title}>
            <Link to="/">Currency Bank</Link>
          </Typography>
          {props.loggedIn && (
            <>
              <Tooltip
                title={
                  props?.user?.userName === undefined ? "" : props.user.userName
                }
              >
                <PersonIcon
                  onClick={(e) => setIsUSerPageOpen(true)}
                  className={classes.iconWithLink}
                />
              </Tooltip>
              <Tooltip title="Logout">
                <ExitToAppIcon
                  onClick={logout}
                  className={classes.iconWithLink}
                />
              </Tooltip>
            </>
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
      <UserPage
        dispatch={props.dispatch}
        isOpen={isUserPageOpen}
        onClose={() => setIsUSerPageOpen(false)}
        user={props.user}
      />
    </div>
  );
}

export default connect()(MainBar);
