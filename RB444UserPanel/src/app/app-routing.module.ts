import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LayoutComponent } from './components/layout/layout.component';
import { AllsportHighlightComponent } from './components/allsport-highlight/allsport-highlight.component';
import { InplayComponent } from './components/inplay/inplay.component';
import { FullmarketComponent } from './components/fullmarket/fullmarket.component';
import { AuthGuard } from './auth.guard';
import { MyprofileComponent } from './components/myprofile/myprofile.component';
import { RollingCommissionComponent } from './components/rolling-commission/rolling-commission.component';
import { AccountStatementComponent } from './components/account-statement/account-statement.component';
import { BetHistoryComponent } from './components/bet-history/bet-history.component';
import { ProfitLossComponent } from './components/profit-loss/profit-loss.component';
import { ActivityLogComponent } from './components/activity-log/activity-log.component';
import { SportsComponent } from './components/sports/sports.component';
import { AccountComponent } from './components/account/account.component';
import { MultimarketComponent } from './components/multimarket/multimarket.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent, children: [
      { path: '', component: HomeComponent },
      { path: 'home', component: HomeComponent },
      { path: 'allsport-highlight/:id', component: AllsportHighlightComponent },
      { path: 'inplay', component: InplayComponent },
      { path: 'fullmarket/:marketId/:eventId/:sportId', component: FullmarketComponent},
      { path: 'multimarket', component: MultimarketComponent},
      { path: 'myprofile', component: MyprofileComponent, canActivate: [AuthGuard] },
      { path: 'account', component: AccountComponent, canActivate: [AuthGuard] },
      { path: 'sports', component: SportsComponent, canActivate: [AuthGuard] },
      { path: 'rolling-commission', component: RollingCommissionComponent, canActivate: [AuthGuard] },
      { path: 'account-statement', component: AccountStatementComponent, canActivate: [AuthGuard] },
      { path: 'bet-history', component: BetHistoryComponent, canActivate: [AuthGuard] },
      { path: 'profit-loss', component: ProfitLossComponent, canActivate: [AuthGuard] },
      { path: 'activity-log', component: ActivityLogComponent, canActivate: [AuthGuard] }
     ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
