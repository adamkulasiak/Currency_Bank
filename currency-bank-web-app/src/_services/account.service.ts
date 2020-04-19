import { IUser } from "./../interfaces/login/IUser";
import { IAccount } from "./../interfaces/IAccount";
import { INewAccount } from "./../interfaces/NewAccount/INewAccount";
import { post } from "../utils/ApiRequest";

export const accountService = {
  openNew,
};

function openNew(newAccount: INewAccount) {
  return post<IAccount>(`account/create`, newAccount).then((account) => {
    const userWithAccounts = localStorage.getItem("user");
    if (userWithAccounts !== null) {
      const parsedUserWithAccounts: IUser = JSON.parse(userWithAccounts);
      parsedUserWithAccounts.accounts.push(account);
    }
    return account;
  });
}
