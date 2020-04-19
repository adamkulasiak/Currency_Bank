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
import { IUserForUpdateDto } from "../interfaces/user/IUserForUpdateDto";
import { userService } from "../_services/user.service";
import { alertActions } from "../_actions/alert.actions";

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
  dispatch: any;
  user: IUser | null;
  isOpen: boolean;
  onClose: () => void;
}

export default function UserPage(props: IProps) {
  const { user } = props;
  const [userId, setUserId] = useState<number>();
  const [username, setUsername] = useState<string>("");
  const [firstname, setFirstname] = useState<string>("");
  const [lastname, setLastname] = useState<string>("");
  const [email, setEmail] = useState<string>("");
  const [pesel, setPesel] = useState<string>("");

  useEffect(() => {
    if (user !== null) {
      setUserId(user.id);
      setUsername(user.userName);
      setFirstname(user.firstName);
      setLastname(user.lastName);
      setEmail(user.email);
      setPesel(user.pesel);
    }
  }, [user]);

  const handleUpdateUser = () => {
    const userForUpdate: IUserForUpdateDto = {
      Id: userId ?? 0,
      UserName: username,
      FirstName: firstname,
      LastName: lastname,
      Email: email
    };
    userService.update(userForUpdate).then(() => {
      props.dispatch(alertActions.success("Changes successfull!"));
      props.onClose();
    });
  };

  return (
    <div>
      <Dialog onClose={props.onClose} open={props.isOpen}>
        <DialogTitle id="user-page-title" onClose={props.onClose}>
          User Page
        </DialogTitle>
        <DialogContent dividers>
          <TextField
            disabled
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="userId"
            label="User ID"
            type="text"
            id="userId"
            value={userId}
          />
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
            name="email"
            label="Email"
            type="text"
            id="email"
            value={email}
            onChange={e => setEmail(e.target.value)}
          />
          <TextField
            disabled
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="pesel"
            label="Pesel"
            type="text"
            id="pesel"
            value={pesel}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleUpdateUser} color="primary">
            Save changes
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
