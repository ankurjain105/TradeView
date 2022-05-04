import { browser, by, logging } from 'protractor';

describe('Angular App', function() {

  it('Dashboard loads correctly with Snapshot', async () => {
    browser.get(browser.baseUrl);

    let expected = "Trade View";
    let actual = await browser.getTitle();

    expect(actual).toEqual(expected);
    let header = await browser.driver.findElement(by.css('app-dashboard h2')).getText();
    expect(header).toEqual("Dashboard");

    let dataRows = await browser.driver.findElements(by.css('app-dashboard .ag-center-cols-viewport .ag-row'));
    expect(dataRows.length).toEqual(3);
  });

  it('Pnl History loads graphs correctly', async () => {
    browser.get(browser.baseUrl + '/dashboard/pnl-history');

    let expected = "Trade View";
    let actual = await browser.getTitle();

    expect(actual).toEqual(expected);
    let header = await browser.driver.findElement(by.css('app-pnl-history h2')).getText();
    expect(header).toEqual("PnL History");

    let charts = await browser.driver.findElements(by.css('app-pnl-history ngx-charts-line-chart'));
    expect(charts.length).toEqual(2);
  });

  it('Trade History loads grid correctly', async () => {
    browser.get(browser.baseUrl + '/dashboard/trade-history');

    let expected = "Trade View";
    let actual = await browser.getTitle();

    expect(actual).toEqual(expected);
    let header = await browser.driver.findElement(by.css('app-trade-history h2')).getText();
    expect(header).toEqual("Trade History");

    let modelDd = await browser.driver.findElements(by.css('app-trade-history #model li.cc-filter'));
    expect(modelDd.length).toEqual(3);

    let history = await browser.driver.findElements(by.css('app-trade-history ag-grid-angular .ag-center-cols-viewport .ag-row'));
    expect(history.length).toEqual(7);
  });

  afterEach(async () => {
    const logs = await browser.manage().logs().get(logging.Type.BROWSER);
    expect(logs).not.toContain(jasmine.objectContaining({
      level: logging.Level.SEVERE,
    } as logging.Entry));
  });

}); 