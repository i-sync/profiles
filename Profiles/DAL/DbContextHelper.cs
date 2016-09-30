using System;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Profiles.DAL
{
    public class DbContextHelper<TModel> where TModel : class
    {
        private SqlConnectionSetting _connectionSetting;

        public SqlConnectionSetting ConnectionSetting
        {
            get
            {
                if (_connectionSetting == null)
                {
                    return SqlConnectionSetting.Default;
                }
                return _connectionSetting;
            }
            set { _connectionSetting = value; }
        }

        public DbContextHelper(SqlConnectionSetting setting)
        {
            ConnectionSetting = setting;
        }

        public DbContextHelper() : this(SqlConnectionSetting.Default)
        {
        }

        /// <summary>
        /// get the read connection of context
        /// </summary>
        protected MySqlConnection ReadConnection 
        {
            get{
                return new MySqlConnection(ConnectionSetting.ReadConnection);
            }
        }

        /// <summary>
        /// get the read connection of context
        /// </summary>
        protected MySqlConnection WriteConnection
        {
            get
            {
                return new MySqlConnection(ConnectionSetting.WriteConnection);
            }
        }

        /// <summary>
        /// Get all data from the database
        /// usage:
        ///     var helper = new DbContextHelper&lt;ArticleBrowse&gt;();
        ///     var all = helper.GetAll();
        /// </summary>
        /// <returns></returns>
        public TModel[] GetAll()
        {
            try
            {
                using (var ctx = new DataContext(ReadConnection))
                {
                    var table = ctx.GetTable<TModel>();
                    return table.ToArray();
                }
            }
            catch (Exception ex)
            {
                //Log.Error("Database", ex);
                return new TModel[0];
            }
        }

        /// <summary>
        /// Get single model from database
        /// useage: 
        ///     var helper = new DbContextHelper&lt;ArticleBrowse&gt;();
        ///     var single = helper.WhereAny(x => x.ArticleGuid == Sitecore.Context.Item.ID.Guid);
        /// </summary>
        /// <param name="expression">LINQ expression</param>
        /// <returns></returns>
        public TModel FirstOrDefault(Expression<Func<TModel, bool>> expression)
        {
            try
            {
                using (var ctx = new DataContext(ReadConnection))
                {
#if DEBUG
                    //ctx.Log = new DebugTextWriter();
#endif
                    var table = ctx.GetTable<TModel>();
                    return table.FirstOrDefault(expression);
                }
            }
            catch (Exception ex)
            {
                //Log.Error("Database", ex);
                return null;
            }
        }

        /// <summary>
        /// Get single model from database
        /// </summary>
        /// <param name="sql">the select sql query string, it always starts with "select top 1 * from "</param>
        /// <param name="params">the select sql query paramenters</param>
        /// <returns>the result model</returns>
        public TModel FirstOrDefault(string sql, params object[] @params)
        {
            var model = Where(sql, @params);
            return model == null || model.Length < 1 ? null : model[0];
        }

        /// <summary>
        /// Get model array from database
        /// usage:
        ///     var helper = new DbContextHelper&lt;ArticleBrowse&gt;();
        ///     var list = helper.Where(x=>x.BrowseCount == 5);
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TModel[] Where(Expression<Func<TModel, bool>> expression)
        {
            try
            {
                using (var ctx = new DataContext(ReadConnection))
                {
                    var table = ctx.GetTable<TModel>();
                    return table.Where(expression).ToArray();
                }
            }
            catch (Exception ex)
            {
                //Log.Error("Database", ex);
                return new TModel[0];
            }
        }

        public TModel[] Where(string selectQuery, params object[] parameters)
        {
            try
            {
                TModel[] array;
                using (var ctx = new DataContext(ReadConnection))
                {
                    array = ctx.ExecuteQuery<TModel>(selectQuery, parameters).ToArray();
                }
                return array;
            }
            catch (Exception ex)
            {
                //Log.Error("Database", ex, false);
                return new TModel[0];
            }
        }

        /// <summary>
        /// execute the sql command
        /// </summary>
        /// <param name="sqlCmd"></param>
        /// <returns></returns>
        public bool Execute(MySqlCommand sqlCmd)
        {
            using (var conn = new MySqlConnection(ConnectionSetting.WriteConnection))
            {
                sqlCmd.Connection = conn;
                bool result;
                try
                {
                    conn.Open();
                    sqlCmd.ExecuteNonQuery();
                    conn.Close();
                    result = true;
                }
                catch (Exception ex)
                {
                    //Log.Error("Database", ex, false);
                    result = false;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
                return result;
            }
        }

        /// <summary>
        /// insert data into database
        /// </summary>
        /// <param name="model">data model</param>
        /// <returns></returns>
        public bool Insert(TModel model)
        {
            if (model == null)
            {
                return false;
            }
            try
            {
                using (var ctx = new DataContext(WriteConnection))
                {
                    var table = ctx.GetTable<TModel>();
                    table.InsertOnSubmit(model);
                    ctx.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Log.Error("Database", ex);
                return false;
            }
        }

        public bool Update(Expression<Func<TModel, bool>> whereExp, Action<TModel> setValue)
        {
            try
            {
                using (var ctx = new DataContext(WriteConnection))
                {
#if DEBUG
                    //ctx.Log = new DebugTextWriter();
#endif
                    var model = ctx.GetTable<TModel>().FirstOrDefault(whereExp);
                    if (model == null || setValue == null)
                        return false;
                    setValue(model);
                    try
                    {
                        ctx.SubmitChanges();
                    }
                    catch (ChangeConflictException ex)
                    {
                        //Log.Error("Database/change_conflict", new {ex, conflicts = ctx.ChangeConflicts});
                        foreach (var conflict in ctx.ChangeConflicts)
                        {
                            conflict.Resolve(RefreshMode.KeepChanges);
                        }
                        ctx.SubmitChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Log.Error("Database", ex);
                return false;
            }
        }

        /// <summary>
        /// updata data
        /// </summary>
        /// <param name="cmd">Sql Command</param>
        /// <returns></returns>
        public bool Update(MySqlCommand cmd)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionSetting.WriteConnection))
                {
                    cmd.Connection = conn;
                    conn.Open();
                    int result;
                    try
                    {
                        result = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //Log.Error("Database", ex);
                        result = 0;
                    }
                    conn.Close();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                //Log.Error("Database", ex);
                return false;
            }
        }

        /// <summary>
        /// remove the data from database
        /// </summary>
        /// <param name="expression">the where linq expression</param>
        /// <returns></returns>
        public bool Remove(Expression<Func<TModel, bool>> expression)
        {
            try
            {
                using (var ctx = new DataContext(WriteConnection))
                {
                    var table = ctx.GetTable<TModel>();
                    var models = table.Where(expression);
                    table.DeleteAllOnSubmit(models);
                    ctx.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                //Log.Error("Database", ex);
                return false;
            }
        }
    }
}