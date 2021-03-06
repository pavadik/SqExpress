﻿namespace SqExpress.SqlExport.Statement.Internal
{
    internal class InformationSchemaTables : TableBase
    {
        public readonly StringTableColumn TableCatalog;

        public readonly StringTableColumn TableSchema;

        public readonly StringTableColumn TableName;

        public readonly StringTableColumn TableType;

        public InformationSchemaTables(string? database = null, Alias alias = default) 
            :  base(database, "INFORMATION_SCHEMA", "TABLES", alias)
        {
            this.TableCatalog = this.CreateStringColumn("TABLE_CATALOG", null);
            this.TableSchema = this.CreateStringColumn("TABLE_SCHEMA", null);
            this.TableName = this.CreateStringColumn("TABLE_NAME", null);
            this.TableType = this.CreateStringColumn("TABLE_TYPE", null);
        }
    }
}