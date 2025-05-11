using LibraryManagementSystem.model;
using LibraryManagementSystem.repository;

namespace LibraryManagementSystem.service;

public class PersonService
{
    private IPersonRepository personRepository;

    public PersonService(PersonRepository pRepository)
    {
        this.personRepository = pRepository;
    }

    public void addPerson(string name, string phone)
    {
        personRepository.save(new Person(name, phone));
    }

    public Person getPersonByPhoneNumber(string phone)
    {
        return personRepository.findOneByPhone(phone);
    }
    
    
}