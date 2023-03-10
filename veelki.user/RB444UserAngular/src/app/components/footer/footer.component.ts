import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  constructor(private authService: AuthService) { }

  isLoginUser: boolean = false;

  ngOnInit(): void {
    this.authService._isLoginUser.subscribe(
      (res:boolean) => {
        this.isLoginUser = res;
      }
    )
  }

}
