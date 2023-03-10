import { Component, OnInit } from '@angular/core';
import { ResponseModel } from 'src/app/models/responseModel';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-multimarket',
  templateUrl: './multimarket.component.html',
  styleUrls: ['./multimarket.component.css']
})
export class MultimarketComponent implements OnInit {

  constructor(private service : HttpService) { }

  multiMarketData : any;

  ngOnInit(): void {
    this.service.get('exchange/GetSportEvents?SportId=0').subscribe(
      (res:ResponseModel) => {
        if(res.data != null && res.isSuccess == true){
          this.multiMarketData = res.data.filter((x:any) => x.isPinnedMatch == true)
        }else{
          this.multiMarketData = null
        }
      }
    )
  }

}
