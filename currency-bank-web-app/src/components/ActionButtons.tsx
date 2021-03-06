import * as React from "react";
import { Grid, Button } from "@material-ui/core";
import { Link } from "react-router-dom";

interface IProps {
  createAccountOpen: boolean;
  onOpenCreateAccount: () => void;
  onOpenTransfer: () => void;
  onOpenExchange: () => void;
  onOpenCashOut: () => void;
  onOpenCashIn: () => void;
}

export default function ActionButtons(props: IProps) {
  return (
    <Grid container spacing={4}>
      <Grid item>
        <Button
          fullWidth
          variant="contained"
          color="primary"
          onClick={props.onOpenCreateAccount}
        >
          Open new account
        </Button>
      </Grid>
      <Grid item>
        <Button
          fullWidth
          variant="contained"
          color="primary"
          onClick={props.onOpenTransfer}
        >
          New Transfer
        </Button>
      </Grid>
      <Grid item>
        <Button
          fullWidth
          variant="contained"
          color="primary"
          onClick={props.onOpenExchange}
        >
          Exchange
        </Button>
      </Grid>
      <Grid item>
        <Button
          fullWidth
          variant="contained"
          color="primary"
          onClick={props.onOpenCashOut}
        >
          Withdrawal
        </Button>
      </Grid>
      <Grid item>
        <Button
          fullWidth
          variant="contained"
          color="primary"
          onClick={props.onOpenCashIn}
        >
          Deposit
        </Button>
      </Grid>
      <Grid item>
        <Button
          fullWidth
          variant="contained"
          color="primary"
          onClick={(e) => console.log("history")}
        >
          <Link to="/history">History</Link>
        </Button>
      </Grid>
    </Grid>
  );
}
