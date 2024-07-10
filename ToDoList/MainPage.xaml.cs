using Microsoft.Maui.Controls;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList
{
    public partial class MainPage : ContentPage
    {
        private DbService databaseService;
        private TaskItem selectedTask;

        public MainPage()
        {
            InitializeComponent();
            databaseService = new DbService(App.DbPath);
            LoadTasks();
        }

        private void LoadTasks()
        {
            var tasks = databaseService.GetTasks();
            tasksListView.ItemsSource = tasks;
        }

        private void OnAddTaskButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(taskEntry.Text))
            {
                var task = new TaskItem { Name = taskEntry.Text, IsCompleted = false };
                databaseService.SaveTask(task);
                taskEntry.Text = string.Empty;
                LoadTasks();
            }
        }

        private void OnTaskSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedTask = e.SelectedItem as TaskItem;
        }

        private void OnDeleteTaskButtonClicked(object sender, EventArgs e)
        {
            if (selectedTask != null)
            {
                databaseService.DeleteTask(selectedTask);
                selectedTask = null;
                LoadTasks();
            }
        }
    }

}
