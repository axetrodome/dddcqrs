using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interfaces.Persistence;


public interface IUserRepository {
    User? GetUserByEmail(string Email);

    void AddUser(User user);
}