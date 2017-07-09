
namespace DALLayer
{
    using System.Data;

    /// <summary>
    /// DataProvider enum
    /// </summary>
    public enum DataProvider
    {
        Odbc, OleDb, SqlClient
    }
    
    /// <summary>
    /// IDBManager interface
    /// </summary>
    public interface IDBManager
    {
        /// <summary>
        /// property of DataProvider
        /// </summary>
        DataProvider ProviderType
        {
            get;
            set;
        }
        /// <summary>
        /// property of ConnectionString
        /// </summary>
        string ConnectionString
        {
            get;
            set;
        }
        /// <summary>
        /// property of IDbConnection
        /// </summary>
        IDbConnection Connection
        {
            get;
        }
        /// <summary>
        /// property of IDbTransaction
        /// </summary>
        IDbTransaction Transaction
        {
            get;
        }
        /// <summary>
        /// property of IDataReader
        /// </summary>
        IDataReader DataReader
        {
            get;
        }
        /// <summary>
        /// property of IDbCommand
        /// </summary>
        IDbCommand Command
        {
            get;
        }
        /// <summary>
        /// property of IDbDataParameter
        /// </summary>
        IDbDataParameter[] Parameters
        {
            get;
        }
        /// <summary>
        /// This method is used to open connection
        /// </summary>
        void Open();
        /// <summary>
        /// This method is used to BeginTransaction
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// This method is used to CommitTransaction
        /// </summary>
        void CommitTransaction();
        /// <summary>
        /// This method is used to CreateParameters
        /// </summary>
        /// <param name="paramsCount"></param>
        void CreateParameters(int paramsCount);
        /// <summary>
        /// This method is used to AddParameters 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="objValue"></param>
        void AddParameters(string paramName, object value);
        /// <summary>
        /// This method is used to AddParameters 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="objValue"></param>
        /// <param name="pType"></param>
        /// <param name="paramDir"></param>
        void AddParameters(string paramName, object value, DbType pType,  ParameterDirection paramDir);
        /// <summary>
        /// This method is used to AddParameters 
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="objValue"></param>
        /// <param name="paramDir"></param>
        void AddParameters(string paramName, object value, ParameterDirection paramDir);
        /// <summary>
        /// This method is used to ExecuteReader 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(CommandType commandType, string commandText);
        /// <summary>
        /// This method is used to ExecuteDataSet 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(CommandType commandType, string commandText);
        /// <summary>
        /// This method is used to ExecuteScalar
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        object ExecuteScalar(CommandType commandType, string commandText);
        /// <summary>
        /// This method is used to ExecuteNonQuery 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        int ExecuteNonQuery(CommandType commandType, string commandText);
        /// <summary>
        /// This method is used to CloseReader 
        /// </summary>
        void CloseReader();
        /// <summary>
        /// This method is used to Close 
        /// </summary>
        void Close();
        /// <summary>
        /// This method is used to Dispose 
        /// </summary>
        void Dispose();
    }
}
