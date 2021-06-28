using GalaSoft.MvvmLight.Messaging;
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
using ToDoListApp.Models;
using ToDoListApp.ViewModels;

namespace ToDoListApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.MouseDown += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            this.DataContext = new MainViewModel();

            Messenger.Default.Register<TaskInfo>(this, "Expand", ExpandColumn);
        }

        private void txtNewTask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var taskVal = txtNewTask.Text;

                if (!string.IsNullOrEmpty(taskVal))
                {
                    var vm = this.DataContext as MainViewModel;

                    vm.AddTaskInfo(taskVal);

                    txtNewTask.Text = string.Empty;
                }
            }
        }

        private void ExpandColumn(TaskInfo taskInfo)
        {
            var cdf = grdCenter.ColumnDefinitions;

            var setWidth = cdf[1].Width.Value;
            var setColor = Colors.White;

            if (setWidth == 0 && taskInfo != null)
            {
                setWidth = 280;
                setColor = Colors.Black;
            }
            else
            {
                setWidth = 0;
            }

            cdf[1].Width = new GridLength(setWidth);

            btnMin.Foreground = new SolidColorBrush(setColor);
            btnMax.Foreground = new SolidColorBrush(setColor);
            btnClose.Foreground = new SolidColorBrush(setColor);
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
