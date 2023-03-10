import { createAction, props } from "@ngrx/store";

export const GET_SPORT = createAction(
    "GET_SPORT"
)

export const GET_SPORT_SUCCESS = createAction(
    "GET_SPORT_SUCCESS",
    props<{sport : any}>()
)