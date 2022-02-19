﻿using DailyRoutines.Application.Extensions;
using DailyRoutines.Application.Generator;
using DailyRoutines.Domain.DTOs.Access;
using DailyRoutines.Domain.DTOs.User;
using DailyRoutines.Domain.Entities.Access;
using DailyRoutines.Domain.Entities.User;
using DailyRoutines.Domain.Interfaces;
using DailyRoutines.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DailyRoutines.Infrastructure.Repositories;

public class AccessRepository : IAccessRepository
{
    private readonly DailyRoutinesDbContext _context;

    public AccessRepository(DailyRoutinesDbContext context)
    {
        _context = context;
    }

    public FilterUsersDTO GetUsersWithRole(FilterUsersDTO filter)
    {
        IQueryable<User> users = _context.Users.Where(c => c.UserRoles.Any());


        switch (filter.Type)
        {
            case "all":
                {
                    users = users.IgnoreQueryFilters();

                    filter.Type = "all";

                    break;
                }
            case "active":
                {
                    filter.Type = "active";

                    break;
                }
            case "blocked":
                {
                    users = users.Where(c => c.IsBlock)
                        .IgnoreQueryFilters();

                    filter.Type = "all";

                    break;
                }
            default:
                {
                    filter.Type = "active";

                    break;
                }
        }


        if (!string.IsNullOrEmpty(filter.Search))
            users = users.Where(c => c.FirstName.Contains(filter.Search) ||
                                     c.LastName.Contains(filter.Search) ||
                                     c.PhoneNumber.Contains(filter.Search) ||
                                     c.Email.Contains(filter.Search));

        int pagesCount = (int)Math.Ceiling(users.Count() / (double)filter.TakeEntity);

        var pager = Pager.Build(pagesCount, filter.PageId, filter.TakeEntity);

        var categories = users
            .Select(c => new UsersListDTO(c.Id, c.FullName, c.PhoneNumber, c.Email, c.IsBlock))
            .Paging(pager).ToList();

        return filter.SetItems(categories)
            .SetPaging(pager);
    }

    public FilterRolesDTO GetRoles(FilterRolesDTO filter)
    {
        IQueryable<Role> roles = _context.Roles;

        if (!string.IsNullOrEmpty(filter.Search))
            roles = roles.Where(c => c.RoleName.Contains(filter.Search));

        int pagesCount = (int)Math.Ceiling(roles.Count() / (double)filter.TakeEntity);

        var pager = Pager.Build(pagesCount, filter.PageId, filter.TakeEntity);

        var rolesList = roles
            .Select(c => new RolesListDTO(c.Id, c.RoleName))
            .Paging(pager).ToList();

        return filter.SetItems(rolesList)
            .SetPaging(pager);
    }

    public EditRoleDTO GetRoleForEdit(Guid roleId) =>
        _context.Roles.Where(c => c.Id == roleId)
            .Select(c => new EditRoleDTO()
            {
                RoleId = c.Id,
                RoleName = c.RoleName
            }).SingleOrDefault();

    public Role GetRoleById(Guid roleId) =>
        _context.Roles.Find(roleId);

    public void AddRole(Role role) =>
        _context.Roles.Add(role);

    public void updateRole(Role role) =>
        _context.Roles.Update(role);

    public void saveChanges() =>
        _context.SaveChanges();
}