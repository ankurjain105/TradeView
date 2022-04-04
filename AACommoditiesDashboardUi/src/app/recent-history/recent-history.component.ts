import { Component, OnInit } from '@angular/core';
import { CommoditiesService } from '../services/commodities.service';
import { CommodityRecentHistory } from '../types/commodity-recent-history';

@Component({
  selector: 'app-recent-history',
  templateUrl: './recent-history.component.html',
  styleUrls: ['./recent-history.component.scss']
})
export class RecentHistoryComponent implements OnInit {

  elements: any = [];
  headElements: string[] = ['Model', 'Commodity', 'Date', 'New Trade Action', 'Price', 'Position', 'PnL Daily'];

  constructor(private commoditiesService: CommoditiesService) { }

  ngOnInit() {
    this.commoditiesService
        .getRecentHistory()
        .subscribe(x => this.populateTableData(x));
  }

  private populateTableData(history: CommodityRecentHistory[]): void {
    this.elements = [];
    history.forEach(x => x.dataPoints.forEach(o => this.elements.push({
      model: x.model,
      commodity: x.commodity,
      date: o.date,
      newTradeAction: o.newTradeAction,
      price: o.price,
      position: o.position,
      pnlDaily: o.pnlDaily
    })));
    this.elements = this.elements
      .sort((x: { date: number; }, y: { date: number; }) => +x.date - +y.date)
      .reverse();
  }
}
