import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AgGridModule } from 'ag-grid-angular';
import {NgxChartsModule} from '@swimlane/ngx-charts';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { CoreModule } from '../../core';
import { SharedModule } from '../../shared';
import { TradeHistoryComponent } from './components/tradehistory.component';
import { PnlHistoryComponent } from './components/pnlhistory.component';
import { DashboardDataService } from './services/data.service';
import { DashboadComponent } from './components/dashboard.component';
@NgModule({
  declarations: [
    TradeHistoryComponent,
    PnlHistoryComponent,
    DashboadComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    CoreModule,
    SharedModule,
    AgGridModule.withComponents([]),
    NgxChartsModule,
    //NoopAnimationsModule
  ],
  providers: [
    DashboardDataService
  ],
})
export class DashboardModule { }
