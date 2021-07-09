using System;
using Microsoft.CSharp;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace DigitBridge.CommerceCentral.YoPoco
{
    public class StructureInfo
    {
        public string ClassName { get; set; }
        public SqlTableInfo MainTable { get; set; }
        public SqlTableInfo DetailTable { get; set; }
        public Dictionary<string, StructureTable> StructureTables { get; set; }
        public StructureInfo(string className, params StructureTable[] tables)
        {
            ClassName = className;
            StructureTables = new Dictionary<string, StructureTable>();
            foreach (var t in tables)
                StructureTables.Add(t.Name, t);
        }
    }

    public class StructureTable
    {
        public string Name { get; set; }
        public string ParentName { get; set; }
        public string oneToOneChildrenName { get; set; }
        public string oneToManyChildrenName { get; set; }
        public bool MainTable { get; set; }
        public bool DetailTable { get; set; }
        public bool OneToOne { get; set; }
        public string LoadByColumn { get; set; }
        public SqlTableInfo Table { get; set; }
        public SqlTableInfo ParentTable { get; set; }
    }

    public class SqlTableInfo
    {
        public string Name { get; set; }
        public string ParameterName { get { return $"{Name.First().ToString().ToLower()}{Name.Substring(1)}"; } }
        public string DtoName { get; set; }
        public List<SqlColumnInfo> Columns { get; set; }
        public string Text { get; set; }

        public string PrimaryKey { get; set; }
        public string UniqueKey { get; set; }
        public List<List<string>> ForeignKey { get; set; }
        public List<string> BlankKey { get; set; }

        public bool HasForeignKey { get { return ForeignKey != null && ForeignKey.Count > 0; } }
        public bool HasBlankKey { get { return BlankKey != null && BlankKey.Count > 0; } }

        public List<string> Atrributes { get; set; }
        public string LoadByColumnName { get; set; }

        public bool HasStringProperties { get { return StringProperties != null && StringProperties.Count > 1; } }
        public bool HasIdProperties { get { return IdProperties != null && IdProperties.Count > 0; } }
        public List<string> StringProperties { get; set; }
        public List<string> IdProperties { get; set; }
        public void GetRandomProperties()
        {
            StringProperties = new List<string>();
            IdProperties = new List<string>();
            foreach (var col in Columns)
            {
                if (!col.Name.Equals(PrimaryKey, StringComparison.CurrentCultureIgnoreCase) &&
                    !col.Name.Equals(UniqueKey, StringComparison.CurrentCultureIgnoreCase) &&
                    col.Type.Equals("string", StringComparison.CurrentCultureIgnoreCase) &&
                    !col.Name.EndsWith("Id", StringComparison.CurrentCultureIgnoreCase))
                {
                    StringProperties.Add(col.Name);
                }

                if (!col.Name.Equals(PrimaryKey, StringComparison.CurrentCultureIgnoreCase) &&
                    !col.Name.Equals(UniqueKey, StringComparison.CurrentCultureIgnoreCase) &&
                    col.Type.Equals("string", StringComparison.CurrentCultureIgnoreCase) &&
                    col.Name.EndsWith("Id", StringComparison.CurrentCultureIgnoreCase))
                {
                    IdProperties.Add(col.Name);
                }
            }
        }

        public string BlankMatchString()
        {
            if (!HasBlankKey) return null;
            var sb = new StringBuilder();
            foreach (var fKey in BlankKey)
            {
                if (string.IsNullOrEmpty(fKey)) continue;
                var colKey = Columns.FirstOrDefault(x => x.Name.Equals(fKey, StringComparison.CurrentCultureIgnoreCase));
                if (colKey == null) continue;
                if (sb.Length > 1) sb.Append("&& ");
                if (colKey.Type == "string")
                    sb.Append($"string.IsNullOrWhiteSpace({colKey.Name}) ");
                else
                    sb.Append($"({colKey.Name} is null || {colKey.Name} == {colKey.GetDefaultValue()}) ");
            }
            return sb.ToString();
        }

        public bool ExistColumn(string name)
        {
            return (Columns.FirstOrDefault(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)) != null);
        }

        public string CheckColumnNameOrRowNum(string name)
        {
            return (Columns.FirstOrDefault(c => c.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)) != null)
                ? name
                : "RowNum";
        }

        public string StructureName { get; set; }
        public SqlTableInfo Parent { get; set; }
        public bool HasParent { get { return Parent != null && !string.IsNullOrEmpty((Parent.Name)); } }
        public List<SqlTableInfo> oneToOneChildren { get; set; }
        public bool HasOneToOneChildren { get { return oneToOneChildren != null && oneToOneChildren.Count > 0; } }

        public List<SqlTableInfo> oneToManyChildren { get; set; }
        public bool HasOneToManyChildren { get { return oneToManyChildren != null && oneToManyChildren.Count > 0; } }

        public bool OneToOneInStructure { get; set; }
        public bool IsChildrenOfChildren { get; set; }
    }

    public class SqlColumnInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string DataType { get; set; }
        public string Length { get; set; }
        public int Decimals { get; set; }
        
        public bool Identity { get; set; }
        public bool Key { get; set; }
        public bool Null { get; set; }
        public bool IsDefault { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public bool IsUniqueKey { get; set; }

        public List<string> FieldAtrributes { get; set; }
        public List<string> PropertyAtrributes { get; set; }
        public List<string> DtoAtrributes { get; set; }

        public string PropertyType
        {
            get
            {
                var t = FieldType;
                return t.ToLower().Contains("byte") ? "bool" : t;
            }
        }

        public string FieldType
        {
            get
            {
                return (
                    !Null ||
                    Type == "byte[]" ||
                    Type == "string" ||
                    Type == "Guid" ||
                    Type == "object")
                ? Type
                : $"{Type}?";
            }
        }

        public string DtoPropertyType
        {
            get
            {
                var t = PropertyType;
                if (isDateTimeLikely)
                    t = "DateTime";

                return t.EndsWith("?")
                    ? t
                    : (t.ToLower() == "byte[]" ||
                       t.ToLower() == "string" ||
                       t.ToLower() == "object")
                        ? t
                        : $"{t}?";
            }
        }

        public string ConvertPropertyTypeDtoToData
        {
            get
            {
                var t = PropertyType;
                var td = DtoPropertyType;
                return (t == td)
                    ? string.Empty
                    : $".To{RemoveNullable(ToCapital(PropertyType))}()";
            }
        }

        public string ConvertPropertyTypeDataToDto
        {
            get
            {
                var t = PropertyType;
                var td = DtoPropertyType;
                return (t == td || $"{t}?" == td)
                    ? string.Empty
                    : $".To{RemoveNullable(ToCapital(td))}()";
            }
        }

        public string propertyName
        {
            get { return $"{Name}"; }
        }
        public string privateName
        {
            get { return $"_{parameterName}"; }
        }
        public string parameterName
        {
            get { return $"{Name.First().ToString().ToLower()}{Name.Substring(1)}"; }
        }
        public bool ignoreGenerate
        {
            get
            {
                return (
                    Name.Equals("RowNum", StringComparison.CurrentCultureIgnoreCase) ||
                    Name.Equals("EnterDateUtc", StringComparison.CurrentCultureIgnoreCase) ||
                    Name.Equals("DigitBridgeGuid", StringComparison.CurrentCultureIgnoreCase)
                );
            }
        }
        public bool isString
        {
            get { return (Type == "string"); }
        }
        public bool isMaxLength
        {
            get { return (Type.ToLower() == "string" && Length.ToLower() == "max"); }
        }
        public bool hasLength
        {
            get { return (Type.ToLower() == "string" && !string.IsNullOrEmpty(Length) && Length.ToLower() != "max"); }
        }
        public bool isNumber { get { return (isDecimal || isInt || isDouble); } }
        public bool isDecimal { get { return (Type == "decimal" || Type == "decimal?"); } }
        public bool isInt { get { return (Type == "int" || Type == "int?" || Type == "long" || Type == "long?" || Type == "short" || Type == "short?"); } }
        public bool isDouble { get { return (Type == "double" || Type == "double?"); } }
        public bool isByte { get { return (Type == "byte" || Type == "byte?"); } }

        public bool isQty
        {
            get { return isNumber && Name.ToLower().EndsWith("qty"); }
        }
        public bool isAmount
        {
            get { return isNumber && Name.ToLower().EndsWith("amount"); }
        }
        public bool isPrice
        {
            get { return isNumber && Name.ToLower().EndsWith("price"); }
        }
        public bool isRate
        {
            get { return isNumber && Name.ToLower().EndsWith("rate"); }
        }
        public bool isCost
        {
            get { return isNumber && Name.ToLower().EndsWith("cost"); }
        }
        public bool isDateTime
        {
            get { return Type.ToLower().Contains("datetime"); }
        }
        public bool isDate
        {
            get { return isDateTime && DataType.ToLower().Contains("sqldbtype.date"); }
        }
        public bool isTime
        {
            get { return Type.ToLower().Contains("timespan"); }
        }
        public bool isDateTimeLikely
        {
            get { return isDateTime || isDate || isTime; }
        }

        public string GetDefaultValue()
        {
            string defaultExp = null;
            switch (Type.ToLower())
            {
                case "string":
                    defaultExp = "String.Empty";
                    break;
                case "bool":
                    defaultExp = "false";
                    break;
                case "byte[]":
                    defaultExp = "null";
                    break;
                case "datetime":
                case "smalldatetime":
                    defaultExp = "new DateTime().MinValueSql()";
                    break;
                case "timespan":
                    defaultExp = "new TimeSpan().MinValueSql()";
                    break;

                default:
                    defaultExp = string.Format("default({0})", Type.TrimEnd());
                    break;
            }

            return defaultExp;
        }

        public string GetFakeRule()
        {
            if (Name.Equals("UOM", StringComparison.CurrentCultureIgnoreCase))
                return $".RuleFor(u => u.{Name}, f => f.PickRandom(TestHelper.UOM))";
            if (Name.Equals("PackType", StringComparison.CurrentCultureIgnoreCase))
                return $".RuleFor(u => u.{Name}, f => f.PickRandom(TestHelper.PackType))";
            if (Name.Equals("PriceRule", StringComparison.CurrentCultureIgnoreCase))
                return $".RuleFor(u => u.{Name}, f => f.PickRandom(TestHelper.PriceRule))";
            if (Name.Equals("InvoiceItemType", StringComparison.CurrentCultureIgnoreCase))
                return $".RuleFor(u => u.{Name}, f => f.PickRandom(TestHelper.InvoiceItemType))";
            if (Name.Equals("InvoiceItemStatus", StringComparison.CurrentCultureIgnoreCase))
                return $".RuleFor(u => u.{Name}, f => f.PickRandom(TestHelper.InvoiceItemStatus))";


            var gen = $"f => null";
            var t = Type.ToLower();
            switch (t)
            {
                case "guid":
                    gen = $"f => f.Random.Guid()";
                    break;

                case "string":
                    gen = (IsUniqueKey || Name.ToLower().EndsWith("id"))
                        ? $"f => f.Random.Guid().ToString()"
                        : (Length == "50")
                            ? $"f => f.Random.AlphaNumeric({this.Length})"
                            : (Length == "max")
                                ? $"f => f.Lorem.Sentence()"
                                : $"f => f.Lorem.Sentence().TruncateTo({this.Length})";
                    break;
                case "bool":
                case "byte":
                case "byte?":
                    gen = $"f => f.Random.Bool()";
                    break;
                case "datetime":
                case "smalldatetime":
                    gen = $"f => f.Date.Past(0).Date";
                    break;
                case "timespan":
                    gen = $"f => f.Date.Timespan()";
                    break;
                case "decimal":
                    if (Name.EndsWith("Rate"))
                    {
                        gen = $"f => f.Random.Decimal(0.01m, 0.99m, {this.Decimals})";
                    }
                    else
                    {
                        gen = $"f => f.Random.Decimal(1, 1000, {this.Decimals})";
                    }
                    break;
                case "int":
                    gen = $"f => f.Random.Int(1, 100)";
                    break;

                default:
                    gen = $"f => default({t})";
                    break;
            }

            return $".RuleFor(u => u.{Name}, {gen})";
        }

        public string ToCamel(string s)
        {
            return $"{s.First().ToString().ToLower()}{s.Substring(1)}";
        }
        public string ToCapital(string s)
        {
            return $"{s.First().ToString().ToUpper()}{s.Substring(1)}";
        }
        public string RemoveNullable(string s)
        {
            return s.EndsWith("?")
                ? $"{s.Substring(0, s.Length - 1)}"
                : s;
        }
    }

    /// <summary>
    /// Parse Table create SQL file.
    /// Table name must like [dbo].[TableName]
    /// Primary Key must like PRIMARY KEY ([RowNum])
    /// Unique key must include UNIQUE INDEX [UK_Keyname] ()
    /// Foreign key must include INDEX [FK_Keyname] ()
    /// Column must start with [ColumnName] Type NULL/NOT NULL, and comment cannot include []
    /// </summary>
    public class SQLFileParser
    {
        private string _databaseProjectPath;
        private List<string> _files;
        public Dictionary<string, SqlTableInfo> Tables;
        public StructureInfo Structure;

        public SQLFileParser(string projectPath, string files, StructureInfo structure)
        {
            _databaseProjectPath = projectPath;
            _files = new List<string>();
            if (!string.IsNullOrEmpty(files))
                _files.AddRange(files.ToLower().Trim().Split(','));
            Structure = structure;
        }

        public void Parse()
        {
            GetTableFiles();
            GetStructure();
        }

        public StructureInfo GetStructure()
        {
            if (Structure == null || !Structure.StructureTables.Any() || !Tables.Any())
                return null;
            foreach (var stu in Structure.StructureTables)
            {
                var stuTb = stu.Value;
                // get current table object
                stuTb.Table = Tables[stu.Key];
                if (stuTb.Table == null)
                    continue;

                stuTb.Table.StructureName = Structure.ClassName;
                stuTb.Table.OneToOneInStructure = stuTb.OneToOne;
                if (stuTb.MainTable)
                    Structure.MainTable = stuTb.Table;
                if (stuTb.DetailTable)
                    Structure.DetailTable = stuTb.Table;

                // get parent table object
                if (!string.IsNullOrEmpty(stuTb.ParentName))
                {
                    stuTb.ParentTable = Tables[stuTb.ParentName];
                    stuTb.Table.Parent = stuTb.ParentTable;
                    stuTb.Table.IsChildrenOfChildren = !stuTb.Table.Parent.Name.Equals(Structure.MainTable.Name, StringComparison.CurrentCultureIgnoreCase);
                }
                // get children table names
                if (stuTb.oneToOneChildrenName != null)
                {
                    stuTb.Table.oneToOneChildren = new List<SqlTableInfo>();
                    var nms = stuTb.oneToOneChildrenName.Split(',').ToList();
                    foreach (var nm in nms)
                        if (Tables[nm] != null) stuTb.Table.oneToOneChildren.Add(Tables[nm]);
                }
                // get one to many children table names
                if (stuTb.oneToManyChildrenName != null)
                {
                    stuTb.Table.oneToManyChildren = new List<SqlTableInfo>();
                    var nms = stuTb.oneToManyChildrenName.Split(',').ToList();
                    foreach (var nm in nms)
                        if (Tables[nm] != null) stuTb.Table.oneToManyChildren.Add(Tables[nm]);
                }


            }
            return Structure;
        }

        private bool IsFileSkip(string name)
        {
            if (_files.Count == 0)
                return false;
            return !_files.Contains(name.ToLower());
        }

        public Dictionary<string, SqlTableInfo> GetTableFiles()
        {
            var path = _databaseProjectPath;
            this.Tables = new Dictionary<string, SqlTableInfo>();
            if (Directory.Exists(path))
            {
                var folder = new DirectoryInfo(path);
                foreach (var file in folder.GetFiles())
                {
                    if (!file.Extension.Equals(".sql", StringComparison.CurrentCultureIgnoreCase))
                        continue;
                    if (IsFileSkip(Path.GetFileNameWithoutExtension(file.Name)))
                        continue;

                    var content = File.ReadAllText(file.FullName, Encoding.UTF8);
                    var table = ParseSQLFile(content);
                    if (table == null)
                        continue;

                    Tables[table.Name] = table;
                }
            }
            return Tables;
        }

        protected SqlTableInfo ParseSQLFile(string content)
        {
            var table = GetSqlTableInfo(content);
            if (table == null) return null;

            table.Columns = GetSqlColumnInfo(content, table);
            table.GetRandomProperties();
            return table;
        }

        protected SqlTableInfo GetSqlTableInfo(string content)
        {
            var tableNameRegx = new Regex(@"CREATE TABLE\s+(?:\[dbo\].)?\[(\w+)\]", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var primaryKeyRegx = new Regex(@"PRIMARY KEY [^\(]*\(([^\)]+)\)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var uniqueKeyRegx = new Regex(@"UNIQUE[^\(]*INDEX \[UK_[^\[]*][^\(]*\(([^\)]+)\)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var foreignKeyRegx = new Regex(@"INDEX \[FK_[^\]]+\][^\(]*\(([^\)]+)\)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var blankKeyRegx = new Regex(@"INDEX \[BLK_[^\]]+\][^\(]*\(([^\)]+)\)", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            var table = new SqlTableInfo();
            table.Text = content;

            // get table name
            var match = tableNameRegx.Match(content);
            if (match.Success)
                table.Name = match.Groups[1].Value;
            if (string.IsNullOrEmpty(table.Name)) return null;

            // get table primaryKey
            var matchKeys = primaryKeyRegx.Matches(content);
            if (matchKeys.Count > 0)
            {
                var arrayKeys = matchKeys[0].Groups[1].Value.Split(',');
                var key = arrayKeys[0];
                table.PrimaryKey = key.Replace("[", "").Replace("]", "").Replace("ASC", "").Replace("DESC", "").Trim();
            }

            // get table uniqueKey
            matchKeys = uniqueKeyRegx.Matches(content);
            if (matchKeys.Count > 0)
            {
                var arrayKeys = matchKeys[0].Groups[1].Value.Split(',');
                var key = arrayKeys[0];
                table.UniqueKey = key.Replace("[", "").Replace("]", "").Replace("ASC", "").Replace("DESC", "").Trim();
            }

            // get table foreignKey
            var matchKeys2 = foreignKeyRegx.Matches(content);
            table.ForeignKey = new List<List<string>>();
            if (matchKeys2.Count > 0)
            {
                foreach (Match m in matchKeys2)
                {
                    var newKey = new List<string>();
                    var arrayKeys = m.Groups[1].Value.Split(',');
                    foreach (var key in arrayKeys)
                    {
                        newKey.Add(key.Replace("[", "").Replace("]", "").Replace("ASC", "").Replace("DESC", "").Trim());
                    }
                    table.ForeignKey.Add(newKey);
                }
            }

            // get table blankKey for check for record blank 
            matchKeys2 = blankKeyRegx.Matches(content);
            table.BlankKey = new List<string>();
            if (matchKeys2.Count > 0)
            {
                foreach (Match m in matchKeys2)
                {
                    var arrayKeys = m.Groups[1].Value.Split(',');
                    var key = arrayKeys[0];
                    table.BlankKey.Add(key.Replace("[", "").Replace("]", "").Replace("ASC", "").Replace("DESC", "").Trim());
                }
            }

            table.DtoName = $"{table.Name}Dto";
            this.GetTableAtrribute(table);
            return table;
        }

        protected List<SqlColumnInfo> GetSqlColumnInfo(string content, SqlTableInfo table)
        {
            var columnRegx = new Regex(@"\[(\S+)\]\s+\[?(\w+)\]?(?:\((\d+|max|\d+,\s?\d+)\))?(?:\s+(?:[\(\w\,'\(\)]+)){1,10},", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var columns = new List<SqlColumnInfo>();
            var descriptions = GetDescription(content);

            try
            {
                var matchCols = columnRegx.Matches(content);
                if (matchCols.Count > 0)
                {
                    foreach (Match m in matchCols)
                    {
                        var text = m.Groups[0].Value;
                        var col = new SqlColumnInfo
                        {
                            Name = m.Groups[1].Value,
                            Type = ChangeToCSharpType(m.Groups[2].Value, !IsNotNull(text)),
                            DataType = "SqlDbType." + Enum.Parse(typeof(SqlDbType), m.Groups[2].Value, true).ToString(),
                            Length = m.Groups[3].Value, 
                            Identity = IsIdentity(text),
                            Null = !IsNotNull(text),
                            IsDefault = !string.IsNullOrWhiteSpace(GetColumnDefault(m.Groups[0].Value)),
                            Description = descriptions.ContainsKey(m.Groups[1].Value)
                                ? descriptions[m.Groups[1].Value]
                                : m.Groups[1].Value,
                            Text = text
                        };
                        col.Decimals = col.Length != null && col.Length.Contains(',') ? int.Parse(col.Length.Split(',')[1]) : 0;
                        col.Key = !string.IsNullOrEmpty(table.PrimaryKey) && table.PrimaryKey.Equals(col.Name, StringComparison.CurrentCultureIgnoreCase);
                        col.IsUniqueKey = !string.IsNullOrEmpty(table.UniqueKey) && table.UniqueKey.Equals(col.Name, StringComparison.CurrentCultureIgnoreCase);
                        GetFieldAtrribute(col);
                        GetPropertyAtrribute(col);
                        GetDtoAtrribute(col);
                        columns.Add(col);
                    }
                }
                return columns;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected bool IsIdentity(string content)
        {
            return content.ToUpper().Contains("IDENTITY");
        }

        protected bool IsNotNull(string content)
        {
            if (IsIdentity(content))
                return true;

            return content.ToUpper().Contains("NOT NULL");
        }

        protected string GetColumnDefault(string content)
        {
            var columnRegx = new Regex(@"DEFAULT\s+([\s\S]+),", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var match = columnRegx.Match(content);
            if (match.Success)
                return match.Groups[1].Value;

            return string.Empty;
        }

        protected Dictionary<string, string> GetDescription(string content)
        {
            var dic = new Dictionary<string, string>();
            var columnRegx = new Regex(@"EXEC\s+sp_addextendedproperty\s+@name\s+\=\s+N'MS_Description',\s+@value\s+\=\s+N'(\S+)'[\s\S]+?@level2name\s+\=\s+N'(\S+)'", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var matches = columnRegx.Matches(content);
            foreach (Match match in matches)
            {
                dic[match.Groups[2].Value] = match.Groups[1].Value;
            }
            return dic;
        }

        protected string ChangeToCSharpType(string type, bool isnull)
        {
            var reval = string.Empty;
            switch (type.ToLower())
            {
                case "bigint":
                    reval = "long";
                    break;
                case "binary":
                    reval = "byte[]";
                    break;
                case "bit":
                    reval = "bool";
                    break;
                case "char":
                    reval = "string";
                    break;
                case "datetime":
                    reval = "DateTime";
                    break;
                case "decimal":
                    reval = "decimal";
                    break;
                case "float":
                    reval = "double";
                    break;
                case "image":
                    reval = "byte[]";
                    break;
                case "int":
                    reval = "int";
                    break;
                case "money":
                    reval = "decimal";
                    break;
                case "nchar":
                    reval = "string";
                    break;
                case "ntext":
                    reval = "string";
                    break;
                case "numeric":
                    reval = "float";
                    break;
                case "nvarchar":
                    reval = "string";
                    break;
                case "real":
                    reval = "float";
                    break;
                case "uniqueidentifier":
                    reval = "Guid";
                    break;
                case "smalldatetime":
                    reval = "DateTime";
                    break;
                case "smallint":
                    reval = "short";
                    break;
                case "smallmoney":
                    reval = "decimal";
                    break;
                case "text":
                    reval = "string";
                    break;
                case "timestamp":
                    reval = "byte[]";
                    break;
                case "tinyint":
                    reval = "byte";
                    break;
                case "varbinary":
                    reval = "byte[]";
                    break;
                case "varchar":
                    reval = "string";
                    break;
                case "variant":
                    reval = "object";
                    break;
                case "xml":
                    reval = "string";
                    break;
                case "udt":
                    reval = "object";
                    break;
                case "structured":
                    reval = "object";
                    break;
                case "date":
                    reval = "DateTime";
                    break;
                case "time":
                    reval = "TimeSpan";
                    break;
                case "datetime2":
                    reval = "DateTime";
                    break;
                case "datetimeoffset":
                    reval = "TimeSpan";
                    break;
                default:
                    reval = "string";
                    break;
            }
            return reval;
        }

        protected SqlColumnInfo GetFieldAtrribute(SqlColumnInfo col)
        {
            if (col == null) return col;
            col.FieldAtrributes = new List<string>();

            // [Column]
            var strAtrribute = "[Column(\"{0}\",{1}{2}{3}{4}{5})]";
            var strNotNull = col.Null ? "" : ",NotNull=true";
            var strKey = col.Key ? ",IsKey=true" : "";
            var strIdendity = col.Identity ? ",IsIdentity=true" : "";
            var strDefault = col.IsDefault ? ",IsDefault=true" : "";
            col.FieldAtrributes.Add(string.Format(strAtrribute, col.Name, col.DataType, strIdendity, strKey, strNotNull, strDefault));

            return col;
        }

        protected SqlColumnInfo GetDtoAtrribute(SqlColumnInfo col)
        {
            if (col == null) return col;
            col.DtoAtrributes = new List<string>();

            // String length
            if (col.hasLength)
                col.DtoAtrributes.Add($"[StringLength({col.Length}, ErrorMessage = \"The {col.Name} value cannot exceed {col.Length} characters. \")]");

            // Datetime
            if (col.isDateTimeLikely)
                col.DtoAtrributes.Add($"[DataType(DataType.DateTime)]");

            return col;
        }

        protected SqlColumnInfo GetPropertyAtrribute(SqlColumnInfo col)
        {
            if (col == null) return col;
            col.PropertyAtrributes = new List<string>();

            return col;
        }

        protected SqlTableInfo GetTableAtrribute(SqlTableInfo tbl)
        {
            if (tbl == null) return tbl;
            tbl.Atrributes = new List<string>();

            // [Column]
            tbl.Atrributes.Add("[Serializable()]");
            tbl.Atrributes.Add("[ExplicitColumns]");
            tbl.Atrributes.Add($"[TableName(\"{tbl.Name.Trim()}\")]");
            if (!string.IsNullOrEmpty(tbl.PrimaryKey))
                tbl.Atrributes.Add($"[PrimaryKey(\"{tbl.PrimaryKey.Trim()}\", AutoIncrement = true)]");
            if (!string.IsNullOrEmpty(tbl.UniqueKey))
                tbl.Atrributes.Add($"[UniqueId(\"{tbl.UniqueKey.Trim()}\")]");
            if (!string.IsNullOrEmpty(tbl.DtoName))
                tbl.Atrributes.Add($"[DtoName(\"{tbl.DtoName.Trim()}\")]");

            return tbl;
        }

        public string GetCamalName(string name, bool firstCaption, bool keepCase)
        {
            var strName = "";
            var index = 0;
            for (var i = 0; i < name.Length; i++)
            {
                if ((name[i] >= 'a' && name[i] <= 'z') || (name[i] >= 'A' && name[i] <= 'Z'))
                {
                    index = i + 1;
                    break;
                }
            }

            var str = name.Split(new char[2] { '-', '_' });
            if (firstCaption)
            {

                foreach (var s in str)
                {
                    if (!keepCase)
                    {
                        strName = strName + s[0].ToString().ToUpper() + s.Substring(1).ToLower();
                    }
                    else
                    {
                        strName = strName + s[0].ToString().ToUpper() + s.Substring(1);
                    }
                }
            }
            else
            {
                var i = 0;
                foreach (var s in str)
                {
                    if (!keepCase)
                    {
                        strName = strName + s[0].ToString().ToLower() + s.Substring(1).ToLower();
                    }
                    else
                    {
                        strName = strName + s[0].ToString().ToLower() + s.Substring(1);
                    }
                }
            }
            return strName;
        }

    }
}