import { Injectable } from '@angular/core'
import { ApiClient } from './apiclient.service';
import { Subject, Observable, of, BehaviorSubject } from 'rxjs'
import { KeyValuePair } from './models/keyvaluepair.model';
import { LookupResult } from './models/lookup.model';
import { CommodityModel } from './models/commoditymodel.model';

@Injectable()
export class ReferenceDataService {
  sub;
  private models: BehaviorSubject<KeyValuePair[]> = new BehaviorSubject(new Array<KeyValuePair>());
  private commodities: BehaviorSubject<KeyValuePair[]> = new BehaviorSubject(new Array<KeyValuePair>());
  private tradeActions: BehaviorSubject<KeyValuePair[]> = new BehaviorSubject(new Array<KeyValuePair>());
  private commodityModels: BehaviorSubject<CommodityModel[]> = new BehaviorSubject(new Array<CommodityModel>());
  
  constructor(private apiClient: ApiClient) {
    this.sub = this.apiClient.get<LookupResult>('commodities/lookups').subscribe(x => {
        this.models.next(x.models);
        this.commodities.next(x.commodities);
        this.commodityModels.next(x.commodityModels);
        this.tradeActions.next(x.tradeActions);
    });
  }

  getModels() : Observable<any>{
    return this.models;
  }

  getCommodities() : Observable<any>{
    return this.commodities;
  }

  getCommodityModels() : Observable<any>{
    return this.commodityModels;
  }

  getTradeActions() : Observable<any>{
    return this.tradeActions;
  }
}