using Models.DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface IPersonManagement
    {
        bool AddPerson(PersonDTO person);
        bool EditPerson(string pn, PersonDTO person);
        bool DeletePerson(string pn);
        IEnumerable<PersonDTO> GetAllPerson();
        PersonDTO GetPerson(string pn);
    }
}
