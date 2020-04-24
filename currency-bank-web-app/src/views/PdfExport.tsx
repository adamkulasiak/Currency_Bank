import React, { useState } from "react";
import Button from "@material-ui/core/Button";
import Dialog from "@material-ui/core/Dialog";
import { TextField, FormControlLabel, Switch } from "@material-ui/core";
import { alertActions } from "../_actions/alert.actions";
import { DialogTitle, DialogContent, DialogActions } from "../components/Modal";
import { pdfService } from "../_services/pdf.service";
import { IPdfParameters } from "../interfaces/Pdf/IPdfParameters";

interface IProps {
  dispatch: any;
  isOpen: boolean;
  onClose: () => void;
}

export default function PdfExport(props: IProps) {
  const [filename, setFilename] = useState<string>("");
  const [saveToDefinedPath, setSaveToDefinedPath] = useState<boolean>(false);

  const handleSave = () => {
    const obj: IPdfParameters = {
      Filename: filename,
      SaveToDefinedPath: saveToDefinedPath,
    };

    pdfService
      .getAll(obj)
      .then((msg) => {
        props.dispatch(alertActions.success(msg));
      })
      .finally(() => {
        props.onClose();
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
