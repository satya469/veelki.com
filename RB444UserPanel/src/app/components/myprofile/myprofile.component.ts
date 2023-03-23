import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/app/services/http.service';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from 'src/app/services/notification.service';
import { SessionService } from 'src/app/services/session.service';
import { ResponseModel } from 'src/app/models/responseModel';

@Component({
  selector: 'app-myprofile',
  templateUrl: './myprofile.component.html',
  styleUrls: ['./myprofile.component.css']
})
export class MyprofileComponent implements OnInit {

  userData? : UserData;
  rollingData? : RollingData;
  changePasswordForm: FormGroup;
  isFormSubmit = false;
  constructor(private service: HttpService, private modalService: NgbModal, private fb: FormBuilder, private notification : NotificationService, private sessionService : SessionService) {
    this.changePasswordForm = this.fb.group(
      {
        oldPassword: ['', [Validators.required]],
        newPassword: ['', [Validators.required, Validators.pattern('[a-zA-Z0-9@]{6,}')]],
        confirmPass: ['', [Validators.required]]
      }
    );
  }

  openRollingCommissionModal(content: any) {
    let userData = this.sessionService.getLoggedInUser();
    this.service.get(`Common/GetRollingCommission?UserId=${userData.id}&Type=1&ParentId=0`)
    .subscribe((response:ResponseModel) => {
      if(response.isSuccess == true && response.data != null){
        this.rollingData = <RollingData>response.data;        
      }else{
        this.rollingData = <RollingData>{
          fancy:0,
          matka:0,
          casino:0,
          binary:0,
          sportBook:0,
          bookmaker:0
        }
      }
      this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });       
    }); 
  }

  getUserData() : void {
    let userId = this.sessionService.getLoggedInUser().id;
    this.service.get(`Account/GetUserDetail?UserId=${userId}`)
      .subscribe((response:ResponseModel) => {
        if(response.isSuccess == true && response.data !== null){
          this.userData = <UserData>response.data;
        }
      });
  }

  openChangePasswordModal(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }
  onPasswordChange() {
    if (this.confirm_password.value.trim() == this.password.value.trim()) {
      this.confirm_password.setErrors(null);
    } else {
      this.confirm_password.setErrors({ mismatch: true });
    }
  }

  get password(): AbstractControl {
    return this.changePasswordForm.controls['newPassword'];
  }

  get confirm_password(): AbstractControl {
    return this.changePasswordForm.controls['confirmPass'];
  }

  formSubmit() {
    this.isFormSubmit = true;
    if (this.changePasswordForm.status === "INVALID") {
      return false;
    }
    let userid = this.sessionService.getLoggedInUser().id;
    let passwordData = {
      "UserId": userid.toString(),
      "OldPassword": this.changePasswordForm.controls.oldPassword.value,
      "Password": this.changePasswordForm.controls.newPassword.value,
      "ConfirmPassword": this.changePasswordForm.controls.confirmPass.value
    }
    this.service.post('Account/ResetPassword', passwordData)
    .subscribe((response:ResponseModel) => {
        if(response.isSuccess == true){
          this.notification.showSuccess(response.message);
        }else{
          this.notification.showError(response.message);
        }
        this.modalService.dismissAll();
        this.changePasswordForm.patchValue({
          oldPassword: '',
          newPassword: '',
          confirmPass: ''
        });        
      });
    return true;
  }

  ngOnInit(): void {
    this.getUserData();
  }

}

interface UserData{
  id : number;
  userName : string;
  normalizedUserName : string;
  fullName : string;
  email : string;
  normalizedEmail : string;
  emailConfirmed : boolean;
  passwordHash : string;
  phoneNumber : string;
  phoneNumberConfirmed : boolean;
  twoFactorEnabled : boolean    ;
  roleId : number;
  createdDate : string;
  rollingCommission : boolean;
  assignCoin : number;
  commision : number;
  exposureLimit : number;
  parentId : number;
  status : number;
}

interface RollingData{
  id : number;
  fromUserId : number;
  toUserId : number;
  fancy : number;
  matka : number;
  casino : number;
  binary : number;
  sportBook : number;
  bookmaker : number;
}