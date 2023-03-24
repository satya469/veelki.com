import { Component, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ResponseModel } from 'src/app/models/responseModel';
import { AuthService } from 'src/app/services/auth.service';
import { ConfirmService } from 'src/app/services/confirm.service';
import { HttpService } from 'src/app/services/http.service';
import { SessionService } from 'src/app/services/session.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  //userData : UserData;
  loginForm: FormGroup;
  isSubmitted = false;
  formSubmitError: string = "";
  loginUserName: string = "";
  loginExposureLimit: number = 0;
  loginBalance: number = 0;

  constructor(private formBuilder: FormBuilder, private service: HttpService, private router: Router, private authService: AuthService, private sessionService: SessionService, private modalService: NgbModal,
    private confirmService: ConfirmService) {

    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  isLoginUser: boolean = false;

  get f() { return this.loginForm.controls; }

  loginFormSubmit() {
    this.isSubmitted = true;
    if (this.loginForm.invalid) {
      this.formSubmitError = "Enter username and password.";
      return;
    }

    this.confirmService.openConfirmBox(true);

    this.service.post('Account/Login', this.loginForm.value)
      .subscribe((response: ResponseModel) => {
        if (response.status == 200 && response.data != null) {
          this.formSubmitError = "";
          this.authService.login(response.data);
          //this.modalService.dismissAll();
        }
        if (response.data == null) {
          this.formSubmitError = response.message;
        }
      });



  }

  userLogout() {
    this.authService.logout();
    this.router.navigateByUrl('/home');
    this.authService._isLoginUser.next(false);
  }

  openLoginModal(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' });
  }

  ngOnInit(): void {
    this.authService._isLoginUser.subscribe(
      (res) => {
        this.isLoginUser = res;
        if (res && this.sessionService.getLoggedInUser() !== null && this.sessionService.getLoggedInUser().id > 0) {
          this.loginUserName = this.sessionService.getLoggedInUser().fullName;
          this.loginExposureLimit = this.sessionService.getLoggedInUser().exposureLimit;
          this.loginBalance = this.sessionService.getLoggedInUser().assignCoin;
        }
      }
    )

  }

}
