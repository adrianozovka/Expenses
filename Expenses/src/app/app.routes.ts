import { Routes } from '@angular/router'

import { HomeComponent } from './component/home/home.component'
import { ReportByMonthComponent } from './component/report-by-month/report-by-month.component'
import { ReportByCategoryComponent } from './component/report-by-category/report-by-category.component'
import { ReportBySourceComponent } from './component/report-by-source/report-by-source.component'
import { NotfoundComponent } from './component/notfound/notfound.component'
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


