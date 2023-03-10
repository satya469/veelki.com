import { Component, Input, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HttpService } from 'src/app/services/http.service';
import { SportList } from 'src/app/models/sportList';
import { ResponseModel } from 'src/app/models/responseModel';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {

  constructor(private service : HttpService) { }

  @Input() pageMenu? : SportList[];

  sportTournamentsList : SportTournamentsList[] = [];
  tournamentEventsList : TournamentEventsList[] = [];    
  isSportTournaments : boolean = false;
  isSportTournamentsCollapsed : boolean[] = [];

  getSportList:any = () =>{
    this.isSportTournaments = false;
    this.isSportTournamentsCollapsed.length = 0;
  }

  getSportTournamentsList:any = (sportId:string) =>{
    this.service.get(`exchange/GetSeries?SportId=${sportId}&type=1`)
    .subscribe((response:ResponseModel) => {
      if(response.data != null && response.isSuccess == true){
        this.sportTournamentsList = response.data;
        this.isSportTournaments = true;
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