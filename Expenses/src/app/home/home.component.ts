import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { LoginService } from '../services/login.service';
import { User } from '../model/user.model'
import { ActivatedRoute, Router } from '@angular/router'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {

  loginForm: FormGroup
  navigateTo: string
  error: string

  constructor(private fb: FormBuilder, private loginService: LoginService, private activatedRoute: ActivatedRoute, private router: Router) {
    this.loginService.logout();
    this.error = '';
  }

  ngOnInit() {

   
    this.error = '';
    this.loginForm = this.fb.group(
      {
        username: this.fb.control('', [Validators.required]),
        password: this.fb.control('', [Validators.required])
      }
    )
    this.navigateTo = this.activatedRoute.snapshot.params['to'] || btoa('/reportByMonth');
    
   
  }


  login() {
    this.loginService.login(this.loginForm.value.username, this.loginForm.value.password).subscribe(
      loginAccess =>
        console.log("login " + loginAccess.user.username),
      response => //HttpErrorResponse
        this.error = response.error.message,       
      () => {
        console.log("navigate" + [atob(this.navigateTo)]),
        this.router.navigate([atob(this.navigateTo)])
        
      }
    )
  }
}
