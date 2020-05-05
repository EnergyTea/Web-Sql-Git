﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSqlGit.Data.Model;

namespace WebSqlGit.Data.Interface
{
    public interface ICategoryInterface
    {
        IEnumerable<Category> GetAll();
        Category GetCategory(int id);
        void DeleteCategory(int id);
        void CreateCategory(Category category);
    }
}