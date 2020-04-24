import { IAccount } from "./../interfaces/IAccount";
import { INewAccount } from "./../interfaces/NewAccount/INewAccount";
import { _post, _get, _delete, _put } from "../utils/ApiRequest";
import { IHistoryAccounts } from "../interfaces/IHistoryAccounts";

export const accountService = {
  getAllAccountsForCurrentUser,
  getHistoryForUserAccounts,
  openNew,
  deleteAccount,
  cashOut,
  cashIn,
  transfer,
  exchange,
};

function getAllAccountsForCurrentUser() {
  return _get<IAccount[]>(`account/getAccounts`).then((a) => {
    return a;
  });
}

function getHistoryForUserAccounts() {
  return _get<IHistoryAccounts[]>(`account/getHistory`).then((a) => {
    return a;
  });
}

function openNew(newAccount: INewAccount) {
  return _post<IAccount>(`account/create`, newAccount).then((account) => {
    return account;
  });
}

function deleteAccount(accountId: number) {
  return _delete<string>(`account/deleteAccount?accountId=${accountId}`).then(
    (message) => {
      return message;
    }
  );
}

function cashOut(accountId: number, ammount: number) {
  return _put<IAccount>(
    `account/cashout?accountId=${accountId}&ammount=${ammount}`
  ).then((account) => {
    return account;
  });
}

function cashIn(accountId: number, ammount: number) {
  return _put<IAccount>(
    `account/cashIn?accountId=${accountId}&ammount=${ammount}`
  ).then((account) => {
    return account;
  });
}

function transfer(
  accountId: number,
  destAccountNumber: string,
  ammount: number
) {
  return _post<IAccount[]>(
    `account/transferMoney?principalAccountId=${accountId}&destinationAccountNumber=${destAccountNumber}&ammount=${ammount}`
  ).then((accounts) => {
    return accounts;
  });
}

function exchange(
  srcAccountId: number,
  destAccountId: number,
  ammount: number
) {
  return _put<IAccount[]>(
    `account/exchange?sourceAccountId=${srcAccountId}&destinationAccountId=${destAccountId}&ammount=${ammount}`
  ).then((accounts) => {
    return accounts;
  });
}
