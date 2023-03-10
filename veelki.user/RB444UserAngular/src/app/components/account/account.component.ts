import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserData } from 'src/app/models/userData';
import { AuthService } from 'src/app/services/auth.service';
import { HttpService } from 'src/app/services/http.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent implements OnInit {

  constructor(private service: HttpService, private router: Router, private authService: AuthService, private sessionService: SessionService) { }


  userData?:UserData;

  ngOnInit(): void {
    this.userData = this.sessionService.getLoggedInUser();
  }

  userLogout() {
    this.authService.logout();
    this.router.navigateByUrl('/home');
    this.authService._isLoginUser.next(false);
  }

}
