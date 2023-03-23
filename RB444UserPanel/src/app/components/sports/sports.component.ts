import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { SportList } from 'src/app/models/sportList';
import { HttpService } from 'src/app/services/http.service';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
  selector: 'app-sports',
  templateUrl: './sports.component.html',
  styleUrls: ['./sports.component.css']
})
export class SportsComponent implements OnInit {

  constructor(private service : HttpService, private loaderService : LoaderService) { }

  sidebarList : SportList[] = [];

  getSportList():Observable<SportList[]> {
    return this.service.get('exchange/GetSports')
    .pipe(map(response => {
      return response.data;
    }),
    catchError(() => {
      return of([]);
    }));
  }

  ngOnInit(): void {
    this.getSportList().subscribe(res => this.sidebarList = res);
  }

}
