import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ResponseModel } from 'src/app/models/responseModel';
import { HttpService } from 'src/app/services/http.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-activity-log',
  templateUrl: './activity-log.component.html',
  styleUrls: ['./activity-log.component.css']
})
export class ActivityLogComponent implements OnInit {

  constructor(private service: HttpService, private sessionService : SessionService) { }

  activityLog? : UserLog[];

  getActiveLog() : void {
    let userId = this.sessionService.getLoggedInUser().id;
    this.service.get(`Common/GetUserActivityLog?UserId=${userId}`)
      .subscribe((response:ResponseModel) => {
        if(response.isSuccess == true && response.data !== null){
          this.activityLog = <Array<UserLog>>([...response.data])
        }
      });
  }

  ngOnInit(): void {
    this.getActiveLog();
  }
}

interface UserLog{  
    id : number; 
    loginDate : string; 
    ipAddress : string; 
    isp : string; 
    address : string;    
    userId : number; 
    userName : string;
}
