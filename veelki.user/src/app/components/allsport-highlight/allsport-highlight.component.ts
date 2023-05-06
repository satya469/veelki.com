import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResponseModel } from 'src/app/models/responseModel';
import { HttpService } from 'src/app/services/http.service';
import { PerfectScrollbarConfigInterface,
  PerfectScrollbarComponent, PerfectScrollbarDirective } from 'ngx-perfect-scrollbar';
import { SportList } from 'src/app/models/sportList';
import { Store } from '@ngrx/store';
import { GET_INPLAY } from 'src/app/store/actions/inplay.action';

@Component({
  selector: 'app-allsport-highlight',
  templateUrl: './allsport-highlight.component.html',
  styleUrls: ['./allsport-highlight.component.css']
})
export class AllsportHighlightComponent implements OnInit {

  constructor(private service : HttpService, private activatedRoute: ActivatedRoute, private store: Store<{ InplayData: any }>) { }

  eventList : any;
  activatedId:number=0;
  public config: PerfectScrollbarConfigInterface = {};
  sportsEventInPlay : any[] = [];

  getAllEventsList:any = (sportId : number) =>{
    this.eventList = null;
    this.service.get(`exchange/GetSportEvents?SportId=${sportId}`)
    .subscribe((response:ResponseModel) => {
      if(response.isSuccess == true && response.data != null){
        this.eventList = response.data;
      }
    });
  }

  sidebarList : SportList[] = [];

  getSportList(): void {
    this.service.get('exchange/GetSports')
    .subscribe((res:ResponseModel) => {
      if(res.data != null && res.isSuccess == true){
        this.sidebarList.push(...res.data);
      }
    });
  }

  ngOnInit(): void {
    this.getSportList();
    this.activatedRoute.params.subscribe((paramsId:any) => {
      this.activatedId = paramsId?.id;
      this.getAllEventsList(paramsId?.id);
    });
    this.store.select(data => data.InplayData).subscribe(res => {
      if (res != null) {
        this.sportsEventInPlay = res.sportsEventModelInPlay;
      } else {
        this.store.dispatch(GET_INPLAY());
      }
    })
  }

  getInplayCount(sportId:any){
    if(this.sportsEventInPlay.length > 0){
      return this.sportsEventInPlay.filter((x:any)=> x.eid == sportId).length
    }    
    return 0;
  }

  ngOnChanges(): void {  
  }

}
