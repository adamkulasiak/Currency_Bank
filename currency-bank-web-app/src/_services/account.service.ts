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
  return _get<IAccount[]>(`account/getAccounts`);
}

function getHistoryForUserAccounts() {
  return _get<IHistoryAccounts[]>(`account/getHistory`);
}

function openNew(newAccount: INewAccount) {
  return _post<IAccount>(`account/create`, newAccount);
}

function deleteAccount(accountId: number) {
  return _delete<string>(`account/deleteAccount?accountId=${accountId}`);
}

function cashOut(accountId: number, ammount: number) {
  return _put<IAccount>(
    `account/cashout?accountId=${accountId}&ammount=${ammount}`
  );
}

function cashIn(accountId: number, ammount: number) {
  return _put<IAccount>(
    `account/cashIn?accountId=${accountId}&ammount=${ammount}`
  );
}

function transfer(
  accountId: number,
  destAccountNumber: string,
  ammount: number
) {
  return _post<IAccount[]>(
    `account/transferMoney?principalAccountId=${accountId}&destinationAccountNumber=${destAccountNumber}&ammount=${ammount}`
  );
}

function exchange(
  srcAccountId: number,
  destAccountId: number,
  ammount: number
) {
  return _put<IAccount[]>(
    `account/exchange?sourceAccountId=${srcAccountId}&destinationAccountId=${destAccountId}&ammount=${ammount}`
  );
}
