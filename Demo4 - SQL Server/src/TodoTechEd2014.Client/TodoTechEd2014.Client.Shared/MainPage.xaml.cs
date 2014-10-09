using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Linq;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TodoTechEd2014.Client
{
    // REV 1
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MobileServiceCollection<Customer, Customer> items;
        private IMobileServiceSyncTable<Customer> customerTable = App.MobileService.GetSyncTable<Customer>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.MobileService.SyncContext.IsInitialized)
            {
                var store = new MobileServiceSQLiteStore("local-sql-backend.db");
                store.DefineTable<Customer>();

                customerTable.SupportedOptions = MobileServiceRemoteTableOptions.None;

                await App.MobileService.SyncContext.InitializeAsync(store, new SyncHandler());
            }

            await RefreshTodoItems();
        }

        private async Task RefreshTodoItems()
        {
            LoadingIndicator.IsActive = true;

            MobileServiceInvalidOperationException exception = null;
            try
            {
                items = await customerTable
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

        private async Task PushAsync()
        {
            string errorString = null;

            try
            {
                await App.MobileService.SyncContext.PushAsync();
            }
            catch (MobileServicePushFailedException ex)
            {
                errorString = "Push failed because of sync errors: " + ex.PushResult.Errors.Count() + ", message: " + ex.Message;
            }
            catch (Exception ex)
            {
                errorString = "Push failed: " + ex.Message;
            }

            if (errorString != null)
            {
                MessageDialog d = new MessageDialog(errorString);
                await d.ShowAsync();
            }
        }

        private async Task InsertTodoItem(Customer todoItem)
        {
            await customerTable.InsertAsync(todoItem);
            items.Add(todoItem);
        }

        private async void ButtonSave_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var todoItem = new Customer { Firstname = TextInput.Text };

            await InsertTodoItem(todoItem);
        }

        private void Edit_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditItem), ((Button)sender).Tag);
        }

        private async void SyncData_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            LoadingIndicator.IsActive = true;

            Exception pullException = null;

            try
            {
                await PushAsync();

                await customerTable.PullAsync();

                await RefreshTodoItems();
            }
            catch (Exception ex)
            {
                pullException = ex;
            }

            if (pullException != null)
            {
                MessageDialog d = new MessageDialog("Pull failed: " + pullException.Message);
                await d.ShowAsync();
            }
        }
    }
}
