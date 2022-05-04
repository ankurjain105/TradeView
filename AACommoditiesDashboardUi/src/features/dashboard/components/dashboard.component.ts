import { Component, OnDestroy, OnInit } from '@angular/core';
import { ColDef } from 'ag-grid-community';
import { Subscription } from 'rxjs';
import { DashboardDataService } from '../services/data.service';
import { SnapshotSummary } from '../services/models/snapshot.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styles: []
})
export class DashboadComponent implements OnDestroy, OnInit {
  snapshotSubscription : Subscription | null = null;
  snapshots: any[] = [];
  constructor(private dataService: DashboardDataService) {
  }

  ngOnInit(){
    this.refreshData();
  }

  ngOnDestroy(){
    if (this.snapshotSubscription) {
        this.snapshotSubscription.unsubscribe();
      }
  }

  columnDefs: ColDef[] = [
    {
        headerName: 'Commodity Model',
        valueGetter: function (params) { return params.data.commodityModel }, sortable: true
      },
    {
      headerName: 'Model',
      valueGetter: function (params) { return params.data.model }, sortable: true
    },
    {
      headerName: 'Commodity',
      valueGetter: function (params) { return params.data.commodity }, sortable: true
    },
    {
      headerName: 'Open Position',
      valueGetter: function (params) { return params.data.openPosition }, sortable: true
    },
    {
        headerName: 'Pnl',
        valueGetter: function (params) { return params.data.pnl }, sortable: true
      },
      {
        headerName: 'Pnl YTD',
        valueGetter: function (params) { return params.data.pnlYtd }, sortable: true
      },
  ];
  refreshData() {
    if (this.snapshotSubscription) {
        this.snapshotSubscription.unsubscribe();
      }
    
      this.snapshotSubscription = this.dataService.getCurrentSnaphsot().subscribe(x => {
        this.transform(x.snapshots);
      });
  }

  transform(snapshots: SnapshotSummary[]) {
    console.log(snapshots);
    let result: any[] = [];
    snapshots.forEach((summary: SnapshotSummary) => {
        result.push({ 
            commodityModel: summary.commodityModel?.name, 
            commodity: summary.commodityModel?.commodity?.value,
            model: summary.commodityModel?.model?.value,
            pnl: summary.pnl,
            pnlYtd: summary.pnlYtd,
            openPosition: summary.openPosition,
          });
    });
    this.snapshots = result;
  }
}
