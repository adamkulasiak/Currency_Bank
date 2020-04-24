import * as React from "react";
import { useState, useEffect } from "react";
import MUIDataTable, { MUIDataTableOptions } from "mui-datatables";
import { IColumn } from "../interfaces/Datatable/IColumn";
import TableRow from "@material-ui/core/TableRow";
import TableCell from "@material-ui/core/TableCell";
import { IHistoryAccounts } from "../interfaces/IHistoryAccounts";
import { IAccountToDisplay } from "../interfaces/Datatable/IAccountToDisplay";
import { Currency } from "../enums/Currency";
import { makeStyles } from "@material-ui/core";
const format = require("date-format");

const useStyles = makeStyles((theme) => ({
  tableCell: {
    padding: 5,
  },
  cellValue: {
    marginLeft: 50,
  },
}));

interface IProps {
  dispatch: any;
  history: IHistoryAccounts[];
}

export default function HistoryDataTable(props: IProps) {
  const classes = useStyles();

  const [columns] = useState<IColumn[]>([
    { name: "id", label: "Id" },
    { name: "accountNumber", label: "Account number" },
    { name: "currency", label: "Currency" },
    { name: "balance", label: "Balance" },
  ]);

  useEffect(() => {}, [props.history]);

  const getData = () => {
    const accountsToDisplay: IAccountToDisplay[] = [];
    props.history.map((a) => {
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
    selectableRows: "none",
    expandableRows: true,
    print: false,
    download: false,
    search: false,
    renderExpandableRow: (rowData, rowMeta) => {
      const colSpan = rowData.length + 1;
      const account = props.history.find((a) => a.id === parseInt(rowData[0]));
      if (account?.history?.length > 0) {
        return account?.history.map((h, index) => (
          <TableRow key={index}>
            <TableCell className={classes.tableCell} colSpan={colSpan}>
              <span className={classes.cellValue}>
                {`Date: ${format.asString(
                  "dd-MM-yyyy hh:mm:ss",
                  new Date(h.timestamp)
                )} ammount: ${h.ammount} ${Currency[account.currency]}`}
              </span>
            </TableCell>
          </TableRow>
        ));
      } else {
        return (
          <TableRow>
            <TableCell className={classes.tableCell} colSpan={colSpan}>
              No history avaliable for this account
            </TableCell>
          </TableRow>
        );
      }
    },
    textLabels: {
      body: {
        noMatch: "You don't have any accounts",
      },
    },
  };
  return (
    <MUIDataTable
      title={"History"}
      data={getData()}
      columns={columns}
      options={options}
    />
  );
}
