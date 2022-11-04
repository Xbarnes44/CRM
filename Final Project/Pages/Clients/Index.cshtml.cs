using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace Final_Project.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet() // This method allows us to access the database and display it on the https.
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-A1HM4ND;Initial Catalog=mystore;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    //Created query that allows us to read data from the clients table >>
                    String sql = "SELECT * FROM clients";

                    // Allows us to execute sql query 
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.id = "" + reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.email = reader.GetString(2);
                                clientInfo.phone = reader.GetString(3);
                                clientInfo.address = reader.GetString(4);
                                clientInfo.created_at = reader.GetDateTime(5).ToString();

                                //Adds the clientInfo properties into the list from the database
                                listClients.Add(clientInfo);
                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                //Show error on the console in case error

                Console.WriteLine("Exception: "+ ex.ToString());
            }
        }
    }


    // Stores data of just one client from database.
    public class ClientInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String address;
        public String created_at;
    }
}
