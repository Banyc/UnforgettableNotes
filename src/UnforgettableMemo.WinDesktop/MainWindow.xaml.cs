using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using UnforgettableMemo.Shared;
using UnforgettableMemo.Shared.Models;

namespace UnforgettableMemo.WinDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string settingsDirectory = "desktop";
        private readonly string settingsFilename = "mainWindowSettings.json";
        private readonly MemoScheduler memoScheduler;
        private readonly DispatcherTimer timer;

        public MainWindow()
        {
            MainWindowSettings settings = LoadSettings();
            this.mainWindowSettings = settings;
            // restore the last window position
            if (settings.LeftLocation != null)
            {
                this.Left = settings.LeftLocation.Value;
            }
            if (settings.TopLocation != null)
            {
                this.Top = settings.TopLocation.Value;
            }

            InitializeComponent();

            // initiate memoScheduler
            MemoSchedulerFactory memoSchedulerFactory = new MemoSchedulerFactory("memos/");
            this.memoScheduler = memoSchedulerFactory.GetMemoScheduler();
            this.memoScheduler.Load();

            UpdateDisplayingMemo();

            // initiate timer
            this.timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMinutes(30)
            };
            this.timer.Tick += Timer_Tick;
        }

        // Display the least memorized memo
        private void UpdateDisplayingMemo()
        {
            this.memoScheduler.OrderByRetrievability();
            if (this.memoScheduler.Memos.Count == 0)
            {
                this.memoScheduler.GetNewMemo();
            }
            this.viewModel.DisplayingMemo = this.memoScheduler.Memos[0];
            UpdateFrontend();

            this.memoScheduler.Save();
        }

        // update view only
        private void UpdateFrontend()
        {
            UpdateTxtContent();
            // set window topmost if the displaying memo is somewhat not remembered
            if (this.mainWindowSettings.IsPreemptive)
            {
                if (this.viewModel.DisplayingMemo.Retrievability < this.mainWindowSettings.RetrievabilityThreshold)
                {
                    this.Topmost = true;
                    this.topBar.Background = Brushes.OrangeRed;
                }
                else
                {
                    this.Topmost = false;
                    this.topBar.Background = this.Background;
                }
            }
        }

        private void UpdateTxtContent()
        {
            BindingExpression binding = this.txtContent.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }
    }
}
