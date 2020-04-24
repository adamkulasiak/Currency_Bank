import * as React from "react";
import { useState } from "react";
import { IAccount } from "../interfaces/IAccount";
import MUIDataTable, { MUIDataTableOptions } from "mui-datatables";
import { IAccountToDisplay } from "../interfaces/Datatable/IAccountToDisplay";
import { IColumn } from "../interfaces/Datatable/IColumn";
import { Currency } from "../enums/Currency";
import { accountService } from "../_services/account.service";
import { loadingActions } from "../_actions/loading.actions";
import { alertActions } from "../_actions/alert.actions";
import { useConfirm } from "material-ui-confirm";

interface IProps {
  dispatch: any;
  accounts: IAccount[];
  onRefreshAccounts: () => void;
}

export default function DataTable(props: IProps) {
  const confirm = useConfirm();

  const [columns] = useState<IColumn[]>([
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

  const handleDelete = (accountToDelete: IAccount) => {
    if (accountToDelete.balance !== 0) {
      props.dispatch(
        alertActions.error(
          "Your have to cash out all money before deleting this account"
        )
      );
      return;
    }
    confirm({
      description: "Are you sure you want to delete this account?",
    }).then(() => {
      props.dispatch(loadingActions.enableLoading());
      accountService
        .deleteAccount(accountToDelete.id)
        .then((msg) => {
          props.onRefreshAccounts();
          props.dispatch(alertActions.success(msg));
        })
        .catch(() =>
          props.dispatch(alertActions.error("Error during deleting account."))
        )
        .finally(() => props.dispatch(loadingActions.disableLoading()));
    });
  };

  const options: MUIDataTableOptions = {
    filterType: "checkbox",
    selectableRows: "single",
    textLabels: {
      body: {
        noMatch: "You don't have any accounts",
      },
    },
    onRowsDelete: (rowsDeleted: any): boolean => {
      const index: number = rowsDeleted.data[0].index;
      const accountToDelete = props.accounts[index];
      handleDelete(accountToDelete);
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
