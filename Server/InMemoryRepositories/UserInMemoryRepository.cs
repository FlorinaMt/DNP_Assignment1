﻿using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private List<User> users;

    public UserInMemoryRepository()
    {
        users = new List<User>();
        users.Add(new User{Username = "betelgeuse", Password = "first_password"});
        users.Add(new User{Username = "orion", Password = "second_password"});
        users.Add(new User{Username = "rigel", Password = "third_password"});
        users.Add(new User{Username = "bohr", Password = "fourth_password"});
        users.Add(new User{Username = "faraday", Password = "fifth_password"});
    }
    public Task<User> AddUserAsync(User user)
    {
        user.UserId = users.Any() ? users.Max(u => u.UserId) + 1 : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateUserAsync(User user)
    {
        User userToUpdate = GetUserByIdAsync(user.UserId).Result;
        
        users.Remove(userToUpdate);
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteUserAsync(int userId)
    {
        User userToDelete = GetUserByIdAsync(userId).Result;
        users.Remove(userToDelete);
        return Task.CompletedTask;

    }

    public Task<User> GetUserByIdAsync(int userId)
    {
        User? foundUser = users.FirstOrDefault(u => u.UserId == userId);
        if (foundUser is null)
            throw new InvalidOperationException($"No user with ID {userId} found.");
        return Task.FromResult(foundUser);
    }

    public IQueryable<User> GetUsers()
    {
        return users.AsQueryable();

    }
}