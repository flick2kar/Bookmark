namespace DALLayer
{
    using System.Data;
    using System.Data.Odbc;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    /// <summary>
    /// This sealed class represents the DBManagerFactory
    /// </summary>
    public sealed class DBManagerFactory
    {
        private DBManagerFactory()
        {
        }
        /// <summary>
        /// This method is used to GetConnection
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static IDbConnection GetConnection(DataProvider connProviderType)
        {
            IDbConnection iDbConnection = null;
            switch (connProviderType)
            {
                case DataProvider.SqlClient:
                    iDbConnection = new SqlConnection();
                    break;
                case DataProvider.OleDb:
                    iDbConnection = new OleDbConnection();
                    break;
                case DataProvider.Odbc:
                    iDbConnection = new OdbcConnection();
                    break;
                default:
                    return null;
            }
            return iDbConnection;
        }
        /// <summary>
        /// This method is used to GetCommand
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static IDbCommand GetCommand(DataProvider cmdProviderType)
        {
            switch (cmdProviderType)
            {
                case DataProvider.SqlClient:
                    return new SqlCommand();
                case DataProvider.OleDb:
                    return new OleDbCommand();
                case DataProvider.Odbc:
                    return new OdbcCommand();
                default:
                    return null;
            }
        }
        /// <summary>
        /// This method is used to GetDataAdapter
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static IDbDataAdapter GetDataAdapter(DataProvider adptProviderType)
        {
            switch (adptProviderType)
            {
                case DataProvider.SqlClient:
                    return new SqlDataAdapter();
                case DataProvider.OleDb:
                    return new OleDbDataAdapter();
                case DataProvider.Odbc:
                    return new OdbcDataAdapter();
                default:
                    return null;
            }
        }
        /// <summary>
        /// This method is used to GetTransaction
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static IDbTransaction GetTransaction(DataProvider transProviderType)
        {
            IDbConnection iDbConnection = GetConnection(transProviderType);
            IDbTransaction iDbTransaction = iDbConnection.BeginTransaction();
            return iDbTransaction;
        }
        /// <summary>
        /// This method is used to GetParameter
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static IDataParameter GetParameter(DataProvider paramProviderType)
        {
            IDataParameter iDataParameter = null;
            switch (paramProviderType)
            {
                case DataProvider.SqlClient:
                    iDataParameter = new SqlParameter();
                    break;
                case DataProvider.OleDb:
                    iDataParameter = new OleDbParameter();
                    break;
                case DataProvider.Odbc:
                    iDataParameter = new OdbcParameter();
                    break;
            }
            return iDataParameter;
        }
        /// <summary>
        /// This method is used to GetParameters
        /// </summary>
        /// <param name="providerType"></param>
        /// <param name="paramsCount"></param>
        /// <returns></returns>
        public static IDbDataParameter[] GetParameters(DataProvider providerType, int paramsCount)
        {
            IDbDataParameter[] idbParams = new IDbDataParameter[paramsCount];

            switch (providerType)
            {
                case DataProvider.SqlClient:
                    for (int i = 0; i < paramsCount; ++i)
                    {
                        idbParams[i] = new SqlParameter();
                    }
                    break;
                case DataProvider.OleDb:
                    for (int i = 0; i < paramsCount; ++i)
                    {
                        idbParams[i] = new OleDbParameter();
                    }
                    break;
                case DataProvider.Odbc:
                    for (int i = 0; i < paramsCount; ++i)
                    {
                        idbParams[i] = new OdbcParameter();
                    }
                    break;
                default:
                    idbParams = null;
                    break;
            }
            return idbParams;
        }
    }
}