import * as React from "react";
import { useState, useEffect } from "react";
import { IAccount } from "../interfaces/IAccount";
import MUIDataTable, { MUIDataTableOptions } from "mui-datatables";
import { IAccountToDisplay } from "../interfaces/Datatable/IAccountToDisplay";
import { IColumn } from "../interfaces/Datatable/IColumn";
import { Currency } from "../enums/Currency";
import { accountService } from "../_services/account.service";
import { loadingActions } from "../_actions/loading.actions";
import { alertActions } from "../_actions/alert.actions";

interface IProps {
  dispatch: any;
  accounts: IAccount[];
  onRefreshAccounts: () => void;
}

export default function DataTable(props: IProps) {
  const [columns, setColumns] = useState<IColumn[]>([
    { name: "id", label: "Id" },
    { name: "accountNumber", label: "Account number" },
    { name: "currency", label: "Currency" },
    { name: "balance", label: "Balance" },
  ]);

  const getData = () => {
    const accountsToDisplay: IAccountToDisplay[] = [];
    props.accounts.map((a) => {
      const accountToDisplay: IAccountToDisplay = {
        id: a.id,
        accountNumber: a.accountNumber,
        currency: Currency[a.currency],
        balance: a.balance,
      };
      accountsToDisplay.push(accountToDisplay);
    });
    return accountsToDisplay;
  };

  const options: MUIDataTableOptions = {
    filterType: "checkbox",
    selectableRows: "single",
    onRowsDelete: (rowsDeleted: any): boolean => {
      props.dispatch(loadingActions.enableLoading());
      const index: number = rowsDeleted.data[0].index;
      console.log(index);
      const accountToDelete = props.accounts[index];
      accountService
        .deleteAccount(accountToDelete.id)
        .then((msg) => {
          props.onRefreshAccounts();
          props.dispatch(alertActions.success(msg));
          return false;
        })
        .catch(() =>
          props.dispatch(alertActions.error("Error during deleting account."))
        )
        .finally(() => props.dispatch(loadingActions.disableLoading()));
      return false;
    },
  };
  return (
    <MUIDataTable
      title={"Your accounts"}
      data={getData()}
      columns={columns}
      options={options}
    />
  );
}
