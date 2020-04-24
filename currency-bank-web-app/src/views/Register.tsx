import * as React from "react";
import {
  Container,
  Typography,
  CssBaseline,
  TextField,
  Button,
  Grid,
  makeStyles,
} from "@material-ui/core";
import { Link } from "react-router-dom";
import { useState } from "react";
import { connect } from "react-redux";
import { authActions } from "../_actions/auth.actions";
import { IUserForRegisterDto } from "../interfaces/register/IUserForRegisterDto";
import logo from "../../src/assets/128.png";

const useStyles = makeStyles((theme) => ({
  paper: {
    marginTop: theme.spacing(8),
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main,
  },
  form: {
    width: "100%",
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
}));

interface IProps {
  dispatch: any;
}

function Register(props: IProps) {
  const classes = useStyles();

  const [firstname, setFirstname] = useState<string>("");
  const [lastname, setLastname] = useState<string>("");
  const [username, setUsername] = useState<string>("");
  const [email, setEmail] = useState<string>("");
  const [pesel, setPesel] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [password2, setPassword2] = useState<string>("");

  const createUserForRegister = () => {
    const obj: IUserForRegisterDto = {
      FirstName: firstname,
      LastName: lastname,
      UserName: username,
      Email: email,
      Pesel: pesel,
      Password: password,
    };

    return obj;
  };

  const handleRegister = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    const { dispatch } = props;
    dispatch(authActions.register(createUserForRegister()));
  };

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <div className={classes.paper}>
        <Grid item xs={12}>
          <div>
            <img src={logo} alt="logo" width={128} />
          </div>
        </Grid>
        <Typography component="h1" variant="h5">
          Sign up
        </Typography>
        <form
          className={classes.form}
          noValidate
          onSubmit={(e) => handleRegister(e)}
        >
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            id="firstname"
            label="Firstname"
            name="firstname"
            autoComplete="firstname"
            value={firstname}
            autoFocus
            onChange={(e) => setFirstname(e.target.value)}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            id="lastname"
            label="Lastname"
            name="lastname"
            autoComplete="lastname"
            value={lastname}
            onChange={(e) => setLastname(e.target.value)}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            id="username"
            label="Username"
            name="username"
            autoComplete="username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            id="email"
            label="Email"
            name="email"
            autoComplete="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            id="pesel"
            label="Pesel"
            name="pesel"
            autoComplete="pesel"
            value={pesel}
            onChange={(e) => setPesel(e.target.value)}
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
            onChange={(e) => setPassword(e.target.value)}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="password2"
            label="Confirm password"
            type="password"
            id="password2"
            value={password2}
            onChange={(e) => setPassword2(e.target.value)}
          />
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
          >
            Sign Up
          </Button>
          <Grid container>
            <Grid item>
              <Link to="/login">{"Or Sign In"}</Link>
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
    token,
  };
}

export default connect(mapStateToProps)(Register);
