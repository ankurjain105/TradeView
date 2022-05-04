import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { ReferenceDataService } from 'src/core/reference.data.service';
import { DashboardDataService } from '../services/data.service';
import { TradeHistoryComponent } from './tradehistory.component';
 
import { of } from 'rxjs';
import { By } from '@angular/platform-browser';
describe('TradeHistoryComponent', () => { // 1
  let component: TradeHistoryComponent;
  let fixture: ComponentFixture<TradeHistoryComponent>;
  let dashboardDataService: any;
  let referenceDataService: any;

  beforeEach(async(() => { // 2
    // Create jasmine spy object 
    dashboardDataService = jasmine.createSpyObj('DashboardDataService', ['getTradeHistory']);
    dashboardDataService.getTradeHistory.and.returnValue(of({trades: ['data']}));

    referenceDataService = jasmine.createSpyObj('ReferenceDataService', ['getModels', 'getCommodities', 'getTradeActions']);
    referenceDataService.getModels.and.returnValue(of([{key: 1, value: 'Model1'}, {key: 1, value: 'Model1'}]));
    referenceDataService.getCommodities.and.returnValue(of([{key: 1, value: 'C1'}, {key: 1, value: 'C2'}]));
    referenceDataService.getTradeActions.and.returnValue(of([{key: 1, value: 'Buy'}, {key: 1, value: 'Sell'}]));

    TestBed.configureTestingModule({
      declarations: [ TradeHistoryComponent ],
      providers: [ { provide: DashboardDataService, useValue: dashboardDataService}, 
        { provide: ReferenceDataService, useValue: referenceDataService}
      ]
    })
    .compileComponents();
  }));
 
  beforeEach(() => { // 3
    fixture = TestBed.createComponent(TradeHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
 
  it('should create and populates filter dropdowns', () => { // 4
    expect(component).toBeTruthy();
    fixture.detectChanges();
    const { debugElement } = fixture;
    const modelDropDown = debugElement.query(By.css('dropdown[id="model"]'));
    expect(modelDropDown).toBeTruthy();
    expect(modelDropDown.properties.selectedItem).toEqual({key: '', value: "All"});
    expect(modelDropDown.properties.items).toEqual([{key: '', value: "All"}, {key: 1, value: 'Model1'}, {key: 1, value: 'Model1'}]);
  
    const commsDropDown = debugElement.query(By.css('dropdown[id="commodity"]'));
    expect(commsDropDown).toBeTruthy();
    expect(commsDropDown.properties.selectedItem).toEqual({key: '', value: "All"});
    expect(commsDropDown.properties.items).toEqual([{key: '', value: "All"}, {key: 1, value: 'C1'}, {key: 1, value: 'C2'}]);

    const actionsDropDown = debugElement.query(By.css('dropdown[id="action"]'));
    expect(actionsDropDown).toBeTruthy();
    expect(actionsDropDown.properties.selectedItem).toEqual({key: '', value: "All"});
    expect(actionsDropDown.properties.items).toEqual([{key: '', value: "All"}, {key: 1, value: 'Buy'}, {key: 1, value: 'Sell'}]);

    });

    it('Data Grid is initialized', () => { // 4
        expect(component).toBeTruthy();
        fixture.detectChanges();
        const { debugElement } = fixture;
        const grid = debugElement.query(By.css('ag-grid-angular'));
        expect(grid).toBeTruthy();
        expect(grid.properties.columnDefs.length).toBe(5);
        console.log(grid.properties);
        expect(grid.properties.rowData).toEqual(['data']);
    });

    it('On Model change, data is updated', () => { 
        //Arrange
        const { debugElement } = fixture;
        dashboardDataService.getTradeHistory.and.returnValue(of({trades: ['modelChange']}));
        const modelDropDown = debugElement.query(By.css('dropdown[id="model"]'));
        expect(modelDropDown).toBeTruthy();

        //Act
        modelDropDown.triggerEventHandler('change', modelDropDown.properties.items[1]);
        
        //Assert
        fixture.detectChanges();
        const grid = debugElement.query(By.css('ag-grid-angular'));
        expect(grid).toBeTruthy();
        expect(grid.properties.columnDefs.length).toBe(5);
        expect(grid.properties.rowData).toEqual(['modelChange']);
        expect(dashboardDataService.getTradeHistory).toHaveBeenCalledWith(modelDropDown.properties.items[1].key, null, null);
    });

    it('On Commodity change, data is updated', () => { 
        //Arrange
        const { debugElement } = fixture;
        dashboardDataService.getTradeHistory.and.returnValue(of({trades: ['CommChange']}));
        const commodityDropDown = debugElement.query(By.css('dropdown[id="commodity"]'));
        expect(commodityDropDown).toBeTruthy();

        //Act
        commodityDropDown.triggerEventHandler('change', commodityDropDown.properties.items[1]);
        
        //Assert
        fixture.detectChanges();
        const grid = debugElement.query(By.css('ag-grid-angular'));
        expect(grid).toBeTruthy();
        expect(grid.properties.columnDefs.length).toBe(5);
        expect(grid.properties.rowData).toEqual(['CommChange']);
        expect(dashboardDataService.getTradeHistory).toHaveBeenCalledWith(null, commodityDropDown.properties.items[1].key, null);
    });

    it('On Action change, data is updated', () => { 
        //Arrange
        const { debugElement } = fixture;
        dashboardDataService.getTradeHistory.and.returnValue(of({trades: ['ActionChange']}));
        const actionDropDown = debugElement.query(By.css('dropdown[id="action"]'));
        expect(actionDropDown).toBeTruthy();

        //Act
        actionDropDown.triggerEventHandler('change', actionDropDown.properties.items[1]);
        
        //Assert
        fixture.detectChanges();
        const grid = debugElement.query(By.css('ag-grid-angular'));
        expect(grid).toBeTruthy();
        expect(grid.properties.columnDefs.length).toBe(5);
        expect(grid.properties.rowData).toEqual(['ActionChange']);
        expect(dashboardDataService.getTradeHistory).toHaveBeenCalledWith(null, null, actionDropDown.properties.items[1].key);
    });
});