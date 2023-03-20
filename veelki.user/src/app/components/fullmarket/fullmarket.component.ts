import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ResponseModel } from 'src/app/models/responseModel';
import { AuthService } from 'src/app/services/auth.service';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-fullmarket',
  templateUrl: './fullmarket.component.html',
  styleUrls: ['./fullmarket.component.css']
})
export class FullmarketComponent implements OnInit {

  sportId : number = 0;
  marketId: number = 0;
  gameId : number = 0;

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
    this.activatedRoute.params.subscribe((paramsId:any) => {
      this.sportId = paramsId?.eId;
      this.marketId = paramsId?.marketId;
      this.gameId = paramsId?.gameId;
      this.getMarketsOfEventList(this.marketId, this.gameId, this.sportId);
    });
  }  

  getMarketsOfEventList: any = (marketId:number, gameId: number, sportId: number) => {
    this.service.get(`exchange/GetMatchOdds?marketId=${marketId}&eventId=${gameId}&SportId=${sportId}`)
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
              this.getMarketsOfEventList(this.marketId, this.gameId, this.sportId);
            }, 2000);
          }
        }
      });
  }

  ngOnInit(): void {
    this.getMarketsOfEventList(this.marketId, this.gameId, this.sportId);    
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
