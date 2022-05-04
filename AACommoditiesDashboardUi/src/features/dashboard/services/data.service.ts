import { Injectable } from '@angular/core'
import {
  HttpClient,
  HttpRequest,
  HttpEventType,
  HttpResponse,
  HttpHeaders,
} from '@angular/common/http'
import { Subject, Observable, of, BehaviorSubject } from 'rxjs'

import { ApiClient } from 'src/core';
import { TradeActionHistory } from './models/tradeActionHistory.model';
import { PnlHistory } from './models/pnlHistory,model';
import { Snapshot } from './models/snapshot.model';

@Injectable()
export class DashboardDataService {
  constructor(private apiClient: ApiClient) {
    this.apiClient = apiClient;
  }

  getTradeHistory(modelId: number | null, commodityId: number | null, actionId: number | null) 
      : Observable<TradeActionHistory>{
    let query = '';
    if (modelId) {
      query = query + 'modelId=' + modelId;
    }
    if (commodityId) {
      query = query + 'commodityId=' + commodityId;
    }
    if (actionId) {
      query = query + 'tradeAction=' + actionId;
    }
    return this.apiClient.get<TradeActionHistory>('commodities/trade-action-history?' + query);
  }

  getPnlHistory() 
      : Observable<PnlHistory>{
    return this.apiClient.get<PnlHistory>('commodities/pnl-history');
  }

  getCurrentSnaphsot() 
  : Observable<Snapshot>{
      return this.apiClient.get<Snapshot>('commodities/current-snapshot');
  }
}