import React, { useState, useEffect } from "react";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import Autocomplete from "@material-ui/lab/Autocomplete";
import { TextField, Chip, CircularProgress } from "@material-ui/core";
import { IAccount } from "../interfaces/IAccount";
import { loadingActions } from "../_actions/loading.actions";
import { accountService } from "../_services/account.service";
import { alertActions } from "../_actions/alert.actions";
import { DialogTitle, DialogContent, DialogActions } from "../components/Modal";
import { createCustomAccountsDropdown, getOption } from "../utils/Accounts";
import "./Exchange.css";
import { Currency } from "../enums/Currency";
import { ratesService } from "../_services/rates.service";

interface IProps {
  dispatch: any;
  isOpen: boolean;
  accounts: IAccount[];
  onClose: () => void;
  onRefreshAccounts: () => void;
}

export default function Exchange(props: IProps) {
  const [sourceAccountId, setSourceAccountId] = useState<number>();
  const [destinationAccountId, setDestinationAccountId] = useState<number>();
  const [ammount, setAmmount] = useState<string>("");
  const [currencyChip, setCurrencyChip] = useState<string>("");
  const [sumOfAmmount, setSumOfAmmount] = useState<string>("");
  const [destCurrency, setDestCurrency] = useState<string>("");
  const [loadingChip, setLoadingChip] = useState<boolean>(false);

  useEffect(() => {
    getRate();
    getSumOfAmmount();
  }, [sourceAccountId]);

  useEffect(() => {
    getRate();
    getSumOfAmmount();
    if (destinationAccountId !== undefined) {
      const destAccount = props.accounts.find(
        (a) => a.id === destinationAccountId
      );
      setDestCurrency(Currency[destAccount.currency]);
    }
  }, [destinationAccountId]);

  useEffect(() => {
    getSumOfAmmount();
  }, [ammount]);

  const getRate = () => {
    if (sourceAccountId !== undefined && destinationAccountId !== undefined) {
      setLoadingChip(true);
      const sourceAccount = props.accounts.find(
        (a) => a.id === sourceAccountId
      );
      const destAccount = props.accounts.find(
        (a) => a.id === destinationAccountId
      );
      const srcCurrency = Currency[sourceAccount.currency];
      const destCurrency = Currency[destAccount.currency];
      ratesService
        .getRate(srcCurrency, destCurrency)
        .then((currency) => {
          setCurrencyChip(currency);
        })
        .finally(() => setLoadingChip(false));
    } else {
      setCurrencyChip("");
    }
  };

  const getSumOfAmmount = () => {
    if (
      sourceAccountId !== undefined &&
      destinationAccountId !== undefined &&
      ammount !== "" &&
      currencyChip !== ""
    ) {
      let value = (parseFloat(ammount) * parseFloat(currencyChip)).toFixed(2);
      setSumOfAmmount(`${value.toString()} ${destCurrency}`);
    } else {
      setSumOfAmmount("");
    }
  };

  const handleTransfer = () => {
    props.dispatch(loadingActions.enableLoading());
    accountService
      .exchange(
        sourceAccountId ?? 0,
        destinationAccountId ?? 0,
        parseFloat(ammount) ?? 0
      )
      .then(() => {
        props.dispatch(alertActions.success("Exchange succeeded!"));
        props.onRefreshAccounts();
        props.onClose();
        setAmmount("");
        setSourceAccountId(undefined);
        setDestinationAccountId(undefined);
      })
      .catch((err) => {
        props.dispatch(alertActions.error(err.response.data));
      })
      .finally(() => {
        props.dispatch(loadingActions.disableLoading());
      });
  };

  return (
    <div>
      <Dialog
        onClose={props.onClose}
        open={props.isOpen}
        style={{ zIndex: 200 }}
      >
        <DialogTitle id="exchange-title" onClose={props.onClose}>
          Exchange
        </DialogTitle>
        <DialogContent dividers>
          <Autocomplete
            style={{ marginBottom: 12 }}
            options={createCustomAccountsDropdown(
              props.accounts,
              destinationAccountId
            )}
            getOptionLabel={(option) => option.label}
            onChange={(e: any, v: any) =>
              setSourceAccountId(v?.value || undefined)
            }
            value={getOption(sourceAccountId ?? 0, props.accounts)}
            renderInput={(params) => (
              <TextField
                {...params}
                label="Source currency"
                variant="outlined"
              />
            )}
          />
          <Autocomplete
            options={createCustomAccountsDropdown(
              props.accounts,
              sourceAccountId
            )}
            getOptionLabel={(option) => option.label}
            onChange={(e: any, v: any) =>
              setDestinationAccountId(v?.value || undefined)
            }
            value={getOption(destinationAccountId ?? 0, props.accounts)}
            renderInput={(params) => (
              <TextField
                {...params}
                label="Destination currency"
                variant="outlined"
              />
            )}
          />
          <Chip
            className="mt"
            label={currencyChip}
            style={{ display: currencyChip === "" ? "none" : "inline-flex" }}
          />
          {loadingChip && <CircularProgress className="mt" size={20} />}

          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="ammount"
            label="Ammount"
            type="number"
            id="ammount"
            value={ammount}
            onChange={(e) => {
              setAmmount(e.target.value);
            }}
          />
          <Chip
            id="sum-ammount"
            label={sumOfAmmount}
            style={{ display: sumOfAmmount === "" ? "none" : "inline-flex" }}
          />
        </DialogContent>
        <DialogActions>
          <Button
            onClick={() => handleTransfer()}
            color="primary"
            disabled={
              sourceAccountId === undefined ||
              destinationAccountId === undefined ||
              ammount === "" ||
              parseFloat(ammount) <= 0
            }
          >
            Transfer
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
