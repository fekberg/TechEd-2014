using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TodoTechEd2014.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditItem : Page
    {
        private MobileServiceCollection<Customer, Customer> items;
        private IMobileServiceSyncTable<Customer> customerTable = App.MobileService.GetSyncTable<Customer>();

        private Customer current;

        public EditItem()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            current = await customerTable.LookupAsync(e.Parameter.ToString());

            DataContext = current;
        }

        private async void Save_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await customerTable.UpdateAsync(current);

            Frame.GoBack();
        }
    }
}
