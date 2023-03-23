import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import {NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-rolling-commission',
  templateUrl: './rolling-commission.component.html',
  styleUrls: ['./rolling-commission.component.css']
})
export class RollingCommissionComponent implements OnInit {

  todayDate = new Date();
  maxDate = {year: this.todayDate.getFullYear(), month: this.todayDate.getMonth()+1, day: this.todayDate.getDate()};

  rollingForm : FormGroup;

  constructor(private fb : FormBuilder) {
    this.rollingForm = this.fb.group({
      start_date : [this.maxDate],
      end_date : [this.maxDate],
      flag : ['']
    })
   }

  ngOnInit(): void {
  }

}
