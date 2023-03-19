import { Component, OnInit } from '@angular/core';
import { NgbNavConfig } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import { HttpService } from 'src/app/services/http.service';
import { GET_INPLAY } from 'src/app/store/actions/inplay.action';

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

  inplayData: any[] =[];
  todayData: any[] =[];
  tomorrowData: any[] =[];

  active = 1;

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
