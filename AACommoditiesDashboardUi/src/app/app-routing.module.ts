import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RecentHistoryComponent } from './recent-history/recent-history.component';

const routes: Routes = [
  {
    path: 'recent-history',
    component: RecentHistoryComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
