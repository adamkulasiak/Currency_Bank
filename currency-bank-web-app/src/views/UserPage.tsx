import React, { useState, useEffect } from "react";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import { IUser } from "../interfaces/login/IUser";
import { TextField } from "@material-ui/core";
import { IUserForUpdateDto } from "../interfaces/user/IUserForUpdateDto";
import { userService } from "../_services/user.service";
import { alertActions } from "../_actions/alert.actions";
import { loadingActions } from "../_actions/loading.actions";
import { DialogTitle, DialogContent, DialogActions } from "../components/Modal";
import { authActions } from "../_actions/auth.actions";

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
  const [pathToPdfFolder, setPathToPdfFolder] = useState<string>("");

  useEffect(() => {
    if (user !== null) {
      setUserId(user.id);
      setUsername(user.userName);
      setFirstname(user.firstName);
      setLastname(user.lastName);
      setEmail(user.email);
      setPesel(user.pesel);
      setPathToPdfFolder(user.pathToPdfFolder);
    }
  }, [user]);

  const handleUpdateUser = () => {
    props.dispatch(loadingActions.enableLoading());
    const userForUpdate: IUserForUpdateDto = {
      Id: userId ?? 0,
      UserName: username,
      FirstName: firstname,
      LastName: lastname,
      Email: email,
      PathToPdfFolder: pathToPdfFolder,
    };
    userService
      .update(userForUpdate)
      .then(() => {
        props.dispatch(alertActions.success("Changes successfull!"));
        props.onClose();
      })
      .finally(() => props.dispatch(loadingActions.disableLoading()));
  };

  return (
    <div>
      <Dialog
        onClose={props.onClose}
        open={props.isOpen}
        style={{ zIndex: 200 }}
      >
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
            onChange={(e) => setUsername(e.target.value)}
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
            onChange={(e) => setFirstname(e.target.value)}
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
            onChange={(e) => setLastname(e.target.value)}
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
            onChange={(e) => setEmail(e.target.value)}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="pathToPdfFolder"
            label="Path to pdf folder"
            type="text"
            id="pathToPdfFolder"
            value={pathToPdfFolder}
            onChange={(e) => setPathToPdfFolder(e.target.value)}
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
