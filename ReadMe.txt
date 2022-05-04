Some points to note
FOR API
1. In interest of time, I have concentrated more on structure, core working functionality, domain model, 
    some basic unit tests, repository integration tests and Component tests (tests that test integrated functionality
     of whole of the API Application)
2. Component tests could have been written is BDD\Specflow to align to be more business friendly
3. Authentication/Authorization/Full Fleged Logging is missing
4. For tests, basic structure/tests are there all it needs to extending them.
5. Clock implementation is used to assume current day being the last day of the data, to keep it simple as we 
   had data only till certain date
6. Database design\Table design is in the database project (AA.CommoditiesDashboard.Db) within 2 schemas model and trade (model for maintaing model/commodity
   relationships while trade schema for pnl/position etc)


FOR UI
1. In interest of time, I have concentrated more on structure, basic tests, some e2e tests and a working solution
2. Unit tests and E2E tests are not fully complete. They provide basic checks but they are all setup 
    and need to be just extended to include more cases
3. Have used Angular theme from one of the sites rather than writing whole css from scratch
4. Have used feature based code structure as I think it is best for complicated apps and 
    tend to save time on maintainability of code
5. Have used ag grid for grids and ngx-charts for charting
6. There are some elements of prod ready applications that are missing like logging, authentication, authorization, 
    active menu working correctly etc.
7. For PNL History shows 2 charts one daily and one cumulative PNL
