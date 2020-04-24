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
import { createCustomAccountsDropdown, getOption } from "../utils/Accounts";

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
  const [ammount, setAmmount] = useState<number>();

  const handleTransfer = () => {
    props.dispatch(loadingActions.enableLoading());
    accountService
      .exchange(sourceAccountId ?? 0, destinationAccountId ?? 0, ammount ?? 0)
      .then(() => {
        props.dispatch(alertActions.success("Exchange succeeded!"));
        props.onRefreshAccounts();
        props.onClose();
      })
      .finally(() => {
        props.dispatch(loadingActions.disableLoading());
        setAmmount(undefined);
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
            id="accounts-dropdown"
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
            id="accounts-dropdown"
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
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="ammount"
            label="Ammount"
            type="number"
            id="ammount"
            value={ammount || undefined}
            onChange={(e) => setAmmount(parseFloat(e.target.value))}
          />
        </DialogContent>
        <DialogActions>
          <Button
            onClick={() => handleTransfer()}
            color="primary"
            disabled={
              sourceAccountId === undefined ||
              destinationAccountId === undefined ||
              ammount === undefined ||
              isNaN(ammount) ||
              ammount <= 0
            }
          >
            Transfer
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
