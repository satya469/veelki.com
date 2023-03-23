import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmService } from 'src/app/services/confirm.service';

@Component({
  selector: 'app-confirm-box',
  templateUrl: './confirm-box.component.html',
  styleUrls: ['./confirm-box.component.css']
})
export class ConfirmBoxComponent implements OnInit {

  constructor(public modalService: NgbModal, private confirmService : ConfirmService) { }

  @ViewChild('confirmModal') confirmModal? : ElementRef; 

  setModelConfirmation(val:boolean){
    this.modalService.dismissAll();
    if(val==true){
      this.confirmService.openConfirmBox(false);            
    }
  }


  ngOnInit(): void {
    this.confirmService.getConfirmData().subscribe(
      (res) => {
        if(res==true){
          this.modalService.open(this.confirmModal, { ariaLabelledBy: 'modal-basic-title' });
        }
      }
    )

  }

  // openChangePasswordModal(content: any) {
  //   this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  // }

}
