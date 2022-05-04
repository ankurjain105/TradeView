import { NgModule } from '@angular/core';
 import {CommonModule} from '@angular/common';

import { HttpClientModule } from '@angular/common/http';
import { DropdownComponent } from './components/dropdown.component';
import { CoreModule } from 'src/core';
@NgModule({
  declarations: [
    DropdownComponent
  ],
  imports: [
    HttpClientModule,
    CoreModule,
    CommonModule
  ],
  providers: [],
  exports: [DropdownComponent]
})
export class SharedModule { }
