import { IHistory } from "./IHistory";
import { Currency } from "../enums/Currency";

export interface IHistoryAccounts {
  id: number;
  accountNumber: string;
  balance: number;
  currency: Currency;
  history: IHistory[];
}
