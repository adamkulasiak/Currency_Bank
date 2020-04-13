import { Currency } from "../../enums/Currency";

export interface IAccount {
  id: number;
  accountNumber: string;
  currency: Currency;
  balance: number;
  isDeleted: boolean;
  deleteTime: Date;
  userId: number;
}
