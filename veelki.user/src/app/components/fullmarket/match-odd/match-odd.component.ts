import { Component, ElementRef, Input, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/models/responseModel';
import { StackLimit } from 'src/app/models/stackLimit';
import { AuthService } from 'src/app/services/auth.service';
import { BetService } from 'src/app/services/getBet.service';
import { HttpService } from 'src/app/services/http.service';
import { LoaderService } from 'src/app/services/loader.service';
import { NotificationService } from 'src/app/services/notification.service';
import { SessionService } from 'src/app/services/session.service';
import { SubjectService } from 'src/app/services/subject.service';

@Component({
  selector: 'app-match-odd',
  templateUrl: './match-odd.component.html',
  styleUrls: ['./match-odd.component.css']
})
export class MatchOddComponent implements OnInit {

  @Input() sportId?: number;
  @Input() matchOddsData?: any[];
  @Input() isUserLogin?: boolean;
  @Input() inPlay?: boolean;
  @Input() marketId?: number;
  userId: number = this.sessionService.getLoggedInUser() ? this.sessionService.getLoggedInUser().id : 0;

  //@ViewChild('acc') getNgbAccordion?: NgbAccordion;
  openBetBox : boolean = false;


  stackData?: Observable<StackLimit[]>;
  stackLimitList: StackLimit[] = [];

  BidData: any[] = [];
  BidDataNew: any[] = [];


  bidPriceInput: number = 0;
  bidOddPrice: number = 0;
  oddBook: boolean = false;


  stackValue : number = 0;
  oddValue : number = 0;
  selectionId : any = 0;
  betType : any = 0;
  openMobView : boolean = false;


  constructor(
    private service: HttpService,
    private sessionService: SessionService,
    private notification: NotificationService,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private betService: BetService,
    private loaderService: LoaderService,
    private subjectService : SubjectService
  ) {
    this.authService._isLoginUser.subscribe((res) => this.isUserLogin = res);
  }

  ngOnChanges(changes: SimpleChanges): void {

  }

  closeBetSlipType($event:boolean){
    if($event === true){
      //this.getNgbAccordion?.toggle("toggle-1");
    }
    this.openMobView = false;
  }

  setBetData?: betData;

  setBetPrice(EventId :number, event:string, MarketId:number, market:string, selection:string, selectionId:number, type:number,oddsRequest:number, amountStake:number, betType:number){
    this.setBetData = {
      sportId: this.sportId,
      EventId: EventId,
      event: event,
      MarketId: MarketId,
      market: market,
      selection: selection,
      selectionId: selectionId,
      OddsType: 1,
      type: type ? 'lay' : 'back',
      oddsRequest: oddsRequest,
      amountStake: amountStake,
      betType: betType,
      isSettlement: 2
    }
    this.selectionId = selectionId;
    this.betType = betType;
    this.openMobView = true;
    this.subjectService.setBetData(this.setBetData);
  }

  getBackLayAmount() {
    this.service.get(`BetApi/GetBackAndLayBetAmount?UserId=${this.userId}&marketId=${this.marketId}&SportId=${this.sportId}`)
      .subscribe((response: ResponseModel) => {
        if (response.isSuccess == true && response.data != null) {
          this.BidData = JSON.parse(response.data);
          this.BidDataNew = JSON.parse(JSON.stringify(this.BidData));
        }
      });
  }

  ngOnInit() {
    this.service.get('Setting/GetStakeLimit').subscribe(res =>{
      if(res.data != null && res.isSuccess == true){
        this.stackLimitList = res.data;
      }
    });

    this.activatedRoute.params.subscribe(paramsId => {
      this.sportId = this.sportId ? this.sportId : paramsId["sportId"];
      this.marketId = this.marketId ? this.marketId : paramsId["marketId"];
      this.getBackLayAmount();
    });

    this.subjectService.betDataSubject.subscribe((data:betData) =>{
      this.setBetData = {...data};
      if(this.setBetData.sportId){
        this.oddValue = Number(this.setBetData.oddsRequest);
        this.stackValue = Number(this.setBetData.amountStake);
      }
      if(this.oddValue > 0 && this.stackValue > 0){
        this.oddBook = true;
      }else{
        this.oddBook = false;
      }
      this.BidDataNew = this.BidDataNew.reduce((a, v, i) => {
        const [key, value] = Object.entries(v);
        this.BidDataNew[i][key[0]] = 0;
        if (key[0] == this.selectionId) {
          v[key[0]] = this.betType == 0 ? parseInt(this.BidData[i][key[0]]) + ((this.oddValue * this.stackValue) - this.stackValue) : parseInt(this.BidData[i][key[0]]) - ((this.oddValue * this.stackValue) - this.stackValue);
        } else {
          v[key[0]] = this.betType == 0 ? parseInt(this.BidData[i][key[0]]) - this.stackValue : parseInt(this.BidData[i][key[0]]) + this.stackValue;
        }
        v[key[0]] = Math.trunc(100*v[key[0]])/100;
        a.push(v);
        return a;
      }, []);
    })

  }
}


export interface betData {
  sportId?: number;
  EventId?: number;
  event?: string;
  MarketId?: number;
  market?: string;
  selection?: string;
  selectionId?: number;
  OddsType?: number;
  type?: string;
  oddsRequest?: number;
  amountStake?: number;
  betType?: number;
  isSettlement?: number;
}

export interface betCalc {
  oddsValue?: number;
  stakeValue?: number;
  selectionId?: number;
  betType?: number;
}