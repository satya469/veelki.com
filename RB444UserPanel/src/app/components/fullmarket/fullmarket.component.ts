import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import { interval, of, Subscription } from 'rxjs';
import { catchError, map, } from 'rxjs/operators';
import { ResponseModel } from 'src/app/models/responseModel';
import { StackLimit } from 'src/app/models/stackLimit';
import { AuthService } from 'src/app/services/auth.service';
import { HttpService } from 'src/app/services/http.service';
import { NotificationService } from 'src/app/services/notification.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-fullmarket',
  templateUrl: './fullmarket.component.html',
  styleUrls: ['./fullmarket.component.css']
})
export class FullmarketComponent implements OnInit, OnDestroy {

  sportId : number = 0;
  marketId: number = 0;
  eventId : number = 0;

  inPlay: boolean = false;
  
  subscription: Subscription = new Subscription();
  
  isUserLogin: boolean = false;

  matchOddsData: any[] = [];
  bookmakerData: any[] = [];
  fancyBetData: any[] = [];  

  constructor(
    private service: HttpService, 
    private activatedRoute: ActivatedRoute, 
    private authService: AuthService
    ) {

    this.authService._isLoginUser.subscribe((res) => this.isUserLogin = res);
    this.activatedRoute.params.subscribe(paramsId => {
      this.sportId = paramsId.sportId;
      this.marketId = paramsId.marketId;
      this.eventId = paramsId.eventId;
      this.getMarketsOfEventList(this.marketId, this.eventId, this.sportId);
    });
  }  

  getMarketsOfEventList: any = (marketId:number, eventId: number, sportId: number) => {
    this.service.get(`exchange/GetMatchOdds?marketId=${marketId}&eventId=${eventId}&SportId=${sportId}`)
      .subscribe((response:ResponseModel) => {
        if (response.isSuccess == true && response.data.data != null) {
          if(response.data.data.matchOddsData != null){
            this.matchOddsData = response.data.data.matchOddsData;
          }
          if(response.data.data.bookmakersData != null){
            this.bookmakerData = response.data.data.bookmakersData;
          }
          if(response.data.data.fancyData != null){
            this.fancyBetData = response.data.data.fancyData;
          }
          if(response.data.data.inPlay == true && this.isUserLogin == true){
            this.inPlay = response.data.data.inPlay;
            setTimeout(() => {
              this.getMarketsOfEventList(this.marketId, this.eventId, this.sportId);
            }, 2000);
          }
        }
      });
  }

  ngOnInit(): void {
    this.getMarketsOfEventList(this.marketId, this.eventId, this.sportId);    
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
