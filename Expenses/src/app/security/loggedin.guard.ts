import { CanLoad, Route , CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router'
import { Injectable } from '@angular/core';
import { LoginService } from '../services/login.service';

@Injectable()
export class LoggedInGuard implements CanLoad, CanActivate {

  constructor(private loginService: LoginService) { }

  checkAuthentication(path: string) {
    const loggedin = this.loginService.isLoggedIn()

    if (!loggedin) {
      console.log(`Caminho checkAuthentication ${path}`);
      this.loginService.handleLogin(`/${path}`);
    }

    return loggedin;
  }

  canLoad(route: Route): boolean {
    console.log(`Caminho canLoad ${route.path}`);
    return this.checkAuthentication(route.path)
  }

  canActivate(activatedRouteSnapshot: ActivatedRouteSnapshot, routerStateSnapshot: RouterStateSnapshot) {
    console.log(`Caminho canActivate ${activatedRouteSnapshot.routeConfig.path}`);
    return this.checkAuthentication(activatedRouteSnapshot.routeConfig.path)
  }

}
