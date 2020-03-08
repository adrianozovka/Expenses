import { CanLoad, Route , CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router'
import { Injectable } from '@angular/core';
import { LoginService } from '../services/login.service';

@Injectable()
export class LoggedInGuard implements CanLoad, CanActivate {

  constructor(private loginService: LoginService) { }

  checkAuthentication(path: string) {
    const loggedin = this.loginService.isLoggedIn()

    if (!loggedin) {
        this.loginService.handleLogin(`/${path}`);
    }

    return loggedin;
  }

  canLoad(route: Route): boolean {
    return this.checkAuthentication(route.path)
  }

  canActivate(activatedRouteSnapshot: ActivatedRouteSnapshot, routerStateSnapshot: RouterStateSnapshot) {
    return this.checkAuthentication(activatedRouteSnapshot.routeConfig.path)
  }

}
