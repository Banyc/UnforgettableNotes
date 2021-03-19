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
        private void TimerUpdateDisplayMemo_Tick(object sender, EventArgs e)
        {
            UpdateViewModelDisplayMemo();
            UpdateView();

            // make sure the timer is set
            this.timerUpdateDisplayMemo.Start();
        }

        private void TimerUpdateEnergy_Tick(object sender, EventArgs e)
        {
            UpdateViewModelEnergy();
            UpdateView();

            // make sure the timer is set
            this.timerUpdateEnergy.Start();
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
            UpdateView();
            this.txtContent.Focus();
            this.memoScheduler.Save();
        }

        // delete the displaying memo from the list and display the least memorized memo
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            this.memoScheduler.RemoveMemo(viewModel.DisplayingMemo);
            UpdateViewModel();
            UpdateView();
        }

        // update the memory state of the displaying memo and display the least memorized memo
        private void btnReview_Click(object sender, RoutedEventArgs e)
        {
            this.viewModel.DisplayingMemo.Review();
            UpdateViewModel();
            UpdateView();
        }

        // Display the least memorized memo
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // reset timer
            this.timerUpdateDisplayMemo.Stop();
            this.timerUpdateDisplayMemo.Start();

            UpdateViewModel();
            UpdateView();
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
        private void topBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        // save memo when text changed
        private void txtContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            // avoid events burst when swapping memos
            if (!this.txtContent.IsFocused)
            {
                return;
            }
            UpdateView();
            this.memoScheduler.Save();
        }

        // prevent window from deactivating
        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Activate();
        }

        // stop swapping memos when user is editing the current memo
        private void txtContent_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            // reset timer
            this.timerUpdateDisplayMemo.Stop();
        }

        // stop swapping memos until user finish editing the current memo
        private void txtContent_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.timerUpdateDisplayMemo.Start();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            // avoid maximum of window
            // the maximum could not reverse
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
        }
    }
}
