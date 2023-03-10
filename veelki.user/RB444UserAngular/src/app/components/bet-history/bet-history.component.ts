import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ResponseModel } from 'src/app/models/responseModel';
import { HttpService } from 'src/app/services/http.service';
import { NotificationService } from 'src/app/services/notification.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-bet-history',
  templateUrl: './bet-history.component.html',
  styleUrls: ['./bet-history.component.css']
})
export class BetHistoryComponent implements OnInit {

  todayDate = new Date();
  maxDate = {year: this.todayDate.getFullYear(), month: this.todayDate.getMonth()+1, day: this.todayDate.getDate()};

  betDataList? : any[] = [];
  betHistoryForm : FormGroup;

  p: any = 1;
  count: any = 5;

  constructor(private service : HttpService, private fb : FormBuilder, private notification : NotificationService, public sessionService : SessionService) { 
    this.betHistoryForm = this.fb.group({
      startDate : [this.maxDate],
      starttime : [''],
      endDate : [this.maxDate],
      endTime : [''],
      sportId : ['', [Validators.required]],
      isSettlement : ['', [Validators.required]]
    });
  }

  resetData(){
    this.p = 1;
  }


  getBetHistory(isToday:boolean = false) {
    let betData;
    if(this.betHistoryForm.controls.sportId.errors !== null){
      this.notification.showError("Please select sport.");
      return;
    }
    if(this.betHistoryForm.controls.isSettlement.errors !== null){
      this.notification.showError("Please select settlement type.");
      return;
    }
    let userId = this.sessionService.getLoggedInUser().id;
    if(isToday){
      betData = {
        "userId": userId,
        "sportId": parseInt(this.betHistoryForm.value.sportId),
        "isSettlement": parseInt(this.betHistoryForm.value.isSettlement),
        "startDate": null,
        "endDate": null,
        "startTime": null,
        "endTime": null,
        "todayHistory": true,
        "columnName": null,
        "orderByColumn": 0,
        "PageNumber":1,
        "PageSize":10
      };
    }else{
      betData = {
        "userId": userId,
        "sportId": parseInt(this.betHistoryForm.value.sportId),
        "isSettlement": parseInt(this.betHistoryForm.value.isSettlement),
        "startDate": `${this.betHistoryForm.value.startDate.year}-${this.betHistoryForm.value.startDate.month}-${this.betHistoryForm.value.startDate.day}`,
        "endDate": `${this.betHistoryForm.value.endDate.year}-${this.betHistoryForm.value.endDate.month}-${this.betHistoryForm.value.endDate.day}`,
        "startTime": this.betHistoryForm.value.starttime,
        "endTime": this.betHistoryForm.value.endTime,
        "todayHistory": false,
        "columnName": null,
        "orderByColumn": 0,
        "PageNumber":1,
        "PageSize":10
      };
    }

    this.service.post('BetApi/GetBetHistory', betData)
      .subscribe((response:ResponseModel) => {
        if(response.isSuccess == true && response.data !== null){
          this.betDataList = response.data.betList;
        }else{
          this.betDataList = [];
        }
      });
  }

  getTodayHistory(): void{
    this.getBetHistory(true);
  }

  ngOnInit(): void {
  }

}
