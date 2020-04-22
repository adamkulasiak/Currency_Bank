import { IUser } from "./../interfaces/login/IUser";
import { IAccount } from "./../interfaces/IAccount";
import { INewAccount } from "./../interfaces/NewAccount/INewAccount";
import { post, get } from "../utils/ApiRequest";

export const accountService = {
  getAllAccountsForCurrentUser,
  openNew,
};

function getAllAccountsForCurrentUser() {
  return get<IAccount[]>(`account/getAccounts`).then((a) => {
    const accounts = localStorage.getItem("accounts");
    if (accounts !== null) {
      localStorage.removeItem("accounts");
      localStorage.setItem("accounts", JSON.stringify(a));
    }
    return a;
  });
}

function openNew(newAccount: INewAccount) {
  return post<IAccount>(`account/create`, newAccount).then((account) => {
    return account;
  });
}
