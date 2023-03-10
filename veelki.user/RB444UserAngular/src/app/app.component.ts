import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { ResponseModel } from './models/responseModel';
import { StackLimit } from './models/stackLimit';
import { GET_SPORT } from './store/actions/getSport.action';
import { GET_INPLAY } from './store/actions/inplay.action';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'RB444-Angular';

  constructor(private store : Store<{StackData : StackLimit[], InplayData: any, SportData : any}>){
  }

  ngOnInit(): void {
    this.store.dispatch({ type : "GET_STACK" });
    this.store.dispatch(GET_INPLAY());
    this.store.dispatch(GET_SPORT());
  }

}
