import { IAccount } from "./../interfaces/IAccount";
import { INewAccount } from "./../interfaces/NewAccount/INewAccount";
import { _post, _get, _delete } from "../utils/ApiRequest";

export const accountService = {
  getAllAccountsForCurrentUser,
  openNew,
  deleteAccount,
};

function getAllAccountsForCurrentUser() {
  return _get<IAccount[]>(`account/getAccounts`).then((a) => {
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
