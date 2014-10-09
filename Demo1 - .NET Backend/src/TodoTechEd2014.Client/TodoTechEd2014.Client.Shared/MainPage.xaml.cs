using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace TodoTechEd2014.Client
{
    public sealed partial class MainPage : Page
    {
        private MobileServiceCollection<TodoItem, TodoItem> items;
        private IMobileServiceTable<TodoItem> todoTable = 
            App.MobileService.GetTable<TodoItem>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await RefreshTodoItems();
        }

        private async Task RefreshTodoItems()
        {
            LoadingIndicator.IsActive = true;

            MobileServiceInvalidOperationException exception = null;
            try
            {
                items = await todoTable
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
            }   

            LoadingIndicator.IsActive = false;
        }

        private async Task InsertTodoItem(TodoItem todoItem)
        {
            await todoTable.InsertAsync(todoItem);
            items.Add(todoItem);
        }

        private async void ButtonSave_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var todoItem = new TodoItem { Text = TextInput.Text };

            await InsertTodoItem(todoItem);
        }

        private void Edit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditItem), ((Button)sender).Tag);
        }
    }
}
