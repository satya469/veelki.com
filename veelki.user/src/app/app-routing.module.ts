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

const routes: Routes = [
  {
    path: '', component: LayoutComponent, children: [
      { path: '', component: HomeComponent },
      { path: 'home', component: HomeComponent },
      { path: 'allsport-highlight/:id', component: AllsportHighlightComponent },
      { path: 'financial-market', component: FinancialMarketComponent },
      { path: 'inplay', component: InplayComponent },
      { path: 'horse-racing', component: HorseRacingComponent },
      { path: 'event-result', component: EventResultComponent },
      { path: 'fullmarket', component: FullmarketComponent},
      { path: 'multimarket', component: MultimarketComponent},
      { path: 'account/my-profile', component: MyProfileComponent },
      { path: 'account/summary', component: SummaryComponent },
      { path: 'account/account-statement', component: AccountStatementComponent },
      { path: 'account/current-bets', component: MyBetsComponent },
      { path: 'account/profit-loss', component: ProfitLossComponent },
      { path: 'account/activity-log', component: ActivityLogComponent },
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
