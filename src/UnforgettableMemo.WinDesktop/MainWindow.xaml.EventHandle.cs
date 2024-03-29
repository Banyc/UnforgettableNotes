using System.Runtime.InteropServices;
using System.Timers;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

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

        private void TimerFrequent_Tick(object sender, EventArgs e)
        {
            // during the cooling period
            if (string.IsNullOrWhiteSpace(this.viewModel.DisplayingMemo.Content) && !this.txtContent.IsFocused)
            {
                // update energy
                UpdateViewModelDisplayMemo();
            }

            UpdateViewModelEnergy();

            UpdateView();

            // make sure the timer is set
            this.timerFrequent.Start();
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
            this.viewModel.DisplayingMemo = this.memoScheduler.GetBlankMemo();
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
            this.memoScheduler.Save();
        }

        // update the memory state of the displaying memo and display the least memorized memo
        private void btnReview_Click(object sender, RoutedEventArgs e)
        {
            this.memoScheduler.StartCooling();
            this.viewModel.DisplayingMemo.Review();
            UpdateViewModel();
            UpdateView();

            this.memoScheduler.Save();
            this.energyScheduler.Save();

            SetAsBottomMost(this);
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
            this.memoScheduler.Save();
            this.energyScheduler.Save();
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

        const int SWP_NOSIZE = 0x1;
        const int SWP_NOMOVE = 0x2;
        const int SWP_NOACTIVATE = 0x10;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
        public void SetAsBottomMost(Window wnd) {
            //  Get the handle to the specified window
            IntPtr hWnd = (new WindowInteropHelper(wnd).Handle);
            //  Set the window position to HWND_BOTTOM
            SetWindowPos(hWnd, new IntPtr(1), 0, 0, 0, 0,
                (SWP_NOSIZE | SWP_NOMOVE | SWP_NOACTIVATE));
        }
    }
}
