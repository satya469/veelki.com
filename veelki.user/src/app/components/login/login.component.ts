import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ResponseModel } from 'src/app/models/responseModel';
import { AuthService } from 'src/app/services/auth.service';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  isSubmitted = false;
  formSubmitError: string = "";
  randomNumber: number = 0;

  constructor(private formBuilder: FormBuilder, private service: HttpService, private authService: AuthService, private route : Router) {

    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
      codeValidator: ['']      
    });

    this.generateRandomNumber();

  }

  generateRandomNumber(){
    this.randomNumber = Math.floor(1000 + Math.random() * 9000);
  }

  ngOnInit(): void {
    this.authService._isLoginUser.subscribe((res:boolean)=>{
      if(res==true){
        this.route.navigateByUrl("/home");
      }
    })
  }

  loginFormSubmit() {
    this.isSubmitted = true;
    if (this.loginForm.invalid) {
      this.formSubmitError = "Enter username and password.";
      return;
    }

    if (this.loginForm.value.codeValidator != this.randomNumber) {
      this.formSubmitError = "Validation code is empty";
      return;
    }

    this.service.post('Account/Login', this.loginForm.value)
      .subscribe((response: ResponseModel) => {
        if (response.status == 200 && response.data != null) {
          this.formSubmitError = "";
          this.authService.login(response.data);
          //this.modalService.dismissAll();
        }
        if (response.data == null) {
          this.formSubmitError = response.message;
          this.loginForm.reset();
          this.generateRandomNumber();
        }
        setTimeout(()=>{
          this.formSubmitError = '';
        },3000);
      });
  }

}
