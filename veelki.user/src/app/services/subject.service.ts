import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SubjectService {

  constructor() { }

  sideBarVisible : BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true);
  asideVisible : BehaviorSubject<boolean> = new BehaviorSubject<boolean>(true);
  footerVisible : BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  setSideBarVisible(data:boolean){
    this.sideBarVisible.next(data);
  }

  setAsideVisible(data:boolean){
    this.asideVisible.next(data);
  }

  setFooterVisible(data:boolean){
    this.footerVisible.next(data);
  }

}
