import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ResponseModel } from 'src/app/models/responseModel';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-allsport-highlight',
  templateUrl: './allsport-highlight.component.html',
  styleUrls: ['./allsport-highlight.component.css']
})
export class AllsportHighlightComponent implements OnInit {

  constructor(private service : HttpService, private activatedRoute: ActivatedRoute) { }

  eventList : any;

  getAllEventsList:any = (sportId : number) =>{
    this.eventList = null;
    this.service.get(`exchange/GetSportEvents?SportId=${sportId}`)
    .subscribe((response:ResponseModel) => {
      if(response.isSuccess == true && response.data != null){
        this.eventList = response.data;
      }
    });
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((paramsId:any) => {
      this.getAllEventsList(paramsId?.id);
    });
  }

  ngOnChanges(): void {  
  }

}
