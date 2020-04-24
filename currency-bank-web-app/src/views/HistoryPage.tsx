import * as React from "react";
import { useState, useEffect } from "react";
import { IHistoryAccounts } from "../interfaces/IHistoryAccounts";
import { accountService } from "../_services/account.service";
import { connect } from "react-redux";
import { State } from "../_reducers";
import { loadingActions } from "../_actions/loading.actions";
import { Container, Grid, makeStyles } from "@material-ui/core";
import HistoryDataTable from "../components/HistoryDatatable";
import PdfExport from "./PdfExport";

const useStyles = makeStyles((theme) => ({
  container: {
    marginTop: 30,
    textAlign: "center",
  },
  datatable: {
    margin: "auto",
    width: "100%",
  },
}));

export interface IProps {
  dispatch: any;
  isLoading: boolean;
}

function HistoryPage(props: IProps) {
  const classes = useStyles();

  const [history, setHistory] = useState<IHistoryAccounts[] | undefined>(
    undefined
  );
  const [saveAsPdfOpen, setSaveAsPdfOpen] = useState<boolean>(false);

  useEffect(() => {
    props.dispatch(loadingActions.enableLoading());
    accountService
      .getHistoryForUserAccounts()
      .then((a) => {
        setHistory(a);
      })
      .finally(() => props.dispatch(loadingActions.disableLoading()));
  }, []);

  return (
    <Container>
      <Grid container className={classes.container}>
        <Grid item className={classes.datatable}>
          {history !== undefined && (
            <HistoryDataTable
              dispatch={props.dispatch}
              history={history}
              onOpenSaveDialog={() => setSaveAsPdfOpen(true)}
            />
          )}
        </Grid>
      </Grid>
      <PdfExport
        dispatch={props.dispatch}
        isOpen={saveAsPdfOpen}
        onClose={() => setSaveAsPdfOpen(false)}
      />
    </Container>
  );
}

function mapStateToProps(state: State) {
  const { isLoading } = state.loading;
  return {
    isLoading,
  };
}

export default connect(mapStateToProps)(HistoryPage);
