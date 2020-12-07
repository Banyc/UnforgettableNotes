using System.Timers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UnforgettableMemo.WinDesktop
{
    public partial class MainWindow
    {
        // Display the least memorized memo
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateDisplayingMemo();

            // make sure the timer is set
            this.timer.Start();
        }

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

        // Display the least memorized memo
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // reset timer
            this.timer.Stop();
            this.timer.Start();

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
            // reset timer
            this.timer.Stop();
            this.timer.Start();

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
