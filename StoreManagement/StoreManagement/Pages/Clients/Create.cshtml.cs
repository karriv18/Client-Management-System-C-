using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoreManagement.Pages.Clients
{
    public class CreateModel : PageModel
    {
        DaoObject dobject = new DaoObject();
        public String ErrorMessage = "";
        public String SuccessMessage = "";
        public ClientInformation clientInfo = new ClientInformation();
        public void OnGet()
        {
        }
        public void OnPost()
        {
            clientInfo.setName(Request.Form["name"]);
            clientInfo.setEmail(Request.Form["email"]);
            clientInfo.setPhone(Request.Form["phone"]);
            clientInfo.setAddress(Request.Form["address"]);
            String[] info = { clientInfo.getName(), clientInfo.getEmail(), clientInfo.getPhone(), clientInfo.getAddress()};
            if (string.IsNullOrEmpty(info[0]) || string.IsNullOrEmpty(info[1]) 
                || string.IsNullOrEmpty(info[2]) || string.IsNullOrEmpty(info[3]))
            {
                ErrorMessage = "All fields is required";
                return;
            }


            dobject.insertClient(clientInfo);
            clientInfo.setName("");
            clientInfo.setEmail("");
            clientInfo.setPhone("");
            clientInfo.setAddress("");
            SuccessMessage = "Insert Succesfully";
        }
 /*       public ClientInformation getClient()
        {
            return this.clientInfo;
        }
        public void setClient(ClientInformation client)
        {
            this.clientInfo = client;
        }*/
    }
}
