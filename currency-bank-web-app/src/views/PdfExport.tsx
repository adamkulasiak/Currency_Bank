import React, { useState } from "react";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import { TextField, FormControlLabel, Switch } from "@material-ui/core";
import { alertActions } from "../_actions/alert.actions";
import { DialogTitle, DialogContent, DialogActions } from "../components/Modal";
import { pdfService } from "../_services/pdf.service";
import { IPdfParameters } from "../interfaces/Pdf/IPdfParameters";
import { loadingActions } from "../_actions/loading.actions";

interface IProps {
  dispatch: any;
  isOpen: boolean;
  onClose: () => void;
}

export default function PdfExport(props: IProps) {
  const [filename, setFilename] = useState<string>("");
  const [saveToDefinedPath, setSaveToDefinedPath] = useState<boolean>(true);

  const handleSave = () => {
    props.dispatch(loadingActions.enableLoading());
    const obj: IPdfParameters = {
      Filename: filename,
      SaveToDefinedPath: saveToDefinedPath,
    };

    pdfService
      .getAll(obj)
      .then((msg: string) => {
        props.dispatch(alertActions.success(msg));
      })
      .catch((err) => {
        props.dispatch(alertActions.error("Error during printing PDF"));
      })
      .finally(() => {
        props.onClose();
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
        <DialogTitle id="pdf-export-title" onClose={props.onClose}>
          Export to PDF
        </DialogTitle>
        <DialogContent dividers>
          <TextField
            variant="outlined"
            margin="normal"
            fullWidth
            name="filename"
            label="Filename"
            type="text"
            id="filename"
            value={filename}
            onChange={(e) => setFilename(e.target.value)}
          />
          <FormControlLabel
            control={
              <Switch
                checked={saveToDefinedPath}
                disabled
                onChange={(e) => setSaveToDefinedPath(e.target.checked)}
                name="saveToDefinedPath"
                color="primary"
              />
            }
            label="Save do defined path"
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => handleSave()} color="primary">
            Save
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}
