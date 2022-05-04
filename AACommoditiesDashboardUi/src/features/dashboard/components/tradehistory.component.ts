import { Component, OnDestroy, OnInit } from '@angular/core';
import { ColDef } from 'ag-grid-community';
import { Subscription } from 'rxjs';
import { formatDate } from "@angular/common";

import { KeyValuePair } from 'src/core/models/keyvaluepair.model';
import { ReferenceDataService } from 'src/core/reference.data.service';
import { DashboardDataService } from '../services/data.service';
import { TradeActionSummary } from '../services/models/tradeActionHistory.model';

class Filter {
  subscription: Subscription | null = null;
  data: KeyValuePair[] = [];
  selectedItem: KeyValuePair | null = null
}

@Component({
  selector: 'app-trade-history',
  templateUrl: './tradehistory.component.html',
  styles: []
})
export class TradeHistoryComponent implements OnDestroy, OnInit {
  modelsFilter : Filter = new Filter();
  commodityFilter : Filter = new Filter();
  tradeActionFilter : Filter = new Filter();
 
  tradeHistorySubscription: Subscription | null = null;
  tradeHistory: TradeActionSummary[] = [];

  columnDefs: ColDef[] = [
    {
      headerName: 'Model',
      valueGetter: function (params) { return params.data.commodityModel.model.value }, sortable: true
    },
    {
      headerName: 'Commodity',
      valueGetter: function (params) { return params.data.commodityModel.commodity.value }, sortable: true
    },
    {
      headerName: 'Action',
      valueGetter: function (params) { return params.data.tradeAction.value }, sortable: true
    },
    { field: 'tradeDate', sortable: true, valueFormatter: function (params) { return formatDate(params.value, 'dd-MMM-yy', 'en-GB') } },
    { field: 'quantity', sortable: true }
  ];

  constructor(private dataService: DashboardDataService, 
    private refDataService: ReferenceDataService) {
  }

  ngOnInit(){
    this.initializeFilters();
    this.refreshData();
  }

  ngOnDestroy(){
    if (this.tradeHistorySubscription) {
      this.tradeHistorySubscription.unsubscribe();
    }
    if (this.modelsFilter.subscription) {
      this.modelsFilter.subscription.unsubscribe();
    }
    if (this.commodityFilter.subscription) {
      this.commodityFilter.subscription.unsubscribe();
    }
    if (this.tradeActionFilter.subscription) {
      this.tradeActionFilter.subscription.unsubscribe();
    }
  }

  initializeFilters(){
    this.modelsFilter.subscription = this.refDataService.getModels().subscribe(x => {
      let model = [...x];
      model.unshift({ key: '', value: 'All' });
      this.modelsFilter.data = model;
      if (!this.modelsFilter.selectedItem) {
        this.modelsFilter.selectedItem = this.modelsFilter.data[0];
      }
    });
    this.commodityFilter.subscription = this.refDataService.getCommodities().subscribe(x => {
      let comms = [...x];
      comms.unshift({ key: '', value: 'All' });
      this.commodityFilter.data = comms;
      if (!this.commodityFilter.selectedItem) {
        this.commodityFilter.selectedItem = comms[0];
      }
    });
    this.tradeActionFilter.subscription = this.refDataService.getTradeActions().subscribe(x => {
      let actions = [...x];
      actions.unshift({ key: '', value: 'All' });
      this.tradeActionFilter.data = actions;
      if (!this.tradeActionFilter.selectedItem) {
        this.tradeActionFilter.selectedItem = actions[0];
      }
    });
  }

  onModelChange(item: any) {
    this.modelsFilter.selectedItem = item;
    this.refreshData();
  }

  onCommsChange(item: any) {
    this.commodityFilter.selectedItem = item;
    this.refreshData();
  }

  onActionChange(item: any) {
    this.tradeActionFilter.selectedItem = item;
    this.refreshData();
  }

  refreshData() {
    if (this.tradeHistorySubscription) {
      this.tradeHistorySubscription.unsubscribe();
    }
    let model = (this.modelsFilter.selectedItem && this.modelsFilter.selectedItem.key) || null;
    let commodity = (this.commodityFilter.selectedItem && this.commodityFilter.selectedItem.key) || null;
    let action = (this.tradeActionFilter.selectedItem && this.tradeActionFilter.selectedItem.key) || null;
    this.tradeHistorySubscription = this.dataService.getTradeHistory(model, commodity, action).subscribe(x => {
      this.tradeHistory = x.trades;
    });
  }
}
