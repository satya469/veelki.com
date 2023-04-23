import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  loginUserName: string = "";
  gmtTimeZone : any = new Date(0).toString();

  constructor(private authService: AuthService, private sessionService: SessionService) {
    this.gmtTimeZone = this.gmtTimeZone.substring(this.gmtTimeZone.indexOf("GMT"),this.gmtTimeZone.indexOf("("));
  }

  ngOnInit(): void {
    this.authService._isLoginUser.subscribe(
      (res) => {
        if (res && this.sessionService.getLoggedInUser() !== null && this.sessionService.getLoggedInUser().id > 0) {
          this.loginUserName = this.sessionService.getLoggedInUser().fullName;
        }
      }
    )
  }

  logout(){
    this.authService.logout();
  }

}
