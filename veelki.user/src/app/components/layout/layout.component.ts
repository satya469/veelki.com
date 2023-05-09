import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, NavigationStart, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { filter } from 'rxjs';
import { ResponseModel } from 'src/app/models/responseModel';
import { SportList } from 'src/app/models/sportList';
import { HttpService } from 'src/app/services/http.service';
import { LoaderService } from 'src/app/services/loader.service';
import { SubjectService } from 'src/app/services/subject.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {

  isSideBarVisible : boolean = false;
  isAsideVisible : boolean = false;
  isFooterVisible : boolean = false;
  isSliderVisible : boolean = true;

  constructor(private service : HttpService, private spinner: NgxSpinnerService, private loaderService : LoaderService, private subjectService : SubjectService, private router : Router) {
    this.router.events.pipe(filter(event => event instanceof NavigationEnd)).subscribe((data:any) => {
      if(data?.url == '/' || data?.url == '/home'){
        this.subjectService.setSideBarVisible(false);
        this.subjectService.setAsideVisible(false);
        this.subjectService.setFooterVisible(true);
        this.subjectService.setBannerVisible(true);
      }else if(data?.url == '/account/activity-log' || data?.url == '/account/account-statement' || data?.url == '/account/profit-loss' || data?.url == '/account/my-profile' || data?.url == '/account/summary' || data?.url == '/account/current-bets'){
        this.subjectService.setSideBarVisible(false);
        this.subjectService.setAsideVisible(false);  
        this.subjectService.setFooterVisible(false);    
        this.subjectService.setBannerVisible(true);  
      }else if(data?.url && data?.url.indexOf("fullmarket") > -1){
        this.subjectService.setSideBarVisible(false);
        this.subjectService.setAsideVisible(true);  
        this.subjectService.setFooterVisible(false);
        this.subjectService.setBannerVisible(false); 
      }else{
        this.subjectService.setSideBarVisible(true);
        this.subjectService.setAsideVisible(true);
        this.subjectService.setFooterVisible(false);
        this.subjectService.setBannerVisible(true);
      }
    })
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
    this.subjectService.sideBarVisible.subscribe((data:boolean) => this.isSideBarVisible = data);
    this.subjectService.asideVisible.subscribe((data:boolean) => this.isAsideVisible = data);
    this.subjectService.footerVisible.subscribe((data:boolean) => this.isFooterVisible = data);
    this.subjectService.bannerVisible.subscribe((data:boolean) => this.isSliderVisible = data);
    this.loaderService.isLoading.subscribe((res) => {
      if(res==true){
        this.spinner.show();
      }else{
        this.spinner.hide();
      }
    })
  }

}
