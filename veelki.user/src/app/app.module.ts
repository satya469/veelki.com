import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutComponent } from './components/layout/layout.component';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { HomeComponent } from './components/home/home.component';
import { InplayComponent } from './components/inplay/inplay.component';
import { AsideComponent } from './components/aside/aside.component';
import { MultimarketComponent } from './components/multimarket/multimarket.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { AllsportHighlightComponent } from './components/allsport-highlight/allsport-highlight.component';
import { FinancialMarketComponent } from './components/financial-market/financial-market.component';
import { HorseRacingComponent } from './components/horse-racing/horse-racing.component';
import { EventResultComponent } from './components/event-result/event-result.component';
import { MyProfileComponent } from './components/my-account/my-profile/my-profile.component';
import { SummaryComponent } from './components/my-account/summary/summary.component';
import { AccountStatementComponent } from './components/my-account/account-statement/account-statement.component';
import { MyBetsComponent } from './components/my-account/my-bets/my-bets.component';
import { ActivityLogComponent } from './components/my-account/activity-log/activity-log.component';
import { FullmarketComponent } from './components/fullmarket/fullmarket.component';
import { ProfileSidebarComponent } from './components/my-account/profile-sidebar/profile-sidebar.component';
import { ProfitLossComponent } from './components/my-account/profit-loss/profit-loss.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NewsComponent } from './components/news/news.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { StackReducer } from './store/reducers/stack.reducer';
import { InplayReducer } from './store/reducers/inplay.reducer';
import { SportReducer } from './store/reducers/getSport.reducer';
import { SportEffect } from './store/effects/getSport.effect';
import { InplayEffect } from './store/effects/inplay.effect';
import { StackEffects } from './store/effects/stack.effect';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { TableRowComponent } from './components/table-row/table-row.component';
import { SliderComponent } from './components/slider/slider.component';
import { CarouselModule } from 'ngx-owl-carousel-o';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatchOddComponent } from './components/fullmarket/match-odd/match-odd.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxSpinnerModule } from "ngx-spinner";
import { AuthService } from './services/auth.service';
import { HttpService } from './services/http.service';
import { SessionService } from './services/session.service';
import { BetService } from './services/getBet.service';
import { InterceptorService } from './http.interceptor';
import { ThousandSuffixesPipe } from './helpers/thousand-suffixes.pipe';
import { NotificationService } from './services/notification.service';
import { NgxMarqueeModule } from 'ngx-marquee';
import { BalanceComponent } from './components/balance/balance.component';
import { NgbAccordion, NgbAccordionModule } from '@ng-bootstrap/ng-bootstrap';
import { LoginComponent } from './components/login/login.component';
import { NavComponent } from './components/nav/nav.component';
import { SportsDataComponent } from './components/sports-data/sports-data.component';
import { PerfectScrollbarModule, PerfectScrollbarConfigInterface,
  PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { AccountComponent } from './components/account/account.component';

  const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    wheelPropagation: true
  };

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    HeaderComponent,
    FooterComponent,
    HomeComponent,
    InplayComponent,
    AsideComponent,
    MultimarketComponent,
    SidebarComponent,
    AllsportHighlightComponent,
    FinancialMarketComponent,
    HorseRacingComponent,
    EventResultComponent,
    MyProfileComponent,
    SummaryComponent,
    AccountStatementComponent,
    MyBetsComponent,
    ActivityLogComponent,
    FullmarketComponent,
    ProfileSidebarComponent,
    ProfitLossComponent,
    NewsComponent,
    TableRowComponent,
    SliderComponent,
    MatchOddComponent,
    ThousandSuffixesPipe,
    BalanceComponent,
    LoginComponent,
    NavComponent,
    SportsDataComponent,
    AccountComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule,
    HttpClientModule,
    CarouselModule,
    NgxSpinnerModule,
    BrowserAnimationsModule,
    NgxMarqueeModule,
    NgbAccordionModule,
    PerfectScrollbarModule,
    ToastrModule.forRoot(),
    StoreModule.forRoot({StackData : StackReducer, InplayData : InplayReducer, SportData : SportReducer}),
    EffectsModule.forRoot([StackEffects,InplayEffect, SportEffect]),
    StoreDevtoolsModule.instrument({
      maxAge : 25,
      logOnly : true,
      autoPause : true
    })
  ],
  providers: [AuthService, NotificationService, HttpService, SessionService, BetService, InterceptorService,
    { provide: HTTP_INTERCEPTORS, useClass: InterceptorService, multi: true },
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ],
  bootstrap: [AppComponent],
})
export class AppModule { }
