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

export function createCustomAccountsDropdown(
  accounts: IAccount[],
  except?: number
) {
  let accountsList: IDropdown<number>[] = [];
  accounts.map((a) => {
    const account: IDropdown<number> = {
      value: a.id,
      label: `${a.accountNumber} - ${a.balance} ${Currency[a.currency]}`,
    };
    accountsList.push(account);
  });
  accountsList = accountsList.filter((x) => x.value !== except);
  return accountsList;
}

export function getOption(selectedValue: number, accounts: IAccount[]) {
  const account = accounts.find((a) => a.id === selectedValue);
  if (account !== undefined) {
    const option: IDropdown<number> = {
      value: account.id,
      label: `${account.accountNumber} - ${account.balance} ${
        Currency[account.currency]
      }`,
    };
    return option;
  }
  return null;
}
