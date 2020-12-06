using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnforgettableMemo.Shared;
using UnforgettableMemo.Shared.Models;

namespace UnforgettableMemo.WinDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class MainWindowViewModel
        {
            public Memo DisplayingMemo { get; set; }
        }

        private readonly MainWindowViewModel viewModel = new MainWindowViewModel();

        private MemoScheduler memoScheduler;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MemoSchedulerFactory memoSchedulerFactory = new MemoSchedulerFactory("memos/");

            this.memoScheduler = memoSchedulerFactory.GetMemoScheduler();
            this.memoScheduler.Load();
            UpdateDisplayingMemo();

            this.DataContext = viewModel;
        }

        private void UpdateDisplayingMemo()
        {
            this.memoScheduler.OrderByRetrievability();
            if (this.memoScheduler.Memos.Count == 0)
            {
                this.memoScheduler.GetNewMemo();
            }
            this.viewModel.DisplayingMemo = this.memoScheduler.Memos[0];
            UpdateTxtContent();

            this.memoScheduler.Save();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.viewModel.DisplayingMemo.Content))
            {
                return;
            }
            this.viewModel.DisplayingMemo = this.memoScheduler.GetNewMemo();
            UpdateTxtContent();
            this.memoScheduler.Save();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            this.memoScheduler.RemoveMemo(viewModel.DisplayingMemo);
            UpdateDisplayingMemo();
        }

        private void btnReview_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.DisplayingMemo.Review();
            UpdateDisplayingMemo();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void topBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void txtContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTxtContent();
            this.memoScheduler.Save();
        }

        private void UpdateTxtContent()
        {
            BindingExpression binding = this.txtContent.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Activate();
        }
    }
}
