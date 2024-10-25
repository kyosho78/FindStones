using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindStones
{
    public class LoginResponse
    {
        public int UserId { get; set; }  // This property should match the API response field for the user ID
        public string Username { get; set; }  // This property should match the API response field for the username
        public string Token { get; set; }  // If the API returns a token, otherwise remove it
    }

}
