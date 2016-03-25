using Profiles.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Profiles.Common
{
    public class Common
    {
        public static Profile getProfile(HttpSessionStateBase session)
        {
            return session["user"] as Profile;
        }
        /// <summary>
        /// for .net 4
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="updatedProperties"></param>
        public static void UpdatedProperties<T>(DbContext db, T entity, params Expression<Func<T, object>>[] updatedProperties) where T : class
        {
            var dbEntity = db.Entry(entity);
            //dbEntity.State = EntityState.Modified;
            //update explicitly mentioned properties
            foreach (var property in updatedProperties)
            {
                dbEntity.Property(property).IsModified = true;
            }
        }

        /// <summary>
        /// for .net4.5
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="excludedProperties"></param>
        public static void UpdateExcluded<T>(DbContext db, T entity, params Expression<Func<T, object>>[] excludedProperties) where T : class
        {
            var dbEntity = db.Entry(entity);
            dbEntity.State = EntityState.Modified;
            foreach (var property in excludedProperties)
            {
                dbEntity.Property(property).IsModified = false;
            }
        }

        public static string encryptPass(string passward)
        {
            byte[] hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(passward));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < hash.Length; i++)
            {
                sBuilder.Append((hash[i] >> 1).ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}