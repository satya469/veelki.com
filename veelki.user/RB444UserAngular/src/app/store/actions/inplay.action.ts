import { createAction, props } from "@ngrx/store";

export const GET_INPLAY = createAction(
    "GET_INPLAY"
)

export const GET_INPLAY_SUCCESS = createAction(
    "GET_INPLAY_SUCCESS",
    props<{inplay : any}>()
)