import { IPdfParameters } from "./../interfaces/Pdf/IPdfParameters";
import { _post } from "../utils/ApiRequest";
const FileDownload = require("js-file-download");
const format = require("date-format");

export const pdfService = {
  getAll,
};

function getAll(pdfParams: IPdfParameters) {
  return _post<any>(`/pdfcreator/getAll`, pdfParams).then((resp) => {
    // FileDownload(
    //   resp,
    //   `${
    //     pdfParams.Filename !== ""
    //       ? pdfParams.Filename
    //       : format.asString("hh:mm:ss", new Date())
    //   }.pdf`
    // );
    return resp;
  });
}
