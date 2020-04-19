import * as React from "react";
import { Grid, Container, makeStyles } from "@material-ui/core";
import { connect } from "react-redux";
import { State } from "../_reducers";
import { IUser } from "../interfaces/login/IUser";
import DataTable from "../components/DataTable";
import ActionButtons from "../components/ActionButtons";
import logo from "../../src/assets/256.png";

const useStyles = makeStyles((theme) => ({
  container: {
    marginTop: 30,
    textAlign: "center",
  },
  datatable: {
    margin: "auto",
    width: "100%",
  },
  actions: {
    marginLeft: "auto",
    marginRight: "auto",
    marginTop: 30,
    marginBottom: 30,
  },
}));

interface IMainPageProps {
  user: IUser | null;
}

function MainPage(props: IMainPageProps) {
  const classes = useStyles();
  return (
    <Container>
      <Grid container className={classes.container}>
        <Grid item xs={12}>
          <div>
            <img src={logo} alt="logo" width={256} />
          </div>
        </Grid>
        <Grid item className={classes.actions}>
          <ActionButtons></ActionButtons>
        </Grid>
        <Grid item className={classes.datatable}>
          <DataTable accounts={props.user?.accounts} />
        </Grid>
      </Grid>
    </Container>
  );
}

function mapStateToProps(state: State) {
  const { user } = state.authentication;
  return {
    user,
  };
}

export default connect(mapStateToProps)(MainPage);
