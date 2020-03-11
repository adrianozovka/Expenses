import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';



@Component({
  selector: 'app-header',
  templateUrl: './header.component.html'
})
export class HeaderComponent implements OnInit {
  
  constructor() {
   
  }


  items: MenuItem[];

  ngOnInit() {

    this.items = [
      { label: 'Despesas por Mês', icon: 'pi pi-fw pi-file', routerLink: '/reportByMonth' },
      { label: 'Despesas por Categoria', icon: 'pi pi-fw pi-file', routerLink: '/reportByCategory' },
      { label: 'Despesas por Fonte', icon: 'pi pi-fw pi-file', routerLink: '/reportBySource' },      
      { label: 'Logout', icon: 'pi pi-fw pi-home', url: '/'}

    ]; 
  }
  
}
