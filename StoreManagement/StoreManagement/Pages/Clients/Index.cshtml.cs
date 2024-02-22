using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace StoreManagement.Pages.Clients
{
    public class IndexModel : PageModel
    {
      
        // this method is executed using http get method
        public void OnGet()
        {

        }
    }
    public class ClientInformation
    {
        private int id;
        private string name;
        private string email;
        private string phone;
        private string address;
        private String create_at;

        public ClientInformation() { }


        public ClientInformation(int id, string name, string email, string phone, string address, String create_at)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.address = address;
            this.create_at = create_at;
        }
        public ClientInformation(String name, String email, String phone, String address) : this(0, name, email, phone, address, ""){}
        /*        public void setId(string id) { this.id = id; }
                public string getId() { return this.id; }*/
        public void setName(string name) { this.name = name; }
        public string getName() { return this.name; }
        public void setEmail(string email) { this.email = email; }
        public string getEmail() { return email; }
        public void setPhone(string phone) { this.phone = phone; }
        public string getPhone(){ return this.phone;}
        public void setAddress(string address) { this.address = address; }
        public String getAddress() { return this.address; }
        public void setCreate_at(String create_at) { this.create_at = create_at; }
        public string getCreate_at() { return this.create_at; }
        public void setId(int id) { this.id = id; }
        public int getId() { return this.id; }
    }
    public class DaoObject
    {
        public SqlConnection getConnection()
        {
            string db_url = "Data Source=DESKTOP-C6DPF7M\\SQLEXPRESS03;Integrated Security=True";
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(db_url);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return conn;
        }

        public List<ClientInformation> getAllClients() 
        {
            string selectAll = "SELECT * FROM clients";
            List<ClientInformation> clients = new List<ClientInformation>();

            try
            {
                SqlConnection conn = getConnection();
                conn.Open();
                SqlCommand sc = new SqlCommand(selectAll, conn);
                SqlDataReader reader = sc.ExecuteReader();


                while (reader.Read())
                {
                    ClientInformation clientInformation = new ClientInformation();
                    clientInformation.setId(reader.GetInt32(0));
                    clientInformation.setName(reader.GetString(1));
                    clientInformation.setEmail(reader.GetString(2));
                    clientInformation.setPhone(reader.GetString(3));
                    clientInformation.setAddress(reader.GetString(4));
                    clientInformation.setCreate_at(reader.GetDateTime(5).ToString());

                    clients.Add(clientInformation);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception " + e.ToString());
            }
            return clients;
        }

        public int insertClient(ClientInformation clientInfo)
        {
            String sql = "INSERT INTO clients(name, email, phone, address)" +
                        "VALUES (@name, @email, @phone, @address)";
            int result = 0;
            try
            {
                SqlConnection conn = getConnection();
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand(sql, conn);

                sqlCmd.Parameters.AddWithValue("@name", clientInfo.getName());
                sqlCmd.Parameters.AddWithValue("@email", clientInfo.getEmail());
                sqlCmd.Parameters.AddWithValue("@phone", clientInfo.getPhone());
                sqlCmd.Parameters.AddWithValue("@address", clientInfo.getAddress());

                sqlCmd.ExecuteNonQuery();

                
            } catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return result;
        }
        public int updateClient(ClientInformation clientInfo)
        {
            String sql = "UPDATE clients " +
                "SET name = @name, email = @email, phone = @email, address = @address" +
                "WHERE id = @id";
            int result = 0; 
            try
            {
                SqlConnection conn = getConnection();
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand(sql, conn);
                Console.WriteLine(clientInfo.getName());
                sqlCmd.Parameters.AddWithValue("@name", clientInfo.getName());
                sqlCmd.Parameters.AddWithValue("@email", clientInfo.getEmail());
                sqlCmd.Parameters.AddWithValue("@phone", clientInfo.getPhone());
                sqlCmd.Parameters.AddWithValue("@address", clientInfo.getAddress());
                sqlCmd.Parameters.AddWithValue("@id", clientInfo.getId());

                result = sqlCmd.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                Console.WriteLine("Error" + e.ToString());
            }
            return result; 
        }
        public ClientInformation getClientById(int id)
        {
            String sql = "SELECT * FROM clients WHERE id = @id"; 
            ClientInformation clientInfo = new ClientInformation();
            try
            {
                SqlConnection conn = getConnection();
                conn.Open();
                SqlCommand sqlCmd = new SqlCommand(sql, conn);
                sqlCmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = sqlCmd.ExecuteReader();

                if (reader.Read())
                {
                    clientInfo.setId(reader.GetInt32(0));
                    clientInfo.setName(reader.GetString(1));
                    clientInfo.setEmail(reader.GetString(2));
                    clientInfo.setPhone(reader.GetString(3));
                    clientInfo.setAddress(reader.GetString(4));
                }
            } catch(Exception E)
            {
                Console.WriteLine(E.ToString());
            }
            return clientInfo;
        }
    }
}
