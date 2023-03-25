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
  sportId : number = 0;
  tournamentId : number = 0;

  getSportList:any = () =>{
    this.isSportTournamentsCollapsed.length = 0;
  }

  resetSidebar(){
    this.sportId = 0;
    this.tournamentId = 0;
    this.sportTournamentsList = [];
    this.tournamentEventsList = [];
  }

  getSportTournamentsList:any = (sportId:string) =>{
    this.sportId = Number(sportId);
    this.service.get(`exchange/GetSeries?SportId=${sportId}&type=1`)
    .subscribe((response:ResponseModel) => {
      if(response.data != null && response.isSuccess == true){
        this.sportTournamentsList = response.data;
        //this.sidebarList = this.sidebarList?.filter((x:any) => x.id == sportId)
      }
    });
    
  }

  getTournamentEventsList:any = (SportId:number, SeriesId:any, index : number) =>{
    this.sportId = Number(SportId);
    this.tournamentId = Number(SeriesId);
    this.service.get(`exchange/GetMatches?SportId=${SportId}&SeriesId=${SeriesId}&type=1`)
    .subscribe((response:ResponseModel) => {
      if(response.data != null){
        this.tournamentEventsList = response.data;
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
  eid: string;
  gameId: string;
  eventName: string;
  marketId: string;
  tournamentId: string;
}