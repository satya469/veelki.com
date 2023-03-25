import { Component, Input, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { ResponseModel } from 'src/app/models/responseModel';
import { StackLimit } from 'src/app/models/stackLimit';
import { AuthService } from 'src/app/services/auth.service';
import { ConfirmService } from 'src/app/services/confirm.service';
import { HttpService } from 'src/app/services/http.service';
import { NotificationService } from 'src/app/services/notification.service';
import { SessionService } from 'src/app/services/session.service';
import { SubjectService } from 'src/app/services/subject.service';
import { UPDATE_STACK } from 'src/app/store/actions/stack.action';

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
  showLoginPopup : boolean = false;
  gmtTimeZone : any = new Date(0).toString();
  stackForm: FormGroup;
  stackLimitList: StackLimit[] = [];
  stackData?: Observable<StackLimit[]>;
  isStackEdit : boolean = false;

  constructor(private formBuilder: FormBuilder, private subjectService : SubjectService, private service: HttpService, private router: Router, private authService: AuthService, private sessionService: SessionService, private modalService: NgbModal,
    private confirmService: ConfirmService, public notification: NotificationService, private store: Store<{ StackData: StackLimit[] }>) {

    this.gmtTimeZone = this.gmtTimeZone.substring(this.gmtTimeZone.indexOf("GMT"),this.gmtTimeZone.length);

    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.stackForm = this.formBuilder.group({
      stackList: this.formBuilder.array([])
    });
    this.stackData = this.store.select(data => data.StackData);

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
          if(this.showLoginPopup){
            this.subjectService.showLoginPopup.next(false);
            this.authService.login(response.data, false);
          }else{
            this.authService.login(response.data);
          }
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

    this.subjectService.showLoginPopup.subscribe((data:boolean)=>{
      this.showLoginPopup = data;
    })

    this.stackData?.subscribe((stack: StackLimit[]) => {
      if (stack.length > 0) {
        this.stackLimitList = stack;
        if (this.stackList.controls.length == 0) {
          stack.forEach((x) => {
            this.stackList.push(this.stackPatchValue(x));
          })
        }
      }
    });

  }

  setLoginPopup(value:boolean = true){
    this.subjectService.showLoginPopup.next(value);
  }

  get stackList(): FormArray {
    return this.stackForm.get("stackList") as FormArray
  }

  stackPatchValue(value: StackLimit): FormGroup {
    return this.formBuilder.group({
      id: [value.id, Validators.required],
      stakeLimitPrice: [value.stakeLimitPrice, Validators.required]
    })
  }

  stackSubmit(): void {
    if (this.stackForm.status == "INVALID") {
      this.notification.showError("Enter a valid number.");
      return;
    }
    this.store.dispatch(UPDATE_STACK({ state : this.stackLimitList , stack: this.stackForm.value.stackList }));
  }

}
