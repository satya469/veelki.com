import { SessionService } from './session.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { HttpService } from './http.service';
import { ResponseModel } from '../models/responseModel';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
  })
export class BetService {

    constructor(private service: HttpService, private session: SessionService, private route: Router) { }

    _getBetData = new BehaviorSubject<Observable<ResponseModel>>(this.getMarketList());


    getMarketList(): Observable<ResponseModel> {
        let userId = this.session.getLoggedInUser() ? this.session.getLoggedInUser().id : 0;
        return this.service.get(`Common/GetMarketList?UserId=${userId}`);
    }

}
