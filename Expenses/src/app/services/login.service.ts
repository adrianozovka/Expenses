import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs/Observable';
import { URL_API } from "../app.api"
import { User } from '../model/user.model';
import { Login } from '../model/login.model';
import { Router, NavigationEnd } from '@angular/router'
import 'rxjs/add/operator/filter'
import 'rxjs/add/operator/do'


@Injectable()
export class LoginService {

  loginAccess: Login

  lastUrl: string

  constructor(private http: HttpClient, private router: Router) {
    this.router.events.filter(e => e instanceof NavigationEnd).subscribe((e: NavigationEnd) => this.lastUrl = e.url)
  }

  isLoggedIn(): boolean {
        return (this.loginAccess !== undefined && this.loginAccess.user !== undefined && this.loginAccess.user.username !== undefined)
  }

  login(username: string, password: string): Observable<Login> {
    return this.http.post<Login>(`${URL_API}/api/home/login/`, { username: username, password: password }).do(login => this.loginAccess = login)
  }


  handleLogin(path: string = this.lastUrl) {
    this.router.navigate(['/login', btoa(path)]);
  }

  logout() {
    this.loginAccess = undefined;
  }

}
