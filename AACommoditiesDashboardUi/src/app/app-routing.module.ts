import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DashboadComponent } from 'src/features/dashboard/components/dashboard.component';

@NgModule({
  imports: [RouterModule.forRoot([
    {
      path: '',
      redirectTo: 'dashboard',
      pathMatch: 'full'
    },
    {
      path: 'dashboard',
      loadChildren: () => import('../features/dashboard/dashboard.module').then(m => m.DashboardModule)
    }
  ])],
  exports: [RouterModule]
})
export class AppRoutingModule { }
