import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { environment } from 'src/environments/environment';
import { CommodityDataPoint } from '../types/commodity-data-point';
import { CommoditiesService } from './commodities.service';

describe('CommoditiesService', () => {
  let service: CommoditiesService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
    });
    service = TestBed.inject(CommoditiesService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  it('should create service', () => {
    expect(service).toBeTruthy();
  });

  it('should get recent history', (done) => {
    const response = [{
      model: 'model',
      commodity: 'commodity',
      dataPoints: [{
        price: 1,
        position: 2,
        newTradeAction: 3,
        pnlDaily: 4,
        date: '1 Jan 2020'
      }]
    }];
    const expected = response.map((x) => {
      return {
        model: x.model,
        commodity: x.commodity,
        dataPoints: mapCommodityDatePointDtos(x.dataPoints)
      }
    });

    service
      .getRecentHistory()
      .subscribe((actual) => {
        expect(actual).toEqual(expected);
        done();
      });

    const commoditiesRequest = httpTestingController.expectOne(
      `${environment.commoditiesApi}/commodities/recent-history`,
    );

    expect(commoditiesRequest.request.method).toEqual('GET');

    commoditiesRequest.flush(response);

    httpTestingController.verify();
  });

  function mapCommodityDatePointDtos(data: {
    date: string,
    newTradeAction: number,
    pnlDaily: number,
    position: number,
    price: number
  }[]): CommodityDataPoint[] {
    return data.map((o => {
      return {
        date: new Date(o.date),
        newTradeAction: o.newTradeAction,
        pnlDaily: o.pnlDaily,
        position: o.position,
        price: o.price
    }}))
  }
});
