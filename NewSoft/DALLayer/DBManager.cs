/***********************************************************************
File          : DBManager.cs
 
Copyright (C) : Copyright  2014 The Boeing Company.  All Rights Reserved

This software source file is confidential and proprietary information of
The Boeing Company("Confidential information"). You shall not disclose such 
Confidential Information and shall use it only in accordance with the terms 
of the license agreement you entered into with The Boeing Company.
 
Created By    : Masthan Oli - HCL Technologies for The Boeing Company      Created Date: 27-Feb-2014

Description   : This file is used to define various methods of Database objects.
 
***********************************************************************/
namespace DALLayer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.Common;
    /// <summary>
    /// This class represents the DBManager and inherits IDBManager, IDisposable
    /// </summary>
    public sealed class DBManager : IDBManager, IDisposable
    {
        private IDbConnection idbConnection;
        private IDataReader idataReader;
        private IDbCommand idbCommand;
        private DataProvider providerType;
        private IDbTransaction idbTransaction = null;
        private IDbDataParameter[] idbParameters = null;
        private List<IDbDataParameter> lstParams = null;
        private string strConnection;
        private bool disposed;
        /// <summary>
        ///  This method checks whether column exists or not.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool ColumnExists(IDataReader reader, string columnName)
        {
            if (reader != null && !string.IsNullOrEmpty(columnName))
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i) == columnName)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Command time out for Command Object
        /// </summary>
        public int CommandTimeOut { get; set; }
        /// <summary>
        /// DBManager Constructor 
        /// </summary>
        public DBManager()
        {
        }

        /// <summary>
        /// Disponse Method 
        /// </summary>
        public void Dispose()
        {
            if (!disposed)
            {
                if (idbConnection != null)
                {
                    idbConnection.Dispose();
                }
                if (idataReader != null)
                {
                    idataReader.Dispose();
                }
                if (idbCommand != null)
                {
                    idbCommand.Dispose();
                }
                if (idbTransaction != null)
                {
                    idbTransaction.Dispose();
                }
                idbParameters = null;
                lstParams = null;
                strConnection = null;
                GC.SuppressFinalize(this);
                if (this != null)
                {
                    this.Close();
                }
                disposed = true;                
            }
        }

        /// <summary>
        /// DBManager Constructor 
        /// </summary>
        /// <param name="providerType"></param>
        public DBManager(DataProvider providerType)
        {
            this.providerType = providerType;
        }
        /// <summary>
        /// DBManager Constructor 
        /// </summary>
        /// <param name="providerType"></param>
        /// <param name="connectionString"></param>
        public DBManager(DataProvider providerType, string connectionString)
        {
            this.providerType = providerType;
            this.strConnection = connectionString;
        }
        /// <summary>
        /// Property of IDbConnection
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }
                return idbConnection;
            }
        }
        /// <summary>
        /// Property of IDataReader
        /// </summary>
        public IDataReader DataReader
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }
                return idataReader;
            }
            set
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }
                idataReader = value;
            }
        }
        /// <summary>
        /// Property of DataProvider
        /// </summary>
        public DataProvider ProviderType
        {
            get
            {
                return providerType;
            }
            set
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }
                providerType = value;
            }
        }
        /// <summary>
        /// Property of ConnectionString
        /// </summary>
        public string ConnectionString
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }
                return strConnection;
            }
            set
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }
                strConnection = value;
            }
        }
        /// <summary>
        /// Property of IDbCommand
        /// </summary>
        public IDbCommand Command
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }
                return idbCommand;
            }
        }
        /// <summary>
        /// Property of IDbTransaction
        /// </summary>
        public IDbTransaction Transaction
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }
                return idbTransaction;
            }
        }
        /// <summary>
        /// Property of IDbDataParameter
        /// </summary>
        public IDbDataParameter[] Parameters
        {
            get
            {
                if (disposed)
                {
                    throw new ObjectDisposedException(GetType().Name);
                }
                return idbParameters;
            }
        }
        /// <summary>
        /// This method is used to Open Connection
        /// </summary>
        public void Open()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            try
            {
                idbConnection = DBManagerFactory.GetConnection(this.providerType);
                idbConnection.ConnectionString = this.ConnectionString;
                if (idbConnection.State != ConnectionState.Open)
                {
                    idbConnection.Open();
                }
                this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            }
            catch (SqlException e)
            {
                throw;
            }
        }
        /// <summary>
        /// This method is used to Close Connection
        /// </summary>
        public void Close()
        {
            if (idbConnection.State != ConnectionState.Closed)
            {
                idbConnection.Close();
            }
        }
       
        /// <summary>
        /// This method is used to create parameters
        /// </summary>
        /// <param name="paramsCount"></param>
        public void CreateParameters(int paramsCount)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            idbParameters = new IDbDataParameter[paramsCount];
            idbParameters = DBManagerFactory.GetParameters(this.ProviderType, paramsCount);
        }

        /// <summary>
        /// This method is used to add parameters
        /// </summary>
        /// <param name="index"></param>
        /// <param name="paramName"></param>
        /// <param name="objValue"></param>
        public void AddParameters(string paramName, object value)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            if (lstParams == null)
            {
                lstParams = new List<IDbDataParameter>();
            }
            if (value == null)
            {
                value = DBNull.Value;
            }
            IDbDataParameter idpp = new SqlParameter();
            idpp.ParameterName = paramName;
            idpp.Value = value;
            lstParams.Add(idpp);
        }

        /// <summary>
        /// This method is used to add parameters
        /// </summary>
        /// <param name="index"></param>
        /// <param name="paramName"></param>
        /// <param name="objValue"></param>
        /// <param name="paramDir"></param>
        public void AddParameters(string paramName, object value, ParameterDirection paramDir)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            if (lstParams == null)
            {
                lstParams = new List<IDbDataParameter>();
            }
            IDbDataParameter idpp = new SqlParameter();
            idpp.ParameterName = paramName;
            idpp.Value = value;
            idpp.Direction = paramDir;
            lstParams.Add(idpp);

            //if (index < idbParameters.Length)
            //{
            //    idbParameters[index].ParameterName = paramName;
            //    idbParameters[index].Value = objValue;
            //    idbParameters[index].Direction = paramDir;
            //}            
        }

        /// <summary>
        /// This method is used to add parameters
        /// </summary>
        /// <param name="index"></param>
        /// <param name="paramName"></param>
        /// <param name="objValue"></param>
        /// <param name="paramDir"></param>
        public void AddParameters(string paramName, object value, DbType pType, ParameterDirection paramDir)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            if (lstParams == null)
            {
                lstParams = new List<IDbDataParameter>();
            }
            IDbDataParameter idpp = new SqlParameter(); 
            idpp.ParameterName = paramName;
            idpp.Value = value;
            idpp.DbType = pType;
            idpp.Direction = paramDir;
            lstParams.Add(idpp);
        }

        /// <summary>
        /// This method is used to Begin the Transaction
        /// </summary>
        public void BeginTransaction()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            if (this.idbTransaction == null)
            {
                idbTransaction = DBManagerFactory.GetTransaction(this.ProviderType);
            }
            this.idbCommand.Transaction = idbTransaction;
        }

        /// <summary>
        /// This method is used to Commit the Transaction
        /// </summary>
        public void CommitTransaction()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            if (this.idbTransaction != null)
            {
                this.idbTransaction.Commit();
            }
            idbTransaction = null;
        }

        /// <summary>
        /// This method sends the CommandText to the Connection and builds a SqlDataReader. 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            this.Open();
            idbCommand.Connection = this.Connection;
            idbCommand.CommandTimeout = 14400;
            PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
            this.DataReader = idbCommand.ExecuteReader();
            idbCommand.Parameters.Clear();
            return this.DataReader;
        }

        /// <summary>
        /// This method is used to Close Reader
        /// </summary>
        public void CloseReader()
        {
            if (this.DataReader != null)
            {
                this.DataReader.Close();
            }
        }

        /// <summary>
        /// This method is used to AttachParameters
        /// </summary>
        /// <param name="command"></param>
        private void AttachParameters(IDbCommand command)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            foreach (IDbDataParameter idbParameter in lstParams)
            {
                if ((idbParameter.Direction == ParameterDirection.InputOutput)
                &&
                  (idbParameter.Value == null))
                {
                    idbParameter.Value = DBNull.Value;
                }
                command.Parameters.Add(idbParameter);
            }
        }

        /// <summary>
        /// This method assigns a transaction to the command and discovers parameters if needed.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        private void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction,
            CommandType commandType, string commandText, IDbDataParameter[] commandParameters)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (transaction != null)
            {
                command.Transaction = transaction;
            }
            if (lstParams != null)
            {
                AttachParameters(command);
            }
        }

        /// <summary>
        /// This method executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            string outParameterValue = string.Empty;
            return ExecuteNonQuery(commandType, commandText, string.Empty, out outParameterValue);
        }

        /// <summary>
        /// This method executes a Transact-SQL statement against the connection and returns the number of rows affected.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText, string outParameterName, out string returnParamValue)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            Open();
            this.idbCommand.CommandTimeout = CommandTimeOut;
            PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
            int returnValue = idbCommand.ExecuteNonQuery();
            if (!string.IsNullOrEmpty(outParameterName))
            {
                returnParamValue = ((DbCommand)idbCommand).Parameters[outParameterName].Value.ToString();
            }
            else
            {
                returnParamValue = string.Empty;
            }
            idbCommand.Parameters.Clear();
            Close();
            return returnValue;
        }

        /// <summary>
        /// This method Executes the query, and returns the first column of the first row in the result set returned by the query.
        /// Additional columns or rows are ignored.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
            Open();
            PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
            object returnValue = idbCommand.ExecuteScalar();
            idbCommand.Parameters.Clear();
            Close();
            return returnValue;
        }

        /// <summary>
        ///  This method executes the commandText interpreted as specified by the commandType and returns the results in a new DataSet.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            using (DataSet dataSet = new DataSet())
            {
                this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
                Open();
                idbCommand.CommandTimeout = 7200;
                PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
                IDbDataAdapter dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
                dataAdapter.SelectCommand = idbCommand;
                dataAdapter.Fill(dataSet);
                idbCommand.Parameters.Clear();
                Close();
                return dataSet;
            }
        }

        /// <summary>
        ///  This method executes the commandText interpreted as specified by the commandType and returns the results in a new DataTable.
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(CommandType commandType, string commandText)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
            using (DataSet dataSet = new DataSet())
            {
                this.idbCommand = DBManagerFactory.GetCommand(this.ProviderType);
                Open();
                idbCommand.CommandTimeout = 3600;
                PrepareCommand(idbCommand, this.Connection, this.Transaction, commandType, commandText, this.Parameters);
                IDbDataAdapter dataAdapter = DBManagerFactory.GetDataAdapter(this.ProviderType);
                dataAdapter.SelectCommand = idbCommand;
                dataAdapter.Fill(dataSet);
                idbCommand.Parameters.Clear();
                Close();
                return dataSet.Tables[0];
            }
        }
    }
}