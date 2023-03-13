import { NgModule } from '@angular/core';
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
    ProfitLossComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
