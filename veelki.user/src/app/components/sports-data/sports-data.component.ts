import { Component, Input, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { ResponseModel } from 'src/app/models/responseModel';
import { HttpService } from 'src/app/services/http.service';
import { GET_SPORT } from 'src/app/store/actions/getSport.action';

@Component({
  selector: 'app-sports-data',
  templateUrl: './sports-data.component.html',
  styleUrls: ['./sports-data.component.css']
})
export class SportsDataComponent implements OnInit {

  constructor(private service : HttpService, private store : Store<{SportData:any}>) { }

  @Input() tableRowList : any[] = [];
  @Input() multiMarketPin : boolean = false;

  setPinData:any = (marketId:number, isPinned:boolean) =>{
   
    this.service.get(`exchange/UpdateEventForPinned?marketId=${marketId}&isPinned=${isPinned}`)
    .subscribe((response:ResponseModel) => {
      if(response.data != null && response.isSuccess == true){
        this.store.dispatch(GET_SPORT());
        this.tableRowList = this.tableRowList.map((x) => {
          x.marketId == marketId ? x.isPinnedMatch = isPinned : x.isPinnedMatch;
          return x;
        })
      }
    });
    
  }

  ngOnInit(): void {
  }

}