import * as React from "react";
import { useState } from "react";
import { IAccount } from "../interfaces/IAccount";
import MUIDataTable, { MUIDataTableOptions } from "mui-datatables";

interface IProps {
  accounts?: IAccount[];
}

export default function DataTable(props: IProps) {
  const [columns, setColumns] = useState<string[]>([
    "Id",
    "Account number",
    "Currency",
    "Balance",
  ]);

  const getData = () => {
    const { accounts } = props;
    const accountsToDisplay: any = [];

    accounts?.map((a) => {
      accountsToDisplay.push(Object.values(a));
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
