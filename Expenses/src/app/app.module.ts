import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, PreloadAllModules } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { ReportByMonthComponent } from './report-by-month/report-by-month.component';
import { ExpenseService } from '../app/services/expense.service';
import { DataTablesModule } from 'angular-datatables';
import { BlockUIModule } from 'ng-block-ui';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ChartModule } from 'primeng/chart';
import { HomeComponent } from './home/home.component';
import { NotfoundComponent } from './notfound/notfound.component';
import { TabMenuModule } from 'primeng/tabmenu';
import { LoginService } from './services/login.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { LoggedInGuard } from './security/loggedin.guard';

import { ROUTES } from './app.routes';
import { HeaderComponent } from './header/header.component';
import { ReportByCategoryComponent } from './report-by-category/report-by-category.component';
import { ReportBySourceComponent } from './report-by-source/report-by-source.component'

@NgModule({
  declarations: [
    AppComponent,
    ReportByMonthComponent,
    HomeComponent,
    NotfoundComponent,
    HeaderComponent,
    ReportByCategoryComponent,
    ReportBySourceComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    DataTablesModule,
    BrowserAnimationsModule,
    ChartModule,
    TabMenuModule,
    FormsModule,
    ReactiveFormsModule,
    BlockUIModule.forRoot({
      delayStart: 500,
      delayStop: 500,
      message: 'Carregando...'
    }),
    RouterModule.forRoot(ROUTES, { preloadingStrategy: PreloadAllModules })
  ],
  providers: [ExpenseService, LoginService, LoggedInGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
