import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { map, mergeMap, switchMap } from "rxjs/operators";
import { HttpService } from "src/app/services/http.service";
import * as SPORT_ACTION from "../actions/getSport.action";



@Injectable()
export class SportEffect {
    constructor(private actions : Actions, private service : HttpService){
    }

    SportData = createEffect(() => this.actions.pipe(
        ofType(SPORT_ACTION.GET_SPORT),
        switchMap(() => this.service.get('exchange/GetSportEvents?SportId=0').pipe(
            map(res => (res.isSuccess == true && res.data != null) ? SPORT_ACTION.GET_SPORT_SUCCESS({sport:res.data}) : SPORT_ACTION.GET_SPORT_SUCCESS({sport:null}) )
        ))
    ));    

}