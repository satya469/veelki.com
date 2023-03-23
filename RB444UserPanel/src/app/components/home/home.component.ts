import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/models/responseModel';
import { HttpService } from 'src/app/services/http.service';
import { GET_SPORT } from 'src/app/store/actions/getSport.action';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  active = 4;
  sportData : any[] = [];
  sportDataList? : Observable<any[]>;


  cricketData : boolean = false;
  soccerData : boolean = false;
  tennisData : boolean = false;

  sportNews : SportNews[] = [];

  constructor(private service : HttpService, private store : Store<{SportData:any}>) {
    this.sportDataList = this.store.select(data => data.SportData);
  }  

  navChange($event:any){
  }
  activeIdChange($event:any){
    this.sportDataList?.subscribe(
      (res) => {
          this.sportData = res.filter(x => x.eid==$event);
      }
    )
  }
  getAllNews(): void {
    this.service.get(`Common/GetAllNews`)
    .subscribe((response:ResponseModel) => {
      if(response.isSuccess == true && response.data != null){
        this.sportNews= response.data;
      }
    });
  }
  ngOnInit(): void {
    this.sportDataList?.subscribe(
      (res) => {
        if(res != null){
          this.cricketData = res.filter(x => x.eid==4).length > 0 ? true : false;
          this.soccerData = res.filter(x => x.eid==1).length > 0 ? true : false;
          this.tennisData = res.filter(x => x.eid==2).length > 0 ? true : false;
          this.sportData = res.filter(x => x.eid==4);
        }else{
          this.store.dispatch(GET_SPORT())
        }
      }
    )
    this.getAllNews();
  }
}

interface SportNews{
    newsText:string;
    status:boolean;
}