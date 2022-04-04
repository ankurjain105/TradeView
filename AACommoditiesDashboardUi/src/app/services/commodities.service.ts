import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CommodityRecentHistory } from '../types/commodity-recent-history';
import { CommodityDataPoint } from '../types/commodity-data-point';

interface CommodityRecentHistoryDto {
  model: string;
  commodity: string;
  dataPoints: CommodityDataPointDto[];
}

interface CommodityDataPointDto {
  price: number;
  position: number;
  newTradeAction: number;
  pnlDaily: number;
  date: string;
}

@Injectable({
  providedIn: 'root'
})
export class CommoditiesService {
  constructor(protected http: HttpClient) {}

  public getRecentHistory(): Observable<CommodityRecentHistory[]> {
    const url = `${environment.commoditiesApi}/commodities/recent-history`;
    return this.http
               .get<CommodityRecentHistoryDto[]>(url)
               .pipe(map((x) => {
                 return x.map((o) => {
                   return <CommodityRecentHistory>{
                     model: o.model,
                     commodity: o.commodity,
                     dataPoints: this.mapCommodityDatePointDtos(o.dataPoints)
                   }
                 });
               }));
  }

  private mapCommodityDatePointDtos(data: CommodityDataPointDto[]): CommodityDataPoint[] {
    return data.map((o => {
      return {
        date: new Date(o.date),
        newTradeAction: o.newTradeAction,
        pnlDaily: o.pnlDaily,
        position: o.position,
        price: o.price
    }}));
  }
}
