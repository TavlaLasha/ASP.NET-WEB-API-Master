using BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DataViewModels;
using DAL.EF;

namespace BLL.Services
{
    public class PersonManagement : IPersonManagement
    {
        PersonDBContext db = new PersonDBContext();
        public bool AddPerson(PersonDTO person)
        {
            try
            {
                if (db.Persons.Any(i => i.PN.Equals(person.PN)))
                    throw new Exception($"Person with ID Number {person.PN} already exists!");

                Person udt = new Person
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    PN = person.PN,
                    BirthDate = person.BirthDate,
                    Photo = person.Photo
                };
                db.Persons.Add(udt);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool DeletePerson(string pn)
        {
            if (!db.Persons.Any(i => i.PN.Equals(pn)))
                throw new Exception($"Person with ID Number {pn} not found!");

            var udt = db.Persons.Where(i => i.PN.Equals(pn)).First();
            db.Persons.Remove(udt);
            db.SaveChanges();
            return true;
        }

        public bool EditPerson(string pn, PersonDTO person)
        {
            if (!db.Persons.Any(i => i.PN.Equals(pn)))
                throw new Exception($"Person with ID Number {person.PN} not found!");

            var udt = db.Persons.Where(i => i.PN.Equals(pn)).First();

            udt.FirstName = person.FirstName;
            udt.LastName = person.LastName;
            udt.Email = person.Email;
            udt.BirthDate = person.BirthDate;
            udt.Photo = person.Photo;

            db.SaveChanges();
            return true;
        }

        public IEnumerable<PersonDTO> GetAllPerson()
        {
            return db.Persons.Select(i => new PersonDTO
            {
                FirstName = i.FirstName,
                LastName = i.LastName,
                Email = i.Email,
                PN = i.PN
            });
        }

        public PersonDTO GetPerson(string pn)
        {
            return db.Persons.Where(i => i.PN.Equals(pn)).Select(i => new PersonDTO
            {
                FirstName = i.FirstName,
                LastName = i.LastName,
                Email = i.Email,
                PN = i.PN,
                BirthDate = i.BirthDate,
                Photo = i.Photo
            }).FirstOrDefault();
        }
    }
}
