import { IHistory } from "./IHistory";

export interface IHistoryAccounts {
  accountId: number;
  accountNumber: string;
  balance: number;
  history: IHistory[];
}
