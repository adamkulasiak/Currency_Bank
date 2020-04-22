import axios from "axios";
import { apiHeader } from "../_helpers/api-header";

export async function _get<T>(url: string): Promise<T> {
  return await axios.get(url, apiHeader()).then((response) => response.data);
}

export async function _post<T>(url: string, data?: any): Promise<T> {
  return await axios
    .post(url, data, apiHeader())
    .then((response) => response.data);
}

export async function _put<T>(url: string, data?: any): Promise<T> {
  return await axios
    .put(url, data, apiHeader())
    .then((response) => response.data);
}

export async function _delete<T>(url: string): Promise<T> {
  return await axios.delete(url, apiHeader()).then((response) => response.data);
}
