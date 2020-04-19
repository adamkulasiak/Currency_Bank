import axios from "axios";
import { apiHeader } from "../_helpers/api-header";

export async function get<T>(url: string): Promise<T> {
  return await axios.get(url, apiHeader()).then(response => response.data);
}

export async function post<T>(url: string, data: any): Promise<T> {
  return await axios
    .post(url, data, apiHeader())
    .then(response => response.data);
}

export async function put<T>(url: string, data: any): Promise<T> {
  return await axios
    .put(url, data, apiHeader())
    .then(response => response.data);
}
