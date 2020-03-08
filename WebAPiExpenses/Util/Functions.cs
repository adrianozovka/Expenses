namespace WebAPiExpenses.Util
{
    public static class Functions
    {

        public static bool IsNumeric(string input, out int value) {
        
            return int.TryParse(input, out value);
        }
        
    }
}