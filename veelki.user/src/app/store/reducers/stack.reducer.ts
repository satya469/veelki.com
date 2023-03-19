import { createReducer, on } from "@ngrx/store";
import { ResponseModel } from "src/app/models/responseModel";
import { StackLimit } from "src/app/models/stackLimit";
import * as StackAction from "../actions/stack.action";


const initialState : StackLimit[] = [];

export const StackReducer = createReducer(
    initialState,
    on(StackAction.GET_STACK_SUCCESS, (state, action) => (action.stack)),
    on(StackAction.UPDATE_STACK_SUCCESS, (state, action) => (
        state = action.stack
    ))
)

