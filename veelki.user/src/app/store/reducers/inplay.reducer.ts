import { createReducer, on } from "@ngrx/store";
import * as INPLAY_ACTION from "../actions/inplay.action";


const initialState :any = null;
export const InplayReducer = createReducer(
    initialState,
    on(INPLAY_ACTION.GET_INPLAY_SUCCESS, (state, action) => (action.inplay))
)