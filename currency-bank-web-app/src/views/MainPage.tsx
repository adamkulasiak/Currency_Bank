import * as React from "react";
import { Grid, Container, makeStyles } from "@material-ui/core";
import { connect } from "react-redux";
import { State } from "../_reducers";
import { IUser } from "../interfaces/login/IUser";
import DataTable from "../components/DataTable";
import ActionButtons from "../components/ActionButtons";
import logo from "../../src/assets/256.png";
import { useState, useEffect } from "react";
import CreateAccount from "./CreateAccount";
import { accountService } from "../_services/account.service";
import { IAccount } from "../interfaces/IAccount";
import { loadingActions } from "../_actions/loading.actions";

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
  dispatch: any;
  user: IUser | null;
}

function MainPage(props: IMainPageProps) {
  const classes = useStyles();

  const [accounts, setAccounts] = useState<IAccount[]>([]);
  const [refreshAccounts, setRefreshAccounts] = useState<boolean>(false);
  const [createAccountOpen, setCreateAccountOpen] = useState<boolean>(false);

  useEffect(() => {
    props.dispatch(loadingActions.enableLoading());
    accountService
      .getAllAccountsForCurrentUser()
      .then((acc) => {
        setAccounts(acc);
      })
      .finally(() => props.dispatch(loadingActions.disableLoading()));
  }, [refreshAccounts]);

  return (
    <Container>
      <Grid container className={classes.container}>
        <Grid item xs={12}>
          <div>
            <img src={logo} alt="logo" width={256} />
          </div>
        </Grid>
        <Grid item className={classes.actions}>
          <ActionButtons
            createAccountOpen={createAccountOpen}
            onOpenCreateAccountOpen={() => setCreateAccountOpen(true)}
          ></ActionButtons>
          <CreateAccount
            dispatch={props.dispatch}
            isOpen={createAccountOpen}
            onClose={() => setCreateAccountOpen(false)}
            onRefreshAccounts={() => setRefreshAccounts(!refreshAccounts)}
          ></CreateAccount>
        </Grid>
        <Grid item className={classes.datatable}>
          <DataTable
            dispatch={props.dispatch}
            accounts={accounts}
            onRefreshAccounts={() => setRefreshAccounts(!refreshAccounts)}
          />
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
