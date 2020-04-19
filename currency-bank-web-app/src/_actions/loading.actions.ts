import { loadingContants } from "../_constants/loading.contants";

export const loadingActions = {
  enableLoading,
  disableLoading
};

function enableLoading() {
  return { type: loadingContants.ENABLE_LOADING };
}

function disableLoading() {
  return { type: loadingContants.DISABLE_LOADING };
}
