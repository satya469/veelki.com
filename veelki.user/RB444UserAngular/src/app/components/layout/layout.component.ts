import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { SportList } from 'src/app/models/sportList';
import { HttpService } from 'src/app/services/http.service';
import { NgxSpinnerService } from "ngx-spinner";
import { LoaderService } from '../../services/loader.service';
import { ResponseModel } from 'src/app/models/responseModel';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  constructor(private service : HttpService, private spinner: NgxSpinnerService, private loaderService : LoaderService) { }

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
    this.loaderService.isLoading.subscribe((res) => {
      if(res==true){
        this.spinner.show();
      }else{
        this.spinner.hide();
      }
    })
  }

}
