using MrHrumsHomeEdition.Data;
using MrHrumsHomeEdition.OtherClasses;
using MrHrumsHomeEdition.Views.Client;
using MrHrumsHomeEdition.Views.Event;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows;
using AppModels = MrHrumsHomeEdition.OtherClasses.Models;

namespace MrHrumsHomeEdition.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class Clients_ViewModel
    {
        public ObservableCollection<Client> Clients => AppModels.ClientsModel.Clients;

        public Client SelectedClient { get; set; }

        // LocalClient using as container of new data for adding or changing selected client
        public Client LocalClient { get; set; }


        public DelegateCommand OpenWindowOfAddClient { get; set; }
        public DelegateCommand OpenWindowOfChangeClient { get; set; }
        public DelegateCommand AddClient { get; set; }
        public DelegateCommand ChangeClient { get; set; }
        public DelegateCommand RemoveClient { get; set; }


        public Clients_ViewModel()
        {
            OpenWindowOfAddClient = new DelegateCommand(obj =>
            {
                LocalClient = new Client() { Visible = true };
                AddClient_Window window = new AddClient_Window();
                window.ShowDialog();
            });

            OpenWindowOfChangeClient = new DelegateCommand(obj =>
            {
                if (SelectedClient == null)
                {
                    MessageBox.Show("Выберите клиента!");
                    return;
                }

                LocalClient = new Client() {
                    Name = SelectedClient.Name,
                    Address = SelectedClient.Address,
                    Number = SelectedClient.Number,
                    Note = SelectedClient.Note,
                    Visible = true
                };

                ChangeClient_Window window = new ChangeClient_Window();
                window.ShowDialog();
            });

            AddClient = new DelegateCommand(obj =>
            {
                if (string.IsNullOrEmpty(LocalClient.Name))
                {
                    MessageBox.Show("Введите имя!");
                    return;
                }

                if (string.IsNullOrEmpty(LocalClient.Address))
                {
                    MessageBox.Show("Введите адрес!");
                    return;
                }

                if (string.IsNullOrEmpty(LocalClient.Number))
                {
                    MessageBox.Show("Введите номер!");
                    return;
                }

                AppModels.ClientsModel.AddClient(LocalClient);
                (obj as Window).Close();
            });

            ChangeClient = new DelegateCommand(obj =>
            {
                if (string.IsNullOrEmpty(LocalClient.Name))
                {
                    MessageBox.Show("Введите имя!");
                    return;
                }

                if (string.IsNullOrEmpty(LocalClient.Address))
                {
                    MessageBox.Show("Введите адрес!");
                    return;
                }

                if (string.IsNullOrEmpty(LocalClient.Number))
                {
                    MessageBox.Show("Введите номер!");
                    return;
                }

                AppModels.ClientsModel.ChangeClient(SelectedClient, LocalClient);
                (obj as Window).Close();
            });

            RemoveClient = new DelegateCommand(obj =>
            {
                if(SelectedClient == null)
                {
                    MessageBox.Show("Выберите клиента!");
                    return;
                }
                AppModels.ClientsModel.RemoveClient(SelectedClient);
            });
        }
    }
}
