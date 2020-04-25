import React, { useState } from "react";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import Autocomplete from "@material-ui/lab/Autocomplete";
import { TextField } from "@material-ui/core";
import { IAccount } from "../interfaces/IAccount";
import { loadingActions } from "../_actions/loading.actions";
import { accountService } from "../_services/account.service";
import { alertActions } from "../_actions/alert.actions";
import { DialogTitle, DialogContent, DialogActions } from "../components/Modal";
import { createAccountsDropdown, getOption } from "../utils/Accounts";

interface IProps {
  dispatch: any;
  isOpen: boolean;
  accounts: IAccount[];
  onClose: () => void;
  onRefreshAccounts: () => void;
}

export default function CashIn(props: IProps) {
  const [selectedAccountId, setSelectedAccountId] = useState<number>();
  const [ammount, setAmmount] = useState<string>("");

  const handleCashIn = () => {
    props.dispatch(loadingActions.enableLoading());
    accountService
      .cashIn(selectedAccountId ?? 0, parseFloat(ammount) ?? 0)
      .then(() => {
        props.dispatch(alertActions.success("Cashin succeeded!"));
        props.onRefreshAccounts();
        props.onClose();
      })
      .finally(() => {
        props.dispatch(loadingActions.disableLoading());
        setSelectedAccountId(undefined);
        setAmmount("");
      });
  };

  return (
    <div>
      <Dialog
        onClose={props.onClose}
        open={props.isOpen}
        style={{ zIndex: 200 }}
      >
        <DialogTitle id="cash-out-title" onClose={props.onClose}>
          Cash In
        </DialogTitle>
        <DialogContent dividers>
          <Autocomplete
            id="accounts-dropdown"
            options={createAccountsDropdown(props.accounts)}
            getOptionLabel={(option) => option.label}
            onChange={(e: any, v: any) => setSelectedAccountId(v?.value)}
            value={getOption(selectedAccountId ?? 0, props.accounts)}
            renderInput={(params) => (
              <TextField {...params} label="Accounts" variant="outlined" />
            )}
          />
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
            onChange={(e) => setAmmount(e.target.value)}
          />
        </DialogContent>
        <DialogActions>
          <Button
            onClick={() => handleCashIn()}
            color="primary"
            disabled={
              selectedAccountId === undefined ||
              ammount === "" ||
              parseFloat(ammount) <= 0
            }
          >
            Proceed
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
