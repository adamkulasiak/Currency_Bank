import React, { useState, useEffect } from "react";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import Autocomplete from "@material-ui/lab/Autocomplete";
import { TextField } from "@material-ui/core";
import { Currency } from "../enums/Currency";
import { accountService } from "../_services/account.service";
import { INewAccount } from "../interfaces/NewAccount/INewAccount";
import { alertActions } from "../_actions/alert.actions";
import { loadingActions } from "../_actions/loading.actions";
import { IDropdown } from "../interfaces/Dropdowns/IDropdown";
import { DialogTitle, DialogContent, DialogActions } from "../components/Modal";

interface IProps {
  dispatch: any;
  isOpen: boolean;
  onClose: () => void;
  onRefreshAccounts: () => void;
}

export default function CreateAccount(props: IProps) {
  const [selectedCurrency, setSelectedCurrency] = useState<IDropdown<number>>();

  const getAllCurrencies = (): IDropdown<number>[] => {
    const currencyList: IDropdown<number>[] = [];
    for (let i = 1; i <= 4; i++) {
      const currency: IDropdown<number> = {
        value: i,
        label: Currency[i],
      };
      currencyList.push(currency);
    }
    return currencyList;
  };

  const currenciesConverter = (): Currency => {
    switch (selectedCurrency?.label) {
      case "PLN":
        return Currency.PLN;
      case "EUR":
        return Currency.EUR;
      case "USD":
        return Currency.USD;
      case "GBP":
        return Currency.GBP;
      default:
        return Currency.PLN;
    }
  };

  const handleOpenAccount = () => {
    props.dispatch(loadingActions.enableLoading());
    const newAccount: INewAccount = {
      Currency: currenciesConverter(),
    };
    accountService
      .openNew(newAccount)
      .then(() => {
        props.dispatch(alertActions.success("Account has been created!"));
        props.onRefreshAccounts();
        props.onClose();
      })
      .finally(() => {
        props.dispatch(loadingActions.disableLoading());
        setSelectedCurrency(undefined);
      });
  };

  useEffect(() => {
    getAllCurrencies();
  }, []);

  return (
    <div>
      <Dialog
        onClose={props.onClose}
        open={props.isOpen}
        style={{ zIndex: 200 }}
      >
        <DialogTitle id="create-account-title" onClose={props.onClose}>
          Create new account
        </DialogTitle>
        <DialogContent dividers>
          <Autocomplete
            id="currency-dropdown"
            options={getAllCurrencies()}
            getOptionLabel={(option) => option.label}
            onChange={(e: any, v: any) => setSelectedCurrency(v)}
            value={selectedCurrency}
            renderInput={(params) => (
              <TextField {...params} label="Currencies" variant="outlined" />
            )}
          />
        </DialogContent>
        <DialogActions>
          <Button
            onClick={handleOpenAccount}
            color="primary"
            disabled={
              selectedCurrency === undefined || selectedCurrency === null
            }
          >
            Open account
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
