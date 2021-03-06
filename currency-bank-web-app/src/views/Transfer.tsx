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

export default function Transfer(props: IProps) {
  const [sourceAccountId, setSourceAccountId] = useState<number>();
  const [destinationAccountNumber, setDestinationAccountNumber] = useState<
    string
  >("");
  const [ammount, setAmmount] = useState<string>("");

  const handleTransfer = () => {
    props.dispatch(loadingActions.enableLoading());
    accountService
      .transfer(
        sourceAccountId ?? 0,
        destinationAccountNumber,
        parseFloat(ammount) ?? 0
      )
      .then(() => {
        props.dispatch(alertActions.success("Transfer succeeded!"));
        props.onRefreshAccounts();
        props.onClose();
        setSourceAccountId(undefined);
        setAmmount("");
        setDestinationAccountNumber("");
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
        <DialogTitle id="transfer-title" onClose={props.onClose}>
          Transfer Money
        </DialogTitle>
        <DialogContent dividers>
          <Autocomplete
            id="accounts-dropdown"
            options={createAccountsDropdown(props.accounts)}
            getOptionLabel={(option) => option.label}
            onChange={(e: any, v: any) => setSourceAccountId(v?.value)}
            value={getOption(sourceAccountId ?? 0, props.accounts)}
            renderInput={(params) => (
              <TextField
                {...params}
                label="Source account"
                variant="outlined"
              />
            )}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="destination-account"
            label="Destination account number"
            type="text"
            id="destination-account"
            value={destinationAccountNumber || ""}
            onChange={(e) => setDestinationAccountNumber(e.target.value)}
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
            onClick={() => handleTransfer()}
            color="primary"
            disabled={
              sourceAccountId === undefined ||
              sourceAccountId === null ||
              destinationAccountNumber.length === 0 ||
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
