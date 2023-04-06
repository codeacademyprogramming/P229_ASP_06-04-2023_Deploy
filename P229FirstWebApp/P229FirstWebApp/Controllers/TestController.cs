namespace P229FirstWebApp.Controllers
{
    public class TestController
    {

        public string Test1(int number,string name,int id )
        {
            return name+$" Hello {id} World P229 "+number;
        }
    }
}
