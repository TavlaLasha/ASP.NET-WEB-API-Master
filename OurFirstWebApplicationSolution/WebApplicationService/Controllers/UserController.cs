using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationService.EF;
using WebApplicationService.Models;

namespace WebApplicationService.Controllers
{
    //[Authorize]
    public class UserController : ApiController
    {
        // GET api/User
        public IEnumerable<UserDTO> Get()
        {
            using (LCContext db = new LCContext())
            {
                return db.Users.Select(i => new UserDTO
                {
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Email = i.Email,
                    IDNumber = i.IDNumber,
                    Password = i.Password,
                    PhoneNumber = i.PhoneNumber
                }).ToList();
            }
        }

        // GET api/User?IDNumber=5
        public UserDTO Get(string IDNumber)
        {
            using (LCContext db = new LCContext())
            {
                return db.Users.Where(i => i.IDNumber.Equals(IDNumber)).Select(i => new UserDTO
                {
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Email = i.Email,
                    IDNumber = i.IDNumber,
                    Password = i.Password,
                    PhoneNumber = i.PhoneNumber
                }).FirstOrDefault();
            }
        }

        // POST api/User
        public void Post([FromBody]UserDTO u)
        {
            using (LCContext db = new LCContext())
            {
                if (db.Users.Any(i => i.IDNumber.Equals(u.IDNumber)))
                    throw new Exception($"User with ID Number {u.IDNumber} already exists!");

                User udt = new User
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    IDNumber = u.IDNumber,
                    Password = u.Password,
                    PhoneNumber = u.PhoneNumber
                };
                db.Users.Add(udt);
                db.SaveChanges();
            }
        }

        // PUT api/User?IDNumber=5
        public void Put(string IDNumber, [FromBody]UserDTO u)
        {
            using (LCContext db = new LCContext())
            {
                if (!db.Users.Any(i => i.IDNumber.Equals(IDNumber)))
                    throw new Exception($"User with ID Number {IDNumber} not found!");

                var udt = db.Users.Where(i => i.IDNumber.Equals(IDNumber)).First();

                udt.FirstName = u.FirstName;
                udt.LastName = u.LastName;
                udt.Email = u.Email;
                udt.Password = u.Password;
                udt.PhoneNumber = u.PhoneNumber;

                db.SaveChanges();
            }
        }

        // DELETE api/User/5
        public void Delete(string IDNumber)
        {
            using (LCContext db = new LCContext())
            {
                if (!db.Users.Any(i => i.IDNumber.Equals(IDNumber)))
                    throw new Exception($"User with ID Number {IDNumber} not found!");

                var udt = db.Users.Where(i => i.IDNumber.Equals(IDNumber)).First();
                db.Users.Remove(udt);
                db.SaveChanges();
            }
        }
    }
}
