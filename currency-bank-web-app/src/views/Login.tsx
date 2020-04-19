import * as React from "react";
import {
  Container,
  Avatar,
  Typography,
  CssBaseline,
  TextField,
  Button,
  Grid,
  makeStyles,
  Backdrop,
  CircularProgress
} from "@material-ui/core";
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import { Link } from "react-router-dom";
import { useState } from "react";
import { connect } from "react-redux";
import { authActions } from "../_actions/auth.actions";

const useStyles = makeStyles(theme => ({
  paper: {
    marginTop: theme.spacing(8),
    display: "flex",
    flexDirection: "column",
    alignItems: "center"
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main
  },
  form: {
    width: "100%",
    marginTop: theme.spacing(1)
  },
  submit: {
    margin: theme.spacing(3, 0, 2)
  },
  backdrop: {
    zIndex: theme.zIndex.drawer + 1,
    color: "#fff"
  }
}));

interface IProps {
  loggingIn: boolean;
  loggedIn: boolean;
  token: string | null;
  dispatch: any;
}

function Login(props: IProps) {
  const classes = useStyles();

  const [login, setLogin] = useState<string>("jkowalski");
  const [password, setPassword] = useState<string>("test12345");

  const handleLogin = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const { dispatch } = props;
    if (login && password) {
      dispatch(authActions.login(login, password));
    }
  };

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Sign in
        </Typography>
        <form
          className={classes.form}
          noValidate
          onSubmit={e => handleLogin(e)}
        >
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            id="login"
            label="Login"
            name="login"
            autoComplete="login"
            autoFocus
            value={login}
            onChange={e => setLogin(e.target.value)}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="password"
            label="Password"
            type="password"
            id="password"
            value={password}
            onChange={e => setPassword(e.target.value)}
          />
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
          >
            Sign In
          </Button>
          <Backdrop className={classes.backdrop} open={props.loggingIn}>
            <CircularProgress color="inherit" />
          </Backdrop>
          <Grid container>
            <Grid item>
              <Link to="/register">{"Or Sign Up"}</Link>
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
}

function mapStateToProps(state: any) {
  const { loggingIn, loggedIn, token } = state.authentication;
  return {
    loggingIn,
    loggedIn,
    token
  };
}

export default connect(mapStateToProps)(Login);
