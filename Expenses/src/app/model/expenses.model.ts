
export interface Expenses {
  movimentYear: number,
  movimentMonth: number,
  movimentMonthDescription: string,
  amountPaid: number,
  categoryCode: number,
  categoryName: string,
  sourcePaymentCode: number,
  sourcePaymentoName: string
}


export interface APIExpenseResult {

  resultCode: string,       
  resultDescription: string,              
  lstExpense: Expenses[]  
}
