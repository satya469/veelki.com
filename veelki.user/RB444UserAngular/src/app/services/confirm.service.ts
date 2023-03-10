import { Injectable } from '@angular/core';
import { Subject, observable, Observable, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfirmService {

  constructor() { }

  private subject = new BehaviorSubject<boolean>(false);

  openConfirmBox(value:boolean){
    this.subject.next(value);
  }

  getConfirmData():Observable<boolean>{
    return this.subject.asObservable();
  }


}
