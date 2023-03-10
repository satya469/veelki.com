import { Component, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { map, mergeMap, tap } from 'rxjs/operators';
import { ResponseModel } from 'src/app/models/responseModel';
import { AuthService } from 'src/app/services/auth.service';
import { BetService } from 'src/app/services/getBet.service';
import { HttpService } from 'src/app/services/http.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-aside',
  templateUrl: './aside.component.html',
  styleUrls: ['./aside.component.css']
})
export class AsideComponent implements OnInit, OnChanges {

  isLoginUser: boolean = false;
  marketList: MarketList[] = [];
  showBetInfo: boolean[] = [];
  constructor(
    private authService: AuthService, 
    private sessionService: SessionService, 
    private service: HttpService,
    private betService: BetService
  ) { }

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

  ngOnChanges(changes: SimpleChanges): void {
    console.log("changes")
  }

  getMarketData(){
    return this.betService._getBetData.pipe(
      mergeMap(data => data)
    );
  }


  ngOnInit(): void {
    this.authService._isLoginUser.subscribe(
      (res) => {
        this.isLoginUser = res;
        if(res==true){
          this.getMarketData().subscribe(response => {
            if (response.isSuccess == true && response.data !== null) {
              this.marketList = response.data;
            }
          })
        }
      }
    )
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
