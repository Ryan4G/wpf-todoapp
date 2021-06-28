using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListApp.Models
{
    public class MenuModel : ViewModelBase
    {
        private int _count { get; set; }

        public string IconFont { get; set; }

        public string Title { get; set; }

        public string BackColor { get; set; }

        public bool DisplayIcon { get; set; } = true;

        private ObservableCollection<TaskInfo> taskInfos;

        public ObservableCollection<TaskInfo> TaskInfos
        {
            get { if (taskInfos == null) { taskInfos = new ObservableCollection<TaskInfo>(); } return taskInfos; }
            set { taskInfos = value; RaisePropertyChanged(); }
        }
    }

    public class TaskInfo
    {
        public string Id { get; set; }
        public string Content { get; set; }
    }
}
