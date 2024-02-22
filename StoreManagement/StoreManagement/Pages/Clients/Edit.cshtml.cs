using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StoreManagement.Pages.Clients
{   
    public class EditModel : PageModel
    {
        private DaoObject dob = new DaoObject();
        public ClientInformation clientInfo = new ClientInformation();
        public String ErrorMessage = ""; 
        public String SuccessMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            clientInfo = dob.getClientById(Int32.Parse(id));

        }
        public void OnPost()
        {
            clientInfo.setName(Request.Form["name"]);
            clientInfo.setEmail(Request.Form["email"]);
            clientInfo.setPhone(Request.Form["phone"]);
            clientInfo.setAddress(Request.Form["address"]);
            

            String[] info = { clientInfo.getName(), clientInfo.getEmail(), clientInfo.getPhone(), clientInfo.getAddress() };
            if (string.IsNullOrEmpty(info[0]) || string.IsNullOrEmpty(info[1])
                || string.IsNullOrEmpty(info[2]) || string.IsNullOrEmpty(info[3]))
            {
                ErrorMessage = "All fields is required";
                return;
            }
           
            int result = dob.updateClient(clientInfo);
            if (result == 0)
            {
                ErrorMessage = "An Error Occur";
                return;
            }
            Response.Redirect("/Clients/Index");
        }
    }
}
