namespace RegexAnalyzer.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private RegexAnalyzerViewModel _regexanalyzerviewmodel;

        public RegexAnalyzerViewModel RegexAnalyzer
        {
            get => _regexanalyzerviewmodel ??= new RegexAnalyzerViewModel(); // retrieve a UserRegisterViewModel instance ("ListUserViewModel is required to update Datagrid when add someone to database")  ,
                                                                             // if don't exist, create one
        }
    }
}