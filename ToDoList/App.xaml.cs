namespace ToDoList
{
    public partial class App : Application
    {
        public static string DbPath;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
        public static void SetDatabasePath(string databasePath)
        {
            DbPath = databasePath;
        }

        protected override void OnStart()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TasksDatabase.mdf");
            SetDatabasePath(dbPath);
        }
    }
}
