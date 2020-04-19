import * as React from "react";
import { Grid, Button } from "@material-ui/core";

interface IProps {}

export default function ActionButtons(props: IProps) {
  return (
    <Grid container spacing={4}>
      <Grid item>
        <Button fullWidth variant="contained" color="primary">
          Open new account
        </Button>
      </Grid>
      <Grid item>
        <Button fullWidth variant="contained" color="primary">
          New Transfer
        </Button>
      </Grid>
      <Grid item>
        <Button fullWidth variant="contained" color="primary">
          Exchange
        </Button>
      </Grid>
      <Grid item>
        <Button fullWidth variant="contained" color="primary">
          Withdrawal
        </Button>
      </Grid>
      <Grid item>
        <Button fullWidth variant="contained" color="primary">
          Deposit
        </Button>
      </Grid>
    </Grid>
  );
}
