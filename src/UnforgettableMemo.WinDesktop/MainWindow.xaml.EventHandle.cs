using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UnforgettableMemo.WinDesktop
{
    public partial class MainWindow
    {
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = viewModel;
        }

        // Add a new memo and display it
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.viewModel.DisplayingMemo.Content))
            {
                return;
            }
            this.viewModel.DisplayingMemo = this.memoScheduler.GetNewMemo();
            UpdateFrontend();
            this.memoScheduler.Save();
        }

        // delete the displaying memo from the list and display the least memorized memo
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            this.memoScheduler.RemoveMemo(viewModel.DisplayingMemo);
            UpdateDisplayingMemo();
        }

        // update the memory state of the displaying memo and display the least memorized memo
        private void btnReview_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.DisplayingMemo.Review();
            UpdateDisplayingMemo();
        }

        // save settings and exit
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings(new MainWindowSettings()
            {
                LeftLocation = this.Left,
                TopLocation = this.Top,
                IsPreemptive = this.mainWindowSettings.IsPreemptive,
                RetrievabilityThreshold = this.mainWindowSettings.RetrievabilityThreshold
            });
            this.Close();
        }

        // drag the topBar and move the window
        private void topBar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        // save memo when text changed
        private void txtContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateFrontend();
            this.memoScheduler.Save();
        }

        // prevent window from deactivating
        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Activate();
        }
    }
}
