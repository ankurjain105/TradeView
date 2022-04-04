import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { of } from 'rxjs';
import { CommoditiesService } from '../services/commodities.service';
import { createCommodityRecentHistory } from '../types/types.fixtures';
import { RecentHistoryComponent } from './recent-history.component';

describe('RecentHistoryComponent', () => {
  let component: RecentHistoryComponent;
  let fixture: ComponentFixture<RecentHistoryComponent>;
  let service: CommoditiesService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecentHistoryComponent ],
      imports: [
        HttpClientTestingModule,
        MDBBootstrapModule.forRoot(),
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecentHistoryComponent);
    service = TestBed.inject(CommoditiesService);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialise table headings', () => {
    const expected = ['Model', 'Commodity', 'Date', 'New Trade Action', 'Price', 'Position', 'PnL Daily'];

    expect(component.headElements).toEqual(expected);
  });

  it('should get commodity recent history on initialisation', () => {
    const spy = spyOn(service, 'getRecentHistory').and.returnValue(of());

    component.ngOnInit();

    expect(spy).toHaveBeenCalledTimes(1);
  });

  it('should populate table data on initialisation', () => {
    const history = [
      createCommodityRecentHistory(),
      createCommodityRecentHistory()
    ];
    spyOn(service, 'getRecentHistory').and.returnValue(of(history));
    let expected: any[] = [];
    history.forEach(x => x.dataPoints.forEach(o => expected.push({
      model: x.model,
      commodity: x.commodity,
      date: o.date,
      newTradeAction: o.newTradeAction,
      price: o.price,
      position: o.position,
      pnlDaily: o.pnlDaily
    })));
    expected = expected
      .sort((x: { date: number; }, y: { date: number; }) => +x.date - +y.date)
      .reverse();

    component.ngOnInit();

    expect(component.elements).toEqual(expected);
  });
});
