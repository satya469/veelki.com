import { Component, OnInit } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { ResponseModel } from 'src/app/models/responseModel';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-slider',
  templateUrl: './slider.component.html',
  styleUrls: ['./slider.component.css']
})
export class SliderComponent implements OnInit {

  constructor(private service : HttpService) { }

  sliderList : sliderModel[] = [];

  HomeSliderOptions: OwlOptions = {
    loop: true,
    mouseDrag: true,
    touchDrag: true,
    pullDrag: false,
    dots: true,
    navSpeed: 700,
    navText: ['', ''],
    items: 1,
    nav: false,
    autoHeight:true,
    autoplay:true
  }


  getAllSlider:any = () =>{
    this.service.get('Common/GetAllSliders').subscribe((response:ResponseModel) => {
      if(response.isSuccess == true && response.data != null){
        this.sliderList.push(...response.data)
      }
    });
  }

  ngOnInit(): void {
    this.getAllSlider();
  }
}
interface sliderModel {
    id : number; 
    fileName : string;  
    filePath: string; 
    status: boolean;
}