import { NgModule } from '@angular/core';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule  }   from '@angular/forms';
import { InterceptorService } from './http.interceptor';
import { NgxSpinnerModule } from "ngx-spinner";
import { NgxPaginationModule } from 'ngx-pagination';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutComponent } from './components/layout/layout.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { HomeComponent } from './components/home/home.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { AsideComponent } from './components/aside/aside.component';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { SliderComponent } from './components/slider/slider.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AllsportHighlightComponent } from './components/allsport-highlight/allsport-highlight.component';
import { InplayComponent } from './components/inplay/inplay.component';
import { TableRowComponent } from './components/table-row/table-row.component';
import { FullmarketComponent } from './components/fullmarket/fullmarket.component';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { ThousandSuffixesPipe } from './helpers/thousand-suffixes.pipe';
import { MyprofileComponent } from './components/myprofile/myprofile.component';
import { RollingCommissionComponent } from './components/rolling-commission/rolling-commission.component';
import { AccountStatementComponent } from './components/account-statement/account-statement.component';
import { BetHistoryComponent } from './components/bet-history/bet-history.component';
import { ProfitLossComponent } from './components/profit-loss/profit-loss.component';
import { ActivityLogComponent } from './components/activity-log/activity-log.component';
import { AuthService } from './services/auth.service';
import { HttpService } from './services/http.service';
import { SessionService } from './services/session.service';
import { ToastrModule } from 'ngx-toastr';
import { StackSettingsComponent } from './components/stack-settings/stack-settings.component';
import { SportsComponent } from './components/sports/sports.component';
import { AccountComponent } from './components/account/account.component';
import { MatchOddsComponent } from './components/fullmarket/match-odds/match-odds.component';
import { BookmakerComponent } from './components/fullmarket/bookmaker/bookmaker.component';
import { FancyBetComponent } from './components/fullmarket/fancy-bet/fancy-bet.component';
import { ConfirmBoxComponent } from './components/confirm-box/confirm-box.component';
import { BalanceComponent } from './components/balance/balance.component';
import { MultimarketComponent } from './components/multimarket/multimarket.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StackEffects } from './store/effects/stack.effect';
import { StackReducer } from './store/reducers/stack.reducer';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { InplayEffect } from './store/effects/inplay.effect';
import { InplayReducer } from './store/reducers/inplay.reducer';
import { SportEffect } from './store/effects/getSport.effect';
import { SportReducer } from './store/reducers/getSport.reducer';
import { BetService } from './services/getBet.service';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true,
  wheelPropagation: true
};

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    DashboardComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    SidebarComponent,
    AsideComponent,
    SliderComponent,
    AllsportHighlightComponent,
    InplayComponent,
    TableRowComponent,
    FullmarketComponent,
    ThousandSuffixesPipe,
    MyprofileComponent,
    RollingCommissionComponent,
    AccountStatementComponent,
    BetHistoryComponent,
    ProfitLossComponent,
    ActivityLogComponent,
    StackSettingsComponent,
    SportsComponent,
    AccountComponent,
    MatchOddsComponent,
    BookmakerComponent,
    FancyBetComponent,
    ConfirmBoxComponent,
    BalanceComponent,
    MultimarketComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    CarouselModule,
    NgbModule,
    PerfectScrollbarModule,
    FormsModule,
    ReactiveFormsModule,
    NgxSpinnerModule,
    NgxPaginationModule,
    ToastrModule.forRoot(),
    StoreModule.forRoot({StackData : StackReducer, InplayData : InplayReducer, SportData : SportReducer}),
    EffectsModule.forRoot([StackEffects,InplayEffect, SportEffect]),
    StoreDevtoolsModule.instrument({
      maxAge : 25,
      logOnly : true,
      autoPause : true
    })
  ],
  providers: [AuthService, HttpService, SessionService, BetService, InterceptorService,
    { provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
