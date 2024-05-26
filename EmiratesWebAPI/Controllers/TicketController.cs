//using emirathes.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace EmiratesWebAPI.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class TicketController : ControllerBase
//    {
//        private readonly AppDbContent appDbContent;
//        public TicketController(AppDbContent _appDbContent)
//        {
//            appDbContent = _appDbContent;
//        }
//        [HttpGet]
//        public IEnumerable<Tickets> Get()
//        {
//           var x= appDbContent.Ticktes.ToList();
//            return x;
//        }



//        [HttpPost]
//        public int Post(Tickets model)
//        {
//            model.IsAvailable = true;
//            appDbContent.Ticktes.Add(model);
//            appDbContent.SaveChanges();
//            return model.Id;
//        }

        //[HttpPost]
        //public OkResult Post(Tickts model)
        //{
        //    model.IsAvailable = true;
        //    appDbContent.Ticktes.Add(model);
        //    appDbContent.SaveChanges();
        //    return Ok();
        //}

        //eger istesen bunu bu curde yaza bilersen




    }
}
