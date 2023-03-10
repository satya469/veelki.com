import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-fancy-bet',
  templateUrl: './fancy-bet.component.html',
  styleUrls: ['./fancy-bet.component.css']
})
export class FancyBetComponent implements OnInit {

  @Input() fancyBetData? : any[];

  constructor() { }

  ngOnInit(): void {
  }

}
