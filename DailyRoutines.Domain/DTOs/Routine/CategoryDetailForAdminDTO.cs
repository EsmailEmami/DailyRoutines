﻿using System;

namespace DailyRoutines.Domain.DTOs.Routine;

public class CategoryDetailForAdminDTO
{
    public CategoryDetailForAdminDTO(Guid userId, string fullName, Guid categoryId, string categoryTitle, string lastUpdate, int actionsCount)
    {
        UserId = userId;
        FullName = fullName;
        CategoryId = categoryId;
        CategoryTitle = categoryTitle;
        LastUpdate = lastUpdate;
        ActionsCount = actionsCount;
    }

    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryTitle { get; set; }
    public string LastUpdate { get; set; }
    public int ActionsCount { get; set; }
}