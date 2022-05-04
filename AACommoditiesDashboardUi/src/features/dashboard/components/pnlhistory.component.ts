import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { formatDate } from "@angular/common";

import { KeyValuePair } from 'src/core/models/keyvaluepair.model';
import { ReferenceDataService } from 'src/core/reference.data.service';
import { DashboardDataService } from '../services/data.service';
import { TradeActionSummary } from '../services/models/tradeActionHistory.model';
import { ColorHelper, ScaleType } from '@swimlane/ngx-charts';
import { Pnl, PnlSummary } from '../services/models/pnlHistory,model';


@Component({
  selector: 'app-pnl-history',
  templateUrl: './pnlhistory.component.html',
  styles: []
})
export class PnlHistoryComponent implements OnDestroy, OnInit  {
    pnlHistorySubscription: Subscription | null = null;
    pnlHistory: any[] = [];
    pnlHistoryCumulative: any[] = [];
    constructor(private dataService: DashboardDataService) {
    }

    ngOnInit(): void {
        this.refreshData();
    }
    ngOnDestroy(): void {
        if (this.pnlHistorySubscription) {
            this.pnlHistorySubscription.unsubscribe();
        }
    }

    refreshData() {
        if (this.pnlHistorySubscription) {
          this.pnlHistorySubscription.unsubscribe();
        }
        this.pnlHistorySubscription = this.dataService.getPnlHistory().subscribe(x => {
          console.log(x);
          this.transform(x.pnlSummaries);
          this.transformCumulative(x.pnlSummaries);
        });
      }

    transform(pnlSummary: PnlSummary[]) {
      let result: any[] = [];
      pnlSummary.forEach((summary: PnlSummary) =>{
        let line: {name: string | any, series: any[] } = { name: summary.model?.name, series: [] };
        summary.pnls?.forEach((pnl: Pnl) => {
          line.series.push({name: formatDate(pnl.date, 'dd-MMM-yy', 'en-GB'), value: pnl.dailyPnl});
        })
        result.push(line);
      });
      this.pnlHistory = result;
      console.log(this.pnlHistory);
    }

    transformCumulative(pnlSummary: PnlSummary[]) {
      let result: any[] = [];
      pnlSummary.forEach((summary: PnlSummary) =>{
        let line: {name: string | any, series: any[] } = { name: summary.model?.name, series: [] };
        let runningTotal = 0;
        summary.pnls?.forEach((pnl: Pnl) => {
          runningTotal = runningTotal + (pnl.dailyPnl || 0); 
          line.series.push({name: formatDate(pnl.date, 'dd-MMM-yy', 'en-GB'), value: runningTotal });
        })
        result.push(line);
      });
      this.pnlHistoryCumulative = result;
      console.log(this.pnlHistoryCumulative);
    }
      colorScheme = {
        domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA'],
        group: ScaleType.Ordinal, 
        selectable: true, 
        name: 'Commodity Model'
      };

      onSelect(event:any) {
        console.log(event);
      }
}
