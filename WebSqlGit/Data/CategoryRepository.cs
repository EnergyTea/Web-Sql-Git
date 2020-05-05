﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebSqlGit.Data.Interface;
using WebSqlGit.Data.Model;

namespace WebSqlGit.Data
{
    public class CategoryRepository : ICategoryInterface
    {
        readonly string connectionString;
        public CategoryRepository(string conn)
        {
            connectionString = conn;
        }

        public void CreateCategory(Category category)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = "INSERT INTO Categories (Name) VALUES(@Name)";
            db.Execute(sqlQuery, category);
        }

        public void DeleteCategory(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            var sqlQuery = "DELETE FROM Categories WHERE Id = @id";
            db.Execute(sqlQuery, new { id });
        }

        /*public Category DeleteCategory(int id)
        {
            var category = Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return null;
            }
            Categories = Categories.Where(c => c.Id != id).ToList();
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                ScriptId = category.ScriptId
            };
        }*/

        public IEnumerable<Category> GetAll()
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Category>("SELECT * FROM Categories");
        }

        public Category GetCategory(int id)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return db.Query<Category>("SELECT * FROM Categories WHERE Id = @id", new { id }).FirstOrDefault();
        }
    }
}