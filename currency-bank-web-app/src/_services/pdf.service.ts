import { IPdfParameters } from "./../interfaces/Pdf/IPdfParameters";
import { _post } from "../utils/ApiRequest";

export const pdfService = {
  getAll,
};

function getAll(pdfParams: IPdfParameters) {
  return _post<any>(`/pdfcreator/getAll`, pdfParams);
}
