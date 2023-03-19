import { state } from "@angular/animations";
import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, mergeMap, switchMap } from "rxjs/operators";
import { HttpService } from "src/app/services/http.service";
import { NotificationService } from "src/app/services/notification.service";
import * as StackAction from "../actions/stack.action";

@Injectable()
export class StackEffects {
    constructor(
        private actions : Actions, 
        private service: HttpService,
        public notification: NotificationService,
        ){        
    }

    StackData = createEffect(() => this.actions.pipe(
        ofType(StackAction.GET_STACK),
        switchMap(() => this.service.get('Setting/GetStakeLimit').pipe(
            map((res) => {
                if(res.data != null && res.isSuccess == true){
                    return StackAction.GET_STACK_SUCCESS({stack : res.data})
                }
                return StackAction.GET_STACK_SUCCESS({stack : []})
            })
        ))
    ))

    StackDataUpdate = createEffect(() => this.actions.pipe(
        ofType(StackAction.UPDATE_STACK),
        switchMap((data) => {
            return this.service.post('Setting/UpdateStakeLimit', data.stack).pipe(
            map((res) => {
                if(res.isSuccess == true){
                    this.notification.showSuccess(res.message);
                    return StackAction.UPDATE_STACK_SUCCESS({stack : data.stack})
                }
                this.notification.showError(res.message);
                return StackAction.UPDATE_STACK_SUCCESS({stack : data.stack })
            })
        )
})
    ))

}