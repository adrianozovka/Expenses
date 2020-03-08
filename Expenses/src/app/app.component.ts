import { Component } from '@angular/core';
import { ExpenseService } from '../app/services/expense.service'
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { LoginService } from './services/login.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  @BlockUI() blockUI: NgBlockUI;
  title = 'Expenses';

  loginService: LoginService;
  isLoggedin: boolean;


  constructor(private expenseService: ExpenseService, private loginServiceInj: LoginService) {

    this.loginService = loginServiceInj;

     this.blockUI.start(); // Start blocking
 
    setTimeout(() => {
      this.blockUI.stop(); // Stop blocking
    }, 1000);

    this.isLoggedIn();
  }
   
  ngOnInit() {

    this.isLoggedIn();
  }


  isLoggedIn(): boolean {
    
    this.isLoggedin = this.loginService.isLoggedIn();
    return this.isLoggedin;
  }

}
