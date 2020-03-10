import { Injectable } from "@angular/core"
import { HttpClient, HttpHeaders } from "@angular/common/http"
import { Expenses, APIExpenseResult } from "../model/expenses.model";
import { URL_API } from "../app.api"
import { Observable } from "rxjs/Observable";
import { LoginService } from '../services/login.service'

@Injectable()
export class ExpenseService {

  expenses: Expenses[]

  constructor(private http: HttpClient, private loginService: LoginService) { }

  getReportPerMonth(): Observable<APIExpenseResult> {

    let headers = new HttpHeaders()
    if (this.loginService.isLoggedIn()) {
      headers = headers.set('Authorization', `Bearer ${this.loginService.loginAccess.token}`)
    }
    return this.http.get<APIExpenseResult>(`${URL_API}/api/Expenses/GetTotalExpensesPerMonth`, { headers: headers })
  }


  getReportTotalExpensesPerCategory(): Observable<APIExpenseResult> {

    let headers = new HttpHeaders()
    if (this.loginService.isLoggedIn()) {
      headers = headers.set('Authorization', `Bearer ${this.loginService.loginAccess.token}`)
    }

    return this.http.get<APIExpenseResult>(`${URL_API}/api/Expenses/GetTotalExpensesPerCategory`, { headers: headers })
  }

  getReportPaymentPerSource(): Observable<APIExpenseResult> {

    let headers = new HttpHeaders()
    if (this.loginService.isLoggedIn()) {
      headers = headers.set('Authorization', `Bearer ${this.loginService.loginAccess.token}`)
    }

    return this.http.get<APIExpenseResult>(`${URL_API}/api/Expenses/GetPaymentPerSource`, { headers: headers })
  }


  setExpenses(expenses: Expenses[]) {
    this.expenses = expenses;
  }



}
