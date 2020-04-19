import { loadingContants } from "../_constants/loading.contants";

export interface ILoadingState {
  isLoading: boolean;
}

const initialState: ILoadingState = { isLoading: false };

export function loading(state = initialState, action: any) {
  switch (action.type) {
    case loadingContants.ENABLE_LOADING:
      return {
        isLoading: true
      };
    case loadingContants.DISABLE_LOADING:
      return {
        isLoading: false
      };
    default:
      return state;
  }
}
