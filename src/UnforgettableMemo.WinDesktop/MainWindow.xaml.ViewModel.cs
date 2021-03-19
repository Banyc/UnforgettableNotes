using UnforgettableMemo.Shared.Models;

namespace UnforgettableMemo.WinDesktop
{
    public partial class MainWindow
    {
        private readonly MainWindowViewModel viewModel = new MainWindowViewModel();
        public class MainWindowViewModel
        {
            public Memo DisplayingMemo { get; set; }
            public int Energy { get; set; }
        }
    }
}
