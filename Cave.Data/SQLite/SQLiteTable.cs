using System;
using System.Linq;
using Cave.Data.Sql;

namespace Cave.Data.SQLite
{
    /// <summary>Provides a sqlite table implementation.</summary>
    public class SQLiteTable : SqlTable
    {
        #region Static

        /// <summary>Connects to the specified database and tablename.</summary>
        /// <param name="database">Database to connect to.</param>
        /// <param name="flags">Flags used to connect to the table.</param>
        /// <param name="tableName">The table to connect to.</param>
        /// <returns>Returns a new <see cref="SQLiteTable" /> instance.</returns>
        public static SQLiteTable Connect(SQLiteDatabase database, TableFlags flags, string tableName)
        {
            var table = new SQLiteTable();
            table.Initialize(database, flags, tableName);
            return table;
        }

        #endregion

        #region Constructors

        /// <summary>Initializes a new instance of the <see cref="SQLiteTable" /> class.</summary>
        protected SQLiteTable() { }

        #endregion

        #region Overrides

        /// <inheritdoc />
        protected override void CreateLastInsertedRowCommand(SqlCommandBuilder commandBuilder, Row row)
        {
            if (commandBuilder == null)
            {
                throw new ArgumentNullException(nameof(commandBuilder));
            }

            if (row == null)
            {
                throw new ArgumentNullException(nameof(row));
            }

            var idField = Layout.Identifier.Single();
            commandBuilder.AppendLine($"SELECT * FROM {FQTN} WHERE {Storage.EscapeFieldName(idField)} = last_insert_rowid();");
        }

        /// <inheritdoc />
        public override Row GetRowAt(int index) => QueryRow($"SELECT * FROM {FQTN} LIMIT {index},1");

        #endregion
    }
}
