import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { StackLimit } from './models/stackLimit';
import { GET_INPLAY } from './store/actions/inplay.action';
import { GET_SPORT } from './store/actions/getSport.action';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'veelki.user';
  constructor(private store : Store<{StackData : StackLimit[], InplayData: any, SportData : any}>){
  }

  ngOnInit(): void {
    this.store.dispatch({ type : "GET_STACK" });
    this.store.dispatch(GET_INPLAY());
    this.store.dispatch(GET_SPORT());
  }
}
