import * as React from "react";
import { useState, useEffect } from "react";
import { IAccount } from "../interfaces/IAccount";
import MUIDataTable, { MUIDataTableOptions } from "mui-datatables";
import { IAccountToDisplay } from "../interfaces/Datatable/IAccountToDisplay";
import { IColumn } from "../interfaces/Datatable/IColumn";
import { Currency } from "../enums/Currency";

interface IProps {
  accounts: IAccount[];
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
