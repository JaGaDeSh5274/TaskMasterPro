using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TaskMasterPro.Models;

namespace TaskMasterPro.Data
{
    public static class DataExtensions
    {
        public static async Task<List<T>> ExecuteStoredProcedure<T>(this DbContext context, string storedProcedureName) where T : class
        {
            return await context.Set<T>().FromSql($"EXEC {storedProcedureName}").ToListAsync();
        }
        public static async Task<List<T>> ExecuteStoredProcedure<T>(this DbContext context, string storedProcedureName, Dictionary<string, dynamic> parameters) where T : class
        {
            var sqlParameters = parameters.Select(p => new SqlParameter(p.Key, p.Value ?? DBNull.Value)).ToArray();
            var parameterString = string.Join(", ", parameters.Keys);
            return await context.Set<T>()
                .FromSqlRaw($"EXEC {storedProcedureName} {parameterString}", sqlParameters)
                .ToListAsync();
        }

        // public static async Task<(List<T1>, List<T2>)> ExecuteStoredProcedureWithMultipleResults<T1, T2>(
        //    this DbContext context,
        //    string storedProcedureName)
        //    where T1 : class
        //    where T2 : class
        //     {
        //         var Result1 = await context.Set<T1>()
        //             .FromSqlRaw($"EXEC {storedProcedureName}")
        //             .ToListAsync();

        //         var Result2 = await context.Set<T2>()
        //             .FromSqlRaw($"EXEC {storedProcedureName}")
        //             .ToListAsync();

        //          return (Result1 ?? new List<T1>(), Result2 ?? new List<T2>());
        //         // return (Result1, Result2);
        //     }

        // }
        public static async Task<(List<T1>, List<T2>)> ExecuteStoredProcedureWithMultipleResults<T1, T2>(
        this DbContext context,
        string storedProcedureName)
        where T1 : class
        where T2 : class
        {
            using var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = storedProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            if (context.Database.GetDbConnection().State != ConnectionState.Open)
                await context.Database.GetDbConnection().OpenAsync();

            using var result = await command.ExecuteReaderAsync();
            var Result1 = new List<T1>();
            var Result2 = new List<T2>();

            // Read first result set
            while (await result.ReadAsync())
            {
                var item = Activator.CreateInstance<T1>();
                var properties = typeof(T1).GetProperties();
                foreach (var property in properties)
                {
                    if (!await result.IsDBNullAsync(result.GetOrdinal(property.Name)))
                        property.SetValue(item, result[property.Name]);
                }
                Result1.Add(item);
            }

            // Move to second result set and read
            if (await result.NextResultAsync())
            {
                while (await result.ReadAsync())
                {
                    var item = Activator.CreateInstance<T2>();
                    var properties = typeof(T2).GetProperties();
                    foreach (var property in properties)
                    {
                        if (!await result.IsDBNullAsync(result.GetOrdinal(property.Name)))
                            property.SetValue(item, result[property.Name]);
                    }
                    Result2.Add(item);
                }
            }

            return (Result1, Result2);
        }
    }
}