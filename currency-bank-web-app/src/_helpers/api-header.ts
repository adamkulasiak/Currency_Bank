export function apiHeader() {
  const token = localStorage.getItem("token");
  if (token) {
    return { headers: { Authorization: `Bearer ${JSON.parse(token)}` } };
  } else {
    return {};
  }
}
