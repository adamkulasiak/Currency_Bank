import React, { useEffect } from "react";
import Snackbar from "@material-ui/core/Snackbar";
import MuiAlert, { AlertProps } from "@material-ui/lab/Alert";
import { AlertType } from "../enums/AlertType";

interface IProps {
  message: string;
}

function Alert(props: AlertProps) {
  return <MuiAlert elevation={6} variant="filled" {...props} />;
}

export default function Snack(props: IProps) {
  const [open, setOpen] = React.useState(false);

  const handleClose = () => {
    setOpen(false);
  };

  useEffect(() => {
    setOpen(true);
    setTimeout(() => {
      setOpen(false);
    }, 4000);
  }, [props.message]);

  return (
    <div>
      <Snackbar
        anchorOrigin={{
          vertical: "bottom",
          horizontal: "right",
        }}
        open={open}
        onClose={handleClose}
      >
        <Alert onClose={handleClose} severity="success">
          {props.message}
        </Alert>
      </Snackbar>
    </div>
  );
}