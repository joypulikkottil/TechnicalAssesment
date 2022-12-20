using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace WebService.Filters
{
    public class BasicAuthAttirbute : TypeFilterAttribute
    {
        public BasicAuthAttirbute(string realm=@"My Realm"):base(typeof (BasicAuthFilter))
        {
            Arguments=new object[] {realm};
        }
    }
}
