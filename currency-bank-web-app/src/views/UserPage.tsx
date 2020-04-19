import React, { useState, useEffect } from "react";
import {
  createStyles,
  Theme,
  withStyles,
  WithStyles
} from "@material-ui/core/styles";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import MuiDialogTitle from "@material-ui/core/DialogTitle";
import MuiDialogContent from "@material-ui/core/DialogContent";
import MuiDialogActions from "@material-ui/core/DialogActions";
import IconButton from "@material-ui/core/IconButton";
import CloseIcon from "@material-ui/icons/Close";
import Typography from "@material-ui/core/Typography";
import { IUser } from "../interfaces/login/IUser";
import { TextField } from "@material-ui/core";

const styles = (theme: Theme) =>
  createStyles({
    root: {
      margin: 0,
      padding: theme.spacing(2)
    },
    closeButton: {
      position: "absolute",
      right: theme.spacing(1),
      top: theme.spacing(1),
      color: theme.palette.grey[500]
    }
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
    padding: theme.spacing(2)
  }
}))(MuiDialogContent);

const DialogActions = withStyles((theme: Theme) => ({
  root: {
    margin: 0,
    padding: theme.spacing(1)
  }
}))(MuiDialogActions);

interface IProps {
  user: IUser | null;
  isOpen: boolean;
  onClose: () => void;
}

export default function UserPage(props: IProps) {
  const { user } = props;
  const [username, setUsername] = useState<string>("");
  const [firstname, setFirstname] = useState<string>("");
  const [lastname, setLastname] = useState<string>("");
  const [pesel, setPesel] = useState<string>("");

  useEffect(() => {
    if (user !== null) {
      setUsername(user.userName);
      setFirstname(user.firstName);
      setLastname(user.lastName);
      setPesel(user.pesel);
    }
  }, [user]);

  return (
    <div>
      <Dialog onClose={props.onClose} open={props.isOpen}>
        <DialogTitle id="user-page-title" onClose={props.onClose}>
          User Page
        </DialogTitle>
        <DialogContent dividers>
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="username"
            label="Username"
            type="text"
            id="username"
            value={username}
            onChange={e => setUsername(e.target.value)}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="firstname"
            label="Firstname"
            type="text"
            id="firstname"
            value={firstname}
            onChange={e => setFirstname(e.target.value)}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="lastname"
            label="Lastname"
            type="text"
            id="lastname"
            value={lastname}
            onChange={e => setLastname(e.target.value)}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="pesel"
            label="Pesel"
            type="text"
            id="pesel"
            value={pesel}
            onChange={e => setPesel(e.target.value)}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={props.onClose} color="primary">
            Save changes
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
