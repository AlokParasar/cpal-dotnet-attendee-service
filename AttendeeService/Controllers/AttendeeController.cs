using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AttendeeService.Controllers
{
    public class AttendeeController : ApiController
    {
        /// <summary>
        /// This is to create an object for the AttendeeRepository
        /// </summary>
        readonly IAttendeeRepository _repository;

        public AttendeeController(IAttendeeRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        // GET 
        public List<DataAccessLayer.Models.AttendeeModel> Get()
        {
            var items = new List<DataAccessLayer.Models.AttendeeModel>();
            try
            {
                  items = _repository.All().ToList();
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            return items;
        }


        [HttpPost]
        // POST 
        public DataAccessLayer.Models.AttendeeModel Post(DataAccessLayer.Models.AttendeeModel item)
        {
            try
            {
                if (null != item)
                {
                    var record = _repository.Create(item);
                    if (null != record)
                    {
                        item.Id = record.Id;
                    }
                }

               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return item;

        }

       
    }
}
