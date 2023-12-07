using BLL.Services;
using Models.DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplicationService.Controllers
{
    public class PersonController : ApiController
    {
        PersonManagement person = new PersonManagement();

        // GET: api/Person
        public IEnumerable<PersonDTO> GetAllPersons() => person.GetAllPerson();

        // GET api/<controller>/5
        public PersonDTO GetPersonById(string pn) => person.GetPerson(pn);

        // POST api/<controller>
        public bool CreatePerson([FromBody] PersonDTO u) => person.AddPerson(u);

        // PUT api/<controller>/5
        public bool EditPerson(string pn, [FromBody] PersonDTO u) => person.EditPerson(pn, u);

        // DELETE api/<controller>/5
        public bool DeletePerson(string pn) => person.DeletePerson(pn);
    }
}
