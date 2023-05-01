import { Component, OnInit } from '@angular/core';
import { ResponseModel } from 'src/app/models/responseModel';
import { BetService } from 'src/app/services/getBet.service';
import { HttpService } from 'src/app/services/http.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-balance',
  templateUrl: './balance.component.html',
  styleUrls: ['./balance.component.css']
})
export class BalanceComponent implements OnInit {

  constructor(private service: HttpService, private sessionService : SessionService, private betService : BetService) { }

  openingBalance : number = 0;
  exposureBalance : number = 0;

  getOpeningBalance() : void {
    let userId = this.sessionService.getLoggedInUser().id;
    this.service.get(`Account/GetOpeningBalance?UserId=${userId}`)
      .subscribe((response:ResponseModel) => {
        if(response.isSuccess == true && response.data != null){
          this.openingBalance = response.data;
        }
      });
  }
  getExposureBalance() : void {
    let userId = this.sessionService.getLoggedInUser().id;
    this.service.get(`Account/GetBetExposureStack?UserId=${userId}`)
      .subscribe((response:ResponseModel) => {
        if(response.isSuccess == true && response.data != null){
          this.exposureBalance = response.data;
        }
      });
  }

  refreshBalance(){
    this.getOpeningBalance();
    this.getExposureBalance();
  }

  ngOnInit(): void {
    this.getOpeningBalance();
    this.getExposureBalance();
    this.refreshBalance();

    this.betService._getBetData.subscribe(res => {
      this.refreshBalance();
    });
  }

}

