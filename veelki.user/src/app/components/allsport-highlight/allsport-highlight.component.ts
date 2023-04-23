import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResponseModel } from 'src/app/models/responseModel';
import { HttpService } from 'src/app/services/http.service';
import { PerfectScrollbarConfigInterface,
  PerfectScrollbarComponent, PerfectScrollbarDirective } from 'ngx-perfect-scrollbar';
import { SportList } from 'src/app/models/sportList';

@Component({
  selector: 'app-allsport-highlight',
  templateUrl: './allsport-highlight.component.html',
  styleUrls: ['./allsport-highlight.component.css']
})
export class AllsportHighlightComponent implements OnInit {

  constructor(private service : HttpService, private activatedRoute: ActivatedRoute) { }

  eventList : any;
  activatedId:number=0;
  public config: PerfectScrollbarConfigInterface = {};

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
  }

  ngOnChanges(): void {  
  }

}
