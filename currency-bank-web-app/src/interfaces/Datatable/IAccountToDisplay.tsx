import { Currency } from "../../enums/Currency";

export interface IAccountToDisplay {
  id: number;
  accountNumber: string;
  currency: string;
  balance: number;
}
