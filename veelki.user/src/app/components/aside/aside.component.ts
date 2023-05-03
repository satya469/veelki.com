import { SubjectService } from 'src/app/services/subject.service';
import { betData } from './../fullmarket/match-odd/match-odd.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Store } from '@ngrx/store';
import { StackLimit } from 'src/app/models/stackLimit';
import { Observable, mergeMap } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { ResponseModel } from 'src/app/models/responseModel';
import { HttpService } from 'src/app/services/http.service';
import { LoaderService } from 'src/app/services/loader.service';
import { SessionService } from 'src/app/services/session.service';
import { NotificationService } from 'src/app/services/notification.service';
import { BetService } from 'src/app/services/getBet.service';
import { NgbAccordion, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-aside',
  templateUrl: './aside.component.html',
  styleUrls: ['./aside.component.css']
})
export class AsideComponent implements OnInit {


  stackData?: Observable<StackLimit[]>;
  stackLimitList: StackLimit[] = [];
  liabilityAmount : number = 0;
  oddsRequest : number = 0;
  amountStake : number = 0;

  betData !: betData;
  isUserLogin: boolean = false;
  userId: number = this.sessionService.getLoggedInUser() ? this.sessionService.getLoggedInUser().id : 0;
  visibleBetSlip : boolean = true;
  visibleOpenBet : boolean = true;

  marketList: MarketList[] = [];
  @ViewChild('acc') getNgbAccordion?: NgbAccordion;

  constructor(private modalService: NgbModal, private subjectService : SubjectService, private betService : BetService, public notification: NotificationService, private sessionService: SessionService, private service: HttpService, private loaderService : LoaderService, private authService: AuthService, private store: Store<{ StackData: StackLimit[] }> ) { 
    this.stackData = this.store.select(data => data.StackData);
    this.authService._isLoginUser.subscribe((res) => this.isUserLogin = res);
  }

  openPlaceBet(content:any) {
		this.modalService.open(content);
	}


  toggleAccordian($event:boolean){
    if($event === true){
      this.getNgbAccordion?.toggle("toggle-1");
    }
  }

  addStackPrice(num:any){
    this.betData.amountStake = Number(this.betData.amountStake + `${num}`);
    this.amountStake = Number(this.amountStake + `${num}`);
    this.subjectService.setBetData(this.betData);
  }

  removeStackPrice(){
    this.betData.amountStake = Number(this.betData.amountStake?.toString().substring(0, this.betData.amountStake?.toString().length-1));
    this.amountStake = Number(this.amountStake?.toString().substring(0, this.amountStake?.toString().length-1));
    this.subjectService.setBetData(this.betData);
  }

  incDecStack(type:string){
    if(type=='plus'){
      this.betData.amountStake = this.betData.amountStake ? this.betData.amountStake+1 : this.betData.amountStake;
      this.amountStake += 1;
      this.subjectService.setBetData(this.betData);      
    }
    if(type=='minus'){
      this.betData.amountStake = this.betData.amountStake ? this.betData.amountStake-1 : this.betData.amountStake;
      this.amountStake -= 1;
      this.subjectService.setBetData(this.betData);      
    }
  }

  ngOnInit(): void {
    this.subjectService.betDataSubject.subscribe((data:betData) =>{
      this.betData = {...data};
      this.oddsRequest = Number(this.betData.oddsRequest);
      this.amountStake = Number(this.betData.amountStake);
      this.liabilityAmount = 0;
    })
    this.stackData?.subscribe((stack: StackLimit[]) => {
      if (stack.length > 0) {
        this.stackLimitList = stack;
      }
    });
    if(this.isUserLogin){
      // this.getMarketData().subscribe(response => {
      //   if (response.isSuccess == true && response.data !== null) {
      //     this.marketList = response.data;
      //   }
      // })
    }
  }

  deleteBet(){
    this.subjectService.setBetData({})    
  }

  setStackPrice(amount:number){
    this.betData.amountStake = this.amountStake = amount;
    this.subjectService.setBetData(this.betData);
  }

  setOddsPrice(amount:number){
    if(amount > 1.01){
      this.betData.oddsRequest = this.oddsRequest = amount;
    }else{
      this.betData.oddsRequest = this.oddsRequest = 1.01;      
    }
    this.subjectService.setBetData(this.betData);
  }

  saveMatchOdds() {
    if (!this.isUserLogin) {
      this.subjectService.showLoginPopup.next(true);
      return;
    }
    this.userId = this.sessionService.getLoggedInUser() ? this.sessionService.getLoggedInUser().id : 0;
    let placeBetData = {
      "id": 0,
      "sportId": Number(this.betData?.sportId),
      "EventId": Number(this.betData?.EventId),
      "event": this.betData?.event,
      "MarketId": this.betData?.MarketId,
      "market": this.betData?.market,
      "selection": this.betData?.selection,
      "OddsType": this.betData?.OddsType,
      "type": this.betData?.type,
      "oddsRequest": this.betData?.oddsRequest,
      "amountStake": Number(this.betData?.amountStake),
      "betType": this.betData?.betType,
      "isSettlement": this.betData?.isSettlement,
      "userId": this.userId,
      "SelectionId": this.betData?.selectionId
    };
    

    this.service.post('BetApi/SaveBets', placeBetData)
      .subscribe((response: ResponseModel) => {
        if (response.isSuccess == true) {
          this.notification.showSuccess(response.message);
          this.deleteBet();
          window.location.reload();
          //this.betService._getBetData.next(this.betService.getMarketList());
        } else {
          this.notification.showError(response.message);
        }
        this.modalService.dismissAll();
        this.loaderService.isLoading.next(false);
      });
  }

  getMarketData(){
    return this.betService._getBetData.pipe(
      mergeMap(data => data)
    );
  }

  selectBetId: number = 0;
  betListData: BetData[] = [];
  getBetList(): void {
    if (this.selectBetId == 0) {
      this.betListData = [];
      return;
    }
    let userId = this.sessionService.getLoggedInUser().id;
    this.service.get(`Common/GetOpenBetList?UserId=${userId}&EventId=${this.selectBetId}`)
      .subscribe((response: ResponseModel) => {
        if (response.isSuccess == true && response.data !== null) {
          this.betListData.length = 0;
          this.betListData.push(...response.data);
        }
      });
  }

}

interface MarketList {
  eventId: number;
  event: string;
}

interface BetData {
  id: number,
  betId: string,
  sportId: number,
  eventId: number,
  event: string,
  marketId: number,
  market: string,
  selectionId: number,
  selection: string,
  oddsType: number,
  type: string,
  oddsRequest: number,
  amountStake: number,
  betType: number,
  placeTime: Date,
  matchedTime: Date,
  settleTime: Date,
  isSettlement: number,
  status: boolean,
  userId: number,
  updatedBy: any,
  updatedDate: any,
  resultType: any,
  resultAmount: number
}
