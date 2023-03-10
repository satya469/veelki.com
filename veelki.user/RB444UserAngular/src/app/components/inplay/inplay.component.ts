import { Component, OnInit } from '@angular/core';
import { NgbNavConfig } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpService } from 'src/app/services/http.service';
import { GET_INPLAY } from 'src/app/store/actions/inplay.action';
import { ResponseModel } from '../../models/responseModel';

@Component({
  selector: 'app-inplay',
  templateUrl: './inplay.component.html',
  styleUrls: ['./inplay.component.css'],
  providers: [NgbNavConfig]
})
export class InplayComponent implements OnInit {

  constructor(private service: HttpService, config: NgbNavConfig, private store: Store<{ InplayData: any }>) {
    config.destroyOnHide = false;
    config.roles = false;
  }

  inplayData: any;
  todayData: any;
  tomorrowData: any;


  // getDaysWiseEvents = (): void => {
  //   this.service.get('exchange/GetInPlaySportEvents').subscribe((response: ResponseModel) => {
  //     if (response.isSuccess == true && response.data != null) {
  //       this.inplayData = this.getInPlayData(response.data.sportsEventModelInPlay);
  //       this.todayData = this.getInPlayData(response.data.sportsEventModelToday);
  //       this.tomorrowData = this.getInPlayData(response.data.sportsEventModelTommorow);
  //     }
  //   });
  // }

  getInPlayData = (data?: any[]) => {
    if (data && data.length > 0) {
      return Array(
        {
          "sportName": "Soccer",
          "oddsData": data.filter((x) => { return x.eid == "1" })
        },
        {
          "sportName": "Tennis",
          "oddsData": data.filter((x) => { return x.eid == "2" })
        },
        {
          "sportName": "Cricket",
          "oddsData": data.filter((x) => { return x.eid == "4" })
        }
      )
    }
    return [];
  }

  ngOnInit(): void {
    this.store.select(data => data.InplayData).subscribe(res => {
      if (res != null) {
        this.inplayData = this.getInPlayData(res.sportsEventModelInPlay);
        this.todayData = this.getInPlayData(res.sportsEventModelToday);
        this.tomorrowData = this.getInPlayData(res.sportsEventModelTommorow);
      } else {
        this.store.dispatch(GET_INPLAY());
      }
    })
  }

}
