import { Component, ElementRef, Input, OnInit, ViewChild, DoCheck, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable, of } from 'rxjs';
import { ResponseModel } from 'src/app/models/responseModel';
import { StackLimit } from 'src/app/models/stackLimit';
import { AuthService } from 'src/app/services/auth.service';
import { BetService } from 'src/app/services/getBet.service';
import { HttpService } from 'src/app/services/http.service';
import { LoaderService } from 'src/app/services/loader.service';
import { NotificationService } from 'src/app/services/notification.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-match-odds',
  templateUrl: './match-odds.component.html',
  styleUrls: ['./match-odds.component.css']
})
export class MatchOddsComponent implements OnInit, OnChanges, DoCheck {

  @Input() sportId?: number;
  @Input() matchOddsData?: any[];
  @Input() isUserLogin?: boolean;
  @Input() inPlay?: boolean;
  @Input() marketId?: number;
  userId : number = this.sessionService.getLoggedInUser() ? this.sessionService.getLoggedInUser().id : 0;

  @ViewChild('stackAmountEle') stackAmountEle? : ElementRef;
  @ViewChild('oddRequestEle') oddRequestEle ? : ElementRef;


  stackData?: Observable<StackLimit[]>;
  stackLimitList: StackLimit[] = [];

  matchOddsForm: FormGroup;
  OldBidData: any[] = [];
  BidData: any[] = [];
  BidDataNew: any[] = [];

  exEventId?: number;
  selectionId: number = 0;
  selectedIndex?: number;
  betType?: number;
  selectedValue?: number;
  runnerName?: string;


  bidPriceInput: number = 0;
  bidOddPrice: number = 0;
  oddBook: boolean = false;

  constructor(
    private service: HttpService,
    private sessionService: SessionService,
    private notification: NotificationService,
    private activatedRoute: ActivatedRoute,
    private authService: AuthService,
    private fb: FormBuilder,
    private betService : BetService,
    private loaderService : LoaderService,
    private store: Store<{ StackData: StackLimit[] }>
  ) {

    this.matchOddsForm = this.fb.group({
      sportId: [this.sportId],
      EventId: [''],
      event: [''],
      MarketId: [''],
      market: [''],
      selection: [''],
      selectionId: [],
      OddsType: [1],
      type: [''],
      oddsRequest: [''],
      amountStake: [''],
      betType: [],
      isSettlement: [2]
    })
    this.stackData = this.store.select(data => data.StackData);
  }
  ngDoCheck(): void {
    //console.log("ngDoCheck");
    if(this.oddsRequestFocusVal == true){
      this.oddRequestEle?.nativeElement.focus();
    }
    if(this.stackAmountFocusVal == true){
      this.stackAmountEle?.nativeElement.focus();
    }
  }
  ngOnChanges(changes: SimpleChanges): void {

  }

  openOrderRow: any = (index: number, exEventId: number, exMarketId: number, selectionId: number, betType: number, price: number, runnerName: string) => {

    this.selectedIndex = index;
    this.selectionId = selectionId;
    this.betType = betType;
    this.bidPriceInput = price;
    this.runnerName = runnerName;

    this.matchOddsForm.patchValue({
      betType: this.selectedIndex,
      selectionId: this.selectionId,
      selection: this.runnerName,
      type: this.betType,
      sportId: this.sportId
    })

  }

  getBackLayAmount(){
    this.service.get(`BetApi/GetBackAndLayBetAmount?UserId=${this.userId }&marketId=${this.marketId}&SportId=${this.sportId}`)
      .subscribe((response: ResponseModel) => {
        if (response.isSuccess == true && response.data != null) {
          this.BidData = JSON.parse(response.data);
          this.BidDataNew = JSON.parse(JSON.stringify(this.BidData));
        }
      });
  }

  ngOnInit() {

    this.stackData?.subscribe((stack: StackLimit[]) => {
      if (stack.length > 0) {
        this.stackLimitList = stack;
      }
    });

    this.activatedRoute.params.subscribe(paramsId => {
      this.sportId = this.sportId ? this.sportId : paramsId.sportId;
      this.marketId = this.sportId ? this.marketId : paramsId.marketId;

      this.getBackLayAmount();
    });


  }

  /******** Match Odds Bid Price *******/
  oddsRequestFocusVal : boolean  = false;
  oddsRequestFocus(value:boolean){
    this.oddsRequestFocusVal = value;
  }
  stackAmountFocusVal : boolean  = false;
  stackAmountFocus(value:boolean){
    this.stackAmountFocusVal = value;
  }

  OpenBidPrice(stackValue?: any, oddsRequest?: any, betType: number = 0, selectionId?: string) {
    betType = this.betType || 0;
    if (typeof stackValue == 'object') {
      stackValue = stackValue.target.value;
    }
    if (typeof oddsRequest == 'object') {
      oddsRequest = oddsRequest.target.value;
    }
    this.oddBook = true;
    this.BidDataNew = this.BidDataNew.reduce((a, v, i) => {
      const [key, value] = Object.entries(v);
      this.BidDataNew[i][key[0]] = 0;
      if (key[0] == selectionId) {
        v[key[0]] = betType == 0 ? parseInt(this.BidData[i][key[0]]) + ((oddsRequest * parseInt(stackValue)) - parseInt(stackValue)) : parseInt(this.BidData[i][key[0]]) - ((oddsRequest * parseInt(stackValue)) - parseInt(stackValue));
      } else {
        v[key[0]] = betType == 0 ? parseInt(this.BidData[i][key[0]]) - parseInt(stackValue) : parseInt(this.BidData[i][key[0]]) + parseInt(stackValue);
      }
      v[key[0]] = Math.trunc(100*v[key[0]])/100;
      a.push(v);
      return a;
    }, []);

  }

  closeOrderRow() {
    this.oddBook = false;
    this.selectionId = 0;
    this.bidOddPrice = 0;
  }


  setBidPrice(stackValue?: any, oddsRequest?: any, betType?: any, selectionId?: string, type?: string) {
    if (type == 'plus') {
      if (oddsRequest > 20) {
        oddsRequest += 1;
      } else if (oddsRequest > 10) {
        oddsRequest = oddsRequest + 0.5;
      } else {
        oddsRequest = oddsRequest + 0.2;
      }
    }
    if (type == 'minus') {
      if (parseInt(oddsRequest) > 1) {
        if (oddsRequest > 20) {
          oddsRequest -= 1;
        } else if (oddsRequest > 10) {
          oddsRequest = oddsRequest - 0.5;
        } else {
          oddsRequest = oddsRequest - 0.2;
        }
      }
    }

    this.bidPriceInput = parseFloat(oddsRequest.toFixed(2));

    this.bidOddPrice = stackValue;
    this.OpenBidPrice(stackValue, oddsRequest, betType, selectionId);
  }

  saveMatchOdds() {

    if(!this.isUserLogin){
      return;
    }
    let placeBetData = {
      "id": 0,
      "sportId": parseInt(this.matchOddsForm.value.sportId),
      "EventId": parseInt(this.matchOddsForm.value.EventId),
      "event": this.matchOddsForm.value.event,
      "MarketId": this.matchOddsForm.value.MarketId,
      "market": this.matchOddsForm.value.market,
      "selection": this.matchOddsForm.value.selection,
      "OddsType": this.matchOddsForm.value.OddsType,
      "type": this.matchOddsForm.value.type ? 'lay' : 'back',
      "oddsRequest": this.matchOddsForm.value.oddsRequest,
      "amountStake": this.matchOddsForm.value.amountStake,
      "betType": this.matchOddsForm.value.betType,
      "isSettlement": this.matchOddsForm.value.isSettlement,
      "userId": this.userId,
      "SelectionId": this.matchOddsForm.value.selectionId
    };

    this.service.post('BetApi/SaveBets', placeBetData)
      .subscribe((response: ResponseModel) => {
        if (response.isSuccess == true) {
          this.notification.showSuccess(response.message);
          this.closeOrderRow();
          this.getBackLayAmount();
          this.betService._getBetData.next(this.betService.getMarketList());
        } else {
          this.notification.showError(response.message);
        }
        this.loaderService.isLoading.next(false);
      });
  }

}
