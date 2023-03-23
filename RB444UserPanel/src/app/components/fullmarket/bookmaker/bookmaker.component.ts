import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-bookmaker',
  templateUrl: './bookmaker.component.html',
  styleUrls: ['./bookmaker.component.css']
})
export class BookmakerComponent implements OnInit {

  @Input() bookmakerData?: any[];

  constructor() { }

  ngOnInit(): void {
  }

}
