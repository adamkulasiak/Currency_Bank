import axios from "axios";

export function post(url: string, data: any) {
  return axios.post(url, data).then((response) => response.data);
}
