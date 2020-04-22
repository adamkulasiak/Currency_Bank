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
import CashOut from "./CashOut";
import CashIn from "./CashIn";
import Transfer from "./Transfer";

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
  const [transferOpen, setTransferOpen] = useState<boolean>(false);
  const [cashOutOpen, setCashOutOpen] = useState<boolean>(false);
  const [cashInOpen, setCashInOpen] = useState<boolean>(false);

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
            onOpenCreateAccount={() => setCreateAccountOpen(true)}
            onOpenTransfer={() => setTransferOpen(true)}
            onOpenCashOut={() => setCashOutOpen(true)}
            onOpenCashIn={() => setCashInOpen(true)}
          ></ActionButtons>
          <CreateAccount
            dispatch={props.dispatch}
            isOpen={createAccountOpen}
            onClose={() => setCreateAccountOpen(false)}
            onRefreshAccounts={() => setRefreshAccounts(!refreshAccounts)}
          />
          <Transfer
            dispatch={props.dispatch}
            isOpen={transferOpen}
            accounts={accounts}
            onClose={() => setTransferOpen(false)}
            onRefreshAccounts={() => setRefreshAccounts(!refreshAccounts)}
          />
          <CashOut
            dispatch={props.dispatch}
            isOpen={cashOutOpen}
            accounts={accounts}
            onClose={() => setCashOutOpen(false)}
            onRefreshAccounts={() => setRefreshAccounts(!refreshAccounts)}
          />
          <CashIn
            dispatch={props.dispatch}
            isOpen={cashInOpen}
            accounts={accounts}
            onClose={() => setCashInOpen(false)}
            onRefreshAccounts={() => setRefreshAccounts(!refreshAccounts)}
          />
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
