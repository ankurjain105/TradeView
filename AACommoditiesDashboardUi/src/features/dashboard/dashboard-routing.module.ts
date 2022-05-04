import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DashboadComponent } from './components/dashboard.component';
import { PnlHistoryComponent } from './components/pnlhistory.component';
import { TradeHistoryComponent } from './components/tradehistory.component';
@NgModule({
  imports: [RouterModule.forChild([
    {
      path: '',
      component: DashboadComponent
    },
    {
      path: 'pnl-history',
      component: PnlHistoryComponent
    },
    {
      path: 'trade-history',
      component: TradeHistoryComponent
    }
  ])],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
