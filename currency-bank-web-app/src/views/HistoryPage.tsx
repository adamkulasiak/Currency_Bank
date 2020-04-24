import * as React from "react";
import { useState, useEffect } from "react";
import { IHistoryAccounts } from "../interfaces/IHistoryAccounts";
import { accountService } from "../_services/account.service";
import { connect } from "react-redux";
import { State } from "../_reducers";
import { loadingActions } from "../_actions/loading.actions";

export interface IProps {
  dispatch: any;
  isLoading: boolean;
}

function HistoryPage(props: IProps) {
  const [history, setHistory] = useState<IHistoryAccounts | undefined>(
    undefined
  );

  useEffect(() => {
    props.dispatch(loadingActions.enableLoading());
    accountService
      .getHistoryForUserAccounts()
      .then((a) => {
        setHistory(a);
      })
      .finally(() => props.dispatch(loadingActions.disableLoading()));
  }, []);

  return <div>History</div>;
}

function mapStateToProps(state: State) {
  const { isLoading } = state.loading;
  return {
    isLoading,
  };
}

export default connect(mapStateToProps)(HistoryPage);
