import { NgModule } from '@angular/core';
import { ApiClient } from './apiclient.service';
import { HttpClientModule } from '@angular/common/http';
import { ReferenceDataService } from './reference.data.service';
@NgModule({
  declarations: [
  ],
  imports: [
    HttpClientModule
  ],
  providers: [ApiClient, ReferenceDataService],
})
export class CoreModule { }
