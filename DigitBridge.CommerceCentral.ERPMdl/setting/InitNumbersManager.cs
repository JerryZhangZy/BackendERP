//-------------------------------------------------------------------------
// This document is generated by T4
// It will overwrite your changes, please keep it as it is
// <copyright company="DigitBridge">
//     Copyright (c) DigitBridge Inc.  All rights reserved.
// </copyright>
//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

using DigitBridge.Base.Utility;
using DigitBridge.CommerceCentral.YoPoco;
using DigitBridge.CommerceCentral.ERPDb;
using Microsoft.AspNetCore.Http;

namespace DigitBridge.CommerceCentral.ERPMdl
{
    /// <summary>
    /// Represents a InitNumbersService.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    public class InitNumbersManager :  IInitNumbersManager , IMessage
    {

        public InitNumbersManager() : base() {}

        public InitNumbersManager(IDataBaseFactory dbFactory)
        {
            SetDataBaseFactory(dbFactory);
        }
        
        [XmlIgnore, JsonIgnore]
        protected InitNumbersService _initNumbersService;
        [XmlIgnore, JsonIgnore]
        public InitNumbersService initNumbersService
        {
            get
            {
                if (_initNumbersService is null)
                    _initNumbersService = new InitNumbersService(dbFactory);
                return _initNumbersService;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected InitNumbersDataDtoCsv _initNumbersDataDtoCsv;
        [XmlIgnore, JsonIgnore]
        public InitNumbersDataDtoCsv initNumbersDataDtoCsv
        {
            get
            {
                if (_initNumbersDataDtoCsv is null)
                    _initNumbersDataDtoCsv = new InitNumbersDataDtoCsv();
                return _initNumbersDataDtoCsv;
            }
        }

        [XmlIgnore, JsonIgnore]
        protected InitNumbersList _initNumbersList;
        [XmlIgnore, JsonIgnore]
        public InitNumbersList initNumbersList
        {
            get
            {
                if (_initNumbersList is null)
                    _initNumbersList = new InitNumbersList(dbFactory);
                return _initNumbersList;
            }
        }

        #region DataBase
        [XmlIgnore, JsonIgnore]
        protected IDataBaseFactory _dbFactory;

        [XmlIgnore, JsonIgnore]
        public IDataBaseFactory dbFactory
        {
            get
            {
                if (_dbFactory is null)
                    _dbFactory = DataBaseFactory.CreateDefault();
                return _dbFactory;
            }
        }

        public void SetDataBaseFactory(IDataBaseFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion DataBase

        #region Messages
        protected IList<MessageClass> _messages;
        [XmlIgnore, JsonIgnore]
        public virtual IList<MessageClass> Messages
        {
            get
            {
                if (_messages is null)
                    _messages = new List<MessageClass>();
                return _messages;
            }
            set { _messages = value; }
        }
        public IList<MessageClass> AddInfo(string message, string code = null) =>
             Messages.Add(message, MessageLevel.Info, code);
        public IList<MessageClass> AddWarning(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Warning, code);
        public IList<MessageClass> AddError(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Error, code);
        public IList<MessageClass> AddFatal(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Fatal, code);
        public IList<MessageClass> AddDebug(string message, string code = null) =>
            Messages.Add(message, MessageLevel.Debug, code);

        #endregion Messages
    }
}
