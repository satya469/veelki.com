import { Injectable, OnInit } from '@angular/core';
import { SessionService } from './session.service';
import { UserData } from '../models/userData';
import { BehaviorSubject, Subject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private session : SessionService, private route : Router) { }

  _isLoginUser = new BehaviorSubject<boolean>(this.isLoggedIn());


  public login(userData : UserData, redirect:boolean = true){
    this.session.setSession('USER_DATA', userData);
    this._isLoginUser.next(true);
    if(redirect){
      this.route.navigateByUrl("/home");
    }
  }

  public isLoggedIn(){
    let userData = this.session.getSession('USER_DATA');
    if(userData !== null && userData.userName !== null && userData.id > 0 && userData.roleId > 0){
      return true;
    }
    this.route.navigateByUrl("/login");
    return false;
  }
  public logout(){
    this.session.removeSession('USER_DATA');
    this._isLoginUser.next(false);
    this.route.navigateByUrl("/home");
  }
  

}
