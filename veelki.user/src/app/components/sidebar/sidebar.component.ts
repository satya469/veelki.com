import { Component, Input, OnInit } from '@angular/core';
import { ResponseModel } from 'src/app/models/responseModel';
import { SportList } from 'src/app/models/sportList';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  constructor(private service : HttpService) { }

  @Input() sidebarList? : SportList[];

  sportTournamentsList : SportTournamentsList[] = [];
  tournamentEventsList : TournamentEventsList[] = [];    
  isSportTournamentsCollapsed : boolean[] = [];

  getSportList:any = () =>{
    this.isSportTournamentsCollapsed.length = 0;
  }

  getSportTournamentsList:any = (sportId:string) =>{
    this.service.get(`exchange/GetSeries?SportId=${sportId}&type=1`)
    .subscribe((response:ResponseModel) => {
      if(response.data != null && response.isSuccess == true){
        this.sidebarList = response.data;
      }
    });
    
  }

  getTournamentEventsList:any = (SportId:number, SeriesId:any, index : number) =>{
    this.service.get(`exchange/GetMatches?SportId=${SportId}&SeriesId=${SeriesId}&type=1`)
    .subscribe((response:ResponseModel) => {
      if(response.data != null){
        this.tournamentEventsList = response.data;
        for(let a = 0; a < this.sportTournamentsList.length; a++){
          if(a != index){
            this.isSportTournamentsCollapsed[a] = false;
          }else{
            console.log(this.isSportTournamentsCollapsed[a]);
            this.isSportTournamentsCollapsed[a] = (!this.isSportTournamentsCollapsed[a] ? true : false);
          }
        }
      }
    });
  }

  updateSportTournamentsList($event:string): void{
    this.getSportTournamentsList($event);
  }

  ngOnInit(): void {
  }

}


interface SportTournamentsList {
  sportId: number;
  tournamentId: string;
  tournamentName: string;
}

interface TournamentEventsList {
  sportId: string;
  eventId: string;
  eventName: string;
  marketId: string;
}