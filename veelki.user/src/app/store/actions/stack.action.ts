import { createAction, props } from "@ngrx/store";
import { ResponseModel } from "src/app/models/responseModel";
import { StackLimit } from "src/app/models/stackLimit";

export const GET_STACK = createAction("GET_STACK");
export const GET_STACK_SUCCESS = createAction(
    "GET_STACK_SUCCESS",
    props<{stack : StackLimit[]}>()
)
export const UPDATE_STACK = createAction(
    "UPDATE_STACK",
    props<{state : StackLimit[], stack : StackLimit[]}>()
)

export const UPDATE_STACK_SUCCESS = createAction(
    "UPDATE_STACK_SUCCESS",
    props<{stack : StackLimit[]}>()
)

export const ERROR_STACK = createAction(
    "ERROR_STACK",
    props<{error : ResponseModel}>()
)
