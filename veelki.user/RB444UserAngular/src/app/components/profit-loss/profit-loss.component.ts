import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ResponseModel } from 'src/app/models/responseModel';
import { HttpService } from 'src/app/services/http.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-profit-loss',
  templateUrl: './profit-loss.component.html',
  styleUrls: ['./profit-loss.component.css']
})
export class ProfitLossComponent implements OnInit {

  todayDate = new Date();
  maxDate = { year: this.todayDate.getFullYear(), month: this.todayDate.getMonth()+1, day: this.todayDate.getDate() };

  profitLossData? : any[];

  constructor(private sessionService: SessionService, private service: HttpService) { }

  getProfitLoss(): void {
    let userId = this.sessionService.getLoggedInUser().id;
    this.service.get(`Common/GetProfitAndLoss?UserId=${userId}`)
      .subscribe((response: ResponseModel) => {
        if (response.isSuccess == true && response.data !== null) {
          this.profitLossData = <Array<ProfitAndLoss>>([...response.data]);
        }else{
          this.profitLossData = <Array<ProfitAndLoss>>([]);
        }
      });
  }

  ngOnInit(): void {
    this.getProfitLoss();
  }

}

interface ProfitAndLoss {
  id: number;
  betId: string;
  sportName: string;
  event: string;
  market: string;
  selection: string;
  oddsType: string;
  type: string;
  oddsRequest: number;
  amountStake: number;
  resultType: string;
  resultAmount: number;
}