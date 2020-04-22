import { IAccount } from "../interfaces/IAccount";
import { IDropdown } from "../interfaces/Dropdowns/IDropdown";
import { Currency } from "../enums/Currency";

export function createAccountsDropdown(accounts: IAccount[]) {
  const accountsList: IDropdown<number>[] = [];
  accounts.map((a) => {
    const account: IDropdown<number> = {
      value: a.id,
      label: `${a.accountNumber} - ${a.balance} ${Currency[a.currency]}`,
    };
    accountsList.push(account);
  });
  return accountsList;
}
