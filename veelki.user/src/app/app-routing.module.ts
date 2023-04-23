import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AllsportHighlightComponent } from './components/allsport-highlight/allsport-highlight.component';
import { EventResultComponent } from './components/event-result/event-result.component';
import { FinancialMarketComponent } from './components/financial-market/financial-market.component';
import { FullmarketComponent } from './components/fullmarket/fullmarket.component';
import { HomeComponent } from './components/home/home.component';
import { HorseRacingComponent } from './components/horse-racing/horse-racing.component';
import { InplayComponent } from './components/inplay/inplay.component';
import { LayoutComponent } from './components/layout/layout.component';
import { MultimarketComponent } from './components/multimarket/multimarket.component';
import { AccountStatementComponent } from './components/my-account/account-statement/account-statement.component';
import { ActivityLogComponent } from './components/my-account/activity-log/activity-log.component';
import { MyBetsComponent } from './components/my-account/my-bets/my-bets.component';
import { MyProfileComponent } from './components/my-account/my-profile/my-profile.component';
import { ProfitLossComponent } from './components/my-account/profit-loss/profit-loss.component';
import { SummaryComponent } from './components/my-account/summary/summary.component';
import { LoginComponent } from './components/login/login.component';
import { AccountComponent } from './components/account/account.component';
import { AuthGuard } from './auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  {
    path: '', component: LayoutComponent, children: [
      { path: '', component: HomeComponent },
      { path: 'home', component: HomeComponent },
      { path: 'allsport-highlight', component: AllsportHighlightComponent, canActivate: [AuthGuard] },
      { path: 'allsport-highlight/:id', component: AllsportHighlightComponent, canActivate: [AuthGuard] },
      { path: 'financial-market', component: FinancialMarketComponent },
      { path: 'inplay', component: InplayComponent, canActivate: [AuthGuard] },
      { path: 'horse-racing', component: HorseRacingComponent, canActivate: [AuthGuard] },
      { path: 'event-result', component: EventResultComponent, canActivate: [AuthGuard] },
      { path: 'fullmarket/:marketId/:gameId/:eId', component: FullmarketComponent, canActivate: [AuthGuard]},
      { path: 'multimarket', component: MultimarketComponent, canActivate: [AuthGuard]},
      { path: 'account', component: AccountComponent, canActivate: [AuthGuard] },
      { path: 'account/my-profile', component: MyProfileComponent, canActivate: [AuthGuard] },
      { path: 'account/summary', component: SummaryComponent, canActivate: [AuthGuard] },
      { path: 'account/account-statement', component: AccountStatementComponent, canActivate: [AuthGuard] },
      { path: 'account/current-bets', component: MyBetsComponent, canActivate: [AuthGuard] },
      { path: 'account/profit-loss', component: ProfitLossComponent, canActivate: [AuthGuard] },
      { path: 'account/activity-log', component: ActivityLogComponent, canActivate: [AuthGuard] },
      // { path: 'sports', component: SportsComponent, canActivate: [AuthGuard] },
      // { path: 'rolling-commission', component: RollingCommissionComponent, canActivate: [AuthGuard] },
      // { path: 'account-statement', component: AccountStatementComponent, canActivate: [AuthGuard] },
      // { path: 'bet-history', component: BetHistoryComponent, canActivate: [AuthGuard] },
      // { path: 'profit-loss', component: ProfitLossComponent, canActivate: [AuthGuard] },
      // { path: 'activity-log', component: ActivityLogComponent, canActivate: [AuthGuard] }
     ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
