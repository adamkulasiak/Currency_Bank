import { IAccount } from "./IAccount";

export interface IUser {
  id: number;
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  pesel: string;
  createdDate: Date;
  token: string;
  accounts: IAccount[];
}
