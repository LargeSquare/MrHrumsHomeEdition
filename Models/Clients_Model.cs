using MrHrumsHomeEdition.Data;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using static MrHrumsHomeEdition.OtherClasses.DataBaseConnection;

namespace MrHrumsHomeEdition.Models
{
    [AddINotifyPropertyChangedInterface]
    class Clients_Model
    {
        public ObservableCollection<Client> Clients { get; set; }


        public Clients_Model()
        {
            DB.Client.Load();

            Clients = DB.Client.Local;
        }

        public void AddClient(Client client)
        {
            Clients.Add(client);
            DB.SaveChanges();
        }

        public void ChangeClient(Client OldClient, Client NewClient)
        {
            Client item = Clients.FirstOrDefault(c => c.Id == OldClient.Id);
            if (item == null)
            {
                MessageBox.Show(
                    "Невозможно редактировать клиента! Обратитесь к администратору.", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            item.Name = NewClient.Name;
            item.Address = NewClient.Address;
            item.Number = NewClient.Number;
            item.Note = NewClient.Note;
            DB.SaveChanges();
        }

        public void RemoveClient(Client client)
        {
            client.Visible = false;
            DB.SaveChanges();
        }
    }
}
