using MrHrumsHomeEdition.Models;

namespace MrHrumsHomeEdition.OtherClasses
{
    /// <summary>
    /// This class responsible for storage models instances
    /// </summary>
    static class Models
    {
        public static Events_Model EventsModel = new Events_Model();
        public static Clients_Model ClientsModel= new Clients_Model();
        public static Budget_Model BudgetModel= new Budget_Model();
    }
}
