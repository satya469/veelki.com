import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { ResponseModel } from 'src/app/models/responseModel';
import { HttpService } from 'src/app/services/http.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-account-statement',
  templateUrl: './account-statement.component.html',
  styleUrls: ['./account-statement.component.css']
})
export class AccountStatementComponent implements OnInit {

  accountStatementList? : AccountStatement[];

  constructor(public sessionService : SessionService, private service : HttpService) { }

  getAccountStatement() : void {
    let userId = this.sessionService.getLoggedInUser().id;
    this.service.get(`Common/GetAccountStatement?UserId=${userId}`)
      .subscribe((response:ResponseModel) => {
        if(response.isSuccess == true && response.data !== null){
          this.accountStatementList = <Array<AccountStatement>>([...response.data]);
        }
      });
  }

  ngOnInit(): void {
    this.getAccountStatement();
  }
}
interface AccountStatement{
    id : number;
    createdDate : string;
    deposit : number;
    withdraw : number;
    balance : number;
    remark : string;
    fromUser : string;
    toUser : string;
}
