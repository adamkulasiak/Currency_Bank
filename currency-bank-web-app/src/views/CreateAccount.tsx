import React, { useState, useEffect } from "react";
import {
  createStyles,
  Theme,
  withStyles,
  WithStyles,
} from "@material-ui/core/styles";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import MuiDialogTitle from "@material-ui/core/DialogTitle";
import MuiDialogContent from "@material-ui/core/DialogContent";
import MuiDialogActions from "@material-ui/core/DialogActions";
import IconButton from "@material-ui/core/IconButton";
import CloseIcon from "@material-ui/icons/Close";
import Typography from "@material-ui/core/Typography";
import Autocomplete from "@material-ui/lab/Autocomplete";
import { TextField } from "@material-ui/core";
import { Currency } from "../enums/Currency";
import { ICurrency } from "../interfaces/Dropdowns/ICurrency";
import { accountService } from "../_services/account.service";
import { INewAccount } from "../interfaces/NewAccount/INewAccount";
import { alertActions } from "../_actions/alert.actions";
import { loadingActions } from "../_actions/loading.actions";
import { authActions } from "../_actions/auth.actions";

const styles = (theme: Theme) =>
  createStyles({
    root: {
      margin: 0,
      padding: theme.spacing(2),
    },
    closeButton: {
      position: "absolute",
      right: theme.spacing(1),
      top: theme.spacing(1),
      color: theme.palette.grey[500],
    },
  });

export interface DialogTitleProps extends WithStyles<typeof styles> {
  id: string;
  children: React.ReactNode;
  onClose: () => void;
}

const DialogTitle = withStyles(styles)((props: DialogTitleProps) => {
  const { children, classes, onClose, ...other } = props;
  return (
    <MuiDialogTitle disableTypography className={classes.root} {...other}>
      <Typography variant="h6">{children}</Typography>
      {onClose ? (
        <IconButton
          aria-label="close"
          className={classes.closeButton}
          onClick={onClose}
        >
          <CloseIcon />
        </IconButton>
      ) : null}
    </MuiDialogTitle>
  );
});

const DialogContent = withStyles((theme: Theme) => ({
  root: {
    padding: theme.spacing(2),
    width: 500,
  },
}))(MuiDialogContent);

const DialogActions = withStyles((theme: Theme) => ({
  root: {
    margin: 0,
    padding: theme.spacing(1),
  },
}))(MuiDialogActions);

interface IProps {
  dispatch: any;
  isOpen: boolean;
  onClose: () => void;
}

export default function CreateAccount(props: IProps) {
  const [selectedCurrency, setSelectedCurrency] = useState<ICurrency>();

  const getAllCurrencies = (): ICurrency[] => {
    const currencyList: ICurrency[] = [];
    for (let i = 1; i <= 4; i++) {
      const currency: ICurrency = {
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
        props.dispatch(authActions.refresh());
        props.onClose();
      })
      .finally(() => props.dispatch(loadingActions.disableLoading()));
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
            renderInput={(params) => (
              <TextField {...params} label="Currencies" variant="outlined" />
            )}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleOpenAccount} color="primary">
            Open account
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
