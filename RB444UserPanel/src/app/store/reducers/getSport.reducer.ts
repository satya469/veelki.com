import { createReducer, on } from "@ngrx/store";
import * as SPORT_ACTION from "../actions/getSport.action";


const initialState :any = null;
export const SportReducer = createReducer(
    initialState,
    on(SPORT_ACTION.GET_SPORT_SUCCESS, (state, action) => (action.sport))
)