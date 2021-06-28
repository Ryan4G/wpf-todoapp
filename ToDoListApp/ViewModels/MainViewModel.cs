using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<MenuModel> menuModels; 

        public ObservableCollection<MenuModel> MenuModels
        {
            get { return menuModels; }
            set { menuModels = value; RaisePropertyChanged(); }
        }

        public MainViewModel()
        {
            MenuModels = new ObservableCollection<MenuModel>();

            menuModels.Add(new MenuModel { Title = "我的一天", IconFont = "\xe635", BackColor="#18a05e" , DisplayIcon=false});
            menuModels.Add(new MenuModel { Title = "重要", IconFont = "\xe6b6", BackColor = "#ffcd43" });
            menuModels.Add(new MenuModel { Title = "已计划日程", IconFont = "\xe6e1", BackColor = "#b848ff" });
            menuModels.Add(new MenuModel { Title = "已分配给我", IconFont = "\xe614", BackColor = "#ffcd43" });
            menuModels.Add(new MenuModel { Title = "任务", IconFont = "\xe755", BackColor = "#dc4d41" });

            MenuModel = MenuModels.First();

            SelectedCommand = new RelayCommand<MenuModel>(x =>
            {
                Select(x);
            });

            SelectedTaskCommand = new RelayCommand<TaskInfo>(x =>
            {
                SelectedTask(x);
            });

            DeleteTaskCommand = new RelayCommand<TaskInfo>(x =>
            {
                DeleteTask(x);
            });
        }

        public RelayCommand<MenuModel> SelectedCommand { get; set; }

        public RelayCommand<TaskInfo> SelectedTaskCommand { get; set; }
        public RelayCommand<TaskInfo> DeleteTaskCommand { get; set; }

        private MenuModel _model;

        public MenuModel MenuModel
        {
            get
            {
                return _model;
            }

            set
            {
                _model = value;
                RaisePropertyChanged();
            }
        }

        private TaskInfo _info;

        public TaskInfo Info
        {
            get
            {
                return _info;
            }

            set
            {
                _info = value;
                RaisePropertyChanged();
            }
        }

        private void Select(MenuModel model)
        {
            if (model.Title != _model.Title)
            {
                SelectedTask(null);
            }

            MenuModel = model;
        }

        public void AddTaskInfo(string content)
        {
            MenuModel.TaskInfos.Add(new TaskInfo { 
                Id = Guid.NewGuid().ToString(),
                Content = content 
            });
        }

        public void SelectedTask(TaskInfo taskInfo)
        {
            var expandSelectedOther = taskInfo != null && Info != null && Info.Id != taskInfo.Id;

            Info = taskInfo;

            if (!expandSelectedOther) 
            {
                Messenger.Default.Send(taskInfo, "Expand");
            }
        }

        public void DeleteTask(TaskInfo taskInfo)
        {
            Messenger.Default.Send<TaskInfo>(null, "Expand");

            var delTask = _model.TaskInfos.FirstOrDefault(x => x.Id == taskInfo.Id);

            if (delTask != null)
            {
                Info = null;

                this.MenuModel.TaskInfos.Remove(delTask);
            }
        }
    }
}
