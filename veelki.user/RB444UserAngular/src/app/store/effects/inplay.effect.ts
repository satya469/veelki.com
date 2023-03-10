import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { map, mergeMap, switchMap } from "rxjs/operators";
import { HttpService } from "src/app/services/http.service";
import * as INPLAY_ACTION from "../actions/inplay.action";



@Injectable()
export class InplayEffect {
    constructor(private actions : Actions, private service : HttpService){
    }

    InplayData = createEffect(() => this.actions.pipe(
        ofType(INPLAY_ACTION.GET_INPLAY),
        switchMap(() => this.service.get('exchange/GetInPlaySportEvents').pipe(
            map(res => (res.isSuccess == true && res.data != null) ? INPLAY_ACTION.GET_INPLAY_SUCCESS({inplay:res.data}) : INPLAY_ACTION.GET_INPLAY_SUCCESS({inplay:null}) )
        ))
    ));    

}