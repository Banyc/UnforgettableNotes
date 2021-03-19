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
using UnforgettableMemo.Shared.Energy;
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
        private readonly EnergyScheduler energyScheduler;
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
            (this.memoScheduler, this.energyScheduler) = memoSchedulerFactory.GetSchedulers();

            UpdateViewModel();

            // initiate timer
            this.timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMinutes(30)
            };
            this.timer.Tick += Timer_Tick;
        }

        // Display the least memorized memo
        private void UpdateViewModel()
        {
            // memo
            this.memoScheduler.OrderByRetrievability();
            var memo = this.memoScheduler.GetLeastRetrievedMemo();
            if (memo == null)
            {
                memo = this.memoScheduler.GetNewMemo();
            }
            this.viewModel.DisplayingMemo = memo;
            this.memoScheduler.Save();

            // energy
            this.viewModel.Energy = this.energyScheduler.Energy;
            this.energyScheduler.Save();

            UpdateView();
        }

        // update view only
        private void UpdateView()
        {
            UpdateTxtEnergy();
            UpdateTxtContent();
            // set window topmost if the displaying memo is somewhat not remembered
            if (this.mainWindowSettings.IsPreemptive)
            {
                if (this.viewModel.DisplayingMemo?.Retrievability < this.mainWindowSettings.RetrievabilityThreshold)
                {
                    this.Topmost = false;
                    this.Topmost = true;
                    this.topBar.Background = Brushes.OrangeRed;
                }
                else
                {
                    this.Topmost = false;
                    this.topBar.Background = this.Background;
                }
            }
            this.UpdateLayout();
        }

        private void UpdateTxtContent()
        {
            BindingExpression binding = this.txtContent.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }

        private void UpdateTxtEnergy()
        {
            // BindingExpression binding = this.txtEnergy.GetBindingExpression(TextBox.TextProperty);
            // binding.UpdateSource();
            this.txtEnergy.Text = this.viewModel.Energy.ToString();
        }
    }
}
