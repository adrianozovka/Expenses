import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { ExpenseService } from '../../app/services/expense.service'
import { Expenses } from '../../app/model/expenses.model';


@Component({
  selector: 'app-report-by-source',
  templateUrl: './report-by-source.component.html'
})
export class ReportBySourceComponent implements OnInit {

  data: any;
  labelsChart: string[] = [];
  dataChart: number[] = [];
  options = {
    responsive: false,
    maintainAspectRatio: false
  };

  dtOptions: DataTables.Settings = {};
  expenses: Expenses[] = [];
  // We use this trigger because fetching the list of persons can be quite long,
  // thus we ensure the data is fetched before rendering
  dtTrigger: Subject<object> = new Subject<object>();
  orderOptions: number[] = [1];

  constructor(private expenseService: ExpenseService) {
    this.data = {
      labels: [],
      datasets: [
        {
          label: 'Despesas p/ Mês',
          backgroundColor: '#42A5F5',
          borderColor: '#1E88E5',
          data: []


        }
      ]

    }
  }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 12,
      order: this.orderOptions,

      language: {

        //search: "Pesquisar",
        url: "https://cdn.datatables.net/plug-ins/1.10.20/i18n/Portuguese-Brasil.json"

      }
    };


    this.expenseService.getReportPaymentPerSource()
      .subscribe(expenses => {

        this.expenses = expenses.lstExpense;

        this.feedChart(this.expenses);

        this.dtTrigger.next();

      });

  }

  feedChart(expenses: Expenses[]) {
    if (expenses !== null && expenses.length > 0) {
      expenses.sort((a, b) => (a.sourcePaymentoName < b.sourcePaymentoName) ? -1 : 1);

      for (var index in expenses) {
        this.labelsChart.push(expenses[index].sourcePaymentoName);
        this.dataChart.push(expenses[index].amountPaid);
      }
      this.data.labels = this.labelsChart;
      this.data.datasets[0].data = this.dataChart;
    }
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }




}
