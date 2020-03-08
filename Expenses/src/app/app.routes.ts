import { Routes } from '@angular/router'

import { HomeComponent } from './home/home.component'
import { ReportByMonthComponent } from '../app/report-by-month/report-by-month.component'
import { ReportByCategoryComponent } from '../app/report-by-category/report-by-category.component'
import { ReportBySourceComponent } from '../app/report-by-source/report-by-source.component'
import { NotfoundComponent } from '../app/notfound/notfound.component'
import { LoggedInGuard } from './security/loggedin.guard';

export const ROUTES: Routes = [
  { path: '', component: HomeComponent },
  { path: 'login', component: HomeComponent },
  { path: 'login/:to', component: HomeComponent },
  { path: 'reportByMonth', component: ReportByMonthComponent, canLoad: [LoggedInGuard], canActivate: [LoggedInGuard] },
  { path: 'reportByCategory', component: ReportByCategoryComponent, canLoad: [LoggedInGuard], canActivate: [LoggedInGuard] },
  { path: 'reportBySource', component: ReportBySourceComponent, canLoad: [LoggedInGuard], canActivate: [LoggedInGuard] },
  
  { path: '**', component: NotfoundComponent }

]


